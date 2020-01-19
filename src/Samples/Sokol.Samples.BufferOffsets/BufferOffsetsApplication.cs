using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

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
        
        private SgBuffer _vertexBuffer;
        private SgBuffer _indexBuffer;
        private SgBindings _bindings;
        private SgShader _shader;
        private SgPipeline _pipeline;
        private SgPassAction _frameBufferPassAction;

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
            var vertexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<Vertex>() * (3 + 4)
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = Sg.MakeBuffer(ref vertexBufferDesc);
            
            // use memory from the thread's stack to create the quad indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2, // triangle
                0, 1, 2, // triangle 1 of quad
                0, 2, 3 // triangle 2 of quad
            };
            
            // describe an immutable index buffer
            var indexBufferDesc = new SgBufferDescription()
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Index,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) indices,
                Size = Marshal.SizeOf<Vertex>() * (3 + 6)
            };
            
            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = Sg.MakeBuffer(ref indexBufferDesc);
            
            // describe the binding of the vertex and index buffer (not applied yet!)
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.IndexBuffer = _indexBuffer;

            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            string vertexShaderStageSourceCode;
            string fragmentShaderStageSourceCode;
            // specify shader stage source code for each graphics backend
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
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            shaderDesc.VertexShader.SourceCode = Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.FragmentShader.SourceCode = Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _shader = Sg.MakeShader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(shaderDesc.VertexShader.SourceCode);
            Marshal.FreeHGlobal(shaderDesc.FragmentShader.SourceCode);
            
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float2;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float3;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            
            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
            
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(0x8080FFFF);
        }
        
        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);

            // apply the render pipeline for the render pass
            Sg.ApplyPipeline(_pipeline);

            // set and apply the bindings necessary to render the triangle for the render pass
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.VertexBufferOffset(0) = 0;
            _bindings.IndexBuffer = _indexBuffer;
            _bindings.IndexBufferOffset = 0;
            Sg.ApplyBindings(ref _bindings);

            // draw the triangle into the target of the render pass
            Sg.Draw(0, 3, 1);
            
            // set and apply the bindings necessary to render the quad for the render pass
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.VertexBufferOffset(0) = 3 * Marshal.SizeOf<Vertex>();
            _bindings.IndexBuffer = _indexBuffer;
            _bindings.IndexBufferOffset = 3 * Marshal.SizeOf<ushort>();
            Sg.ApplyBindings(ref _bindings);

            // draw the quad into the target of the render pass
            Sg.Draw(0, 6, 1);

            // end the framebuffer render pass
            Sg.EndPass();
        }
    }
}