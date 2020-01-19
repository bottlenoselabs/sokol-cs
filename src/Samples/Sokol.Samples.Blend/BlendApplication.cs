using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Blend
{
    public class BlendApplication : App
    {
        private struct QuadVertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _vertexBuffer;
        private SgBindings _bindings;
        private SgShader _backgroundShader;
        private SgPipeline _backgroundPipeline;
        private SgShader _quadShader;
        private SgPipeline[] _quadPipelines;
        private SgPassAction _frameBufferPassAction;

        private float _tick;
        private float _rotation;

        private const int NUM_BLEND_FACTORS = 15;

        public BlendApplication()
            : base(desc: new SgDescription
            {
                PipelinePoolSize = NUM_BLEND_FACTORS * NUM_BLEND_FACTORS + 1
            })
        {
            CreateVertexBuffer();
            
            CreateBackgroundShader();
            CreateBackgroundPipeline();
            
            CreateQuadShader();
            CreateQuadPipelines();
            
            SetBindings();
            SetFrameBufferPassAction();
        }

        private void SetFrameBufferPassAction()
        {
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.DontCare;
        }

        private void SetBindings()
        {
            // describe the binding of the vertex (not applied yet!)
            _bindings.VertexBuffer(0) = _vertexBuffer;
        }

        private void CreateQuadPipelines()
        {
            // describe the quad render pipelines
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            pipelineDesc.Shader = _quadShader;
            pipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            pipelineDesc.Blend.IsEnabled = true;
            pipelineDesc.Blend.BlendColor = RgbaFloat.Red;
            pipelineDesc.Rasterizer.SampleCount = 4;

            _quadPipelines = new SgPipeline[NUM_BLEND_FACTORS * NUM_BLEND_FACTORS];
            for (var src = 0; src < NUM_BLEND_FACTORS; src++)
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++)
                {
                    var srcBlend = (SgBlendFactor) (src + 1);
                    var dstBlend = (SgBlendFactor) (dst + 1);

                    pipelineDesc.Blend.SourceFactorRgb = srcBlend;
                    pipelineDesc.Blend.DestinationFactorRgb = dstBlend;
                    pipelineDesc.Blend.SourceFactorAlpha = SgBlendFactor.One;
                    pipelineDesc.Blend.DestinationFactorAlpha = SgBlendFactor.Zero;

                    var index = dst + src * NUM_BLEND_FACTORS;
                    ref var pipeline = ref _quadPipelines[index];
                    pipeline = Sg.MakePipeline(ref pipelineDesc);
                }
            }
        }

        private void CreateQuadShader()
        {
            // describe the quad shader program
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
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/quadVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/quadFrag.metal");
            }
            else
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/quad.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/quad.frag");
            }

            // create the quad shader resource from the description
            _quadShader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private void CreateBackgroundPipeline()
        {
            // describe the background render pipeline
            var pipelineDesc = new SgPipelineDescription();
            // note: reusing the vertices of the 3D quads, but only using the first two floats from the position
            pipelineDesc.Layout.Buffer(0).Stride = 28;
            pipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float2;
            pipelineDesc.Shader = _backgroundShader;
            pipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            pipelineDesc.Rasterizer.SampleCount = 4;

            // create the background pipeline resource from the description
            _backgroundPipeline = Sg.MakePipeline(ref pipelineDesc);
        }

        private void CreateBackgroundShader()
        {
            // describe the background shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.FragmentShader.UniformBlock(0).Size = Marshal.SizeOf<float>();
            ref var tickUniform = ref shaderDesc.FragmentShader.UniformBlock(0).Uniform(0);
            tickUniform.Name = new AsciiString16("tick");
            tickUniform.Type = SgShaderUniformType.Float;
            // specify shader stage source code for each graphics backend
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/backgroundVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/backgroundFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/background.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/background.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            
            // create the background shader resource from the description
            _backgroundShader = Sg.MakeShader(ref shaderDesc, vertexShaderSourceCode, fragmentShaderSourceCode);
        }

        private unsafe void CreateVertexBuffer()
        {
            // use memory from the thread's stack for the triangle vertices
            var vertices = stackalloc QuadVertex[4];

            // describe the vertices of the quad
            vertices[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            vertices[0].Color = new RgbaFloat(1.0f, 0.0f, 0.0f, 0.5f);
            vertices[1].Position = new Vector3(+1.0f, -1.0f, 0.0f);
            vertices[1].Color = new RgbaFloat(0.0f, 1.0f, 0.0f, 0.5f);
            vertices[2].Position = new Vector3(-1.0f, +1.0f, 0.0f);
            vertices[2].Color = new RgbaFloat(0.0f, 0.0f, 1.0f, 0.5f);
            vertices[3].Position = new Vector3(+1.0f, +1.0f, 0.0f);
            vertices[3].Color = new RgbaFloat(1.0f, 1.0f, 0.0f, 0.5f);

            // describe an immutable vertex buffer
            var vertexBufferDesc = new SgBufferDescription
            {
                Usage = SgUsage.Immutable,
                Type = SgBufferType.Vertex,
                Content = (IntPtr) vertices,
                // immutable buffers need to specify the data/size in the description
                Size = Marshal.SizeOf<QuadVertex>() * 4
            };

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = Sg.MakeBuffer(ref vertexBufferDesc);
        }

        protected override void Draw(int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (90.0f * Math.PI / 180), (float)width / height,
                0.01f, 100.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 0.0f, 25.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            
            // apply the background render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_backgroundPipeline);
            Sg.ApplyBindings(ref _bindings);

            // apply the background tick uniform to the background fragment shader
            Sg.ApplyUniforms(SgShaderStageType.Fragment, 0, ref _tick);

            // draw the background quad into the target of the render pass
            Sg.Draw(0, 4, 1);

            // for every type of blend factor (each one is a different render pipeline)...
            var rotationForBlendFactor = _rotation;
            for (var src = 0; src < NUM_BLEND_FACTORS; src++) 
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++, rotationForBlendFactor += 0.6f)
                {
                    // rotate quad and create vertex shader mvp matrix
                    var rotationMatrix = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, rotationForBlendFactor);
                    var x = (dst - NUM_BLEND_FACTORS / 2) * 3.0f;
                    var y = (src - NUM_BLEND_FACTORS / 2) * 2.2f;
                    var translationMatrix = Matrix4x4.CreateTranslation(x, y, 0.0f);
                    var modelMatrix = rotationMatrix * translationMatrix;
                    var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;

                    // apply the quad render pipeline and bindings for the render pass
                    var pipelineIndex = dst + src * NUM_BLEND_FACTORS;
                    Sg.ApplyPipeline(_quadPipelines[pipelineIndex]);
                    Sg.ApplyBindings(ref _bindings);

                    // apply the mvp matrix to the vertex shader
                    Sg.ApplyUniforms(SgShaderStageType.Vertex, 0, ref modelViewProjectionMatrix);
       
                    // draw the quad into the target of the render pass
                    Sg.Draw(0, 4, 1);
                }
            }
            
            // end the framebuffer render pass
            Sg.EndPass();
            
            // update rotation and tick values for next frame
            _rotation += 0.6f * 0.20f;
            _tick += 1.0f * 0.20f;
        }
    }
}