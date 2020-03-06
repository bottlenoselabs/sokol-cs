using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Triangle
{
    public class TriangleApplication : App
    {
        private struct TriangleVertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _triangleVertexBuffer;
        private SgBindings _triangleBindings;
        private SgShader _shader;
        private SgPipeline _pipeline;
        private SgPassAction _frameBufferPassAction;

        public TriangleApplication()
        {
            CreateTriangleVertexBuffer();
            CreateShader();
            CreatePipeline();
            
            SetTriangleBindings();
            SetFrameBufferPassAction();
        }

        private void SetFrameBufferPassAction()
        {
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Red);
        }

        private void SetTriangleBindings()
        {
            // describe the binding of the vertex buffer (not applied yet!)
            _triangleBindings.VertexBuffer(0) = _triangleVertexBuffer;
        }

        private unsafe void CreateTriangleVertexBuffer()
        {
            // create some memory for the triangle vertices
            var vertices = new TriangleVertex[3];
            using var verticesMemoryHandle = vertices.AsMemory().Pin();
            
            // describe the vertices of the triangle
            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = RgbaFloat.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = RgbaFloat.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = RgbaFloat.Blue;

            // describe an immutable vertex buffer
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) verticesMemoryHandle.Pointer,
                Size = Marshal.SizeOf<TriangleVertex>() * 3
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _triangleVertexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private void CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription
            {
                Shader = _shader
            };
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;

            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateShader()
        {
            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            
            // allocate some memory on the unmanaged heap for the attribute names
            shaderDesc.Attribute(0).Name = Marshal.StringToHGlobalAnsi("position");
            shaderDesc.Attribute(1).Name = Marshal.StringToHGlobalAnsi("color0");

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

            // create the shader resource from the description
            _shader = Sg.MakeShader(ref shaderDesc, vertexShaderStageSourceCode, fragmentShaderStageSourceCode);
            
            // after we create the shader, release any allocations we had to make
            Marshal.FreeHGlobal(shaderDesc.Attribute(0).Name);
            Marshal.FreeHGlobal(shaderDesc.Attribute(1).Name);
        }

        protected override void Draw(int width, int height)
        {
            // begin a frame buffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);

            // apply the render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_pipeline);
            Sg.ApplyBindings(ref _triangleBindings);
            
            // draw the triangle into the target of the render pass
            Sg.Draw(0, 3, 1);
            
            // end the frame buffer render pass
            Sg.EndPass();
        }
    }
}