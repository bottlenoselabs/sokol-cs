using System;
using System.IO;
using System.Numerics;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.NonInterleaved
{
    public class NonInterleavedApplication : App
    {
        private readonly sg_pipeline _pipeline;
        private readonly SgBindings _bindings = new SgBindings();
        private readonly SgBuffer _vertexBuffer;
        private readonly SgBuffer _indexBuffer;
        private readonly SgShader _shader;
        private readonly SgUniform _modelViewProjectionUniform;
        private sg_pass_action _clearAction;

        private float _rotationX;
        private float _rotationY;

        public NonInterleavedApplication()
        {
            var vertices = new []
            {
                /* positions */
                -1.0f, -1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 
                1.0f, 1.0f, -1.0f, -1.0f, 1.0f, -1.0f,
                
                -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f,
                
                -1.0f, -1.0f, -1.0f, -1.0f, 1.0f, -1.0f,
                -1.0f, 1.0f, 1.0f, -1.0f, -1.0f, 1.0f,
                
                1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, -1.0f, 1.0f,
                
                -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, 1.0f, 
                1.0f, -1.0f, 1.0f, 1.0f, -1.0f, -1.0f,
                
                -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, -1.0f,

                /* colors */
                1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 
                1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f,
                
                0.5f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.0f, 1.0f, 
                0.5f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.0f, 1.0f,
                
                0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 
                0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f,
                
                1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 
                1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f,
                
                0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 
                0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f,
                
                1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 
                1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f
            };

            _vertexBuffer = new SgBuffer<float>(SgBufferType.Vertex, SgBufferUsage.Immutable, vertices);
            
            var indices = new ushort[]
            {
                0, 1, 2,  
                0, 2, 3,
                
                6, 5, 4,  
                7, 6, 4,
                
                8, 9, 10,  
                8, 10, 11,
                
                14, 13, 12,  
                15, 14, 12,
                
                16, 17, 18,  
                16, 18, 19,
                
                22, 21, 20, 
                23, 22, 20
            };
            
            _indexBuffer = new SgBuffer<ushort>(SgBufferType.Index, SgBufferUsage.Immutable, indices);

            _bindings.SetVertexBuffer(0, _vertexBuffer);
            _bindings.SetVertexBuffer(1, _vertexBuffer, 12 * 6 * sizeof(float));
            _bindings.SetIndexBuffer(_indexBuffer);
            
            _modelViewProjectionUniform = new SgUniform("mvp", SgShaderStage.Vertex, 0, SgShaderUniformType.Matrix4);
            var vertexShaderDescription = new SgShaderStageDescription
            {
                UniformBlocks = new[]
                {
                    new SgUniformBlock(_modelViewProjectionUniform)
                }
            };
            
            var fragmentShaderDescription = new SgShaderStageDescription();
            
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                vertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else
            {
                vertexShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderDescription.SourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }

            _shader = new SgShader(vertexShaderDescription, fragmentShaderDescription);

            unsafe
            {
                var pipeline = new sg_pipeline_desc();
                var attributes = pipeline.layout.GetAttrs();
                attributes[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
                attributes[0].buffer_index = 0;
                attributes[1].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
                attributes[1].buffer_index = 1;
                pipeline.shader = _shader;
                pipeline.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
                pipeline.depth_stencil.depth_compare_func = sg_compare_func.SG_COMPAREFUNC_LESS_EQUAL;
                pipeline.depth_stencil.depth_write_enabled = true;
                pipeline.rasterizer.cull_mode = sg_cull_mode.SG_CULLMODE_BACK;
                pipeline.rasterizer.sample_count = 4;
                
                _pipeline = sg_make_pipeline(ref pipeline);  
            }
            
            _clearAction = new sg_pass_action();
        }
        
        protected override void Draw(int width, int height)
        {
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (60.0f * Math.PI / 180), (float)width / height,
                0.01f, 10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;
            
            sg_begin_default_pass(ref _clearAction, width, height);
            
            sg_apply_pipeline(_pipeline);
            _bindings.Apply();

            _rotationX += 1.0f * 0.020f;
            _rotationY += 2.0f * 0.020f;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _rotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            _modelViewProjectionUniform.Apply(ref modelViewProjectionMatrix);
            
            sg_draw(0, 36, 1);
            sg_end_pass();
        }
    }
}