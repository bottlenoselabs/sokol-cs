// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.Offscreen
{
    internal sealed class OffscreenApplication : Application
    {
        private Buffer _indexBuffer;
        private Buffer _vertexBuffer;
        private Pipeline _offscreenPipeline;
        private Shader _offscreenShader;
        private Pipeline _frameBufferPipeline;
        private Shader _frameBufferShader;
        private Image _renderTarget;
        private Pass _offscreenRenderPass;

        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        public OffscreenApplication(AppDescriptor descriptor)
            : base(descriptor)
        {
        }

        protected override void CreateResources()
        {
            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _offscreenShader = CreateOffscreenShader();
            _offscreenPipeline = CreateOffscreenPipeline();

            var (renderTarget, renderTargetDepth) = CreateOffscreenRenderTargets();
            _renderTarget = renderTarget;
            _offscreenRenderPass = CreateOffscreenRenderPass(renderTarget, renderTargetDepth);

            _frameBufferShader = CreateFrameBufferShader();
            _frameBufferPipeline = CreateFrameBufferPipeline();
        }

        protected override void Frame()
        {
            Update();
            Draw();
            GraphicsDevice.Commit();
        }

        private void Draw()
        {
            // begin the offscreen render pass
            var offscreenPassAction = PassAction.Clear(Rgba32F.Black);
            _offscreenRenderPass.Begin(ref offscreenPassAction);

            // describe the bindings for rendering a non-textured cube into the render target
            var offscreenResourceBindings = default(ResourceBindings);
            offscreenResourceBindings.VertexBuffer() = _vertexBuffer;
            offscreenResourceBindings.IndexBuffer = _indexBuffer;

            // apply the render pipeline and bindings for the offscreen render pass
            _offscreenRenderPass.ApplyPipeline(_offscreenPipeline);
            _offscreenRenderPass.ApplyBindings(ref offscreenResourceBindings);

            // apply the mvp matrix to the offscreen vertex shader
            _offscreenRenderPass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);

            // draw the non-textured cube into the target of the offscreen render pass
            _offscreenRenderPass.DrawElements(36);

            // end the offscreen render pass
            _offscreenRenderPass.End();

            // begin a frame buffer render pass
            Rgba32F clearColor = 0x0040FFFF;
            var frameBufferPass = BeginDefaultPass(clearColor);

            // describe the bindings for using the offscreen render target as the sampled texture
            var frameBufferResourceBindings = default(ResourceBindings);
            frameBufferResourceBindings.VertexBuffer() = _vertexBuffer;
            frameBufferResourceBindings.IndexBuffer = _indexBuffer;
            frameBufferResourceBindings.FragmentStageImage() = _renderTarget;

            // apply the render pipeline and bindings for the frame buffer render pass
            frameBufferPass.ApplyPipeline(_frameBufferPipeline);
            frameBufferPass.ApplyBindings(ref frameBufferResourceBindings);

            // apply the mvp matrix to the frame buffer vertex shader
            frameBufferPass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);

            // draw the textured cube into the target of the frame buffer render pass
            frameBufferPass.DrawElements(36);

            // end the frame buffer render pass
            frameBufferPass.End();
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
                new Vector3(0.0f, 1.5f, 6.0f), Vector3.Zero, Vector3.UnitY);
            _viewProjectionMatrix = viewMatrix * projectionMatrix;
        }

        private Pipeline CreateFrameBufferPipeline()
        {
            // describe the frame buffer render pipeline
            var frameBufferPipelineDesc = default(PipelineDescriptor);
            frameBufferPipelineDesc.Layout.Attribute().Format = PipelineVertexAttributeFormat.Float3;
            frameBufferPipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            frameBufferPipelineDesc.Layout.Attribute(2).Format = PipelineVertexAttributeFormat.Float2;
            frameBufferPipelineDesc.Shader = _frameBufferShader;
            frameBufferPipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            frameBufferPipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            frameBufferPipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            frameBufferPipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;

            // create the frame buffer pipeline resource from the description
            return GraphicsDevice.CreatePipeline(ref frameBufferPipelineDesc);
        }

        private Pipeline CreateOffscreenPipeline()
        {
            // describe the offscreen render pipeline
            var pipelineDesc = default(PipelineDescriptor);
            // skip texture coordinates
            pipelineDesc.Layout.Buffer().Stride = 36;
            pipelineDesc.Layout.Attribute().Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            pipelineDesc.Shader = _offscreenShader;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Blend.ColorFormat = PixelFormat.RGBA8;
            pipelineDesc.Blend.DepthFormat = PixelFormat.Depth;
            pipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;

            // create the offscreen pipeline resource from the description
            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateFrameBufferShader()
        {
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock().Size = Marshal.SizeOf<Matrix4x4>();
            ref var frameBufferMvpUniform = ref shaderDesc.VertexStage.UniformBlock().Uniform();
            frameBufferMvpUniform.Name = "mvp";
            frameBufferMvpUniform.Type = ShaderUniformType.Matrix4x4;
            shaderDesc.FragmentStage.Image().Name = "tex";
            shaderDesc.FragmentStage.Image().ImageType = ImageType.Texture2D;

            // describe the vertex shader attributes
            ref var attribute0 = ref shaderDesc.Attribute();
            ref var attribute1 = ref shaderDesc.Attribute(1);
            ref var attribute2 = ref shaderDesc.Attribute(2);

            switch (Backend)
            {
                case GraphicsBackend.OpenGL:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/mainVert.glsl");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/mainFrag.glsl");
                    break;
                case GraphicsBackend.Metal:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
                    break;
                case GraphicsBackend.Direct3D11:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/mainVert.hlsl");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/mainFrag.hlsl");
                    attribute0.SemanticName = "POSITION";
                    attribute1.SemanticName = "COLOR";
                    attribute2.SemanticName = "TEXCOORD";
                    break;
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.WebGPU:
                case GraphicsBackend.Dummy:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // create the shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Shader CreateOffscreenShader()
        {
            // describe the offscreen shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock().Size = Marshal.SizeOf<Matrix4x4>();
            ref var offscreenMvpUniform = ref shaderDesc.VertexStage.UniformBlock().Uniform();
            offscreenMvpUniform.Name = "mvp";
            offscreenMvpUniform.Type = ShaderUniformType.Matrix4x4;

            // describe the vertex shader attributes
            ref var attribute0 = ref shaderDesc.Attribute();
            ref var attribute1 = ref shaderDesc.Attribute(1);

            switch (Backend)
            {
                case GraphicsBackend.OpenGL:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/offscreenVert.glsl");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/offscreenFrag.glsl");
                    break;
                case GraphicsBackend.Metal:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/offscreenVert.metal");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/offscreenFrag.metal");
                    break;
                case GraphicsBackend.Direct3D11:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/offscreenVert.hlsl");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/offscreenFrag.hlsl");
                    attribute0.SemanticName = "POSITION";
                    attribute1.SemanticName = "COLOR";
                    break;
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.WebGPU:
                case GraphicsBackend.Dummy:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // create the offscreen shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private static Pass CreateOffscreenRenderPass(Image renderTarget, Image renderTargetDepth)
        {
            var passDesc = default(PassDescriptor);
            passDesc.ColorAttachment().Image = renderTarget;
            passDesc.DepthStencilAttachment.Image = renderTargetDepth;

            return GraphicsDevice.CreatePass(ref passDesc);
        }

        private static (Image renderTarget, Image renderTargetDepth) CreateOffscreenRenderTargets()
        {
            // describe a 2d texture render target
            var imageDesc = default(ImageDescriptor);
            imageDesc.Usage = ResourceUsage.Immutable;
            imageDesc.Type = ImageType.Texture2D;
            imageDesc.IsRenderTarget = true;
            imageDesc.Width = 512;
            imageDesc.Height = 512;
            imageDesc.Depth = 1;
            imageDesc.MipmapCount = 1;
            imageDesc.Format = PixelFormat.RGBA8;
            imageDesc.MinificationFilter = ImageFilter.Linear;
            imageDesc.MagnificationFilter = ImageFilter.Linear;

            // create the color render target image from the description
            var renderTarget = GraphicsDevice.CreateImage(ref imageDesc);

            // create the depth render target image from the description
            imageDesc.Format = PixelFormat.Depth;
            var renderTargetDepth = GraphicsDevice.CreateImage(ref imageDesc);

            return (renderTarget, renderTargetDepth);
        }

        private static Buffer CreateIndexBuffer()
        {
            // ReSharper disable once RedundantCast
            var indices = (Span<ushort>)stackalloc ushort[]
            {
                0, 1, 2, 0, 2, 3, // quad 1 of cube
                6, 5, 4, 7, 6, 4, // quad 2 of cube
                8, 9, 10, 8, 10, 11, // quad 3 of cube
                14, 13, 12, 15, 14, 12, // quad 4 of cube
                16, 17, 18, 16, 18, 19, // quad 5 of cube
                22, 21, 20, 23, 22, 20 // quad 6 of cube
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
            var vertices = (Span<Vertex>)stackalloc Vertex[4 * 6];

            // describe the vertices of the cube
            // quad 1
            const uint color1 = 0xFF8080FF;
            vertices[0].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[0].Color = color1;
            vertices[0].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[1].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[1].Color = color1;
            vertices[1].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[2].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[2].Color = color1;
            vertices[2].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[3].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[3].Color = color1;
            vertices[3].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 2
            const uint color2 = 0x80FF80FF;
            vertices[4].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[4].Color = color2;
            vertices[4].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[5].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[5].Color = color2;
            vertices[5].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[6].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[6].Color = color2;
            vertices[6].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[7].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[7].Color = color2;
            vertices[7].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 3
            const uint color3 = 0x8080FFFF;
            vertices[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[8].Color = color3;
            vertices[8].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[9].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[9].Color = color3;
            vertices[9].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[10].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[10].Color = color3;
            vertices[10].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[11].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[11].Color = color3;
            vertices[11].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 4
            const uint color4 = 0xFF8000FF;
            vertices[12].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[12].Color = color4;
            vertices[12].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[13].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[13].Color = color4;
            vertices[13].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[14].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[14].Color = color4;
            vertices[14].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[15].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[15].Color = color4;
            vertices[15].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 5
            const int color5 = 0x0080FFFF;
            vertices[16].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[16].Color = color5;
            vertices[16].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[17].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[17].Color = color5;
            vertices[17].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[18].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[18].Color = color5;
            vertices[18].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[19].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[19].Color = color5;
            vertices[19].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 6
            const uint color6 = 0xFF0080FF;
            vertices[20].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[20].Color = color6;
            vertices[20].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[21].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[21].Color = color6;
            vertices[21].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[22].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[22].Color = color6;
            vertices[22].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[23].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[23].Color = color6;
            vertices[23].TextureCoordinate = new Vector2(0.0f, 1.0f);

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
