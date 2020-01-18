using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Offscreen
{
    public class OffscreenApplication : App
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
        private SgImage _renderTargetColorImage;
        private SgImage _renderTargetDepthImage;
        private SgPass _offscreenRenderPass; 
        private SgBindings _frameBufferBindings;
        private SgBindings _offscreenBindings;
        private SgShader _offscreenShader;
        private SgShader _frameBufferShader;
        private SgPipeline _frameBufferPipeline;
        private SgPipeline _offscreenPipeline;
        private SgPassAction _frameBufferPassAction;
        private SgPassAction _offscreenPassAction;

        private float _rotationX;
        private float _rotationY;

        public unsafe OffscreenApplication()
        {
            // use memory from the thread's stack for the cube vertices
            var vertices = stackalloc Vertex[4 * 6];

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

            // describe a 2d texture render target
            var offscreenImageDesc = new SgImageDescription();
            offscreenImageDesc.Usage = SgUsage.Immutable;
            offscreenImageDesc.Type = SgImageType.Texture2D;
            offscreenImageDesc.IsRenderTarget = true;
            offscreenImageDesc.Width = 512;
            offscreenImageDesc.Height = 512;
            offscreenImageDesc.Depth = 1;
            offscreenImageDesc.MipmapCount = 1;
            offscreenImageDesc.PixelFormat = SgPixelFormat.RGBA8;
            offscreenImageDesc.MinificationFilter = SgTextureFilter.Linear;
            offscreenImageDesc.MagnificationFilter = SgTextureFilter.Linear;
            offscreenImageDesc.SampleCount = Sg.QueryFeatures().MSAARenderTargets ? 4 : 1;
     
            // create the color render target image from the description
            _renderTargetColorImage = Sg.MakeImage(ref offscreenImageDesc);
            
            // create the depth render target image from the description
            offscreenImageDesc.PixelFormat = SgPixelFormat.Depth;
            _renderTargetDepthImage = Sg.MakeImage(ref offscreenImageDesc);
            
            // describe the offscreen render pass
            var passDesc = new SgPassDescription();
            passDesc.ColorAttachment(0).Image = _renderTargetColorImage;
            passDesc.DepthStencilAttachment.Image = _renderTargetDepthImage;
            
            // create offscreen render pass from description
            _offscreenRenderPass = Sg.MakePass(ref passDesc);

            // describe the bindings for rendering a non-textured cube into the render target (not applied yet!)
            _offscreenBindings.VertexBuffer(0) = _vertexBuffer;
            _offscreenBindings.IndexBuffer = _indexBuffer;

            // describe the bindings for using the offscreen render target as the sampled texture (not applied yet!)
            _frameBufferBindings.VertexBuffer(0) = _vertexBuffer;
            _frameBufferBindings.IndexBuffer = _indexBuffer;
            _frameBufferBindings.FragmentShaderImage(0) = _renderTargetColorImage;
            
            // describe the offscreen shader program
            var offscreenShaderDesc = new SgShaderDescription();
            offscreenShaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var offscreenMvpUniform = ref offscreenShaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            offscreenMvpUniform.Name = Marshal.StringToHGlobalAnsi("mvp");
            offscreenMvpUniform.Type = SgShaderUniformType.Matrix4x4;
            // specify shader stage source code for each graphics backend
            string offscreenVertexShaderStageSourceCode;
            string offscreenFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal_macOS)
            {
                offscreenVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/offscreenVert.metal");
                offscreenFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/offscreenFrag.metal");
            }
            else if (GraphicsBackend == GraphicsBackend.OpenGL_Core)
            {
                offscreenVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/offscreen.vert");
                offscreenFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/offscreen.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            offscreenShaderDesc.VertexShader.SourceCode = Marshal.StringToHGlobalAnsi(offscreenVertexShaderStageSourceCode);
            offscreenShaderDesc.FragmentShader.SourceCode = Marshal.StringToHGlobalAnsi(offscreenFragmentShaderStageSourceCode);
            
            // create the offscreen shader resource from the description
            _offscreenShader = Sg.MakeShader(ref offscreenShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(offscreenShaderDesc.VertexShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(offscreenShaderDesc.VertexShader.SourceCode);
            Marshal.FreeHGlobal(offscreenShaderDesc.FragmentShader.SourceCode);
            
            var frameBufferShaderDesc = new SgShaderDescription();
            frameBufferShaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var frameBufferMvpUniform = ref frameBufferShaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            frameBufferMvpUniform.Name = Marshal.StringToHGlobalAnsi("mvp");
            frameBufferMvpUniform.Type = SgShaderUniformType.Matrix4x4;
            frameBufferShaderDesc.FragmentShader.Image(0).Name = Marshal.StringToHGlobalAnsi("tex");
            frameBufferShaderDesc.FragmentShader.Image(0).Type = SgImageType.Texture2D;
            // specify shader stage source code for each graphics backend
            string frameBufferVertexShaderStageSourceCode;
            string frameBufferFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal_macOS)
            {
                frameBufferVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                frameBufferFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else if (GraphicsBackend == GraphicsBackend.Metal_macOS)
            {
                frameBufferVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                frameBufferFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            frameBufferShaderDesc.VertexShader.SourceCode = Marshal.StringToHGlobalAnsi(frameBufferVertexShaderStageSourceCode);
            frameBufferShaderDesc.FragmentShader.SourceCode = Marshal.StringToHGlobalAnsi(frameBufferFragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _frameBufferShader = Sg.MakeShader(ref frameBufferShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(frameBufferShaderDesc.VertexShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(frameBufferShaderDesc.FragmentShader.Image(0).Name);
            Marshal.FreeHGlobal(frameBufferShaderDesc.VertexShader.SourceCode);
            Marshal.FreeHGlobal(frameBufferShaderDesc.FragmentShader.SourceCode);
            
            // describe the offscreen render pipeline
            var offscreenPipelineDesc = new SgPipelineDescription();
            // skip texture coordinates
            offscreenPipelineDesc.Layout.Buffer(0).Stride = 36;
            offscreenPipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            offscreenPipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            offscreenPipelineDesc.Shader = _offscreenShader;
            offscreenPipelineDesc.IndexType = SgIndexType.UInt16;
            offscreenPipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            offscreenPipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            offscreenPipelineDesc.Blend.ColorFormat = SgPixelFormat.RGBA8;
            offscreenPipelineDesc.Blend.DepthFormat = SgPixelFormat.Depth;
            offscreenPipelineDesc.Rasterizer.CullMode = SgCullMode.Back;
            offscreenPipelineDesc.Rasterizer.SampleCount = Sg.QueryFeatures().MSAARenderTargets ? 4 : 1;

            // create the offscreen pipeline resource from the description
            _offscreenPipeline = Sg.MakePipeline(ref offscreenPipelineDesc);
            
            // describe the framebuffer render pipeline
            var frameBufferPipelineDesc = new SgPipelineDescription();
            frameBufferPipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            frameBufferPipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            frameBufferPipelineDesc.Layout.Attribute(2).Format = SgVertexFormat.Float2;
            frameBufferPipelineDesc.Shader = _frameBufferShader;
            frameBufferPipelineDesc.IndexType = SgIndexType.UInt16;
            frameBufferPipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            frameBufferPipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            frameBufferPipelineDesc.Rasterizer.CullMode = SgCullMode.Back;
            frameBufferPipelineDesc.Rasterizer.SampleCount = Sg.QueryFeatures().MSAARenderTargets ? 4 : 1;

            // create the framebuffer pipeline resource from the description
            _frameBufferPipeline = Sg.MakePipeline(ref frameBufferPipelineDesc);
            
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(0x0040FFFF);
            
            // set the offscreen render pass action
            _offscreenPassAction = SgPassAction.Clear(RgbaFloat.Black);
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
            
            // begin the offscreen render pass
            Sg.BeginPass(_offscreenRenderPass, ref _offscreenPassAction);

            // apply the render pipeline and bindings for the offscreen render pass
            Sg.ApplyPipeline(_offscreenPipeline);
            Sg.ApplyBindings(ref _offscreenBindings);

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // apply the mvp matrix to the offscreen vertex shader
            Sg.ApplyUniforms(SgShaderStageType.VertexShader, 0, ref modelViewProjectionMatrix);
            
            // draw the non-textured cube into the target of the offscreen render pass
            Sg.Draw(0, 36, 1);
            
            // end the offscreen render pass
            Sg.EndPass();
            
            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the framebuffer render pass
            Sg.ApplyPipeline(_frameBufferPipeline);
            Sg.ApplyBindings(ref _frameBufferBindings);

            // apply the mvp matrix to the framebuffer vertex shader
            Sg.ApplyUniforms(SgShaderStageType.VertexShader, 0, ref modelViewProjectionMatrix);
            
            // draw the textured cube into the target of the framebuffer render pass
            Sg.Draw(0, 36, 1);
            
            // end the framebuffer render pass
            Sg.EndPass();
        }
    }
}