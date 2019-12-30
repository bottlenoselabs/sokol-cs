using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Sokol.Samples.BufferOffsets
{
    public class BufferOffsetsApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector2 Position;
            public RgbFloat Color;
        }
        
        private sg_buffer _vertexBuffer;
        private sg_buffer _indexBuffer;
        private sg_bindings _bindings;
        private sg_shader _shader;
        private sg_pipeline _pipeline;

        public unsafe BufferOffsetsApplication()
        {
            // use memory from the thread's stack for the triangle and the quad vertices
            var vertices = stackalloc Vertex[7];
            
            // describe the vertices of the quad
            vertices[0].Position = new Vector2(0f, 0.55f);
            vertices[0].Color = RgbFloat.Red;
            vertices[1].Position = new Vector2(0.25f, 0.05f);
            vertices[1].Color = RgbFloat.Green;
            vertices[2].Position = new Vector2(-0.25f, 0.05f);
            vertices[2].Color = RgbFloat.Blue;

            // describe the vertices of the quad
            vertices[3].Position = new Vector2(-0.25f, -0.05f);
            vertices[3].Color = RgbFloat.Blue;
            vertices[4].Position = new Vector2(0.25f, -0.05f);
            vertices[4].Color = RgbFloat.Green;
            vertices[5].Position = new Vector2(0.25f, -0.55f);
            vertices[5].Color = RgbFloat.Red;
            vertices[6].Position = new Vector2(-0.25f, -0.55f);
            vertices[6].Color = RgbFloat.Yellow;

            // describe an immutable vertex buffer
            var vertexBufferDesc = new sg_buffer_desc();
            vertexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            vertexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            vertexBufferDesc.content = vertices;
            vertexBufferDesc.size = Marshal.SizeOf<Vertex>() * (3 + 4);
            
            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = sg_make_buffer(ref vertexBufferDesc);
            
            // use memory from the thread's stack to create the quad indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2, // triangle
                0, 1, 2, // triangle 1 of quad
                0, 2, 3 // triangle 2 of quad
            };
            
            // describe an immutable index buffer
            var indexBufferDesc = new sg_buffer_desc();
            indexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            indexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            indexBufferDesc.content = indices;
            indexBufferDesc.size = Marshal.SizeOf<ushort>() * (3 + 6);
            
            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = sg_make_buffer(ref indexBufferDesc);
            
            // describe the binding of the vertex and index buffer (not applied yet!)
            _bindings.vertex_buffer(0) = _vertexBuffer;
            _bindings.index_buffer = _indexBuffer;
            
            // describe the shader program
            var shaderDesc = new sg_shader_desc();
            string vertexShaderStageSourceCode;
            string fragmentShaderStageSourceCode;
            // specify shader stage source code for each graphics backend
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
            Marshal.FreeHGlobal((IntPtr) shaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.fs.source);
            
            // describe the render pipeline
            var pipelineDesc = new sg_pipeline_desc();
            pipelineDesc.shader = _shader;
            pipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT2;
            pipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            pipelineDesc.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
            
            // create the pipeline resource from the description
            _pipeline = sg_make_pipeline(ref pipelineDesc);
        }
        
        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.clear(0x8080ff);
            sg_begin_default_pass(ref frameBufferPassAction, width, height);

            // apply the render pipeline for the render pass
            sg_apply_pipeline(_pipeline);

            // set and apply the bindings necessary to render the triangle for the render pass
            _bindings.vertex_buffer_offset(0) = 0;
            _bindings.index_buffer_offset = 0;
            sg_apply_bindings(ref _bindings);
            
            // draw the triangle into the target of the render pass
            sg_draw(0, 3, 1);
            
            // set and apply the bindings necessary to render the quad for the render pass
            _bindings.vertex_buffer_offset(0) = 3 * Marshal.SizeOf<Vertex>();
            _bindings.index_buffer_offset = 3 * Marshal.SizeOf<ushort>();
            sg_apply_bindings(ref _bindings);
            
            // draw the quad into the target of the render pass
            sg_draw(0, 6, 1);

            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}