using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.Triangle
{
    public class TriangleApplication : App
    {
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private readonly SgPipeline _pipeline;
        private readonly SgBindings _bindings = new SgBindings();
        private readonly SgBuffer _vertexBuffer;
        private readonly SgShader _shader;
        private sg_pass_action _clearAction;
        
        public unsafe TriangleApplication()
        {
            var vertices = new Vertex[3];
            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = RgbaFloat.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = RgbaFloat.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = RgbaFloat.Blue;
            
            _vertexBuffer = new SgBuffer<Vertex>(SgBufferType.Vertex, SgBufferUsage.Immutable, vertices);

            _bindings.SetVertexBuffer(_vertexBuffer);
            
            string vertexShaderSourceCode;
            string fragmentShaderSourceCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/metal/main.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/metal/main.frag");
            }
            else
            {
                vertexShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            
            _shader = new SgShader(vertexShaderSourceCode, fragmentShaderSourceCode);
            
            _pipeline = new SgPipeline(_shader, new[]
            {
                sg_vertex_format.SG_VERTEXFORMAT_FLOAT3, 
                sg_vertex_format.SG_VERTEXFORMAT_FLOAT4
            });
            
            _clearAction = new sg_pass_action();
            _clearAction.GetColors()[0] = new sg_color_attachment_action()
            {
                action = sg_action.SG_ACTION_CLEAR,
                val = RgbaFloat.Black
            };
        }

        protected override void Draw(int width, int height)
        {
            sg_begin_default_pass(ref _clearAction, width, height);
            _pipeline.Apply();
            _bindings.Apply();
            sg_draw(0, 3, 1);
            sg_end_pass();
        }
    }
}