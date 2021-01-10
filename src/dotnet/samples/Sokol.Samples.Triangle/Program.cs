using System;
using System.IO;
using System.Numerics;

namespace Sokol.Samples.Triangle
{
    internal static class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
        }

        private struct ProgramState
        {
            public GraphicsBuffer VertexBuffer;
            public GraphicsShader Shader;
            public GraphicsPipeline Pipeline;
        }

        private static ProgramState state;

        private static void Main()
        {
            App.CreateResources += CreateResources;
            App.Frame += Frame;

            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Triangle";
            App.Run(descriptor);
        }

        private static void CreateResources()
        {
            state.VertexBuffer = CreateVertexBuffer();
            state.Shader = CreateShader();
            state.Pipeline = CreatePipeline(state.Shader);
        }

        private static void Frame()
        {
            var pass = App.BeginDefaultPass(Rgba32F.Black);

            pass.ApplyPipeline(state.Pipeline);

            var resourceBindings = default(GraphicsResourceBindings);
            resourceBindings.VertexBuffer(0) = state.VertexBuffer;
            pass.ApplyBindings(ref resourceBindings);

            pass.DrawElements(3);

            pass.End();
            Graphics.Commit();
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
                Type = GraphicsBufferType.Vertex
            };

            desc.SetData(vertices);

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
                    attribute0.SemanticName = "POS";
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
            var desc = new GraphicsPipelineDescriptor
            {
                Shader = shader
            };
            desc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            desc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;

            return Graphics.MakePipeline(ref desc);
        }
    }
}
