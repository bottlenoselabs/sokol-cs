using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Sokol.Samples.Blend
{
    public class BlendApplication : App
    {
        // The interleaved vertex data structure
        private struct Vertex
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

        private float _tick;
        private float _rotation;

        private const int NUM_BLEND_FACTORS = 15;

        public unsafe BlendApplication()
            : base(desc: new sg_desc()
            {
                pipeline_pool_size = NUM_BLEND_FACTORS * NUM_BLEND_FACTORS + 1
            })
        {
            // use memory from the thread's stack for the triangle vertices
            var vertices = stackalloc Vertex[4];

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
                Size = Marshal.SizeOf<Vertex>() * 4
            };
  
            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _vertexBuffer = new SgBuffer(ref vertexBufferDesc);
            
            // describe the binding of the vertex (not applied yet!)
            _bindings.SetVertexBuffer(ref _vertexBuffer);

            // describe the background shader program
            var backgroundShaderDesc = new SgShaderDescription();
            backgroundShaderDesc.FragmentShader.UniformBlock(0).Size = Marshal.SizeOf<float>();
            ref var tickUniform = ref backgroundShaderDesc.FragmentShader.UniformBlock(0).Uniform(0);
            tickUniform.Name = Marshal.StringToHGlobalAnsi("tick");
            tickUniform.Type = SgShaderUniformType.Float;
            // specify shader stage source code for each graphics backend
            string backgroundVertexShaderStageSourceCode;
            string backgroundFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                backgroundVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/backgroundVert.metal");
                backgroundFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/backgroundFrag.metal");
            }
            else
            {
                backgroundVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/background.vert");
                backgroundFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/background.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            backgroundShaderDesc.VertexShader.Source = Marshal.StringToHGlobalAnsi(backgroundVertexShaderStageSourceCode);
            backgroundShaderDesc.FragmentShader.Source = Marshal.StringToHGlobalAnsi(backgroundFragmentShaderStageSourceCode);
            
            // create the background shader resource from the description
            _backgroundShader = new SgShader(ref backgroundShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(backgroundShaderDesc.FragmentShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(backgroundShaderDesc.VertexShader.Source);
            Marshal.FreeHGlobal(backgroundShaderDesc.FragmentShader.Source);

            // describe the background render pipeline
            var backgroundPipelineDesc = new SgPipelineDescription();
            // note: reusing the vertices of the 3D quads, but only using the first two floats from the position
            backgroundPipelineDesc.Layout.Buffer(0).Stride = 28;
            backgroundPipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float2;
            backgroundPipelineDesc.Shader = _backgroundShader;
            backgroundPipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            backgroundPipelineDesc.Rasterizer.SampleCount = 4;
            
            // create the background pipeline resource from the description
            _backgroundPipeline = new SgPipeline(ref backgroundPipelineDesc);
            
            // describe the quad shader program
            var quadShaderDesc = new SgShaderDescription();
            quadShaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref quadShaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            mvpUniform.Name = Marshal.StringToHGlobalAnsi("mvp");
            mvpUniform.Type = SgShaderUniformType.Matrix4x4;
            // specify shader stage source code for each graphics backend
            string quadVertexShaderStageSourceCode;
            string quadFragmentShaderStageSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                quadVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/quadVert.metal");
                quadFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/quadFrag.metal");
            }
            else
            {
                quadVertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/quad.vert");
                quadFragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/quad.frag");
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            quadShaderDesc.VertexShader.Source = Marshal.StringToHGlobalAnsi(quadVertexShaderStageSourceCode);
            quadShaderDesc.FragmentShader.Source = Marshal.StringToHGlobalAnsi(quadFragmentShaderStageSourceCode);
            
            // create the quad shader resource from the description
            _quadShader = new SgShader(ref quadShaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(quadShaderDesc.VertexShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(quadShaderDesc.VertexShader.Source);
            Marshal.FreeHGlobal(quadShaderDesc.FragmentShader.Source);
            
            // describe the quad render pipelines
            var quadPipelineDesc = new SgPipelineDescription();
            quadPipelineDesc.Layout.Attribute(0).Format = SgVertexFormat.Float3;
            quadPipelineDesc.Layout.Attribute(1).Format = SgVertexFormat.Float4;
            quadPipelineDesc.Shader = _quadShader;
            quadPipelineDesc.PrimitiveType = SgPrimitiveType.TriangleStrip;
            quadPipelineDesc.Blend.Enabled = true;
            quadPipelineDesc.Blend.BlendColor = RgbaFloat.Red;
            quadPipelineDesc.Rasterizer.SampleCount = 4;
            
            _quadPipelines = new SgPipeline[NUM_BLEND_FACTORS * NUM_BLEND_FACTORS];
            for (var src = 0; src < NUM_BLEND_FACTORS; src++)
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++)
                {
                    var srcBlend = (SgBlendFactor) (src + 1);
                    var dstBlend = (SgBlendFactor) (dst + 1);
                    
                    quadPipelineDesc.Blend.SourceFactorRgb = srcBlend;
                    quadPipelineDesc.Blend.DestinationFactorRgb = dstBlend;
                    quadPipelineDesc.Blend.SourceFactorAlpha = SgBlendFactor.One;
                    quadPipelineDesc.Blend.DestinationFactorAlpha = SgBlendFactor.Zero;

                    var index = dst + src * NUM_BLEND_FACTORS;
                    ref var pipeline = ref _quadPipelines[index];
                    pipeline = new SgPipeline(ref quadPipelineDesc);
                }
            }
        }

        protected override unsafe void Draw(int width, int height)
        {
            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (90.0f * Math.PI / 180), (float)width / height,
                0.01f, 100.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 0.0f, 25.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // begin a framebuffer render pass
            var frameBufferPassAction = sg_pass_action.dontCare();
            sg_begin_default_pass(ref frameBufferPassAction, width, height);
            
            // apply the background render pipeline and bindings for the render pass
            _backgroundPipeline.Apply();
            _bindings.Apply();

            // apply the background tick uniform to the background fragment shader
            var tickPointer = Unsafe.AsPointer(ref _tick);
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_FS, 0, tickPointer, Marshal.SizeOf<float>());
            
            // draw the background quad into the target of the render pass
            sg_draw(0, 4, 1);

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
                    sg_apply_pipeline(_quadPipelines[pipelineIndex]);
                    _bindings.Apply();
                    
                    // apply the mvp matrix to the vertex shader
                    var mvpMatrix = Unsafe.AsPointer(ref modelViewProjectionMatrix);
                    sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, mvpMatrix, Marshal.SizeOf<Matrix4x4>());
       
                    // draw the quad into the target of the render pass
                    sg_draw(0, 4, 1);
                }
            }
            
            // end the framebuffer render pass
            sg_end_pass();
            
            // update rotation and tick values for next frame
            _rotation += 0.6f * 0.20f;
            _tick += 1.0f * 0.20f;
        }
    }
}