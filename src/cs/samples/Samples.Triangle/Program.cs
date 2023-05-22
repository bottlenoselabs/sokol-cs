// Copyright (c) Bottlenose Labs Inc. (https://github.com/bottlenoselabs). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using bottlenoselabs.Sokol;
using static bottlenoselabs.Sokol.PInvoke;

namespace Samples
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
            public Graphics.Shader Shader;
            public Graphics.Pipeline Pipeline;
            public Graphics.Bindings Bindings;
        }

        private static ProgramState _state;

        private static void Main()
        {
            var desc = default(App.Desc);
            desc.InitCb.Pointer = &Initialize;
            desc.FrameCb.Pointer = &Frame;
            desc.Width = 400;
            desc.Height = 300;
            desc.WindowTitle = "Triangle";
            desc.Icon.SokolDefault = true;

            App.Run(&desc);
        }

        [UnmanagedCallersOnly]
        private static void Initialize()
        {
            var desc = default(Graphics.Desc);
            desc.Context = App.Sgcontext();
            Graphics.Setup(&desc);

            CreateResources();
        }

        private static void CreateResources()
        {
            _state.Bindings.VertexBuffers[0] = CreateVertexBuffer();
            _state.Shader = CreateShader();
            _state.Pipeline = CreatePipeline(_state.Shader);
        }

        [UnmanagedCallersOnly]
        private static void Frame()
        {
            var width = App.Width();
            var height = App.Height();
            var action = default(Graphics.PassAction);

            ref var colorAttachment = ref action.Colors[0];
            colorAttachment.Action = Graphics.Action.Clear;
            colorAttachment.Value = Rgba32F.Black;
            Graphics.BeginDefaultPass(&action, width, height);

            Graphics.ApplyPipeline(_state.Pipeline);
            Graphics.ApplyBindings((Graphics.Bindings*)Unsafe.AsPointer(ref _state.Bindings));
            Graphics.Draw(0, 3, 1);

            Graphics.EndPass();
            Graphics.Commit();
        }

        private static Graphics.Buffer CreateVertexBuffer()
        {
            var vertices = (Span<Vertex>)stackalloc Vertex[3];

            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;

            var desc = new Graphics.BufferDesc
            {
                Usage = Graphics.Usage.Immutable,
                Type = Graphics.BufferType.Vertexbuffer
            };

            ref var reference = ref MemoryMarshal.GetReference(vertices);
            desc.Data.Ptr = Unsafe.AsPointer(ref reference);
            desc.Data.Size = (uint)(Marshal.SizeOf<Vertex>() * vertices.Length);

            return Graphics.MakeBuffer(&desc);
        }

        private static Graphics.Shader CreateShader()
        {
            var desc = default(Graphics.ShaderDesc);

            ref var attribute0 = ref desc.Attrs[0];
            ref var attribute1 = ref desc.Attrs[1];

            switch (Graphics.QueryBackend())
            {
                case Graphics.Backend.Glcore33:
                    desc.Vs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainVert.glsl"));
                    desc.Fs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/opengl/mainFrag.glsl"));
                    break;
                case Graphics.Backend.MetalIos:
                case Graphics.Backend.MetalMacos:
                case Graphics.Backend.MetalSimulator:
                    desc.Vs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainVert.metal"));
                    desc.Fs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/metal/mainFrag.metal"));
                    break;
                case Graphics.Backend.D3d11:
                    desc.Vs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainVert.hlsl"));
                    desc.Fs.Source = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "assets/shaders/d3d11/mainFrag.hlsl"));
                    attribute0.SemName = "POS";
                    attribute1.SemName = "COLOR";
                    break;
                case Graphics.Backend.Dummy:
                case Graphics.Backend.Gles3:
                case Graphics.Backend.Wgpu:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Graphics.MakeShader(&desc);
        }

        private static Graphics.Pipeline CreatePipeline(Graphics.Shader shader)
        {
            var desc = new Graphics.PipelineDesc
            {
                Shader = shader
            };

            desc.Layout.Attrs[0].Format = Graphics.VertexFormat.Float3;
            desc.Layout.Attrs[1].Format = Graphics.VertexFormat.Float4;

            return Graphics.MakePipeline(&desc);
        }
    }
}
