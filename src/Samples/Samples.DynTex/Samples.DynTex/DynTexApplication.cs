// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.DynTex
{
    internal sealed class DynTexApplication : Application
    {
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private Image _texture;
        private Shader _shader;
        private Pipeline _pipeline;

        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private Matrix4x4 _worldProjectionMatrix;
        private int _updateCount;

        private readonly Rgba8U _livingColor = Rgba8U.White;
        private readonly Rgba8U _deadColor = Rgba8U.Black;

        // width/height must be power of 2
        private const int _textureWidth = 64;
        private const int _textureHeight = 64;
        private readonly Rgba8U[] _textureData = new Rgba8U[_textureWidth * _textureHeight];
        private readonly Random _random = new Random();

        protected override void CreateResources()
        {
            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _texture = CreateTexture();
            _shader = CreateShader();
            _pipeline = CreatePipeline();

            ResetGameOfLife();
        }

        protected override void Frame()
        {
            Update();
            Draw();
            GraphicsDevice.Commit();
        }

        private void Draw()
        {
            // upload the texture data to the GPU, can only done once per frame per image
            _texture.Update(_textureData.AsMemory());

            // begin a frame buffer render pass
            var pass = BeginDefaultPass(Rgba32F.Black);

            // describe the binding of the vertex and index buffer
            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;
            resourceBindings.IndexBuffer = _indexBuffer;
            resourceBindings.FragmentStageImage() = _texture;

            // apply the render pipeline and bindings for the render pass
            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            // apply the params to the vertex shader
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _worldProjectionMatrix);

            // draw the cube into the target of the render pass
            pass.DrawElements(36);

            // end the frame buffer render pass
            pass.End();
        }

        private void Update()
        {
            CreateViewProjectionMatrix();
            RotateCube();
            UpdateGameOfLife();
        }

        private void RotateCube()
        {
            const float deltaSeconds = 1 / 60f;
            _rotationX += 0.25f * deltaSeconds;
            _rotationY += 0.5f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            _worldProjectionMatrix = modelMatrix * _viewProjectionMatrix;
        }

        private void UpdateGameOfLife()
        {
            for (var y = 0; y < _textureHeight; y++)
            {
                for (var x = 0; x < _textureWidth; x++)
                {
                    var livingNeighboursCount = 0;
                    for (var ny = -1; ny < 2; ny++)
                    {
                        for (var nx = -1; nx < 2; nx++)
                        {
                            if (nx == 0 && ny == 0)
                            {
                                continue;
                            }

                            var indexY = (y + ny) & (_textureWidth - 1);
                            var indexX = (x + nx) & (_textureHeight - 1);
                            if (_textureData[(indexY * _textureHeight) + indexX] == _livingColor)
                            {
                                livingNeighboursCount++;
                            }
                        }
                    }

                    // any live cell...
                    var index = (y * _textureHeight) + x;
                    ref var color = ref _textureData[index];
                    if (color == _livingColor)
                    {
                        if (livingNeighboursCount < 2)
                        {
                            // ... with fewer than 2 living neighbours dies, as if caused by underpopulation
                            color = _deadColor;
                        }
                        else if (livingNeighboursCount > 3)
                        {
                            // ... with more than 3 living neighbours dies, as if caused by overpopulation
                            color = _deadColor;
                        }
                    }
                    else if (livingNeighboursCount == 3)
                    {
                        // any dead cell with exactly 3 living neighbours becomes a live cell, as if by reproduction
                        color = _livingColor;
                    }
                }
            }

            if (_updateCount++ <= 240)
            {
                return;
            }

            ResetGameOfLife();
            _updateCount = 0;
        }

        private void ResetGameOfLife()
        {
            for (var y = 0; y < _textureHeight; y++)
            {
                for (var x = 0; x < _textureWidth; x++)
                {
                    var index = (y * _textureHeight) + x;
                    ref var color = ref _textureData[index];

                    color = _random.Next(0, 255 + 1) > 230 ? _livingColor : _deadColor;
                }
            }
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
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            pipelineDesc.Layout.Attribute(2).Format = PipelineVertexAttributeFormat.Float2;
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

            ref var uniformBlock = ref shaderDesc.VertexStage.UniformBlock();
            uniformBlock.Size = Marshal.SizeOf<Matrix4x4>();
            uniformBlock.Uniform().Name = "mvp";
            uniformBlock.Uniform().Type = ShaderUniformType.Matrix4x4;

            ref var image = ref shaderDesc.FragmentStage.Image();
            image.Name = "tex";
            image.ImageType = ImageType.Texture2D;

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

        private static Image CreateTexture()
        {
            var imageDesc = new ImageDescriptor
            {
                Usage = ResourceUsage.Stream,
                Type = ImageType.Texture2D,
                Width = _textureWidth,
                Height = _textureHeight,
                Format = PixelFormat.RGBA8,
                MinificationFilter = ImageFilter.Nearest,
                MagnificationFilter = ImageFilter.Nearest,
                WrapU = ImageWrap.ClampToEdge,
                WrapV = ImageWrap.ClampToEdge
            };

            return GraphicsDevice.CreateImage(ref imageDesc);
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
