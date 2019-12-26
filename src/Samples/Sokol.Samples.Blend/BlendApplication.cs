using System;
using System.IO;
using System.Numerics;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.Blend
{
    public class BlendApplication : App
    {
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }

        private sg_pass_action _passAction;
        private readonly SgBuffer _vertexBuffer;
        private readonly SgShader _backgroundShader;
        private readonly SgUniform _tickUniform;
        private readonly sg_pipeline _backgroundPipeline;
        private readonly SgShader _quadShader;
        private readonly SgUniform _modelViewProjectionUniform;
        private readonly sg_pipeline[] _quadPipelines;
        private readonly SgBindings _bindings;

        private float _tick;
        private float _rotation;

        private const int NUM_BLEND_FACTORS = 15;

        public unsafe BlendApplication()
            : base(new SgDeviceDescription
            {
                PipelinePoolSize = NUM_BLEND_FACTORS * NUM_BLEND_FACTORS + 1
            })
        {
            var vertices = new Vertex[4];

            vertices[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            vertices[0].Color = new RgbaFloat(1.0f, 0.0f, 0.0f, 0.5f);
            vertices[1].Position = new Vector3(+1.0f, -1.0f, 0.0f);
            vertices[1].Color = new RgbaFloat(0.0f, 1.0f, 0.0f, 0.5f);
            vertices[2].Position = new Vector3(-1.0f, +1.0f, 0.0f);
            vertices[2].Color = new RgbaFloat(0.0f, 0.0f, 1.0f, 0.5f);
            vertices[3].Position = new Vector3(+1.0f, +1.0f, 0.0f);
            vertices[3].Color = new RgbaFloat(1.0f, 1.0f, 0.0f, 0.5f);

            _vertexBuffer = new SgBuffer<Vertex>(SgBufferType.Vertex, SgBufferUsage.Immutable, vertices);
            
            var backgroundVertexShaderDescription = new SgShaderStageDescription();
            
            _tickUniform = new SgUniform("tick", SgShaderStage.Fragment, 0, SgShaderUniformType.Float);
            var backgroundFragmentShaderDescription = new SgShaderStageDescription
            {
                UniformBlocks = new[]
                {
                    new SgUniformBlock(_tickUniform),
                }
            };
            
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                backgroundVertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/backgroundVert.metal");
                backgroundVertexShaderDescription.Entry = "vs_main";
                backgroundFragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/backgroundFrag.metal");
                backgroundFragmentShaderDescription.Entry = "fs_main";
            }
            else
            {
                backgroundVertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/background.vert");
                backgroundFragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/background.frag");
            }
            
            _backgroundShader = new SgShader(backgroundVertexShaderDescription, backgroundFragmentShaderDescription);

            var backgroundPipeline = new sg_pipeline_desc();
            var buffers = backgroundPipeline.layout.GetBuffers();
            buffers[0].stride = 28;
            var attributes = backgroundPipeline.layout.GetAttrs();
            attributes[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT2;
            backgroundPipeline.shader = _backgroundShader.Handle;
            backgroundPipeline.primitive_type = sg_primitive_type.SG_PRIMITIVETYPE_TRIANGLE_STRIP;
            
            _backgroundPipeline = sg_make_pipeline(ref backgroundPipeline);
            
            _modelViewProjectionUniform = new SgUniform("mvp", SgShaderStage.Vertex, 0, SgShaderUniformType.Matrix4);
            var quadVertexShaderDescription = new SgShaderStageDescription
            {
                UniformBlocks = new[]
                {
                    new SgUniformBlock(_modelViewProjectionUniform)
                }
            };

            var quadFragmentShaderDescription = new SgShaderStageDescription();

            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                quadVertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/quadVert.metal");
                quadVertexShaderDescription.Entry = "vs_main";
                quadFragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/quadFrag.metal");
                quadFragmentShaderDescription.Entry = "fs_main";
            }
            else
            {
                quadVertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/quad.vert");
                quadFragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/quad.frag");
            }
            
            _quadShader = new SgShader(quadVertexShaderDescription, quadFragmentShaderDescription);
            
            var quadPipelineDesc = new sg_pipeline_desc();
            var quadPipelineAttributes = quadPipelineDesc.layout.GetAttrs();
            quadPipelineAttributes[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            quadPipelineAttributes[1].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            quadPipelineDesc.shader = _quadShader.Handle;
            quadPipelineDesc.primitive_type = sg_primitive_type.SG_PRIMITIVETYPE_TRIANGLE_STRIP;
            quadPipelineDesc.blend.enabled = true;
            quadPipelineDesc.blend.blend_color = RgbaFloat.Red;
            quadPipelineDesc.rasterizer.sample_count = 4;
            
            _quadPipelines = new sg_pipeline[NUM_BLEND_FACTORS * NUM_BLEND_FACTORS];
            for (var src = 0; src < NUM_BLEND_FACTORS; src++)
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++)
                {
                    var srcBlend = (sg_blend_factor) (src + 1);
                    var dstBlend = (sg_blend_factor) (dst + 1);
                    
                    quadPipelineDesc.blend.src_factor_rgb = srcBlend;
                    quadPipelineDesc.blend.dst_factor_rgb = dstBlend;
                    quadPipelineDesc.blend.src_factor_alpha = sg_blend_factor.SG_BLENDFACTOR_ONE;
                    quadPipelineDesc.blend.dst_factor_alpha = sg_blend_factor.SG_BLENDFACTOR_ZERO;

                    var index = dst + src * NUM_BLEND_FACTORS;
                    ref var pipeline = ref _quadPipelines[index];
                    pipeline = sg_make_pipeline(ref quadPipelineDesc);
                    if (pipeline.id == SG_INVALID_ID)
                    {
                        throw new Exception("Invalid pipeline ID");
                    }
                }
            }
            
            _bindings = new SgBindings();
            _bindings.SetVertexBuffer(0, _vertexBuffer);

            _passAction = new sg_pass_action();
            var colors = _passAction.GetColors();
            colors[0] = new sg_color_attachment_action
            {
                action = sg_action.SG_ACTION_DONTCARE
            };
            _passAction.depth.action = sg_action.SG_ACTION_DONTCARE;
            _passAction.stencil.action = sg_action.SG_ACTION_DONTCARE;
        }

        protected override void Draw(int width, int height)
        {
            sg_begin_default_pass(ref _passAction, width, height);
            
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (90.0f * Math.PI / 180), (float)width / height,
                0.01f, 100.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 0.0f, 25.0f), Vector3.Zero, 
                new Vector3(0.0f, 1.0f, 0.0f) 
            );
            
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            sg_apply_pipeline(_backgroundPipeline);
            _bindings.Apply();
            _tickUniform.Apply(ref _tick);
            sg_draw(0, 4, 1);

            var r0 = _rotation;
            for (var src = 0; src < NUM_BLEND_FACTORS; src++) 
            {
                for (var dst = 0; dst < NUM_BLEND_FACTORS; dst++, r0 += 0.6f)
                {
                    var rotationMatrix = Matrix4x4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), r0);
                    var x = (dst - NUM_BLEND_FACTORS/2) * 3.0f;
                    var y = (src - NUM_BLEND_FACTORS/2) * 2.2f;
                    
                    var translationMatrix = Matrix4x4.CreateTranslation(x, y, 0.0f);
                    var modelMatrix = rotationMatrix * translationMatrix;
                    var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;

                    var pipelineIndex = dst + src * NUM_BLEND_FACTORS;
                    sg_apply_pipeline(_quadPipelines[pipelineIndex]);
                    _bindings.Apply();
                    _modelViewProjectionUniform.Apply(ref modelViewProjectionMatrix);
                    sg_draw(0, 4, 1);
                }
            }
            
            sg_end_pass();
            _rotation += 0.6f;
            _tick += 1.0f;
        }
    }
}