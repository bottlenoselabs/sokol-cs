// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using Sokol.SDL2;

namespace Sokol.Graphics.Samples.Triangle
{
    internal class Application : App
    {
        // the resources used
        private readonly Pipeline _pipeline;
        private readonly Shader _shader;
        private readonly Buffer _vertexBuffer;

        // the resource bindings
        private ResourceBindings _resourceBindings;

        public Application()
        {
            _vertexBuffer = CreateVertexBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline();
            SetResourceBindings();

            // Free any strings we implicitly allocated when creating resources
            // Only call this method AFTER resources are created
            GraphicsDevice.FreeStrings();
        }

        protected override void Draw(int width, int height)
        {
            // begin a frame buffer render pass
            var passAction = PassAction.Clear(Rgba32F.Black);
            var pass = GraphicsDevice.BeginDefaultPass(ref passAction, width, height);

            // apply the render pipeline and bindings for the render pass
            pass.Apply(_pipeline);
            pass.Apply(ref _resourceBindings);

            // draw the triangle into the target of the render pass
            pass.Draw(0, 3);

            // end the frame buffer pass
            pass.End();
        }

        private void SetResourceBindings()
        {
            // describe the binding of the vertex buffer (not applied yet!)
            _resourceBindings.VertexBuffer() = _vertexBuffer;
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
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexFormat.Float4;

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
