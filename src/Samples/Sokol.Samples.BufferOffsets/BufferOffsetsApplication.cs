using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.BufferOffsets
{
    public class BufferOffsetsApplication : App
    {
        private struct Vertex
        {
            public Vector2 Position;
            public RgbFloat Color;
        }
        
        private sg_pass_action _passAction;
        private readonly SgBuffer _vertexBuffer;
        private readonly SgBuffer _indexBuffer;
        private readonly SgBindings _bindings = new SgBindings();
        private readonly SgShader _shader;
        private readonly SgPipeline _pipeline;

        public unsafe BufferOffsetsApplication()
        {
            var vertices = new Vertex[7];
            
            // triangle
            vertices[0].Position = new Vector2(0f, 0.55f);
            vertices[0].Color = RgbFloat.Red;
            vertices[1].Position = new Vector2(0.25f, 0.05f);
            vertices[1].Color = RgbFloat.Green;
            vertices[2].Position = new Vector2(-0.25f, 0.05f);
            vertices[2].Color = RgbFloat.Blue;

            // quad
            vertices[3].Position = new Vector2(-0.25f, -0.05f);
            vertices[3].Color = RgbFloat.Blue;
            vertices[4].Position = new Vector2(0.25f, -0.05f);
            vertices[4].Color = RgbFloat.Green;
            vertices[5].Position = new Vector2(0.25f, -0.55f);
            vertices[5].Color = RgbFloat.Red;
            vertices[6].Position = new Vector2(-0.25f, -0.55f);
            vertices[6].Color = RgbFloat.Yellow;
            
            _vertexBuffer = new SgBuffer<Vertex>(SgBufferType.Vertex, SgBufferUsage.Immutable, vertices);
            
            var indices = new ushort[]
            {
                0, 1, 2,
                0, 1, 2, 0, 2, 3
            };
            _indexBuffer = new SgBuffer<ushort>(SgBufferType.Index, SgBufferUsage.Immutable, indices);
            
            _bindings.SetVertexBuffer(0, _vertexBuffer);
            _bindings.SetIndexBuffer(_indexBuffer);
            
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            if (GraphicsBackend == GraphicsBackend.Metal)
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            
            _shader = new SgShader(vertexShaderSourceCode, fragmentShaderSourceCode);
            
            _pipeline = new SgPipeline(_shader, new[]
            {
                sg_vertex_format.SG_VERTEXFORMAT_FLOAT2, 
                sg_vertex_format.SG_VERTEXFORMAT_FLOAT3
            }, sg_index_type.SG_INDEXTYPE_UINT16);
            
            _passAction = new sg_pass_action();
            var colors = _passAction.GetColors();
            colors[0] = new sg_color_attachment_action
            {
                action = sg_action.SG_ACTION_CLEAR, 
                val = new RgbaFloat(0.5f, 0.5f, 1.0f, 1.0f)
            };

        }
        
        protected override void Draw(int width, int height)
        {
            sg_begin_default_pass(ref _passAction, width, height);
            _pipeline.Apply();
            
            // render triangle
            _bindings.SetVertexBufferOffset(0, 0);
            _bindings.SetIndexBufferOffset(0);
            _bindings.Apply();
            sg_draw(0, 3, 1);
            
            // render quad from the same vertex- and index-buffer
            _bindings.SetVertexBufferOffset(0, 3 * Marshal.SizeOf<Vertex>());
            _bindings.SetIndexBufferOffset(3 * sizeof(ushort));
            _bindings.Apply();
            sg_draw(0, 6, 1);

            sg_end_pass();
        }
    }
}