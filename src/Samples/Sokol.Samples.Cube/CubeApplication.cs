using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Cube
{
    public class CubeApplication : App
    {
        private struct CubeVertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _cubeVertexBuffer;
        private SgBuffer _cubeIndexBuffer;
        private SgBindings _cubeBindings;
        private SgPipeline _pipeline;
        private SgShader _shader;
        private SgPassAction _frameBufferPassAction;

        private float _rotationX;
        private float _rotationY;

        public CubeApplication()
        {
            CreateCubeVertexBuffer();
            CreateCubeIndexBuffer();
            CreateShader();
            CreatePipeline();

            SetCubeBindings();
            SetFrameBufferPassAction();
        }

        private void SetFrameBufferPassAction()
        {
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Gray);
        }

        private void SetCubeBindings()
        {
            // describe the binding of the vertex and index buffer (not applied yet!)
            _cubeBindings.VertexBuffer(0) = _cubeVertexBuffer;
            _cubeBindings.IndexBuffer = _cubeIndexBuffer;
        }

        private void CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Buffer(0).Stride = 28;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
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

        private unsafe void CreateCubeIndexBuffer()
        {
            // use memory from the thread's stack to create the cube indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2, 0, 2, 3, // quad 1 of cube
                6, 5, 4, 7, 6, 4, // quad 2 of cube
                8, 9, 10, 8, 10, 11, // quad 3 of cube
                14, 13, 12, 15, 14, 12, // quad 4 of cube
                16, 17, 18, 16, 18, 19, // quad 5 of cube
                22, 21, 20, 23, 22, 20 // quad 6 of cube
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
            _cubeIndexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private unsafe void CreateCubeVertexBuffer()
        {
            // use memory from the thread's stack for the quad vertices
            var vertices = stackalloc CubeVertex[24];

            // describe the vertices of the cube
            // quad 1
            var color1 = RgbaFloat.Red;
            vertices[0].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[0].Color = color1;
            vertices[1].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[1].Color = color1;
            vertices[2].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[2].Color = color1;
            vertices[3].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[3].Color = color1;
            // quad 2
            var color2 = RgbaFloat.Lime;
            vertices[4].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[4].Color = color2;
            vertices[5].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[5].Color = color2;
            vertices[6].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[6].Color = color2;
            vertices[7].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[7].Color = color2;
            // quad 3
            var color3 = RgbaFloat.Blue;
            vertices[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[8].Color = color3;
            vertices[9].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[9].Color = color3;
            vertices[10].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[10].Color = color3;
            vertices[11].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[11].Color = color3;
            // quad 4
            var color4 = new RgbaFloat(1f, 0.5f, 0f, 1f);
            vertices[12].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[12].Color = color4;
            vertices[13].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[13].Color = color4;
            vertices[14].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[14].Color = color4;
            vertices[15].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[15].Color = color4;
            // quad 5
            var color5 = new RgbaFloat(0f, 0.5f, 1f, 1f);
            vertices[16].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[16].Color = color5;
            vertices[17].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[17].Color = color5;
            vertices[18].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[18].Color = color5;
            vertices[19].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[19].Color = color5;
            // quad 6
            var color6 = new RgbaFloat(1.0f, 0.0f, 0.5f, 1f);
            vertices[20].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[20].Color = color6;
            vertices[21].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[21].Color = color6;
            vertices[22].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[22].Color = color6;
            vertices[23].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[23].Color = color6;

            // describe an immutable vertex buffer
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) vertices,
                Size = Marshal.SizeOf<CubeVertex>() * 6 * 4
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _cubeVertexBuffer = Sg.MakeBuffer(ref bufferDesc);
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
            Sg.ApplyBindings(ref _cubeBindings);

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