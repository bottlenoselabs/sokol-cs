using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static bottlenoselabs.sokol;

namespace Cube
{
    internal static unsafe class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
        }

        private struct VertexShaderParams
        {
            public Matrix4x4 ModelViewProjection;
        }

        private struct ProgramState
        {
            public sg_bindings Bindings;
            public sg_pipeline Pipeline;

            public VertexShaderParams VertexShaderParams;
            public float CubeRotationX;
            public float CubeRotationY;

            public bool PauseUpdate;
        }

        private static ProgramState _state;

        private static void Main()
        {
            var desc = default(sapp_desc);
            desc.init_cb.Pointer = &Initialize;
            desc.frame_cb.Pointer = &Frame;
            desc.event_cb.Pointer = &Event;
            desc.width = 800;
            desc.height = 600;
            desc.sample_count = 4;
            desc.window_title = (Runtime.CString)"Cube";
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
            _state.Bindings.index_buffer = CreateIndexBuffer();
            var shader = CreateShader();
            _state.Pipeline = CreatePipeline(shader);
        }

        [UnmanagedCallersOnly]
        private static void Frame()
        {
            Update();
            Draw();
            sg_commit();
        }

        [UnmanagedCallersOnly]
        private static void Event(sapp_event* e)
        {
            if (e->type == sapp_event_type.SAPP_EVENTTYPE_KEY_UP)
            {
                _state.PauseUpdate = !_state.PauseUpdate;
            }
        }

        private static void Update()
        {
            if (_state.PauseUpdate)
            {
                return;
            }

            RotateCube();
        }

        private static void Draw()
        {
            var width = sapp_width();
            var height = sapp_height();
            var action = default(sg_pass_action);

            ref var colorAttachment = ref action.colors[0];
            colorAttachment.action = sg_action.SG_ACTION_CLEAR;
            colorAttachment.value = Rgba32F.Gray;
            sg_begin_default_pass(&action, width, height);

            sg_apply_pipeline(_state.Pipeline);
            sg_apply_bindings((sg_bindings*) Unsafe.AsPointer(ref _state.Bindings));

            var uniforms = default(sg_range);
            uniforms.ptr = Unsafe.AsPointer(ref _state.VertexShaderParams);
            uniforms.size = (ulong) Marshal.SizeOf<VertexShaderParams>();
            sg_apply_uniforms(sg_shader_stage.SG_SHADERSTAGE_VS, 0, &uniforms);

            // draw the cube (36 indices)
            // try drawing only parts of the cube by specifying 6, 12, 18, 24 or 30 for the number of indices!
            sg_draw(0, 36, 1);

            sg_end_pass();
        }

        private static void RotateCube()
        {
            const float deltaSeconds = 1 / 60f;

            _state.CubeRotationX += 1.0f * deltaSeconds;
            _state.CubeRotationY += 2.0f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _state.CubeRotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _state.CubeRotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;

            var width = sapp_widthf();
            var height = sapp_heightf();
            
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(60.0f * Math.PI / 180),
                width / height,
                0.01f,
                10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f),
                Vector3.Zero,
                Vector3.UnitY);

            _state.VertexShaderParams.ModelViewProjection = modelMatrix * viewMatrix * projectionMatrix;
        }

        private static sg_buffer CreateVertexBuffer()
        {
	        var vertices = stackalloc Vertex[24];

			// model vertices of the cube using standard cartesian coordinate system:
			//    +Z is towards your eyes, -Z is towards the screen
			//    +X is to the right, -X to the left
			//    +Y is towards the sky (up), -Y is towards the floor (down)
			const float leftX = -1.0f;
			const float rightX = 1.0f;
			const float bottomY = -1.0f;
			const float topY = 1.0f;
			const float backZ = -1.0f;
			const float frontZ = 1.0f;

			// each face of the cube is a rectangle (two triangles), each rectangle is 4 vertices
			// rectangle 1; back
			var color1 = Rgba32F.Red; // #FF0000
			vertices[0].Position = new Vector3(leftX, bottomY, backZ);
			vertices[0].Color = color1;
			vertices[1].Position = new Vector3(rightX, bottomY, backZ);
			vertices[1].Color = color1;
			vertices[2].Position = new Vector3(rightX, topY, backZ);
			vertices[2].Color = color1;
			vertices[3].Position = new Vector3(leftX, topY, backZ);
			vertices[3].Color = color1;
			// rectangle 2; front
			var color2 = Rgba32F.Lime; // NOTE: "lime" is #00FF00; "green" is actually #008000
			vertices[4].Position = new Vector3(leftX, bottomY, frontZ);
			vertices[4].Color = color2;
			vertices[5].Position = new Vector3(rightX, bottomY, frontZ);
			vertices[5].Color = color2;
			vertices[6].Position = new Vector3(rightX, topY, frontZ);
			vertices[6].Color = color2;
			vertices[7].Position = new Vector3(leftX, topY, frontZ);
			vertices[7].Color = color2;
			// rectangle 3; left
			var color3 = Rgba32F.Blue; // #0000FF
			vertices[8].Position = new Vector3(leftX, bottomY, backZ);
			vertices[8].Color = color3;
			vertices[9].Position = new Vector3(leftX, topY, backZ);
			vertices[9].Color = color3;
			vertices[10].Position = new Vector3(leftX, topY, frontZ);
			vertices[10].Color = color3;
			vertices[11].Position = new Vector3(leftX, bottomY, frontZ);
			vertices[11].Color = color3;
			// rectangle 4; right
			var color4 = Rgba32F.Yellow; // #FFFF00
			vertices[12].Position = new Vector3(rightX, bottomY, backZ);
			vertices[12].Color = color4;
			vertices[13].Position = new Vector3(rightX, topY, backZ);
			vertices[13].Color = color4;
			vertices[14].Position = new Vector3(rightX, topY, frontZ);
			vertices[14].Color = color4;
			vertices[15].Position = new Vector3(rightX, bottomY, frontZ);
			vertices[15].Color = color4;
			// rectangle 5; bottom
			var color5 = Rgba32F.Aqua; // #00FFFF
			vertices[16].Position = new Vector3(leftX, bottomY, backZ);
			vertices[16].Color = color5;
			vertices[17].Position = new Vector3(leftX, bottomY, frontZ);
			vertices[17].Color = color5;
			vertices[18].Position = new Vector3(rightX, bottomY, frontZ);
			vertices[18].Color = color5;
			vertices[19].Position = new Vector3(rightX, bottomY, backZ);
			vertices[19].Color = color5;
			// rectangle 6; top
			var color6 = Rgba32F.Fuchsia; // #FF00FF
			vertices[20].Position = new Vector3(leftX, topY, backZ);
			vertices[20].Color = color6;
			vertices[21].Position = new Vector3(leftX, topY, frontZ);
			vertices[21].Color = color6;
			vertices[22].Position = new Vector3(rightX, topY, frontZ);
			vertices[22].Color = color6;
			vertices[23].Position = new Vector3(rightX, topY, backZ);
			vertices[23].Color = color6;

			var desc = new sg_buffer_desc
			{
				usage = sg_usage.SG_USAGE_IMMUTABLE,
				type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER,
				data =
				{
					ptr = vertices, 
					size = (uint) (Marshal.SizeOf<Vertex>() * 24)
				}
			};


			return sg_make_buffer(&desc);
        }

        private static sg_buffer CreateIndexBuffer()
        {
	        var indices = stackalloc ushort[]
	        {
		        0, 1, 2, 0, 2, 3, // rectangle 1 of cube, back, clockwise, base vertex: 0
		        6, 5, 4, 7, 6, 4, // rectangle 2 of cube, front, counter-clockwise, base vertex: 4
		        8, 9, 10, 8, 10, 11, // rectangle 3 of cube, left, clockwise, base vertex: 8
		        14, 13, 12, 15, 14, 12, // rectangle 4 of cube, right, counter-clockwise, base vertex: 12
		        16, 17, 18, 16, 18, 19, // rectangle 5 of cube, bottom, clockwise, base vertex: 16
		        22, 21, 20, 23, 22, 20 // rectangle 6 of cube, top, counter-clockwise, base vertex: 20
	        };

	        var desc = new sg_buffer_desc
	        {
		        usage = sg_usage.SG_USAGE_IMMUTABLE,
		        type = sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER,
		        data =
		        {
			        ptr = indices,
			        size = (uint) (Marshal.SizeOf<ushort>() * 36)
		        }
	        };


	        return sg_make_buffer(&desc);
        }

        private static sg_shader CreateShader()
        {
            var desc = default(sg_shader_desc);
            ref var uniformBlock = ref desc.vs.uniform_blocks[0];
            uniformBlock.size = (ulong) Marshal.SizeOf<VertexShaderParams>();
            ref var mvpUniform = ref uniformBlock.uniforms[0];
            mvpUniform.name = (Runtime.CString)"mvp";
            mvpUniform.type = sg_uniform_type.SG_UNIFORMTYPE_MAT4;
            
            switch (sg_query_backend())
            {
                case sg_backend.SG_BACKEND_GLCORE33:
                    desc.vs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainVert.glsl"));
                    desc.fs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainFrag.glsl"));
                    break;
                case sg_backend.SG_BACKEND_METAL_MACOS:
                case sg_backend.SG_BACKEND_METAL_IOS:
                case sg_backend.SG_BACKEND_METAL_SIMULATOR:
                    desc.vs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainVert.metal"));
                    desc.fs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainFrag.metal"));
                    break;
                case sg_backend.SG_BACKEND_D3D11:
                    desc.vs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainVert.hlsl"));
                    desc.fs.source = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainFrag.hlsl"));
                    ref var attribute0 = ref desc.attrs[0];
                    attribute0.sem_name = (Runtime.CString)"POSITION";
                    attribute0.sem_index = 0;
                    ref var attribute1 = ref desc.attrs[1];
                    attribute1.sem_name = (Runtime.CString)"COLOR";
                    attribute1.sem_index = 1;
                    break;
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
            var desc = default(sg_pipeline_desc);
            desc.layout.attrs[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            desc.layout.attrs[1].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            desc.shader = shader;
            desc.index_type = sg_index_type.SG_INDEXTYPE_UINT16;
            desc.cull_mode = sg_cull_mode.SG_CULLMODE_BACK;

            return sg_make_pipeline(&desc);
        }
    }
}