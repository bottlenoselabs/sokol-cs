using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.NonInterleaved
{
    public class NonInterleavedApplication : App
    {
        private SgBuffer _vertexBuffer;
        private SgBuffer _indexBuffer;
        private SgBindings _bindings;
        private SgPipeline _pipeline;
        private SgShader _shader;
        private SgPassAction _frameBufferPassAction;

        private float _rotationX;
        private float _rotationY;

        public NonInterleavedApplication()
        {
            CreateVertexBuffer();
            CreateIndexBuffer();
            CreateShader();
            CreatePipeline();

            SetBindings();
            SetFrameBufferPassAction();
        }

        private void SetFrameBufferPassAction()
        {
            // set the frame buffer pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Gray);
        }

        private void SetBindings()
        {
            // describe the binding of the vertex and index buffers (not applied yet!)
            _bindings.VertexBuffer(0) = _vertexBuffer;
            _bindings.VertexBuffer(1) = _vertexBuffer;
            _bindings.VertexBufferOffset(1) = 12 * 6 * sizeof(float);
            _bindings.IndexBuffer = _indexBuffer;
        }

        private void CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(0).BufferIndex = 0;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            pipelineDesc.Layout.Attribute(1).BufferIndex = 1;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = SgCullMode.Back;

            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateShader()
        {
            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            mvpUniform.Name = new AsciiString16("mvp");
            mvpUniform.Type = SgShaderUniformType.Matrix4x4;

            // specify shader stage source code for each graphics backend
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
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

        private unsafe void CreateIndexBuffer()
        {
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
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Index,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) indices,
                Size = Marshal.SizeOf<ushort>() * 6 * 6
            };

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _indexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private unsafe void CreateVertexBuffer()
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
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<float>() * (12 * 6 + 16 * 6)
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        protected override void Draw(int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (60.0f * Math.PI / 180), (float)width / height,
                0.01f, 10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);

            // apply the render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_pipeline);
            Sg.ApplyBindings(ref _bindings);

            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // apply the mvp matrix to the vertex shader
            Sg.ApplyUniforms(SgShaderStageType.Vertex, 0, ref modelViewProjectionMatrix);

            // draw the cube into the target of the render pass
            Sg.Draw(0, 36, 1);
            
            // end the framebuffer render pass
            Sg.EndPass();
        }
    }
}