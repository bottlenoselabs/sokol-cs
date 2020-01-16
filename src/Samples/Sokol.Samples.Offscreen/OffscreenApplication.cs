using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;
using static SDL2.SDL;

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
        private sg_image _renderTargetColorImage;
        private sg_image _renderTargetDepthImage;
        private sg_pass _offscreenRenderPass; 
        private SgBindings _frameBufferBindings;
        private SgBindings _offscreenBindings;
        private sg_shader _offscreenShader;
        private sg_shader _frameBufferShader;
        private sg_pipeline _frameBufferPipeline;
        private sg_pipeline _offscreenPipeline;

        private float _rotationX;
        private float _rotationY;

        public unsafe OffscreenApplication()
        {
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLEBUFFERS, 1);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLESAMPLES, 4);
            
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
            var vertexBufferDescription = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<Vertex>() * 4 * 6
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = new SgBuffer(ref vertexBufferDescription);
            
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
            _indexBuffer = new SgBuffer(ref indexBufferDesc);

            // describe a 2d texture render target
            var offscreenImageDesc = new sg_image_desc();
            offscreenImageDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            offscreenImageDesc.type = sg_image_type.SG_IMAGETYPE_2D;
            offscreenImageDesc.render_target = true;
            offscreenImageDesc.width = 512;
            offscreenImageDesc.height = 512;
            offscreenImageDesc.depth = 1;
            offscreenImageDesc.num_mipmaps = 1;
            offscreenImageDesc.pixel_format = sg_pixel_format.SG_PIXELFORMAT_RGBA8;
            offscreenImageDesc.min_filter = sg_filter.SG_FILTER_LINEAR;
            offscreenImageDesc.mag_filter = sg_filter.SG_FILTER_LINEAR;
            offscreenImageDesc.sample_count = sg_query_features().msaa_render_targets ? 4 : 1;
     
            // create the color render target image from the description
            _renderTargetColorImage = sg_make_image(ref offscreenImageDesc);
            
            // create the depth render target image from the description
            offscreenImageDesc.pixel_format = sg_pixel_format.SG_PIXELFORMAT_DEPTH;
            _renderTargetDepthImage = sg_make_image(ref offscreenImageDesc);
            
            // describe the offscreen render pass
            var passDesc = new sg_pass_desc();
            passDesc.color_attachment(0).image = _renderTargetColorImage;
            passDesc.depth_stencil_attachment.image = _renderTargetDepthImage;
            
            // create offscreen render pass from description
            _offscreenRenderPass = sg_make_pass(ref passDesc);

            // describe the bindings for rendering a non-textured cube into the render target (not applied yet!)
            _offscreenBindings.SetVertexBuffer(ref _vertexBuffer);
            _offscreenBindings.SetIndexBuffer(ref _indexBuffer);

            // describe the bindings for using the offscreen render target as the sampled texture (not applied yet!)
            _frameBufferBindings.SetVertexBuffer(ref _vertexBuffer);
            _frameBufferBindings.SetIndexBuffer(ref _indexBuffer);
            _frameBufferBindings.CStruct.fs_image(0) = _renderTargetColorImage;
            
            // describe the offscreen shader program
            var offscreenShaderDesc = new sg_shader_desc();
            offscreenShaderDesc.vs.uniformBlock(0).size = Marshal.SizeOf<Matrix4x4>();
            ref var offscreenMvpUniform = ref offscreenShaderDesc.vs.uniformBlock(0).uniform(0);
            offscreenMvpUniform.name = (byte*) Marshal.StringToHGlobalAnsi("mvp");
            offscreenMvpUniform.type = sg_uniform_type.SG_UNIFORMTYPE_MAT4;
            // specify shader stage source code for each graphics backend
            string offscreenVertexShaderStageSourceCode;
            string offscreenFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                offscreenVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/offscreenVert.metal");
                offscreenFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/offscreenFrag.metal");
            }
            else
            {
                offscreenVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/offscreen.vert");
                offscreenFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/offscreen.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            offscreenShaderDesc.vs.source = (byte*) Marshal.StringToHGlobalAnsi(offscreenVertexShaderStageSourceCode);
            offscreenShaderDesc.fs.source = (byte*) Marshal.StringToHGlobalAnsi(offscreenFragmentShaderStageSourceCode);
            
            // create the offscreen shader resource from the description
            _offscreenShader = sg_make_shader(ref offscreenShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal((IntPtr) offscreenShaderDesc.vs.uniformBlock(0).uniform(0).name);
            Marshal.FreeHGlobal((IntPtr) offscreenShaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) offscreenShaderDesc.fs.source);
            
            var frameBufferShaderDesc = new sg_shader_desc();
            frameBufferShaderDesc.vs.uniformBlock(0).size = Marshal.SizeOf<Matrix4x4>();
            ref var frameBufferMvpUniform = ref frameBufferShaderDesc.vs.uniformBlock(0).uniform(0);
            frameBufferMvpUniform.name = (byte*) Marshal.StringToHGlobalAnsi("mvp");
            frameBufferMvpUniform.type = sg_uniform_type.SG_UNIFORMTYPE_MAT4;
            frameBufferShaderDesc.fs.image(0).name = (byte*) Marshal.StringToHGlobalAnsi("tex");
            frameBufferShaderDesc.fs.image(0).type = sg_image_type.SG_IMAGETYPE_2D;
            // specify shader stage source code for each graphics backend
            string frameBufferVertexShaderStageSourceCode;
            string frameBufferFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                frameBufferVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                frameBufferFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else
            {
                frameBufferVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                frameBufferFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            frameBufferShaderDesc.vs.source = (byte*) Marshal.StringToHGlobalAnsi(frameBufferVertexShaderStageSourceCode);
            frameBufferShaderDesc.fs.source = (byte*) Marshal.StringToHGlobalAnsi(frameBufferFragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _frameBufferShader = sg_make_shader(ref frameBufferShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal((IntPtr) frameBufferShaderDesc.vs.uniformBlock(0).uniform(0).name);
            Marshal.FreeHGlobal((IntPtr) frameBufferShaderDesc.fs.image(0).name);
            Marshal.FreeHGlobal((IntPtr) frameBufferShaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) frameBufferShaderDesc.fs.source);
            
            // describe the offscreen render pipeline
            var offscreenPipelineDesc = new sg_pipeline_desc();
            // skip texture coordinates
            offscreenPipelineDesc.layout.buffer(0).stride = 36;
            offscreenPipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            offscreenPipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            offscreenPipelineDesc.shader = _offscreenShader;
            offscreenPipelineDesc.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
            offscreenPipelineDesc.depth_stencil.depth_compare_func = sg_compare_func.SG_COMPAREFUNC_LESS_EQUAL;
            offscreenPipelineDesc.depth_stencil.depth_write_enabled = true;
            offscreenPipelineDesc.blend.color_format = sg_pixel_format.SG_PIXELFORMAT_RGBA8;
            offscreenPipelineDesc.blend.depth_format = sg_pixel_format.SG_PIXELFORMAT_DEPTH;
            offscreenPipelineDesc.rasterizer.cull_mode = sg_cull_mode.SG_CULLMODE_BACK;
            offscreenPipelineDesc.rasterizer.sample_count = 4;

            // create the offscreen pipeline resource from the description
            _offscreenPipeline = sg_make_pipeline(ref offscreenPipelineDesc);
            
            // describe the framebuffer render pipeline
            var frameBufferPipelineDesc = new sg_pipeline_desc();
            frameBufferPipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            frameBufferPipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            frameBufferPipelineDesc.layout.attr(2).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT2;
            frameBufferPipelineDesc.shader = _frameBufferShader;
            frameBufferPipelineDesc.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
            frameBufferPipelineDesc.depth_stencil.depth_compare_func = sg_compare_func.SG_COMPAREFUNC_LESS_EQUAL;
            frameBufferPipelineDesc.depth_stencil.depth_write_enabled = true;
            frameBufferPipelineDesc.rasterizer.cull_mode = sg_cull_mode.SG_CULLMODE_BACK;
            frameBufferPipelineDesc.rasterizer.sample_count = 4;

            // create the framebuffer pipeline resource from the description
            _frameBufferPipeline = sg_make_pipeline(ref frameBufferPipelineDesc);
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
            var offscreenPassAction = sg_pass_action.clear(RgbaFloat.Black);
            sg_begin_pass(_offscreenRenderPass, ref offscreenPassAction);

            // apply the render pipeline and bindings for the offscreen render pass
            sg_apply_pipeline(_offscreenPipeline);
            _offscreenBindings.Apply();

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // apply the mvp matrix to the offscreen vertex shader
            var mvpMatrix = Unsafe.AsPointer(ref modelViewProjectionMatrix);
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, mvpMatrix, Marshal.SizeOf<Matrix4x4>());
            
            // draw the non-textured cube into the target of the offscreen render pass
            sg_draw(0, 36, 1);
            
            // end the offscreen render pass
            sg_end_pass();
            
            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.clear(0x0040FFFF);
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the framebuffer render pass
            sg_apply_pipeline(_frameBufferPipeline);
            _frameBufferBindings.Apply();

            // apply the mvp matrix to the framebuffer vertex shader
            var mvpMatrix2 = Unsafe.AsPointer(ref modelViewProjectionMatrix);
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, mvpMatrix2, Marshal.SizeOf<Matrix4x4>());
            
            // draw the textured cube into the target of the framebuffer render pass
            sg_draw(0, 36, 1);
            
            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}