using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.NonInterleaved
{
    public class NonInterleavedApplication : App
    {
        private sg_buffer _vertexBuffer;
        private sg_buffer _indexBuffer;
        private sg_bindings _bindings;
        private sg_pipeline _pipeline;
        private sg_shader _shader;

        private float _rotationX;
        private float _rotationY;

        public unsafe NonInterleavedApplication()
        {
            // use memory from the thread's stack for the cube's vertices
            var vertices = stackalloc float[]
            {
                // quad 1 (4 Vector3 positions as floats)
                -1.0f, -1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 
                1.0f, 1.0f, -1.0f, -1.0f, 1.0f, -1.0f,
                
                // quad 2
                -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f,
                
                // quad 3
                -1.0f, -1.0f, -1.0f, -1.0f, 1.0f, -1.0f,
                -1.0f, 1.0f, 1.0f, -1.0f, -1.0f, 1.0f,
                
                // quad 4
                1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, -1.0f, 1.0f,
                
                // quad 5
                -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, 1.0f, 
                1.0f, -1.0f, 1.0f, 1.0f, -1.0f, -1.0f,
                
                // quad 6
                -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, -1.0f,

                // color 1 (4 RGBAFloat colors)
                1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 
                1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f,
                
                // color 2
                0.5f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.0f, 1.0f, 
                0.5f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.0f, 1.0f,
                
                // color 3
                0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 
                0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f,
                
                // color 4
                1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 
                1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f,
                
                // color 5
                0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 
                0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f,
                
                // color 6
                1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 
                1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f
            };
            
            // describe an immutable vertex buffer
            var vertexBufferDesc = new sg_buffer_desc();
            vertexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            vertexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            vertexBufferDesc.content = vertices;
            vertexBufferDesc.size = Marshal.SizeOf<float>() * (12 * 6 + 16 * 6);

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = sg_make_buffer(ref vertexBufferDesc);

            // use memory from the thread's stack for the cube's indices
            var indices = stackalloc ushort[]
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
            var indexBufferDesc = new sg_buffer_desc();
            indexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            indexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            indexBufferDesc.content = indices;
            indexBufferDesc.size = Marshal.SizeOf<ushort>() * 6 * 6;
            
            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = sg_make_buffer(ref indexBufferDesc);

            // describe the binding of the vertex and index buffers (not applied yet!)
            _bindings.vertex_buffer(0) = _vertexBuffer;
            _bindings.vertex_buffer(1) = _vertexBuffer;
            _bindings.vertex_buffer_offset(1) = 12 * 6 * sizeof(float);
            _bindings.index_buffer = _indexBuffer;
            
            // describe the shader program
            var shaderDesc = new sg_shader_desc();
            shaderDesc.vs.uniformBlock(0).size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.vs.uniformBlock(0).uniform(0);
            mvpUniform.name = (byte*) Marshal.StringToHGlobalAnsi("mvp");
            mvpUniform.type = sg_uniform_type.SG_UNIFORMTYPE_MAT4;
            
            // specify shader stage source code for each graphics backend
            string vertexShaderStageSourceCode;
            string fragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            shaderDesc.vs.source = (byte*) Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.fs.source = (byte*) Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);
            
            
            // create the shader resource from the description
            _shader = sg_make_shader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal((IntPtr) shaderDesc.vs.uniformBlock(0).uniform(0).name);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.fs.source);

            // describe the render pipeline
            var pipelineDesc = new sg_pipeline_desc();
            pipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            pipelineDesc.layout.attr(0).buffer_index = 0;
            pipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            pipelineDesc.layout.attr(1).buffer_index = 1;
            pipelineDesc.shader = _shader;
            pipelineDesc.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
            pipelineDesc.depth_stencil.depth_compare_func = sg_compare_func.SG_COMPAREFUNC_LESS_EQUAL;
            pipelineDesc.depth_stencil.depth_write_enabled = true;
            pipelineDesc.rasterizer.cull_mode = sg_cull_mode.SG_CULLMODE_BACK;
            pipelineDesc.rasterizer.sample_count = 4;

            // create the pipeline resource from the description
            _pipeline = sg_make_pipeline(ref pipelineDesc);
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
            var frameBufferPassAction = sg_pass_action.clear(RgbaFloat.Gray);
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            sg_apply_pipeline(_pipeline);
            sg_apply_bindings(ref _bindings);

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // apply the mvp matrix to the vertex shader
            var modelViewProjectionMatrixPointer = Unsafe.AsPointer(ref modelViewProjectionMatrix);
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, modelViewProjectionMatrixPointer, Marshal.SizeOf<Matrix4x4>());

            // draw the cube into the target of the render pass
            sg_draw(0, 36, 1);
            
            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}