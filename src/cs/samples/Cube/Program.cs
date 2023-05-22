// Copyright (c) Bottlenose Labs Inc. (https://github.com/bottlenoselabs). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using bottlenoselabs.Sokol;
using static bottlenoselabs.Sokol.PInvoke;

namespace Samples;

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
        public Graphics.Bindings Bindings;
        public Graphics.Pipeline Pipeline;

        public VertexShaderParams VertexShaderParams;
        public float CubeRotationX;
        public float CubeRotationY;

        public bool PauseUpdate;
    }

    private static ProgramState _state;

    private static void Main()
    {
        var desc = default(App.Desc);
        desc.InitCb.Pointer = &Initialize;
        desc.FrameCb.Pointer = &Frame;
        desc.EventCb.Pointer = &Event;
        desc.Width = 800;
        desc.Height = 600;
        desc.SampleCount = 4;
        desc.WindowTitle = "Cube";
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
        _state.Bindings.IndexBuffer = CreateIndexBuffer();
        var shader = CreateShader();
        _state.Pipeline = CreatePipeline(shader);
    }

    [UnmanagedCallersOnly]
    private static void Frame()
    {
        Update();
        Draw();
        Graphics.Commit();
    }

    [UnmanagedCallersOnly]
    private static void Event(App.Event* e)
    {
        if (e->Type == App.EventType.KeyUp)
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
        var width = App.Width();
        var height = App.Height();

        var action = default(Graphics.PassAction);

        ref var colorAttachment = ref action.Colors[0];
        colorAttachment.LoadAction = Graphics.LoadAction.Clear;
        colorAttachment.ClearValue = Rgba32F.Gray;
        Graphics.BeginDefaultPass(&action, width, height);

        Graphics.ApplyPipeline(_state.Pipeline);
        Graphics.ApplyBindings((Graphics.Bindings*)Unsafe.AsPointer(ref _state.Bindings));

        var uniforms = default(Graphics.Range);
        uniforms.Ptr = Unsafe.AsPointer(ref _state.VertexShaderParams);
        uniforms.Size = (ulong)Marshal.SizeOf<VertexShaderParams>();
        Graphics.ApplyUniforms(Graphics.ShaderStage.Vs, 0, &uniforms);

        // draw the cube (36 indices)
        // try drawing only parts of the cube by specifying 6, 12, 18, 24 or 30 for the number of indices!
        Graphics.Draw(0, 36, 1);

        Graphics.EndPass();
    }

    private static void RotateCube()
    {
        const float deltaSeconds = 1 / 60f;

        _state.CubeRotationX += 1.0f * deltaSeconds;
        _state.CubeRotationY += 2.0f * deltaSeconds;
        var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, _state.CubeRotationX);
        var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _state.CubeRotationY);
        var modelMatrix = rotationMatrixX * rotationMatrixY;

        var width = App.Widthf();
        var height = App.Heightf();

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

    private static Graphics.Buffer CreateVertexBuffer()
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

        var desc = new Graphics.BufferDesc
        {
            Usage = Graphics.Usage.Immutable,
            Type = Graphics.BufferType.Vertexbuffer,
            Data =
            {
                Ptr = vertices,
                Size = (uint)(Marshal.SizeOf<Vertex>() * 24)
            }
        };

        return Graphics.MakeBuffer(&desc);
    }

    private static Graphics.Buffer CreateIndexBuffer()
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

        var desc = new Graphics.BufferDesc
        {
            Usage = Graphics.Usage.Immutable,
            Type = Graphics.BufferType.Indexbuffer,
            Data =
            {
                Ptr = indices,
                Size = (uint)(Marshal.SizeOf<ushort>() * 36)
            }
        };

        return Graphics.MakeBuffer(&desc);
    }

    private static Graphics.Shader CreateShader()
    {
        var desc = default(Graphics.ShaderDesc);
        ref var uniformBlock = ref desc.Vs.UniformBlocks[0];
        uniformBlock.Size = (ulong)Marshal.SizeOf<VertexShaderParams>();
        ref var mvpUniform = ref uniformBlock.Uniforms[0];
        mvpUniform.Name = "mvp";
        mvpUniform.Type = Graphics.UniformType.Mat4;

        switch (Graphics.QueryBackend())
        {
            case Graphics.Backend.Glcore33:
                desc.Vs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/opengl/mainVert.glsl"));
                desc.Fs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/opengl/mainFrag.glsl"));
                break;
            case Graphics.Backend.MetalIos:
            case Graphics.Backend.MetalMacos:
            case Graphics.Backend.MetalSimulator:
                desc.Vs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/metal/mainVert.metal"));
                desc.Fs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/metal/mainFrag.metal"));
                break;
            case Graphics.Backend.D3d11:
                desc.Vs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/d3d11/mainVert.hlsl"));
                desc.Fs.Source = File.ReadAllText(Path.Combine(
                    AppContext.BaseDirectory,
                    "assets/shaders/d3d11/mainFrag.hlsl"));
                ref var attribute0 = ref desc.Attrs[0];
                attribute0.SemName = "POSITION";
                attribute0.SemIndex = 0;
                ref var attribute1 = ref desc.Attrs[1];
                attribute1.SemName = "COLOR";
                attribute1.SemIndex = 1;
                break;
            case Graphics.Backend.Gles3:
            case Graphics.Backend.Wgpu:
            case Graphics.Backend.Dummy:
                throw new NotImplementedException();
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Graphics.MakeShader(&desc);
    }

    private static Graphics.Pipeline CreatePipeline(Graphics.Shader shader)
    {
        var desc = default(Graphics.PipelineDesc);
        desc.Layout.Attrs[0].Format = Graphics.VertexFormat.Float3;
        desc.Layout.Attrs[1].Format = Graphics.VertexFormat.Float4;
        desc.Shader = shader;
        desc.IndexType = Graphics.IndexType.Uint16;
        desc.CullMode = Graphics.CullMode.Back;

        return Graphics.MakePipeline(&desc);
    }
}
