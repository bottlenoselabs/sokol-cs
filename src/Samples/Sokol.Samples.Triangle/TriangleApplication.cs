using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Triangle
{
    public class TriangleApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private sg_buffer _vertexBuffer;
        private sg_bindings _bindings;
        private sg_shader _shader;
        private sg_pipeline _pipeline;

        public unsafe TriangleApplication()
        {
            // use memory from the thread's stack for the triangle vertices
            var vertices = stackalloc Vertex[3];
            
            // describe the vertices of the triangle
            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = RgbaFloat.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = RgbaFloat.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = RgbaFloat.Blue;
            
            // describe an immutable vertex buffer
            var vertexBufferDesc = new sg_buffer_desc();
            vertexBufferDesc.usage = sg_usage.SG_USAGE_IMMUTABLE;
            vertexBufferDesc.type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER;
            // immutable buffers need to specify the data/size in the description
            vertexBufferDesc.content = vertices;
            vertexBufferDesc.size = Marshal.SizeOf<Vertex>() * 3;
            
            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = sg_make_buffer(ref vertexBufferDesc);

            // describe the binding of the vertex buffer (not applied yet!)
            _bindings.vertex_buffer(0) = _vertexBuffer;
            
            // describe the shader program
            var shaderDesc = new sg_shader_desc();
            shaderDesc.attr(0).name = (byte*) Marshal.StringToHGlobalAnsi("position");
            shaderDesc.attr(1).name = (byte*) Marshal.StringToHGlobalAnsi("color0");
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
            Marshal.FreeHGlobal((IntPtr) shaderDesc.attr(0).name);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.attr(1).name);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.vs.source);
            Marshal.FreeHGlobal((IntPtr) shaderDesc.fs.source);
            
            // describe the render pipeline
            var pipelineDesc = new sg_pipeline_desc();
            pipelineDesc.shader = _shader;
            pipelineDesc.layout.attr(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            pipelineDesc.layout.attr(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;

            // create the pipeline resource from the description
            _pipeline = sg_make_pipeline(ref pipelineDesc);
        }

        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.clear(RgbaFloat.Black);
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            sg_apply_pipeline(_pipeline);
            sg_apply_bindings(ref _bindings);
            
            // draw the triangle into the target of the render pass
            sg_draw(0, 3, 1);
            
            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}