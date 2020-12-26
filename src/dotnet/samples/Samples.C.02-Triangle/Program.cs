// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NativeTools;
using Sokol;
using static sokol_app;
using static sokol_gfx;

namespace Samples.C.Triangle
{
    internal static unsafe class Program
    {
        private static sg_buffer _vertexBuffer;
        private static sg_pipeline _pipeline;
        private static sg_shader _shader;

        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
        }

        private static void Main()
        {
            LibraryLoader.SetDllImportResolver(Assembly.GetAssembly(typeof(sokol_gfx))!);
            Run();
        }

        private static void Run()
        {
            var descriptor = default(sapp_desc);
            descriptor.window_title = UnmanagedStrings.GetBytes("Triangle");
            FillCallbacks(ref descriptor);
            sapp_run(&descriptor);
        }

        private static void FillCallbacks(ref sapp_desc descriptor)
        {
            var initializeCallbackFunctionPointer = (delegate* unmanaged[Cdecl] <void>)&Initialize;
            descriptor.init_cb = (IntPtr)initializeCallbackFunctionPointer;

            var frameCallbackFunctionPointer = (delegate* unmanaged[Cdecl] <void>)&Frame;
            descriptor.frame_cb = (IntPtr)frameCallbackFunctionPointer;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void Initialize()
        {
            InitializeGraphics();
            CreateResources();
            UnmanagedStrings.Clear();
        }

        private static void InitializeGraphics()
        {
            var graphicsDescriptor = default(sg_desc);
            graphicsDescriptor.context = sokol_glue.sapp_sgcontext();
            sg_setup(&graphicsDescriptor);
        }

        private static void CreateResources()
        {
            _vertexBuffer = CreateVertexBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline(_shader);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void Frame()
        {
            var passAction = default(sg_pass_action);

            ref var color0 = ref passAction.colors();
            color0.action = sg_action.SG_ACTION_CLEAR;
            color0.val = Rgba32F.Black;

            var width = sapp_width();
            var height = sapp_height();
            sg_begin_default_pass(&passAction, width, height);

            var resourceBindings = default(sg_bindings);
            resourceBindings.vertex_buffers() = _vertexBuffer;

            sg_apply_pipeline(_pipeline);
            sg_apply_bindings(&resourceBindings);

            sg_draw(0, 3, 1);

            sg_end_pass();

            sg_commit();
        }

        private static sg_buffer CreateVertexBuffer()
        {
            var vertices = (Span<Vertex>)stackalloc Vertex[3];

            // describe the vertices of the triangle in clip space
            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;

            var bufferDescriptor = new sg_buffer_desc
            {
                usage = sg_usage.SG_USAGE_IMMUTABLE,
                type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER
            };

            ref var reference = ref MemoryMarshal.GetReference(vertices);
            bufferDescriptor.content = Unsafe.AsPointer(ref reference);
            bufferDescriptor.size = Marshal.SizeOf<Vertex>() * vertices.Length;

            return sg_make_buffer(&bufferDescriptor);
        }

        private static sg_shader CreateShader()
        {
            var shaderDesc = default(sg_shader_desc);

            ref var attribute0 = ref shaderDesc.attrs(0);
            ref var attribute1 = ref shaderDesc.attrs(1);

            var backend = sg_query_backend();
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (backend)
            {
                case sg_backend.SG_BACKEND_GLCORE33:
                    shaderDesc.vs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/opengl/mainVert.glsl"));
                    shaderDesc.fs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/opengl/mainFrag.glsl"));
                    break;
                case sg_backend.SG_BACKEND_METAL_MACOS:
                    shaderDesc.vs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/metal/mainVert.metal"));
                    shaderDesc.fs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/metal/mainFrag.metal"));
                    break;
                case sg_backend.SG_BACKEND_D3D11:
                    shaderDesc.vs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/d3d11/mainVert.hlsl"));
                    shaderDesc.fs.source = UnmanagedStrings.GetBytes(File.ReadAllText("assets/shaders/d3d11/mainFrag.hlsl"));
                    attribute0.sem_name = UnmanagedStrings.GetBytes("POS");
                    attribute1.sem_name = UnmanagedStrings.GetBytes("COLOR");
                    break;
                case sg_backend.SG_BACKEND_DUMMY:
                    throw new NotSupportedException();
                default:
                    throw new NotImplementedException();
            }

            return sg_make_shader(&shaderDesc);
        }

        private static sg_pipeline CreatePipeline(sg_shader shader)
        {
            var pipelineDescriptor = new sg_pipeline_desc
            {
                shader = shader
            };
            pipelineDescriptor.layout.attrs(0).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            pipelineDescriptor.layout.attrs(1).format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;

            return sg_make_pipeline(&pipelineDescriptor);
        }
    }
}
