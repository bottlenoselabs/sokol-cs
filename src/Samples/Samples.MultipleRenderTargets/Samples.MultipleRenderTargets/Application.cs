// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.MultipleRenderTargets
{
    public class Application : App
    {
        private readonly Image[] _offScreenRenderTargets = new Image[4];
        private Buffer _cubeIndexBuffer;
        private Buffer _cubeVertexBuffer;
        private Buffer _quadVertexBuffer;
        private Pipeline _debugPipeline;
        private Shader _debugShader;
        private Pipeline _fullScreenPipeline;
        private Shader _fullScreenShader;
        private Pass _offScreenPass;
        private Pipeline _offScreenPipeline;
        private Shader _offScreenShader;

        private bool _paused;
        private Vector2 _offset;
        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        public Application()
        {
            DrawableSizeChanged += OnDrawableSizeChanged;

            var msaaSampleCount = GraphicsDevice.Features.MsaaRenderTargets ? 4 : 1;

            _cubeVertexBuffer = CreateCubeVertexBuffer();
            _cubeIndexBuffer = CreateCubeIndexBuffer();
            _quadVertexBuffer = CreateQuadVertexBuffer();

            _offScreenShader = CreateOffScreenShader();
            _offScreenPipeline = CreateOffScreenPipeline(msaaSampleCount);
            _offScreenRenderTargets = CreateOffScreenRenderTargets(msaaSampleCount);
            _offScreenPass = CreateOffScreenRenderPass();

            _fullScreenShader = CreateFullScreenShader();
            _fullScreenPipeline = CreateFullScreenPipeline(msaaSampleCount);

            _debugShader = CreateDebugShader();
            _debugPipeline = CreateDebugPipeline(msaaSampleCount);
        }

        protected override void HandleInput(InputState state)
        {
            if (state.KeyButton(KeyboardKey.Space).HasEnteredPressed)
            {
                _paused = !_paused;
            }
        }

        protected override void Update(AppTime time)
        {
            if (_paused)
            {
                return;
            }

            var deltaSeconds = time.ElapsedSeconds;

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * deltaSeconds;
            _rotationY += 2.0f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            _modelViewProjectionMatrix = modelMatrix * _viewProjectionMatrix;

            // update offset used in shader
            ref var offset = ref _offset;
            offset.X = (float)(0.1 * Math.Sin(_rotationX));
            offset.Y = (float)(0.1 * Math.Sin(_rotationY));
        }

        protected override void Draw(AppTime time)
        {
            // render cube into offscreen render targets
            var offScreenPassAction = default(PassAction);
            offScreenPassAction.Color(0).Action = PassAttachmentAction.Clear;
            offScreenPassAction.Color(0).Value = new Rgba32F(0.25f, 0.0f, 0.0f, 1.0f);
            offScreenPassAction.Color(1).Action = PassAttachmentAction.Clear;
            offScreenPassAction.Color(1).Value = new Rgba32F(0.0f, 0.25f, 0.0f, 1.0f);
            offScreenPassAction.Color(2).Action = PassAttachmentAction.Clear;
            offScreenPassAction.Color(2).Value = new Rgba32F(0.0f, 0.0f, 0.25f, 1.0f);

            // describe the binding of the off screen cube
            var offScreenBindings = default(ResourceBindings);
            offScreenBindings.VertexBuffer() = _cubeVertexBuffer;
            offScreenBindings.IndexBuffer = _cubeIndexBuffer;

            _offScreenPass.Begin(ref offScreenPassAction);
            _offScreenPass.ApplyPipeline(_offScreenPipeline);
            _offScreenPass.ApplyBindings(ref offScreenBindings);
            _offScreenPass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);
            _offScreenPass.DrawElements(36);
            _offScreenPass.End();

            // describe the binding of the debug quads
            var debugBindings = default(ResourceBindings);
            debugBindings.VertexBuffer() = _quadVertexBuffer;

            // describe the binding of the full screen quad vertex buffer
            var fullScreenBindings = default(ResourceBindings);
            fullScreenBindings.VertexBuffer() = _quadVertexBuffer;
            fullScreenBindings.FragmentStageImage() = _offScreenRenderTargets[0];
            fullScreenBindings.FragmentStageImage(1) = _offScreenRenderTargets[1];
            fullScreenBindings.FragmentStageImage(2) = _offScreenRenderTargets[2];

            // render fullscreen quad with the 'composed image',
            // plus 3 small debug-views with the content of the offscreen render targets
            var pass = BeginDefaultPass();
            pass.ApplyPipeline(_fullScreenPipeline);
            pass.ApplyBindings(ref fullScreenBindings);
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _offset);
            pass.DrawElements(4);
            pass.ApplyPipeline(_debugPipeline);
            for (var i = 0; i < 3; i++)
            {
                pass.ApplyViewport(i * 100, 0, 100, 100);
                debugBindings.FragmentStageImage() = _offScreenRenderTargets[i];
                pass.ApplyBindings(ref debugBindings);
                pass.DrawElements(4);
            }

            pass.End();
        }

        private Pass CreateOffScreenRenderPass()
        {
            var passDesc = default(PassDescriptor);
            passDesc.ColorAttachment(0).Image = _offScreenRenderTargets[0];
            passDesc.ColorAttachment(1).Image = _offScreenRenderTargets[1];
            passDesc.ColorAttachment(2).Image = _offScreenRenderTargets[2];
            passDesc.DepthStencilAttachment.Image = _offScreenRenderTargets[3];

            return GraphicsDevice.CreatePass(ref passDesc);
        }

        private Pipeline CreateDebugPipeline(int msaaSampleCount)
        {
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float2;
            pipelineDesc.Shader = _debugShader;
            pipelineDesc.PrimitiveType = PipelineVertexPrimitiveType.TriangleStrip;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateDebugShader()
        {
            // describe the fullscreen shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.FragmentStage.Image(0).Name = "tex";
            shaderDesc.FragmentStage.Image(0).Type = ImageType.Texture2D;
            // specify shader stage source code for each graphics backend
            if (Backend == GraphicsBackend.OpenGL)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/debug.vert");
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/debug.frag");
            }
            else if (Backend == GraphicsBackend.Metal)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/debugVert.metal");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/debugFrag.metal");
            }

            // create the fullscreen shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Pipeline CreateFullScreenPipeline(int msaaSampleCount)
        {
            // describe the off screen render pipeline
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float2;
            pipelineDesc.Shader = _fullScreenShader;
            pipelineDesc.PrimitiveType = PipelineVertexPrimitiveType.TriangleStrip;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            // create the offscreen pipeline resource from the description
            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateFullScreenShader()
        {
            // describe the fullscreen shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock(0).Size = Marshal.SizeOf<Vector2>();
            ref var offsetUniform = ref shaderDesc.VertexStage.UniformBlock(0).Uniform(0);
            offsetUniform.Name = "offset";
            offsetUniform.Type = ShaderUniformType.Float2;
            shaderDesc.FragmentStage.Image(0).Name = "tex0";
            shaderDesc.FragmentStage.Image(0).Type = ImageType.Texture2D;
            shaderDesc.FragmentStage.Image(1).Name = "tex1";
            shaderDesc.FragmentStage.Image(1).Type = ImageType.Texture2D;
            shaderDesc.FragmentStage.Image(2).Name = "tex2";
            shaderDesc.FragmentStage.Image(2).Type = ImageType.Texture2D;

            // specify shader stage source code for each graphics backend
            if (Backend == GraphicsBackend.OpenGL)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/fullScreen.vert");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/fullScreen.frag");
            }
            else if (Backend == GraphicsBackend.Metal)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/fullScreenVert.metal");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/fullScreenFrag.metal");
            }

            // create the fullscreen shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Buffer CreateQuadVertexBuffer()
        {
            // use memory from the thread's stack to create the full screen quad vertices
            Span<Vector2> vertices = stackalloc Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            // describe an immutable vertex buffer for the full screen quad
            var bufferDesc = default(BufferDescriptor);
            bufferDesc.Usage = ResourceUsage.Immutable;
            bufferDesc.Type = BufferType.VertexBuffer;

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(vertices);

            // create the full screen quad vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Image[] CreateOffScreenRenderTargets(int msaaSampleCount)
        {
            // describe the off screen render target images
            var colorImageDesc = default(ImageDescriptor);
            colorImageDesc.IsRenderTarget = true;
            colorImageDesc.Width = 800;
            colorImageDesc.Height = 600;
            colorImageDesc.MinificationFilter = ImageFilter.Linear;
            colorImageDesc.MagnificationFilter = ImageFilter.Linear;
            colorImageDesc.WrapU = ImageWrap.ClampToEdge;
            colorImageDesc.WrapV = ImageWrap.ClampToEdge;
            colorImageDesc.SampleCount = msaaSampleCount;

            // describe the off screen depth render target image
            var depthImageDesc = colorImageDesc;
            depthImageDesc.Format = PixelFormat.Depth;

            // create the off screen render targets
            var renderTargets = new Image[4];
            renderTargets[0] = GraphicsDevice.CreateImage(ref colorImageDesc);
            renderTargets[1] = GraphicsDevice.CreateImage(ref colorImageDesc);
            renderTargets[2] = GraphicsDevice.CreateImage(ref colorImageDesc);
            renderTargets[3] = GraphicsDevice.CreateImage(ref depthImageDesc);
            return renderTargets;
        }

        private Pipeline CreateOffScreenPipeline(int msaaSampleCount)
        {
            // describe the off screen render pipeline
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float;
            pipelineDesc.Shader = _offScreenShader;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Blend.ColorAttachmentCount = 3;
            pipelineDesc.Blend.DepthFormat = PixelFormat.Depth;
            pipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            // create the offscreen pipeline resource from the description
            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateOffScreenShader()
        {
            // describe the off screen shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexStage.UniformBlock(0).Uniform(0);
            mvpUniform.Name = "mvp";
            mvpUniform.Type = ShaderUniformType.Matrix4x4;
            // specify shader stage source code for each graphics backend
            if (Backend == GraphicsBackend.OpenGL)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/offScreen.vert");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/offScreen.frag");
            }
            else if (Backend == GraphicsBackend.Metal)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/offScreenVert.metal");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/offScreenFrag.metal");
            }

            // create the off screen shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Buffer CreateCubeIndexBuffer()
        {
            // use memory from the thread's stack to create the cube indices
            Span<ushort> cubeIndices = stackalloc ushort[]
            {
                0, 1, 2, 0, 2, 3, // quad 1 of cube
                6, 5, 4, 7, 6, 4, // quad 2 of cube
                8, 9, 10, 8, 10, 11, // quad 3 of cube
                14, 13, 12, 15, 14, 12, // quad 4 of cube
                16, 17, 18, 16, 18, 19, // quad 5 of cube
                22, 21, 20, 23, 22, 20 // quad 6 of cube
            };

            // describe an immutable index buffer for the cube
            var bufferDesc = default(BufferDescriptor);
            bufferDesc.Usage = ResourceUsage.Immutable;
            bufferDesc.Type = BufferType.IndexBuffer;

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(cubeIndices);

            // create the cube index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Buffer CreateCubeVertexBuffer()
        {
            // use memory from the thread's stack for the cube vertices
            Span<Vertex> cubeVertices = stackalloc Vertex[24];

            // describe the vertices of the cube
            // quad 1
            const float brightness1 = 1.0f;
            cubeVertices[0].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[0].Brightness = brightness1;
            cubeVertices[1].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[1].Brightness = brightness1;
            cubeVertices[2].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[2].Brightness = brightness1;
            cubeVertices[3].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[3].Brightness = brightness1;
            // quad 2
            const float brightness2 = 0.8f;
            cubeVertices[4].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[4].Brightness = brightness2;
            cubeVertices[5].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[5].Brightness = brightness2;
            cubeVertices[6].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[6].Brightness = brightness2;
            cubeVertices[7].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[7].Brightness = brightness2;
            // quad 3
            const float brightness3 = 0.6f;
            cubeVertices[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[8].Brightness = brightness3;
            cubeVertices[9].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[9].Brightness = brightness3;
            cubeVertices[10].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[10].Brightness = brightness3;
            cubeVertices[11].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[11].Brightness = brightness3;
            // quad 4
            const float brightness4 = 0.4f;
            cubeVertices[12].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[12].Brightness = brightness4;
            cubeVertices[13].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[13].Brightness = brightness4;
            cubeVertices[14].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[14].Brightness = brightness4;
            cubeVertices[15].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[15].Brightness = brightness4;
            // quad 5
            const float brightness5 = 0.5f;
            cubeVertices[16].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[16].Brightness = brightness5;
            cubeVertices[17].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[17].Brightness = brightness5;
            cubeVertices[18].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[18].Brightness = brightness5;
            cubeVertices[19].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[19].Brightness = brightness5;
            // quad 6
            const float brightness6 = 0.7f;
            cubeVertices[20].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[20].Brightness = brightness6;
            cubeVertices[21].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[21].Brightness = brightness6;
            cubeVertices[22].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[22].Brightness = brightness6;
            cubeVertices[23].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[23].Brightness = brightness6;

            // describe an immutable vertex buffer for the cube
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.VertexBuffer
            };

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(cubeVertices);

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private void OnDrawableSizeChanged(App app, int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(40.0f * Math.PI / 180),
                (float)width / height,
                0.01f,
                10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f), Vector3.Zero, Vector3.UnitY);
            _viewProjectionMatrix = viewMatrix * projectionMatrix;
        }
    }
}
