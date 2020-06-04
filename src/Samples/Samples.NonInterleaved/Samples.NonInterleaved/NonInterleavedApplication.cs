// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.NonInterleaved
{
    internal sealed class NonInterleavedApplication : Application
    {
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private Shader _shader;
        private Pipeline _pipeline;

        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        protected override void CreateResources()
        {
            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline();
        }

        protected override void Frame()
        {
            Update();
            Draw();
            GraphicsDevice.Commit();
        }

        private void Draw()
        {
            // begin a frame buffer render pass
            var pass = BeginDefaultPass(Rgba32F.Gray);

            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;
            resourceBindings.VertexBuffer(1) = _vertexBuffer;
            resourceBindings.VertexBufferOffset(1) = 12 * 6 * sizeof(float);
            resourceBindings.IndexBuffer = _indexBuffer;

            // apply the render pipeline and bindings for the render pass
            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            // apply the mvp matrix to the vertex shader
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);

            // draw the cube into the target of the render pass
            pass.DrawElements(36);

            // end the frame buffer render pass
            pass.End();
        }

        private void Update()
        {
            CreateViewProjectionMatrix();
            RotateCube();
        }

        private void RotateCube()
        {
            const float deltaSeconds = 1 / 60f;

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * deltaSeconds;
            _rotationY += 2.0f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            _modelViewProjectionMatrix = modelMatrix * _viewProjectionMatrix;
        }

        private void CreateViewProjectionMatrix()
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(40.0f * Math.PI / 180),
                (float)Framebuffer.Width / Framebuffer.Height,
                0.01f,
                10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f),
                Vector3.Zero,
                Vector3.UnitY);
            _viewProjectionMatrix = viewMatrix * projectionMatrix;
        }

        private Pipeline CreatePipeline()
        {
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Attribute().Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute().BufferIndex = 0;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            pipelineDesc.Layout.Attribute(1).BufferIndex = 1;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;

            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateShader()
        {
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock().Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexStage.UniformBlock().Uniform();
            mvpUniform.Name = "mvp";
            mvpUniform.Type = ShaderUniformType.Matrix4x4;

            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (Backend)
            {
                case GraphicsBackend.OpenGL:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
                    break;
                case GraphicsBackend.Metal:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
                    break;
            }

            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private static Buffer CreateIndexBuffer()
        {
            // ReSharper disable once RedundantCast
            var indices = (Span<ushort>)stackalloc ushort[]
            {
                // quad 1
                0, 1, 2,
                0, 2, 3,
                // quad 2
                6, 5, 4,
                7, 6, 4,
                // quad 3
                8, 9, 10,
                8, 10, 11,
                // quad 4
                14, 13, 12,
                15, 14, 12,
                // quad 5
                16, 17, 18,
                16, 18, 19,
                // quad 6
                22, 21, 20,
                23, 22, 20
            };

            // describe an immutable index buffer
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.IndexBuffer
            };

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(indices);

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private static Buffer CreateVertexBuffer()
        {
            // ReSharper disable once RedundantCast
            var vertices = (Span<float>)stackalloc float[]
            {
                // quad 1 (4 Vector3 positions as floats)
                -1.0f, -1.0f, -1.0f,
                1.0f, -1.0f, -1.0f,
                1.0f, 1.0f, -1.0f,
                -1.0f, 1.0f, -1.0f,

                // quad 2
                -1.0f, -1.0f, 1.0f,
                1.0f, -1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f, 1.0f,

                // quad 3
                -1.0f, -1.0f, -1.0f,
                -1.0f, 1.0f, -1.0f,
                -1.0f, 1.0f, 1.0f,
                -1.0f, -1.0f, 1.0f,

                // quad 4
                1.0f, -1.0f, -1.0f,
                1.0f, 1.0f, -1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f, -1.0f, 1.0f,

                // quad 5
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, 1.0f,
                1.0f, -1.0f, 1.0f,
                1.0f, -1.0f, -1.0f,

                // quad 6
                -1.0f, 1.0f, -1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, -1.0f,

                // color 1 (4 RGBA32F colors)
                1.0f, 0.5f, 0.0f, 1.0f,
                1.0f, 0.5f, 0.0f, 1.0f,
                1.0f, 0.5f, 0.0f, 1.0f,
                1.0f, 0.5f, 0.0f, 1.0f,

                // color 2
                0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, 1.0f, 0.0f, 1.0f,

                // color 3
                0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, 0.0f, 1.0f, 1.0f,

                // color 4
                1.0f, 0.5f, 1.0f, 1.0f,
                1.0f, 0.5f, 1.0f, 1.0f,
                1.0f, 0.5f, 1.0f, 1.0f,
                1.0f, 0.5f, 1.0f, 1.0f,

                // color 5
                0.5f, 1.0f, 1.0f, 1.0f,
                0.5f, 1.0f, 1.0f, 1.0f,
                0.5f, 1.0f, 1.0f, 1.0f,
                0.5f, 1.0f, 1.0f, 1.0f,

                // color 6
                1.0f, 1.0f, 0.5f, 1.0f,
                1.0f, 1.0f, 0.5f, 1.0f,
                1.0f, 1.0f, 0.5f, 1.0f,
                1.0f, 1.0f, 0.5f, 1.0f
            };

            // describe an immutable vertex buffer
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.VertexBuffer
            };

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(vertices);

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }
    }
}
