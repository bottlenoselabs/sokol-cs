// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.TexCube
{
    public class Application : App
    {
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private Image _texture;
        private Shader _shader;
        private Pipeline _pipeline;

        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        public Application()
        {
            DrawableSizeChanged += OnDrawableSizeChanged;

            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _texture = CreateTexture();
            CreateShader();
            CreatePipeline();
        }

        protected override void HandleInput(InputState state)
        {
        }

        protected override void Update(AppTime time)
        {
            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            _modelViewProjectionMatrix = modelMatrix * _viewProjectionMatrix;
        }

        protected override void Draw(AppTime time)
        {
            // begin a framebuffer render pass
            var pass = BeginDefaultPass(Rgba32F.Gray);

            // describe the binding of the vertex and index buffer
            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;
            resourceBindings.IndexBuffer = _indexBuffer;
            resourceBindings.FragmentStageImage() = _texture;

            // apply the render pipeline and bindings for the render pass
            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            // apply the mvp matrix to the vertex shader
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _modelViewProjectionMatrix);

            // draw the cube into the target of the render pass
            pass.DrawElements(36);

            // end the framebuffer render pass
            pass.End();
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
                new Vector3(0.0f, 1.5f, 6.0f),
                Vector3.Zero,
                Vector3.UnitY);
            _viewProjectionMatrix = viewMatrix * projectionMatrix;
        }

        private void CreatePipeline()
        {
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            pipelineDesc.Layout.Attribute(2).Format = PipelineVertexAttributeFormat.Float2;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = PipelineDepthCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = PipelineTriangleCullMode.Back;
            pipelineDesc.Rasterizer.SampleCount = GraphicsDevice.Features.MsaaRenderTargets ? 4 : 1;

            _pipeline = GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private void CreateShader()
        {
            // describe the shader program
            var shaderDesc = default(ShaderDescriptor);
            shaderDesc.VertexStage.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexStage.UniformBlock(0).Uniform(0);
            mvpUniform.Name = "mvp";
            mvpUniform.Type = ShaderUniformType.Matrix4x4;
            shaderDesc.FragmentStage.Image(0).Type = ImageType.Texture2D;

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
            _shader = GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Image CreateTexture()
        {
            // use memory from the thread's stack to create the checkerboard texture data
            Span<Rgba8U> pixelData = stackalloc Rgba8U[]
            {
                Rgba8U.White, Rgba8U.Black, Rgba8U.White, Rgba8U.Black,
                Rgba8U.Black, Rgba8U.White, Rgba8U.Black, Rgba8U.White,
                Rgba8U.White, Rgba8U.Black, Rgba8U.White, Rgba8U.Black,
                Rgba8U.Black, Rgba8U.White, Rgba8U.Black, Rgba8U.White
            };

            // describe an immutable 2d texture
            var imageDesc = new ImageDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = ImageType.Texture2D,
                Width = 4,
                Height = 4,
                Format = PixelFormat.RGBA8,
                MinificationFilter = ImageFilter.Nearest,
                MagnificationFilter = ImageFilter.Nearest
            };

            // immutable images need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            imageDesc.Content.SubImage().SetData(pixelData);

            // create the image from the descriptor
            // note: for immutable images this "uploads" the data to the GPU
            return GraphicsDevice.CreateImage(ref imageDesc);
        }

        private Buffer CreateIndexBuffer()
        {
            // use memory from the thread's stack to create the cube indices
            Span<ushort> indices = stackalloc ushort[]
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

        private Buffer CreateVertexBuffer()
        {
            // use memory from the thread's stack for the cube vertices
            Span<Vertex> vertices = stackalloc Vertex[4 * 6];

            // describe the vertices of the cube
            // quad 1
            var color1 = Rgba32F.Red;
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
            var color2 = Rgba32F.Lime;
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
            var color3 = Rgba32F.Blue;
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
            var color4 = new Rgba32F(1f, 0.5f, 0f, 1f);
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
            var color5 = new Rgba32F(0f, 0.5f, 1f, 1f);
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
            var color6 = new Rgba32F(1.0f, 0.0f, 0.5f, 1f);
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
