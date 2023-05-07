// Copyright (c) Bottlenose Labs Inc. (https://github.com/bottlenoselabs). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using bottlenoselabs.Sokol;
using static bottlenoselabs.Sokol.SokolPInvoke;

#pragma warning disable CS9080

namespace Triangle
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
            public GraphicsShader Shader;
            // public GraphicsPipeline Pipeline;
            public GraphicsResourceBindings Bindings;
        }

        private static ProgramState _state;

        private static void Main()
        {
            var desc = default(sapp_desc);
            desc.init_cb.Pointer = &Initialize;
            desc.frame_cb.Pointer = &Frame;
            desc.width = 400;
            desc.height = 300;
            desc.window_title = (Runtime.CString)"Triangle";
            desc.icon.sokol_default = true;

            sapp_run(ref desc);
        }

        [UnmanagedCallersOnly]
        private static void Initialize()
        {
            var desc = default(GraphicsDescriptor);
            desc.Context = sapp_sgcontext();
            sg_setup(ref desc);

            CreateResources();
        }

        private static void CreateResources()
        {
            _state.Bindings.VertexBuffers[0] = CreateVertexBuffer();
            _state.Shader = CreateShader();
            // _state.Pipeline = CreatePipeline(_state.Shader);
        }

        [UnmanagedCallersOnly]
        private static void Frame()
        {
            var width = sapp_width();
            var height = sapp_height();
            var action = default(GraphicsPassAction);

            ref var colorAttachment = ref action.Colors[0];
            colorAttachment.Action = GraphicsPassAttachmentAction.Clear;
            colorAttachment.Value = Rgba32F.Black;
            Graphics.BeginDefaultPass(width, height, ref action);

            // sg_apply_pipeline(_state.Pipeline);
            sg_apply_bindings(ref _state.Bindings);

            sg_draw(0, 3, 1);

            sg_end_pass();
            sg_commit();
        }

        private static GraphicsBuffer CreateVertexBuffer()
        {
            var vertices = (Span<Vertex>)stackalloc Vertex[3];

            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;

            var desc = new GraphicsBufferDescriptor
            {
                Usage = GraphicsResourceUsage.Immutable,
                Type = GraphicsBufferType.VertexBuffer
            };

            desc.SetData(vertices);

            return Graphics.MakeBuffer(ref desc);
        }

        private static GraphicsShader CreateShader()
        {
            var desc = default(GraphicsShaderDescriptor);

            ref var attribute0 = ref desc.Attributes[0];
            ref var attribute1 = ref desc.Attributes[1];

            // switch (sg_query_backend())
            // {
            //     case sg_backend.SG_BACKEND_GLCORE33:
            //         desc.VertexStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainVert.glsl"));
            //         desc.FragmentStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainFrag.glsl"));
            //         break;
            //     case sg_backend.SG_BACKEND_METAL_IOS:
            //     case sg_backend.SG_BACKEND_METAL_MACOS:
            //     case sg_backend.SG_BACKEND_METAL_SIMULATOR:
            //         desc.VertexStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainVert.metal"));
            //         desc.FragmentStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainFrag.metal"));
            //         break;
            //     case sg_backend.SG_BACKEND_D3D11:
            //         desc.VertexStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainVert.hlsl"));
            //         desc.FragmentStage.SourceCode = (Runtime.CString)File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainFrag.hlsl"));
            //         attribute0.SemanticName = (Runtime.CString)"POS";
            //         attribute1.SemanticName = (Runtime.CString)"COLOR";
            //         break;
            //     case sg_backend.SG_BACKEND_GLES3:
            //     case sg_backend.SG_BACKEND_WGPU:
            //     case sg_backend.SG_BACKEND_DUMMY:
            //         throw new NotImplementedException();
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }

            return Graphics.MakeShader(ref desc);
        }

        private static GraphicsPipeline CreatePipeline(GraphicsShader shader)
        {
            var desc = new GraphicsPipelineDescriptor
            {
                Shader = shader
            };

            desc.Layout.Attributes[0].Format = GraphicsPipelineVertexAttributeFormat.Float3;
            desc.Layout.Attributes[1].Format = GraphicsPipelineVertexAttributeFormat.Float4;

            return Graphics.MakePipeline(ref desc);
        }
    }
}
