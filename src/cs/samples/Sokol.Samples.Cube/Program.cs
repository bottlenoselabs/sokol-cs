using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.Cube
{
    internal static class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
        }

        private struct VertexShaderParams
        {
            public Matrix4x4 ModelViewProjectionMatrix;
        }

        private struct ProgramState
        {
            public GraphicsShader Shader;
            public GraphicsBuffer VertexBuffer;
            public GraphicsBuffer IndexBuffer;
            public GraphicsPipeline Pipeline;

            public VertexShaderParams VertexShaderParams;
            public float CubeRotationX;
            public float CubeRotationY;

            public bool PauseUpdate;
        }

        private static ProgramState state;

        private static void Main()
        {
            App.CreateResources += CreateResources;
            App.Frame += Frame;
            App.Event += Event;

            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Cube";
            descriptor.SampleCount = 4;
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
            Update();
            Draw();
            Graphics.Commit();
        }

        private static void Event(in AppEvent @event)
        {
            if (@event.Type == AppEventType.KeyUp)
            {
                state.PauseUpdate = !state.PauseUpdate;
            }
        }

        private static void Update()
        {
            if (state.PauseUpdate)
            {
                return;
            }

            RotateCube();
        }

        private static void Draw()
        {
            var pass = App.BeginDefaultPass(Rgba32F.Gray);

            pass.ApplyPipeline(state.Pipeline);

            var resourceBindings = default(GraphicsResourceBindings);
            resourceBindings.VertexBuffer(0) = state.VertexBuffer;
            resourceBindings.IndexBuffer = state.IndexBuffer;
            pass.ApplyBindings(ref resourceBindings);

            pass.ApplyUniforms(GraphicsShaderStageType.Vertex, ref state.VertexShaderParams);

            // draw the cube (36 indices)
            // try drawing only parts of the cube by specifying 6, 12, 18, 24 or 30 for the number of indices!
            pass.DrawElements(36);

            pass.End();
        }

        private static void RotateCube()
        {
            const float deltaSeconds = 1 / 60f;

            state.CubeRotationX += 1.0f * deltaSeconds;
            state.CubeRotationY += 2.0f * deltaSeconds;
            var rotationMatrixX = Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, state.CubeRotationX);
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, state.CubeRotationY);
            var modelMatrix = rotationMatrixX * rotationMatrixY;

            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                (float)(40.0f * Math.PI / 180),
                (float)App.Width / App.Height,
                0.01f,
                10.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 6.0f),
                Vector3.Zero,
                Vector3.UnitY);

            state.VertexShaderParams.ModelViewProjectionMatrix = modelMatrix * viewMatrix * projectionMatrix;
        }

        private static GraphicsBuffer CreateVertexBuffer()
        {
            // model vertices of the cube using standard cartesian coordinate system:
            //    +Z is towards your eyes, -Z is towards the screen
            //    +X is to the right, -X to the left
            //    +Y is towards the sky (up), -Y is towards the floor (down)
            // 8 unique vertices for the corners of the cube
            // giving each unique vertex a distinct color provides an important understanding of cube's depth
            const float leftX = -1.0f;
            const float rightX = 1.0f;
            const float bottomY = -1.0f;
            const float topY = 1.0f;
            const float backZ = -1.0f;
            const float frontZ = 1.0f;

            var topLeftBack = default(Vertex);
            topLeftBack.Position = new Vector3(leftX, topY, backZ);
            topLeftBack.Color = Rgba32F.Lime; // NOTE: "lime" is #00FF00; "green" is actually #008000

            var topRightBack = default(Vertex);
            topRightBack.Position = new Vector3(rightX, topY, backZ);
            topRightBack.Color = Rgba32F.Yellow; // #FFFF00

            var bottomRightBack = default(Vertex);
            bottomRightBack.Position = new Vector3(rightX, bottomY, backZ);
            bottomRightBack.Color = Rgba32F.Red; // #FF0000

            var bottomLeftBack = default(Vertex);
            bottomLeftBack.Position = new Vector3(leftX, bottomY, backZ);
            bottomLeftBack.Color = Rgba32F.Black; // #000000

            var topLeftFront = default(Vertex);
            topLeftFront.Position = new Vector3(leftX, topY, frontZ);
            topLeftFront.Color = Rgba32F.Aqua; // #00FFFF

            var topRightFront = default(Vertex);
            topRightFront.Position = new Vector3(rightX, topY, frontZ);
            topRightFront.Color = Rgba32F.White; // #FFFFFF

            var bottomRightFront = default(Vertex);
            bottomRightFront.Position = new Vector3(rightX, bottomY, frontZ);
            bottomRightFront.Color = Rgba32F.Fuchsia; // #FF00FF

            var bottomLeftFront = default(Vertex);
            bottomLeftFront.Position = new Vector3(leftX, bottomY, frontZ);
            bottomLeftFront.Color = Rgba32F.Blue; // #0000FF

            // each face of the cube is a rectangle (two triangles), each rectangle is 4 vertices
            // we want visible faces to have clockwise triangles when facing the camera
            // this will allow to discard triangles not facing the camera (counter-clockwise) for proper visibility
            // convention used for clockwise rectangle is: (0) top-left, (1) top-right, (2) bottom-right, (3) bottom-left
            // convention used for counter-clockwise rectangle is: (0) top-right, (1) top-left, (2) bottom-left, (3) bottom-right

            var vertices = (Span<Vertex>)stackalloc Vertex[24];
            // back rectangle
            vertices[0] = topRightBack;
            vertices[1] = topLeftBack;
            vertices[2] = bottomLeftBack;
            vertices[3] = bottomRightBack;
            // front rectangle
            vertices[4] = topLeftFront;
            vertices[5] = topRightFront;
            vertices[6] = bottomRightFront;
            vertices[7] = bottomLeftFront;
            // left rectangle
            vertices[8] = topLeftBack;
            vertices[9] = topLeftFront;
            vertices[10] = bottomLeftFront;
            vertices[11] = bottomLeftBack;
            // right rectangle;
            vertices[12] = topRightFront;
            vertices[13] = topRightBack;
            vertices[14] = bottomRightBack;
            vertices[15] = bottomRightFront;
            // bottom rectangle;
            vertices[16] = bottomRightBack;
            vertices[17] = bottomLeftBack;
            vertices[18] = bottomLeftFront;
            vertices[19] = bottomRightFront;
            // top rectangle;
            vertices[20] = topLeftBack;
            vertices[21] = topRightBack;
            vertices[22] = topRightFront;
            vertices[23] = topLeftFront;

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
                0, 1, 2, 0, 2, 3, // rectangle 0
                4, 5, 6, 4, 6, 7, // rectangle 1
                8, 9, 10, 8, 10, 11, // rectangle 2
                12, 13, 14, 12, 14, 15, // rectangle 3
                16, 17, 18, 16, 18, 19, // rectangle 4
                20, 21, 22, 20, 22, 23 // rectangle 5
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
            ref var uniformBlock = ref desc.VertexStage.UniformBlock(0);
            uniformBlock.Size = Marshal.SizeOf<VertexShaderParams>();
            ref var mvpUniform = ref uniformBlock.Uniform(0);
            mvpUniform.Name = "mvp";
            mvpUniform.Type = GraphicsShaderUniformType.Matrix4x4;

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
                    attribute0.SemanticIndex = 0;
                    attribute1.SemanticName = "COLOR";
                    attribute1.SemanticIndex = 1;
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
            var desc = default(GraphicsPipelineDescriptor);
            desc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            desc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;
            desc.Shader = shader;
            desc.IndexType = GraphicsPipelineVertexIndexType.UInt16;
            desc.Rasterizer.CullMode = GraphicsPipelineTriangleCullMode.Back;

            return Graphics.MakePipeline(ref desc);
        }
    }
}
