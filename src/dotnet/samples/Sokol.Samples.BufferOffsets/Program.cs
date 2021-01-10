using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.BufferOffsets
{
    internal static class Program
    {
        private struct Vertex
        {
            public Vector2 Position;
            public Rgb32F Color;
        }

        private struct ProgramState
        {
            public GraphicsBuffer VertexBuffer;
            public GraphicsBuffer IndexBuffer;
            public GraphicsShader Shader;
            public GraphicsPipeline Pipeline;
        }

        private static ProgramState state;

        private static void Main()
        {
            App.CreateResources += CreateResources;
            App.Frame += Frame;

            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Buffer Offsets";
            App.Run(descriptor);
        }

        private static void CreateResources()
        {
            state.VertexBuffer = CreateVertexBuffer();
            state.IndexBuffer = CreateIndexBuffer();
            state.Shader = CreateShader();
            state.Pipeline = CreatePipeline(state.Shader);
        }

        private static void Frame()
        {
            var clearColor = new Rgba32F(0x8080FFFF);
            var pass = App.BeginDefaultPass(clearColor);

            pass.ApplyPipeline(state.Pipeline);

            // set and apply the bindings necessary to draw triangle
            var resourceBindings = default(GraphicsResourceBindings);
            resourceBindings.VertexBuffer(0) = state.VertexBuffer;
            resourceBindings.VertexBufferOffset(0) = 0;
            resourceBindings.IndexBuffer = state.IndexBuffer;
            resourceBindings.IndexBufferOffset = 0;
            pass.ApplyBindings(ref resourceBindings);

            // draw the triangle
            pass.DrawElements(3);

            // set and apply the bindings necessary to draw the rectangle
            resourceBindings.VertexBuffer(0) = state.VertexBuffer;
            resourceBindings.VertexBufferOffset(0) = 3 * Marshal.SizeOf<Vertex>();
            resourceBindings.IndexBuffer = state.IndexBuffer;
            resourceBindings.IndexBufferOffset = 3 * Marshal.SizeOf<ushort>();
            pass.ApplyBindings(ref resourceBindings);

            // draw the quad
            pass.DrawElements(6);

            pass.End();
            Graphics.Commit();
        }

        private static GraphicsBuffer CreateVertexBuffer()
        {
            var vertices = (Span<Vertex>)stackalloc Vertex[7];

            // the vertices of the triangle in clip space using clockwise order
            vertices[0].Position = new Vector2(0f, 0.55f);
            vertices[0].Color = Rgb32F.Red;
            vertices[1].Position = new Vector2(0.25f, 0.05f);
            vertices[1].Color = Rgb32F.Green;
            vertices[2].Position = new Vector2(-0.25f, 0.05f);
            vertices[2].Color = Rgb32F.Blue;

            // the vertices of the rectangle in clip space using clockwise order
            vertices[3].Position = new Vector2(-0.25f, -0.05f);
            vertices[3].Color = Rgb32F.Blue;
            vertices[4].Position = new Vector2(0.25f, -0.05f);
            vertices[4].Color = Rgb32F.Green;
            vertices[5].Position = new Vector2(0.25f, -0.55f);
            vertices[5].Color = Rgb32F.Red;
            vertices[6].Position = new Vector2(-0.25f, -0.55f);
            vertices[6].Color = Rgb32F.Yellow;

            var desc = new GraphicsBufferDescriptor
            {
                Usage = GraphicsResourceUsage.Immutable,
                Type = GraphicsBufferType.Vertex
            };

            desc.SetData(vertices);

            return Graphics.MakeBuffer(ref desc);
        }

        private static GraphicsBuffer CreateIndexBuffer()
        {
            var indices = (Span<ushort>)stackalloc ushort[]
            {
                0, 1, 2, // triangle
                0, 1, 2, // triangle 1 of quad
                0, 2, 3 // triangle 2 of quad
            };

            var desc = new GraphicsBufferDescriptor
            {
                Usage = GraphicsResourceUsage.Immutable,
                Type = GraphicsBufferType.Index
            };

            desc.SetData(indices);

            return Graphics.MakeBuffer(ref desc);
        }

        private static GraphicsShader CreateShader()
        {
            var desc = default(GraphicsShaderDescriptor);

            ref var attribute0 = ref desc.Attribute(0);
            ref var attribute1 = ref desc.Attribute(1);

            switch (Graphics.Backend)
            {
                case GraphicsBackend.OpenGL:
                    desc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/mainVert.glsl");
                    desc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/mainFrag.glsl");
                    break;
                case GraphicsBackend.Metal:
                    desc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                    desc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
                    break;
                case GraphicsBackend.Direct3D11:
                    desc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/mainVert.hlsl");
                    desc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/d3d11/mainFrag.hlsl");
                    attribute0.SemanticName = "POSITION";
                    attribute1.SemanticName = "COLOR";
                    break;
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.WebGPU:
                case GraphicsBackend.Dummy:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Graphics.MakeShader(ref desc);
        }

        private static GraphicsPipeline CreatePipeline(GraphicsShader shader)
        {
            var pipelineDesc = default(GraphicsPipelineDescriptor);

            pipelineDesc.Shader = shader;
            pipelineDesc.IndexType = GraphicsPipelineVertexIndexType.UInt16;

            ref var attribute0 = ref pipelineDesc.Layout.Attribute(0);
            attribute0.Format = PipelineVertexAttributeFormat.Float2;

            ref var attribute1 = ref pipelineDesc.Layout.Attribute(1);
            attribute1.Format = PipelineVertexAttributeFormat.Float3;

            return Graphics.MakePipeline(ref pipelineDesc);
        }
    }
}
