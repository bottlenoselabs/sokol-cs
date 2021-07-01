using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static sokol_app;
using static sokol_gfx;
using static sokol_glue;

namespace Sokol.Samples.Triangle
{
    internal static unsafe class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
        }

        private struct ProgramState
        {
            public sg_shader Shader;
            public sg_pipeline Pipeline;
            public sg_bindings Bindings;
        }

        private static ProgramState _state;

        private static void Main()
        {
            var desc = default(sapp_desc);
            desc.init_cb.Pointer = &Initialize;
            desc.frame_cb.Pointer = &Frame;
            desc.width = 400;
            desc.height = 300;
            desc.gl_force_gles2 = true;
            desc.window_title = "Triangle";
            desc.icon.sokol_default = true;
        
            sapp_run(&desc);
        }
        
        [UnmanagedCallersOnly]
        private static void Initialize()
        {
            var desc = default(sg_desc);
            desc.context = sapp_sgcontext();
            sg_setup(&desc);
            
            CreateResources();
        }

        private static void CreateResources()
        {
            _state.Bindings.vertex_buffers[0] = CreateVertexBuffer();
            _state.Shader = CreateShader();
            _state.Pipeline = CreatePipeline(_state.Shader);
        }

        [UnmanagedCallersOnly]
        private static void Frame()
        {
            var width = sapp_width();
            var height = sapp_height();
            var action = default(sg_pass_action);

            ref var colorAttachment = ref action.colors[0];
            colorAttachment.action = sg_action.SG_ACTION_CLEAR;
            colorAttachment.value = Rgba32F.Black;
            sg_begin_default_pass(&action, width, height);

            sg_apply_pipeline(_state.Pipeline);

            sg_apply_bindings((sg_bindings*) Unsafe.AsPointer(ref _state.Bindings));

            sg_draw(0, 3, 1);

            sg_end_pass();
            sg_commit();
        }

        private static sg_buffer CreateVertexBuffer()
        {
            var vertices = (Span<Vertex>)stackalloc Vertex[3];

            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;

            var desc = new sg_buffer_desc
            {
                usage = sg_usage.SG_USAGE_IMMUTABLE,
                type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER
            };
            
            ref var reference = ref MemoryMarshal.GetReference(vertices);
            desc.data.ptr = Unsafe.AsPointer(ref reference);
            desc.data.size = (uint)(Marshal.SizeOf<Vertex>() * vertices.Length);

            return sg_make_buffer(&desc);
        }

        private static sg_shader CreateShader()
        {
            var desc = default(sg_shader_desc);

            ref var attribute0 = ref desc.attrs[0];
            ref var attribute1 = ref desc.attrs[1];

            switch (sg_query_backend())
            {
                case sg_backend.SG_BACKEND_GLCORE33:
                    desc.vs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainVert.glsl"));
                    desc.fs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainFrag.glsl"));
                    break;
                case sg_backend.SG_BACKEND_METAL_IOS:
                case sg_backend.SG_BACKEND_METAL_MACOS:
                case sg_backend.SG_BACKEND_METAL_SIMULATOR:
                    desc.vs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainVert.metal"));
                    desc.fs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainFrag.metal"));
                    break;
                case sg_backend.SG_BACKEND_D3D11:
                    desc.vs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainVert.hlsl"));
                    desc.fs.source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainFrag.hlsl"));
                    attribute0.sem_name = "POS";
                    attribute1.sem_name = "COLOR";
                    break;
                case sg_backend.SG_BACKEND_GLES2:
                case sg_backend.SG_BACKEND_GLES3:
                case sg_backend.SG_BACKEND_WGPU:
                case sg_backend.SG_BACKEND_DUMMY:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sg_make_shader(&desc);
        }

        private static sg_pipeline CreatePipeline(sg_shader shader)
        {
            var desc = new sg_pipeline_desc
            {
                shader = shader
            };

            desc.layout.attrs[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            desc.layout.attrs[1].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            
            return sg_make_pipeline(&desc);
        }
    }
}
