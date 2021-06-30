using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.CubeTextured
{
    internal static class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Rgba32F Color;
            public Vector2 TextureCoordinates;
        }

        private struct VertexShaderParams
        {
            public Matrix4x4 ModelViewProjectionMatrix;
        }

        private struct ProgramState
        {
            public GraphicsBuffer VertexBuffer;
            public GraphicsBuffer IndexBuffer;
            public GraphicsShader Shader;
            public GraphicsImage Texture;
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
            descriptor.WindowTitle = "Cube Textured";
            descriptor.SampleCount = 4;
            App.Run(descriptor);
        }

        private static void CreateResources()
        {
            state.VertexBuffer = CreateVertexBuffer();
            state.IndexBuffer = CreateIndexBuffer();
            state.Texture = CreateTexture();
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
            resourceBindings.FragmentStageImage(0) = state.Texture;
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
            const float leftX = -1.0f;
            const float rightX = 1.0f;
            const float bottomY = -1.0f;
            const float topY = 1.0f;
            const float backZ = -1.0f;
            const float frontZ = 1.0f;
            // texture coordinates using standard texture coordinate system:
            //    top-left is (0, 0); bottom-right is (1, 1); this is true regardless of the width or height of the texture
            //    U and V are used because X and Y are already taken for model space
            const float leftU = 0.0f;
            const float rightU = 1.0f;
            const float topV = 0.0f;
            const float bottomV = 1.0f;

            var positionTopLeftBack = new Vector3(leftX, topY, backZ);
            var positionTopRightBack = new Vector3(rightX, topY, backZ);
            var positionBottomRightBack = new Vector3(rightX, bottomY, backZ);
            var positionBottomLeftBack = new Vector3(leftX, bottomY, backZ);
            var positionTopLeftFront = new Vector3(leftX, topY, frontZ);
            var positionTopRightFront = new Vector3(rightX, topY, frontZ);
            var positionBottomRightFront = new Vector3(rightX, bottomY, frontZ);
            var positionBottomLeftFront = new Vector3(leftX, bottomY, frontZ);

            var textureTopLeft = new Vector2(leftU, topV);
            var textureTopRight = new Vector2(rightU, topV);
            var textureBottomRight = new Vector2(rightU, bottomV);
            var textureBottomLeft = new Vector2(leftU, bottomV);

            // each face of the cube is a rectangle (two triangles), each rectangle is 4 vertices
            // we want visible faces to have clockwise triangles when facing the camera
            // this will allow to discard triangles not facing the camera (counter-clockwise) for proper visibility
            // convention used for clockwise rectangle is: (0) top-left, (1) top-right, (2) bottom-right, (3) bottom-left
            // convention used for counter-clockwise rectangle is: (0) top-right, (1) top-left, (2) bottom-left, (3) bottom-right

            var vertices = (Span<Vertex>)stackalloc Vertex[24];
            // back rectangle
            var color0 = Rgba32F.Red; // #FF0000
            vertices[0].Position = positionTopRightBack;
            vertices[0].Color = color0;
            vertices[0].TextureCoordinates = textureTopLeft;
            vertices[1].Position = positionTopLeftBack;
            vertices[1].TextureCoordinates = textureTopRight;
            vertices[1].Color = color0;
            vertices[2].Position = positionBottomLeftBack;
            vertices[2].TextureCoordinates = textureBottomRight;
            vertices[2].Color = color0;
            vertices[3].Position = positionBottomRightBack;
            vertices[3].TextureCoordinates = textureBottomLeft;
            vertices[3].Color = color0;
            // front rectangle
            var color1 = Rgba32F.Lime; // NOTE: "lime" is #00FF00; "green" is actually #008000
            vertices[4].Position = positionTopLeftFront;
            vertices[4].Color = color1;
            vertices[4].TextureCoordinates = textureTopLeft;
            vertices[5].Position = positionTopRightFront;
            vertices[5].Color = color1;
            vertices[5].TextureCoordinates = textureTopRight;
            vertices[6].Position = positionBottomRightFront;
            vertices[6].Color = color1;
            vertices[6].TextureCoordinates = textureBottomRight;
            vertices[7].Position = positionBottomLeftFront;
            vertices[7].Color = color1;
            vertices[7].TextureCoordinates = textureBottomLeft;
            // left rectangle
            var color2 = Rgba32F.Blue; // #0000FF
            vertices[8].Position = positionTopLeftBack;
            vertices[8].Color = color2;
            vertices[8].TextureCoordinates = textureTopLeft;
            vertices[9].Position = positionTopLeftFront;
            vertices[9].Color = color2;
            vertices[9].TextureCoordinates = textureTopRight;
            vertices[10].Position = positionBottomLeftFront;
            vertices[10].Color = color2;
            vertices[10].TextureCoordinates = textureBottomRight;
            vertices[11].Position = positionBottomLeftBack;
            vertices[11].Color = color2;
            vertices[11].TextureCoordinates = textureBottomLeft;
            // right rectangle;
            var color3 = Rgba32F.Yellow; // #FFFF00
            vertices[12].Position = positionTopRightFront;
            vertices[12].Color = color3;
            vertices[12].TextureCoordinates = textureTopLeft;
            vertices[13].Position = positionTopRightBack;
            vertices[13].Color = color3;
            vertices[13].TextureCoordinates = textureTopRight;
            vertices[14].Position = positionBottomRightBack;
            vertices[14].Color = color3;
            vertices[14].TextureCoordinates = textureBottomRight;
            vertices[15].Position = positionBottomRightFront;
            vertices[15].Color = color3;
            vertices[15].TextureCoordinates = textureBottomLeft;
            // bottom rectangle;
            var color4 = Rgba32F.Aqua; // #00FFFF
            vertices[16].Position = positionBottomRightBack;
            vertices[16].Color = color4;
            vertices[16].TextureCoordinates = textureTopLeft;
            vertices[17].Position = positionBottomLeftBack;
            vertices[17].Color = color4;
            vertices[17].TextureCoordinates = textureTopRight;
            vertices[18].Position = positionBottomLeftFront;
            vertices[18].Color = color4;
            vertices[18].TextureCoordinates = textureBottomRight;
            vertices[19].Position = positionBottomRightFront;
            vertices[19].Color = color4;
            vertices[19].TextureCoordinates = textureBottomLeft;
            // top rectangle;
            var color5 = Rgba32F.Fuchsia; // #FF00FF
            vertices[20].Position = positionTopLeftBack;
            vertices[20].Color = color5;
            vertices[20].TextureCoordinates = textureTopLeft;
            vertices[21].Position = positionTopRightBack;
            vertices[21].Color = color5;
            vertices[21].TextureCoordinates = textureTopRight;
            vertices[22].Position = positionTopRightFront;
            vertices[22].Color = color5;
            vertices[22].TextureCoordinates = textureBottomRight;
            vertices[23].Position = positionTopLeftFront;
            vertices[23].Color = color5;
            vertices[23].TextureCoordinates = textureBottomLeft;

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

        private static GraphicsImage CreateTexture()
        {
            var pixelData = (Span<Rgba8U>)stackalloc Rgba8U[]
            {
                Rgba8U.White, Rgba8U.Black, Rgba8U.White, Rgba8U.Black,
                Rgba8U.Black, Rgba8U.White, Rgba8U.Black, Rgba8U.White,
                Rgba8U.White, Rgba8U.Black, Rgba8U.White, Rgba8U.Black,
                Rgba8U.Black, Rgba8U.White, Rgba8U.Black, Rgba8U.White,
            };

            var desc = new GraphicsImageDescriptor
            {
                Usage = GraphicsResourceUsage.Immutable,
                Type = GraphicsImageType.Texture2D,
                Width = 4,
                Height = 4,
                Format = GraphicsPixelFormat.RGBA8,
                MinificationFilter = GraphicsImageFilter.Nearest,
                MagnificationFilter = GraphicsImageFilter.Nearest,
                WrapU = GraphicsImageWrap.ClampToEdge,
                WrapV = GraphicsImageWrap.ClampToEdge
            };

            desc.SetData(pixelData);

            return Graphics.MakeImage(ref desc);
        }

        private static GraphicsShader CreateShader()
        {
            var desc = default(GraphicsShaderDescriptor);
            ref var uniformBlock = ref desc.VertexStage.UniformBlock(0);
            uniformBlock.Size = Marshal.SizeOf<VertexShaderParams>();

            ref var mvpUniform = ref uniformBlock.Uniform(0);
            mvpUniform.Name = "mvp";
            mvpUniform.Type = GraphicsShaderUniformType.Matrix4x4;

            ref var texture = ref desc.FragmentStage.Image(0);
            texture.Name = "tex";
            texture.ImageType = GraphicsImageType.Texture2D;

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
            desc.Layout.Attribute(2).Format = PipelineVertexAttributeFormat.Float2;
            desc.Shader = shader;
            desc.IndexType = GraphicsPipelineVertexIndexType.UInt16;
            desc.Rasterizer.CullMode = GraphicsPipelineTriangleCullMode.Back;

            return Graphics.MakePipeline(ref desc);
        }
    }
}
