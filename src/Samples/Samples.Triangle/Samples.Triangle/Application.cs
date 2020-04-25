// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace Samples.Triangle
{
    internal class Application : App
    {
        private Pipeline _pipeline;
        private Shader _shader;
        private Buffer _vertexBuffer;

        public Application()
        {
            _vertexBuffer = CreateVertexBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline();

            // Free any strings we implicitly allocated when creating resources
            // Only call this method AFTER resources are created
            GraphicsDevice.FreeStrings();
        }

        protected override void HandleInput(InputState state)
        {
        }

        protected override void Update(AppTime time)
        {
        }

        protected override void Draw(AppTime time)
        {
            // begin a frame buffer render pass
            var pass = BeginDefaultPass(Rgba32F.Black);

            // describe the binding of the vertex buffer (not applied yet!)
            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;

            // apply the render pipeline and bindings for the render pass
            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            // draw the triangle (3 vertex indices) into the target of the render pass
            pass.DrawElements(3);

            // end the frame buffer pass
            pass.End();
        }

        private static Buffer CreateVertexBuffer()
        {
            // create some memory for the triangle vertices, `Span` is similar to an array except it is type safe
            Span<Vertex> vertices = stackalloc Vertex[3];

            // describe the vertices of the triangle
            vertices[0].Position = new Vector3(0.0f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;

            // describe an immutable vertex buffer
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.VertexBuffer
            };
            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(vertices);

            // create the vertex buffer resource from the descriptor
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Pipeline CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = new PipelineDescriptor
            {
                Shader = _shader
            };
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexAttributeFormat.Float4;

            // create the pipeline resource from the descriptor
            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateShader()
        {
            // describe the shader program
            var shaderDesc = default(ShaderDescriptor);

            // describe the vertex shader attributes
            shaderDesc.Attribute(0).Name = "position";
            shaderDesc.Attribute(1).Name = "color0";

            // specify shader stage source code for each graphics backend
            if (Backend == GraphicsBackend.OpenGL)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else if (Backend == GraphicsBackend.Metal)
            {
                shaderDesc.VertexStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                shaderDesc.FragmentStage.SourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }

            // create the shader resource from the descriptor
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }
    }
}
