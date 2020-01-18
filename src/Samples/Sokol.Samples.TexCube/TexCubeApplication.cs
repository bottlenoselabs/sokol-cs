using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.TexCube
{
    public class TexCubeApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
            public Vector2 TextureCoordinate;
        }
        
        private SgBuffer _vertexBuffer;
        private SgBuffer _indexBuffer;
        private SgImage _image;
        private SgBindings _bindings;
        private SgPipeline _pipeline;
        private SgShader _shader;
        private SgPassAction _frameBufferPassAction;

        private float _rotationX;
        private float _rotationY;

        public unsafe TexCubeApplication()
        {
            // use memory from the thread's stack for the cube vertices
            var vertices = stackalloc Vertex[4 * 6];

            // describe the vertices of the cube
            // quad 1
            var color1 = RgbaFloat.Red;
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
            var color2 = RgbaFloat.Lime;
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
            var color3 = RgbaFloat.Blue;
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
            var color4 = new RgbaFloat(1f, 0.5f, 0f, 1f);
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
            var color5 = new RgbaFloat(0f, 0.5f, 1f, 1f);
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
            var color6 = new RgbaFloat(1.0f, 0.0f, 0.5f, 1f);
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
            var vertexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<Vertex>() * 4 * 6
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = Sg.MakeBuffer(ref vertexBufferDesc);
            
            // use memory from the thread's stack to create the cube indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2,  0, 2, 3, // quad 1 of cube
                6, 5, 4,  7, 6, 4, // quad 2 of cube
                8, 9, 10,  8, 10, 11, // quad 3 of cube
                14, 13, 12,  15, 14, 12, // quad 4 of cube
                16, 17, 18,  16, 18, 19, // quad 5 of cube
                22, 21, 20,  23, 22, 20 // quad 6 of cube
            };
            
            // describe an immutable index buffer
            var indexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Index,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) indices,
                Size = Marshal.SizeOf<ushort>() * 6 * 6
            };

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = Sg.MakeBuffer(ref indexBufferDesc);
            
            // use memory from the thread's stack to create the checkerboard texture data
            var texturePixels = stackalloc Rgba8UInt[] {
                Rgba8UInt.White, Rgba8UInt.Black, Rgba8UInt.White, Rgba8UInt.Black,
                Rgba8UInt.Black, Rgba8UInt.White, Rgba8UInt.Black, Rgba8UInt.White,
                Rgba8UInt.White, Rgba8UInt.Black, Rgba8UInt.White, Rgba8UInt.Black,
                Rgba8UInt.Black, Rgba8UInt.White, Rgba8UInt.Black, Rgba8UInt.White,
            };

            // describe an immutable 2d texture
            var imageDescription = new SgImageDescription();
            imageDescription.Usage = SgUsage.Immutable;
            imageDescription.Type = SgImageType.Texture2D;
            imageDescription.Width = 4;
            imageDescription.Height = 4;
            imageDescription.Depth = 1;
            imageDescription.MipmapCount = 1;
            imageDescription.PixelFormat = SgPixelFormat.RGBA8;
            ref var subImage = ref imageDescription.Content.SubImage(0, 0);
            subImage.Pointer = (IntPtr) texturePixels;
            subImage.Size = 4 * 4 * Marshal.SizeOf<float>();

            // create the image from the description
            // note: for immutable images this "uploads" the data to the GPU
            _image = Sg.MakeImage(ref imageDescription);
 
            // describe the binding of the vertex and index buffer (not applied yet!)
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.IndexBuffer = _indexBuffer;
            _bindings.FragmentShaderImage(0) = _image;

            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            mvpUniform.Name = Marshal.StringToHGlobalAnsi("mvp");
            mvpUniform.Type = SgShaderUniformType.Matrix4x4;
            shaderDesc.FragmentShader.Image(0).Type = SgImageType.Texture2D;
            // specify shader stage source code for each graphics backend
            string vertexShaderStageSourceCode;
            string fragmentShaderStageSourceCode;
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            shaderDesc.VertexShader.SourceCode = Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.FragmentShader.SourceCode = Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _shader = Sg.MakeShader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(shaderDesc.VertexShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(shaderDesc.VertexShader.SourceCode);
            Marshal.FreeHGlobal(shaderDesc.FragmentShader.SourceCode);
            
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            pipelineDesc.Layout.Attribute(2).Format = SgVertexFormat.Float2;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = SgCullMode.Back;
            pipelineDesc.Rasterizer.SampleCount = sokol_gfx.sg_query_features().msaa_render_targets ? 4 : 1;

            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
            
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Gray);
        }

        protected override unsafe void Draw(int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (60.0f * Math.PI / 180), (float)width / height,
                0.01f, 10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_pipeline);
            Sg.ApplyBindings(ref _bindings);

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // apply the mvp matrix to the vertex shader
            var mvpMatrix = Unsafe.AsPointer(ref modelViewProjectionMatrix);
            Sg.ApplyUniforms(SgShaderStageType.VertexShader, 0, ref modelViewProjectionMatrix);

            // draw the cube into the target of the render pass
            Sg.Draw(0, 36, 1);
            
            // end the framebuffer render pass
            Sg.EndPass();
        }
    }
}