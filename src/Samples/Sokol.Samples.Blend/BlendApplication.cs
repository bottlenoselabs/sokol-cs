using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Blend
{
    public class BlendApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private sg_buffer _vertexBuffer;
        private sg_bindings _bindings;
        private sg_shader _backgroundShader;
        private sg_pipeline _backgroundPipeline;
        private sg_shader _quadShader;
        private sg_pipeline[] _quadPipelines;

        private float _tick;
        private float _rotation;

        private const int NUM_BLEND_FACTORS = 15;

        public unsafe BlendApplication()
            : base(desc: new sg_desc()
            {
                pipeline_pool_size = NUM_BLEND_FACTORS * NUM_BLEND_FACTORS + 1
            })
        {
            // use memory from the thread's stack for the triangle vertices
            var vertices = stackalloc Vertex[4];

            // describe the vertices of the quad
            vertices[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            vertices[0].Color = new RgbaFloat(1.0f, 0.0f, 0.0f, 0.5f);
            vertices[1].Position = new Vector3(+1.0f, -1.0f, 0.0f);
            vertices[1].Color = new RgbaFloat(0.0f, 1.0f, 0.0f, 0.5f);
            vertices[2].Position = new Vector3(-1.0f, +1.0f, 0.0f);
            vertices[2].Color = new RgbaFloat(0.0f, 0.0f, 1.0f, 0.5f);
            vertices[3].Position = new Vector3(+1.0f, +1.0f, 0.0f);
            vertices[3].Color = new RgbaFloat(1.0f, 1.0f, 0.0f, 0.5f);
            
            // describe an immutable vertex buffer
            var vertexBufferDesc = new sg_buffer_desc();
            vertexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            vertexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            vertexBufferDesc.content = vertices;
            vertexBufferDesc.size = Marshal.SizeOf<Vertex>() * 4;

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = sg_make_buffer(ref vertexBufferDesc);
            
            // describe the binding of the vertex and index buffer (not applied yet!)
            _bindings.vertex_buffer(0) = _vertexBuffer;

            // describe the background shader program
            var backgroundShaderDesc = new sg_shader_desc();
            backgroundShaderDesc.fs.uniformBlock(0).size = Marshal.SizeOf<float>();
            ref var tickUniform = ref backgroundShaderDesc.fs.uniformBlock(0).uniform(0);
            tickUniform.name = (byte*) Marshal.StringToHGlobalAnsi("tick");
            tickUniform.type = sg_uniform_type.SG_UNIFORMTYPE_FLOAT;
            // specify shader stage source code for each graphics backend
            string backgroundVertexShaderStageSourceCode;
            string backgroundFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                backgroundVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/backgroundVert.metal");
                backgroundFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/backgroundFrag.metal");
            }
            else
            {
                backgroundVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/background.vert");
                backgroundFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/background.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            backgroundShaderDesc.vs.source = (byte*) Marshal.StringToHGlobalAnsi(backgroundVertexShaderStageSourceCode);
            backgroundShaderDesc.fs.source = (byte*) Marshal.StringToHGlobalAnsi(backgroundFragmentShaderStageSourceCode);
            
            // create the background shader resource from the description
            _backgroundShader = sg_make_shader(ref backgroundShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal((IntPtr) backgroundShaderDesc.fs.uniformBlock(0).uniform(0).name);
            Marshal.FreeHGlobal((IntPtr) backgroundShaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) backgroundShaderDesc.fs.source);

            // describe the background render pipeline
            var backgroundPipelineDesc = new sg_pipeline_desc();
            // note: reusing the vertices of the 3D quads, but only using the first two floats from the position
            backgroundPipelineDesc.layout.buffer(0).stride = 28;
            backgroundPipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT2;
            backgroundPipelineDesc.shader = _backgroundShader;
            backgroundPipelineDesc.primitive_type = sg_primitive_type.SG_PRIMITIVETYPE_TRIANGLE_STRIP;
            backgroundPipelineDesc.rasterizer.sample_count = 4;
            
            // create the background pipeline resource from the description
            _backgroundPipeline = sg_make_pipeline(ref backgroundPipelineDesc);
            
            // describe the quad shader program
            var quadShaderDesc = new sg_shader_desc();
            quadShaderDesc.vs.uniformBlock(0).size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref quadShaderDesc.vs.uniformBlock(0).uniform(0);
            mvpUniform.name = (byte*) Marshal.StringToHGlobalAnsi("mvp");
            mvpUniform.type = sg_uniform_type.SG_UNIFORMTYPE_MAT4;
            // specify shader stage source code for each graphics backend
            string quadVertexShaderStageSourceCode;
            string quadFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                quadVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/quadVert.metal");
                quadFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/quadFrag.metal");
            }
            else
            {
                quadVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/quad.vert");
                quadFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/quad.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            quadShaderDesc.vs.source = (byte*) Marshal.StringToHGlobalAnsi(quadVertexShaderStageSourceCode);
            quadShaderDesc.fs.source = (byte*) Marshal.StringToHGlobalAnsi(quadFragmentShaderStageSourceCode);
            
            // create the quad shader resource from the description
            _quadShader = sg_make_shader(ref quadShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal((IntPtr) quadShaderDesc.vs.uniformBlock(0).uniform(0).name);
            Marshal.FreeHGlobal((IntPtr) quadShaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) quadShaderDesc.fs.source);
            
            // describe the quad render pipelines
            var quadPipelineDesc = new sg_pipeline_desc();
            quadPipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            quadPipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            quadPipelineDesc.shader = _quadShader;
            quadPipelineDesc.primitive_type = sg_primitive_type.SG_PRIMITIVETYPE_TRIANGLE_STRIP;
            quadPipelineDesc.blend.enabled = true;
            quadPipelineDesc.blend.blend_color = RgbaFloat.Red;
            quadPipelineDesc.rasterizer.sample_count = 4;
            
            _quadPipelines = new sg_pipeline[NUM_BLEND_FACTORS * NUM_BLEND_FACTORS];
            for (var src = 0; src < NUM_BLEND_FACTORS; src++)
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++)
                {
                    var srcBlend = (sg_blend_factor) (src + 1);
                    var dstBlend = (sg_blend_factor) (dst + 1);
                    
                    quadPipelineDesc.blend.src_factor_rgb = srcBlend;
                    quadPipelineDesc.blend.dst_factor_rgb = dstBlend;
                    quadPipelineDesc.blend.src_factor_alpha = sg_blend_factor.SG_BLENDFACTOR_ONE;
                    quadPipelineDesc.blend.dst_factor_alpha = sg_blend_factor.SG_BLENDFACTOR_ZERO;

                    var index = dst + src * NUM_BLEND_FACTORS;
                    ref var pipeline = ref _quadPipelines[index];
                    pipeline = sg_make_pipeline(ref quadPipelineDesc);
                }
            }
        }

        protected override unsafe void Draw(int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (90.0f * Math.PI / 180), (float)width / height,
                0.01f, 100.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 0.0f, 25.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.dontCare();
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the background render pipeline and bindings for the render pass
            sg_apply_pipeline(_backgroundPipeline);
            sg_apply_bindings(ref _bindings);

            // apply the background tick uniform to the background fragment shader
            var tickPointer = Unsafe.AsPointer(ref _tick);
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_FS, 0, tickPointer, Marshal.SizeOf<float>());
            
            // draw the background quad into the target of the render pass
            sg_draw(0, 4, 1);

            // for every type of blend factor (each one is a different render pipeline)...
            var rotationForBlendFactor = _rotation;
            for (var src = 0; src < NUM_BLEND_FACTORS; src++) 
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++, rotationForBlendFactor += 0.6f)
                {
                    // rotate quad and create vertex shader mvp matrix
                    var rotationMatrix = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, rotationForBlendFactor);
                    var x = (dst - NUM_BLEND_FACTORS / 2) * 3.0f;
                    var y = (src - NUM_BLEND_FACTORS / 2) * 2.2f;
                    var translationMatrix = Matrix4x4.CreateTranslation(x, y, 0.0f);
                    var modelMatrix = rotationMatrix * translationMatrix;
                    var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;

                    // apply the quad render pipeline and bindings for the render pass
                    var pipelineIndex = dst + src * NUM_BLEND_FACTORS;
                    sg_apply_pipeline(_quadPipelines[pipelineIndex]);
                    sg_apply_bindings(ref _bindings);
                    
                    // apply the mvp matrix to the vertex shader
                    var mvpMatrix = Unsafe.AsPointer(ref modelViewProjectionMatrix);
                    sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, mvpMatrix, Marshal.SizeOf<Matrix4x4>());
       
                    // draw the quad into the target of the render pass
                    sg_draw(0, 4, 1);
                }
            }
            
            // end the framebuffer render pass
            sg_end_pass();
            
            // update rotation and tick values for next frame
            _rotation += 0.6f * 0.20f;
            _tick += 1.0f * 0.20f;
        }
    }
}