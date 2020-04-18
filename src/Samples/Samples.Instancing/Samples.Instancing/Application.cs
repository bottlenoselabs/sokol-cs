// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.Instancing
{
    public class Application : App
    {
        private const int _maxParticlesCount = 512 * 1024;
        private const int _particlesCountEmittedPerFrame = 10;

        private Buffer _indexBuffer;
        private Buffer _vertexBuffer;
        private Buffer _instanceBuffer;
        private Shader _shader;
        private Pipeline _pipeline;

        private readonly Random _random = new Random();
        private readonly Vector3[] _positions = new Vector3[_maxParticlesCount];
        private readonly Vector3[] _velocities = new Vector3[_maxParticlesCount];
        private float _rotationY;
        private int _currentParticleCount;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        public Application()
        {
            Debug.Assert(
                GraphicsDevice.IsValid(),
                message: "sokol_gfx should be initialized");
            Debug.Assert(
                GraphicsDevice.Features.Instancing,
                $"instancing is not supported for your hardware with {Backend} API");

            DrawableSizeChanged += OnDrawableSizeChanged;

            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _instanceBuffer = CreateInstanceBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline();
        }

        protected override void HandleInput(InputState state)
        {
        }

        protected override void Update(AppTime time)
        {
            // emit new particles
            for (var i = 0; i < _particlesCountEmittedPerFrame; i++)
            {
                if (_currentParticleCount < _maxParticlesCount)
                {
                    _positions[_currentParticleCount] = Vector3.Zero;
                    _velocities[_currentParticleCount] = new Vector3(
                        ((float)(_random.Next() & 0x7FFF) / 0x7FFF) - 0.5f,
                        ((float)(_random.Next() & 0x7FFF) / 0x7FFF * 0.5f) + 2.0f,
                        ((float)(_random.Next() & 0x7FFF) / 0x7FFF) - 0.5f);
                    _currentParticleCount++;
                }
                else
                {
                    break;
                }
            }

            // update particle positions
            var elapsedSeconds = (float)time.ElapsedTime.TotalSeconds;
            for (var i = 0; i < _currentParticleCount; i++)
            {
                _velocities[i].Y -= 1.0f * elapsedSeconds;
                _positions[i].X += _velocities[i].X * elapsedSeconds;
                _positions[i].Y += _velocities[i].Y * elapsedSeconds;
                _positions[i].Z += _velocities[i].Z * elapsedSeconds;
                // ReSharper disable once InvertIf
                if (_positions[i].Y < -2.0f)
                {
                    _positions[i].Y = -1.8f;
                    _velocities[i].Y = -_velocities[i].Y;
                    _velocities[i].X *= 0.8f;
                    _velocities[i].Y *= 0.8f;
                    _velocities[i].Z *= 0.8f;
                }
            }

            // rotate each particle at the same time and create vertex shader mvp matrix
            _rotationY += 1.0f * 0.020f;
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixY;
            _modelViewProjectionMatrix = modelMatrix * _viewProjectionMatrix;
        }

        protected override void Draw(AppTime time)
        {
            // update instance data
            // NOTE: this is called here instead of in `Update` because buffers can only be updated once per frame
            _instanceBuffer.Update(_positions.AsMemory().Slice(0, _currentParticleCount));

            // begin a frame buffer render pass
            var pass = BeginDefaultPass(Rgba32F.Black);

            // describe the binding of the buffers
            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;
            resourceBindings.VertexBuffer(1) = _instanceBuffer;
            resourceBindings.IndexBuffer = _indexBuffer;

            // apply the render pipeline and bindings for the render pass
            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            // apply the mvp matrix to the vertex shader
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);

            // draw the particles into the target of the render pass
            pass.DrawElements(24, instanceCount: _currentParticleCount);

            // end frame buffer render pass
            pass.End();
        }

        private void OnDrawableSizeChanged(App app, int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(40.0f * Math.PI / 180),
                (float)width / height,
                0.01f,
                50.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 12.0f), Vector3.Zero, Vector3.UnitY);
            _viewProjectionMatrix = viewMatrix * projectionMatrix;
        }

        private Pipeline CreatePipeline()
        {
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Buffer(0).Stride = 28;
            pipelineDesc.Layout.Buffer(1).Stride = 12;
            pipelineDesc.Layout.Buffer(1).StepFunction = PipelineVertexStepFunction.PerInstance;
            ref var positionAttribute = ref pipelineDesc.Layout.Attribute(0);
            positionAttribute.Offset = 0;
            positionAttribute.Format = PipelineVertexAttributeFormat.Float3;
            positionAttribute.BufferIndex = 0;
            ref var colorAttribute = ref pipelineDesc.Layout.Attribute(1);
            colorAttribute.Offset = 12;
            colorAttribute.Format = PipelineVertexAttributeFormat.Float4;
            colorAttribute.BufferIndex = 0;
            ref var attribute2 = ref pipelineDesc.Layout.Attribute(2);
            attribute2.Offset = 0;
            attribute2.Format = PipelineVertexAttributeFormat.Float3;
            attribute2.BufferIndex = 1;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;

            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateShader()
        {
            // describe the shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexStage.UniformBlock(0).Uniform(0);
            mvpUniform.Name = "mvp";
            mvpUniform.Type = ShaderUniformType.Matrix4x4;

            // specify shader stage source code for each graphics backend
            if (Backend == GraphicsBackend.OpenGL)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else if (Backend == GraphicsBackend.Metal)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }

            // create the shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Buffer CreateInstanceBuffer()
        {
            var bufferDesc = default(BufferDescriptor);
            bufferDesc.Usage = ResourceUsage.Stream;
            bufferDesc.Type = BufferType.VertexBuffer;
            bufferDesc.Size = Marshal.SizeOf<Vector3>() * _maxParticlesCount;
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Buffer CreateIndexBuffer()
        {
            // use memory from the thread's stack to create the static geometry indices
            Span<ushort> indices = stackalloc ushort[]
            {
                0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 1,
                5, 1, 2, 5, 2, 3, 5, 3, 4, 5, 4, 1
            };

            // describe an immutable index buffer for static geometry
            var bufferDesc = default(BufferDescriptor);
            bufferDesc.Usage = ResourceUsage.Immutable;
            bufferDesc.Type = BufferType.IndexBuffer;

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(indices);

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Buffer CreateVertexBuffer()
        {
            // use memory from the thread's stack for the static geometry vertices
            Span<Vertex> vertices = stackalloc Vertex[6];

            const float r = 0.05f;
            // describe the vertices of the quad
            vertices[0].Position = new Vector3(0, -r, 0);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(r, 0, r);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(r, 0, -r);
            vertices[2].Color = Rgba32F.Blue;
            vertices[3].Position = new Vector3(-r, 0, -r);
            vertices[3].Color = Rgba32F.Yellow;
            vertices[4].Position = new Vector3(-r, 0, r);
            vertices[4].Color = Rgba32F.Cyan;
            vertices[5].Position = new Vector3(0, r, 0);
            vertices[5].Color = Rgba32F.Magenta;

            // describe an immutable vertex buffer for the static geometry
            var bufferDesc = default(BufferDescriptor);
            bufferDesc.Usage = ResourceUsage.Immutable;
            bufferDesc.Type = BufferType.VertexBuffer;

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(vertices);

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }
    }
}
