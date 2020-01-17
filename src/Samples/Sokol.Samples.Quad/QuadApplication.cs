using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;
using static SDL2.SDL;

namespace Sokol.Samples.Quad
{
    public class QuadApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _vertexBuffer;
        private SgBuffer _indexBuffer;
        private SgBindings _bindings;
        private SgShader _shader;
        private SgPipeline _pipeline;
        private SgPassAction _frameBufferPassAction;

        public unsafe QuadApplication()
        {
            // use memory from the thread's stack for the quad vertices
            var vertices = stackalloc Vertex[4];
            
            // describe the vertices of the quad
            vertices[0].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[0].Color = RgbaFloat.Red;
            vertices[1].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[1].Color = RgbaFloat.Green;
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[2].Color = RgbaFloat.Blue;
            vertices[3].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[3].Color = RgbaFloat.Yellow;

            // describe an immutable vertex buffer
            var vertexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<Vertex>() * 4
            };
            // immutable buffers need to specify the data/size in the description

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = new SgBuffer(ref vertexBufferDesc);
            
            // use memory from the thread's stack to create the quad indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2, // triangle 1 indices
                0, 2, 3 // triangle 2 indices
            };
            
            // describe an immutable index buffer
            var indexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Index,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) indices,
                Size = Marshal.SizeOf<ushort>() * 6
            };

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = new SgBuffer(ref indexBufferDesc);
            
            // describe the binding of the vertex and index buffer (not applied yet!)
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.IndexBuffer = _indexBuffer;

            // describe the shader program
            var shaderDesc = new SgShaderDescription();
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
            shaderDesc.VertexShader.Source = Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.FragmentShader.Source = Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _shader = new SgShader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(shaderDesc.VertexShader.Source);
            Marshal.FreeHGlobal(shaderDesc.FragmentShader.Source);
            
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            
            // create the pipeline resource from the description
            _pipeline = new SgPipeline(ref pipelineDesc);
            
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Black);
        }
        
        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            SgDefaultPass.Begin(ref _frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            _pipeline.Apply();
            _bindings.Apply();
            
            // draw the quad into the target of the render pass
            sg_draw(0, 6, 1);
            
            // end framebuffer render pass
            SgDefaultPass.End();
        }
    }
}