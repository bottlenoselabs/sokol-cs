// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.ArrayTex
{
    internal sealed class ArrayTexApplication : App
    {
        private const int _textureLayersCount = 3;
        private const int _textureWidth = 16;
        private const int _textureHeight = 16;

        private readonly Buffer _vertexBuffer;
        private readonly Buffer _indexBuffer;
        private readonly Image _texture;
        private readonly Shader _shader;
        private readonly Pipeline _pipeline;

        private bool _paused;
        private float _rotationX;
        private float _rotationY;
        private Matrix4x4 _viewProjectionMatrix;
        private int _frameIndex;
        private VertexStageParams _vertexStageParams;

        public ArrayTexApplication()
        {
            DrawableSizeChanged += OnDrawableSizeChanged;

            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _texture = CreateTexture();
            _shader = CreateShader();
            _pipeline = CreatePipeline();

            // Free any strings we implicitly allocated when creating resources
            // Only call this method AFTER resources are created
            GraphicsDevice.FreeStrings();
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
            _rotationX += 0.25f * deltaSeconds;
            _rotationY += 0.5f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            _vertexStageParams.MVP = modelMatrix * _viewProjectionMatrix;

            // calculate texture coordinate offsets (xy = uv, z = texture layer)
            var offset = _frameIndex * 0.0001f;
            _vertexStageParams.Offset0 = new Vector3(-offset, offset, 0);
            _vertexStageParams.Offset1 = new Vector3(offset, -offset, 1);
            _vertexStageParams.Offset2 = new Vector3(0, 0, 2);

            // calculate color interpolation weight
            _vertexStageParams.Weights = new Vector3(1f, 1f, 1f);
        }

        protected override void Draw(AppTime time)
        {
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
            pass.ApplyShaderUniforms(ShaderStageType.VertexStage, ref _vertexStageParams);

            // draw the cube into the target of the render pass
            pass.DrawElements(36);

            // end the frame buffer render pass
            pass.End();

            _frameIndex++;
        }

        private void OnDrawableSizeChanged(App app, int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(35.0f * Math.PI / 180),
                (float)width / height,
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
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float2;
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
            uniformBlock.Size = Marshal.SizeOf<VertexStageParams>();
            uniformBlock.Uniform(0).Name = "mvp";
            uniformBlock.Uniform(0).Type = ShaderUniformType.Matrix4x4;
            uniformBlock.Uniform(1).Name = "offset0";
            uniformBlock.Uniform(1).Type = ShaderUniformType.Float3;
            uniformBlock.Uniform(2).Name = "offset1";
            uniformBlock.Uniform(2).Type = ShaderUniformType.Float3;
            uniformBlock.Uniform(3).Name = "offset2";
            uniformBlock.Uniform(3).Type = ShaderUniformType.Float3;
            uniformBlock.Uniform(4).Name = "weights";
            uniformBlock.Uniform(4).Type = ShaderUniformType.Float3;
            shaderDesc.FragmentStage.Image().ImageType = ImageType.TextureArray;

            ref var image = ref shaderDesc.FragmentStage.Image();
            image.Name = "tex";
            image.ImageType = ImageType.TextureArray;

            switch (Backend)
            {
                // specify shader stage source code for each graphics backend
                case GraphicsBackend.OpenGL:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
                    break;
                case GraphicsBackend.Metal:
                    shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                    shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
                    break;
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.Direct3D11:
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
            var pixelData = ArrayPool<Rgba8U>.Shared.Rent(_textureLayersCount * _textureWidth * _textureHeight);

            for (int layer = 0, even_odd = 0, index = 0; layer < _textureLayersCount; layer++)
            {
                for (var y = 0; y < _textureHeight; y++, even_odd++)
                {
                    for (var x = 0; x < _textureWidth; x++, even_odd++, index++)
                    {
                        ref var color = ref pixelData[index];
                        if ((int)(even_odd & 1) > 0)
                        {
                            color = layer switch
                            {
                                0 => Rgba8U.Red,
                                1 => Rgba8U.Lime,
                                2 => Rgba8U.Blue,
                                _ => color
                            };
                        }
                        else
                        {
                            pixelData[index] = Rgba8U.TransparentBlack;
                        }
                    }
                }
            }

            // describe an immutable 2d texture
            var imageDesc = new ImageDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = ImageType.TextureArray,
                Width = _textureWidth,
                Height = _textureHeight,
                Layers = _textureLayersCount,
                Format = PixelFormat.RGBA8,
                MinificationFilter = ImageFilter.Nearest,
                MagnificationFilter = ImageFilter.Nearest,
                WrapW = ImageWrap.Repeat
            };

            // immutable images need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            imageDesc.SetData(pixelData.AsSpan());

            // create the image from the descriptor
            // note: for immutable images this "uploads" the data to the GPU
            var image = GraphicsDevice.CreateImage(ref imageDesc);

            ArrayPool<Rgba8U>.Shared.Return(pixelData);

            return image;
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
            vertices[0].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[0].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[1].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[1].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[2].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[2].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[3].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[3].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 2
            vertices[4].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[4].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[5].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[5].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[6].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[6].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[7].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[7].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 3
            vertices[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[8].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[9].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[9].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[10].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[10].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[11].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[11].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 4
            vertices[12].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[12].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[13].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[13].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[14].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[14].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[15].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[15].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 5
            vertices[16].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[16].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[17].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[17].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[18].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[18].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[19].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[19].TextureCoordinate = new Vector2(0.0f, 1.0f);
            // quad 6
            vertices[20].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[20].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[21].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[21].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[22].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[22].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertices[23].Position = new Vector3(1.0f, 1.0f, -1.0f);
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
