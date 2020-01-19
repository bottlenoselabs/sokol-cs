using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.MultipleRenderTargets
{
    public class MultipleRenderTargetsApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public float Brightness;
        }

        private SgBuffer _cubeVertexBuffer;
        private SgBuffer _cubeIndexBuffer;
        private SgBindings _offScreenBindings;
        private SgShader _offScreenShader;
        private SgPipeline _offScreenPipeline;
        private readonly SgImage[] _offScreenRenderTargets = new SgImage[3];
        private SgImage _offScreenRenderTargetDepth;
        private SgPass _offScreenPass;
        private SgPassAction _offScreenPassAction;

        private SgBuffer _quadVertexBuffer;
        private SgBindings _fullScreenBindings;
        private SgShader _fullScreenShader;
        private SgPipeline _fullScreenPipeline;
        private SgPassAction _frameBufferPassAction;

        private SgShader _debugShader;
        private SgPipeline _debugPipeline;
        private SgBindings _debugBindings;
        
        private float _rotationX;
        private float _rotationY;

        public MultipleRenderTargetsApplication()
        {
            var msaaSampleCount = Sg.QueryFeatures().MsaaRenderTargets ? 4 : 1;
            
            CreateCubeVertexBuffer();
            CreateCubeIndexBuffer();
            CreateQuadVertexBuffer();
            
            CreateOffScreenShader();
            CreateOffScreenPipeline(msaaSampleCount);
            CreateOffScreenRenderTargets(msaaSampleCount);
            CreateOffScreenRenderPass();
            SetOffScreenBindings();
            SetOffScreenPassAction();

            CreateFullScreenShader();
            CreateFullScreenPipeline(msaaSampleCount);
            SetFullScreenBindings();
            
            CreateDebugShader();
            CreateDebugPipeline(msaaSampleCount);
            SetDebugBindings();

            SetFrameBufferPassAction();
        }

        private void CreateOffScreenRenderPass()
        {
            // describe the off screen render pass
            var passDesc = new SgPassDescription();
            passDesc.ColorAttachment(0).Image = _offScreenRenderTargets[0];
            passDesc.ColorAttachment(1).Image = _offScreenRenderTargets[1];
            passDesc.ColorAttachment(2).Image = _offScreenRenderTargets[2];
            passDesc.DepthStencilAttachment.Image = _offScreenRenderTargetDepth;

            _offScreenPass = Sg.MakePass(ref passDesc);
        }

        private void SetFrameBufferPassAction()
        {
            // set the framebuffer render pass action
            _frameBufferPassAction = SgPassAction.DontCare;
        }

        private void SetFullScreenBindings()
        {
            // describe the binding of the full screen quad vertex buffer (not applied yet!)
            _fullScreenBindings.VertexBuffer(0) = _quadVertexBuffer;
            _fullScreenBindings.FragmentShaderImage(0) = _offScreenRenderTargets[0];
            _fullScreenBindings.FragmentShaderImage(1) = _offScreenRenderTargets[1];
            _fullScreenBindings.FragmentShaderImage(2) = _offScreenRenderTargets[2];
        }

        private void SetDebugBindings()
        {
            // describe the binding of the debug quads (not applied yet!)
            _debugBindings.VertexBuffer(0) = _quadVertexBuffer;
        }

        private void CreateDebugPipeline(int msaaSampleCount)
        {
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float2;
            pipelineDesc.Shader = _debugShader;
            pipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            _debugPipeline = Sg.MakePipeline(ref pipelineDesc);
        }
        
        private void CreateDebugShader()
        {
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            // describe the fullscreen shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.FragmentShader.Image(0).Name = new AsciiString16("tex");
            shaderDesc.FragmentShader.Image(0).Type = SgImageType.Texture2D;
            // specify shader stage source code for each graphics backend
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/debugVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/debugFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/debug.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/debug.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            
            // create the fullscreen shader resource from the description
            _debugShader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private void CreateFullScreenPipeline(int msaaSampleCount)
        {
            // describe the off screen render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float2;
            pipelineDesc.Shader = _fullScreenShader;
            pipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            // create the offscreen pipeline resource from the description
            _fullScreenPipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateFullScreenShader()
        {
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            // describe the fullscreen shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Vector2>();
            ref var offsetUniform = ref shaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            offsetUniform.Name = new AsciiString16("offset");
            offsetUniform.Type = SgShaderUniformType.Float2;
            shaderDesc.FragmentShader.Image(0).Name = new AsciiString16("tex0");
            shaderDesc.FragmentShader.Image(0).Type = SgImageType.Texture2D;
            shaderDesc.FragmentShader.Image(1).Name = new AsciiString16("tex1");
            shaderDesc.FragmentShader.Image(1).Type = SgImageType.Texture2D;
            shaderDesc.FragmentShader.Image(2).Name = new AsciiString16("tex2");
            shaderDesc.FragmentShader.Image(2).Type = SgImageType.Texture2D;
            // specify shader stage source code for each graphics backend
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/fullScreenVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/fullScreenFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/fullScreen.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/fullScreen.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            
            // create the fullscreen shader resource from the description
            _fullScreenShader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private unsafe void CreateQuadVertexBuffer()
        {
            // use memory from the thread's stack to create the full screen quad vertices
            var quadVertices = stackalloc Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            // describe an immutable vertex buffer for the full screen quad
            var bufferDesc = new SgBufferDescription();
            bufferDesc.Usage = SgUsage.Immutable;
            bufferDesc.Type = SgBufferType.Vertex;
            // immutable buffers need to specify the data/size in the description
            bufferDesc.Content = (IntPtr) quadVertices;
            bufferDesc.Size = Marshal.SizeOf<Vector2>() * 4;

            // create the full screen quad vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _quadVertexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private void SetOffScreenPassAction()
        {
            _offScreenPassAction = new SgPassAction();
            _offScreenPassAction.Color(0).Action = SgAction.Clear;
            _offScreenPassAction.Color(0).Value = 0x400000FF;
            _offScreenPassAction.Color(1).Action = SgAction.Clear;
            _offScreenPassAction.Color(1).Value = 0x004000FF;
            _offScreenPassAction.Color(2).Action = SgAction.Clear;
            _offScreenPassAction.Color(2).Value = 0x000040FF;
        }

        private void SetOffScreenBindings()
        {
            // describe the binding of the off screen cube (not applied yet!)
            _offScreenBindings.VertexBuffer(0) = _cubeVertexBuffer;
            _offScreenBindings.IndexBuffer = _cubeIndexBuffer;
        }

        private void CreateOffScreenRenderTargets(int msaaSampleCount)
        {
            // describe the off screen render target images
            var colorImageDesc = new SgImageDescription();
            colorImageDesc.IsRenderTarget = true;
            colorImageDesc.Width = 800;
            colorImageDesc.Height = 600;
            colorImageDesc.MinificationFilter = SgTextureFilter.Linear;
            colorImageDesc.MagnificationFilter = SgTextureFilter.Linear;
            colorImageDesc.WrapU = SgTextureWrap.WrapClampToEdge;
            colorImageDesc.WrapV = SgTextureWrap.WrapClampToEdge;
            colorImageDesc.SampleCount = msaaSampleCount;

            // describe the off screen depth render target image
            var depthImageDesc = colorImageDesc;
            depthImageDesc.PixelFormat = SgPixelFormat.Depth;

            // create the off screen render targets
            _offScreenRenderTargets[0] = Sg.MakeImage(ref colorImageDesc);
            _offScreenRenderTargets[1] = Sg.MakeImage(ref colorImageDesc);
            _offScreenRenderTargets[2] = Sg.MakeImage(ref colorImageDesc);
            _offScreenRenderTargetDepth = Sg.MakeImage(ref depthImageDesc);
        }

        private void CreateOffScreenPipeline(int msaaSampleCount)
        {
            // describe the off screen render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float;
            pipelineDesc.Shader = _offScreenShader;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Blend.ColorAttachmentCount = 3;
            pipelineDesc.Blend.DepthFormat = SgPixelFormat.Depth;
            pipelineDesc.Rasterizer.CullMode = SgCullMode.Back;
            pipelineDesc.Rasterizer.SampleCount = msaaSampleCount;

            // create the offscreen pipeline resource from the description
            _offScreenPipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateOffScreenShader()
        {
            // describe the off screen shader program
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
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/offScreenVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/offScreenFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/offScreen.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/offScreen.frag");
            }
            else
            {
                throw new NotImplementedException();
            }

            // create the off screen shader resource from the description
            _offScreenShader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private unsafe void CreateCubeIndexBuffer()
        {
            // use memory from the thread's stack to create the cube indices
            var cubeIndices = stackalloc ushort[]
            {
                0, 1, 2, 0, 2, 3, // quad 1 of cube
                6, 5, 4, 7, 6, 4, // quad 2 of cube
                8, 9, 10, 8, 10, 11, // quad 3 of cube
                14, 13, 12, 15, 14, 12, // quad 4 of cube
                16, 17, 18, 16, 18, 19, // quad 5 of cube
                22, 21, 20, 23, 22, 20 // quad 6 of cube
            };

            // describe an immutable index buffer for the cube
            var bufferDesc = new SgBufferDescription();
            bufferDesc.Usage = SgUsage.Immutable;
            bufferDesc.Type = SgBufferType.Index;
            // immutable buffers need to specify the data/size in the description
            bufferDesc.Content = (IntPtr) cubeIndices;
            bufferDesc.Size = Marshal.SizeOf<ushort>() * 6 * 6;

            // create the cube index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _cubeIndexBuffer = Sg.MakeBuffer(ref bufferDesc);
        }

        private unsafe void CreateCubeVertexBuffer()
        {
            // use memory from the thread's stack for the cube vertices
            var cubeVertices = stackalloc Vertex[24];

            // describe the vertices of the cube
            // quad 1
            const float brightness1 = 1.0f;
            cubeVertices[0].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[0].Brightness = brightness1;
            cubeVertices[1].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[1].Brightness = brightness1;
            cubeVertices[2].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[2].Brightness = brightness1;
            cubeVertices[3].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[3].Brightness = brightness1;
            // quad 2
            const float brightness2 = 0.8f;
            cubeVertices[4].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[4].Brightness = brightness2;
            cubeVertices[5].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[5].Brightness = brightness2;
            cubeVertices[6].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[6].Brightness = brightness2;
            cubeVertices[7].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[7].Brightness = brightness2;
            // quad 3
            const float brightness3 = 0.6f;
            cubeVertices[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[8].Brightness = brightness3;
            cubeVertices[9].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[9].Brightness = brightness3;
            cubeVertices[10].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[10].Brightness = brightness3;
            cubeVertices[11].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[11].Brightness = brightness3;
            // quad 4
            const float brightness4 = 0.4f;
            cubeVertices[12].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[12].Brightness = brightness4;
            cubeVertices[13].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[13].Brightness = brightness4;
            cubeVertices[14].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[14].Brightness = brightness4;
            cubeVertices[15].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[15].Brightness = brightness4;
            // quad 5
            const float brightness5 = 0.5f;
            cubeVertices[16].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            cubeVertices[16].Brightness = brightness5;
            cubeVertices[17].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            cubeVertices[17].Brightness = brightness5;
            cubeVertices[18].Position = new Vector3(1.0f, -1.0f, 1.0f);
            cubeVertices[18].Brightness = brightness5;
            cubeVertices[19].Position = new Vector3(1.0f, -1.0f, -1.0f);
            cubeVertices[19].Brightness = brightness5;
            // quad 6
            const float brightness6 = 0.7f;
            cubeVertices[20].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            cubeVertices[20].Brightness = brightness6;
            cubeVertices[21].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            cubeVertices[21].Brightness = brightness6;
            cubeVertices[22].Position = new Vector3(1.0f, 1.0f, 1.0f);
            cubeVertices[22].Brightness = brightness6;
            cubeVertices[23].Position = new Vector3(1.0f, 1.0f, -1.0f);
            cubeVertices[23].Brightness = brightness6;

            // describe an immutable vertex buffer for the cube
            var bufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                // immutable buffers need to specify the data/size in the description
                Content = (IntPtr) cubeVertices,
                Size = Marshal.SizeOf<Vertex>() * 6 * 4
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
            
            // rotate cube and create vertex shader mvp matrix
            _rotationX += 1.0f * 0.02f;
            _rotationY += 2.0f * 0.02f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // render cube into offscreen render targets
            var offset = new Vector2(
                (float) Math.Sin(_rotationX * 0.25f) * 0.05f,
                (float) Math.Sin(_rotationY * 0.25f) * 0.05f
            );
            Sg.BeginPass(_offScreenPass, ref _offScreenPassAction);
            Sg.ApplyPipeline(_offScreenPipeline);
            Sg.ApplyBindings(ref _offScreenBindings);
            Sg.ApplyUniforms(SgShaderStageType.Vertex, 0, ref modelViewProjectionMatrix);
            Sg.Draw(0, 36, 1);
            Sg.EndPass();

            // render fullscreen quad with the 'composed image',
            // plus 3 small debug-views with the content of the offscreen render targets
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            Sg.ApplyPipeline(_fullScreenPipeline);
            Sg.ApplyBindings(ref _fullScreenBindings);
            Sg.ApplyUniforms(SgShaderStageType.Vertex, 0, ref offset);
            Sg.Draw(0, 4, 1);
            Sg.ApplyPipeline(_debugPipeline);
            for (var i = 0; i < 3; i++) 
            {
                Sg.ApplyViewport(i * 100, 0, 100, 100);
                _debugBindings.FragmentShaderImage(0) = _offScreenRenderTargets[i];
                Sg.ApplyBindings(ref _debugBindings);
                Sg.Draw(0, 4, 1);
            }
            Sg.EndPass();
        }
    }
}