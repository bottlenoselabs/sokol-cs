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
        
        private SgBuffer _vertexBuffer;
        private SgBindings _bindings;
        private SgShader _shader;
        private SgPipeline _pipeline;

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
            var vertexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<Vertex>() * 3
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = new SgBuffer(ref vertexBufferDesc);

            // describe the binding of the vertex buffer (not applied yet!)
            _bindings.Set(ref _vertexBuffer);

            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.Attribute(0).Name = Marshal.StringToHGlobalAnsi("position");
            shaderDesc.Attribute(1).Name = Marshal.StringToHGlobalAnsi("color0");
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
            shaderDesc.VertexShader.Source = Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.FragmentShader.Source = Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);

            // create the shader resource from the description
            _shader = new SgShader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(shaderDesc.Attribute(0).Name);
            Marshal.FreeHGlobal(shaderDesc.Attribute(1).Name);
            Marshal.FreeHGlobal(shaderDesc.VertexShader.Source);
            Marshal.FreeHGlobal(shaderDesc.FragmentShader.Source);
            
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;

            // create the pipeline resource from the description
            _pipeline = new SgPipeline(ref pipelineDesc);
        }

        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.clear(RgbaFloat.Black);
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            _pipeline.Apply();
            _bindings.Apply();
            
            // draw the triangle into the target of the render pass
            sg_draw(0, 3, 1);
            
            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}