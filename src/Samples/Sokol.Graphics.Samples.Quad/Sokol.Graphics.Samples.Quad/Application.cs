// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using Sokol.SDL2;

namespace Sokol.Graphics.Samples.Quad
{
    public class Application : App
    {
        // the resources used
        private Pipeline _pipeline;
        private Buffer _indexBuffer;
        private Buffer _vertexBuffer;
        private Shader _shader;

        // the resource bindings
        private ResourceBindings _resourceBindings;

        public Application()
        {
            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _shader = CreateShader();
            _pipeline = CreatePipeline();

            SetResourceBindings();
        }

        protected override void Draw(int width, int height)
        {
            // begin a framebuffer render pass
            var passAction = PassAction.Clear(Rgba32F.Black);
            var pass = GraphicsDevice.BeginDefaultPass(ref passAction, width, height);

            // apply the render pipeline and bindings for the render pass
            pass.Apply(_pipeline);
            pass.Apply(ref _resourceBindings);

            // draw the quad into the target of the render pass
            pass.Draw(0, 6);

            // end framebuffer render pass
            pass.End();
        }

        private void SetResourceBindings()
        {
            // describe the binding of the vertex and index buffer (not applied yet!)
            _resourceBindings.VertexBuffer() = _vertexBuffer;
            _resourceBindings.IndexBuffer = _indexBuffer;
        }

        private Pipeline CreatePipeline()
        {
            // describe the render pipeline
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute(0).Format = PipelineVertexFormat.Float3;
            pipelineDesc.Layout.Attribute(1).Format = PipelineVertexFormat.Float4;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;

            // create the pipeline resource from the description
            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private Shader CreateShader()
        {
            // describe the shader program
            var shaderDesc = default(ShaderDescriptor);

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

            // create the shader resource from the description
            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private Buffer CreateIndexBuffer()
        {
            // use memory from the thread's stack to create the quad indices
            Span<ushort> indices = stackalloc ushort[]
            {
                0, 1, 2, // triangle 1 indices
                0, 2, 3 // triangle 2 indices
            };

            // describe an immutable index buffer
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.IndexBuffer
            };

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(indices);

            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private Buffer CreateVertexBuffer()
        {
            // use memory from the thread's stack for the quad vertices
            Span<Vertex> vertices = stackalloc Vertex[4];

            // describe the vertices of the quad
            vertices[0].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[0].Color = Rgba32F.Red;
            vertices[1].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[1].Color = Rgba32F.Green;
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[2].Color = Rgba32F.Blue;
            vertices[3].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[3].Color = Rgba32F.Yellow;

            // describe an immutable vertex buffer
            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.VertexBuffer
            };

            // immutable buffers need to specify the data/size in the descriptor
            // when using a `Memory<T>`, or a `Span<T>` which is unmanaged or already pinned, we do this by calling `SetData`
            bufferDesc.SetData(vertices);

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }
    }
}
