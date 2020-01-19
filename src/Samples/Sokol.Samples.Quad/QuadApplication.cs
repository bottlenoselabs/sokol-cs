using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.Quad
{
    public class QuadApplication : App
    {
        private struct QuadVertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _quadVertexBuffer;
        private SgBuffer _quadIndexBuffer;
        private SgBindings _quadBindings;
        private SgShader _shader;
        private SgPipeline _pipeline;
        private SgPassAction _frameBufferPassAction;

        public QuadApplication()
        {
            CreateQuadVertexBuffer();
            CreateQuadIndexBuffer();
            CreateShader();
            CreatePipeline();

            SetQuadBindings();
            SetFrameBufferPassAction();
        }

        private void SetFrameBufferPassAction()
        {
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Black);
        }

        private void SetQuadBindings()
        {
            // describe the binding of the vertex and index buffer (not applied yet!)
            _quadBindings.VertexBuffer(0) = _quadVertexBuffer;
            _quadBindings.IndexBuffer = _quadIndexBuffer;
        }

        private void CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            pipelineDesc.IndexType = SgIndexType.UInt16;

            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateShader()
        {
            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            // specify shader stage source code for each graphics backend
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            
            // create the shader resource from the description
            _shader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private unsafe void CreateQuadIndexBuffer()
        {
            // use memory from the thread's stack to create the quad indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2, // triangle 1 indices
                0, 2, 3 // triangle 2 indices
            };

            // describe an immutable index buffer
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Index,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) indices,
                Size = Marshal.SizeOf<ushort>() * 6
            };

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _quadIndexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private unsafe void CreateQuadVertexBuffer()
        {
            // use memory from the thread's stack for the quad vertices
            var vertices = stackalloc QuadVertex[4];

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
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<QuadVertex>() * 4
            };
            // immutable buffers need to specify the data/size in the description

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _quadVertexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_pipeline);
            Sg.ApplyBindings(ref _quadBindings);
            
            // draw the quad into the target of the render pass
            Sg.Draw(0, 6, 1);
            
            // end framebuffer render pass
            Sg.EndPass();
        }
    }
}