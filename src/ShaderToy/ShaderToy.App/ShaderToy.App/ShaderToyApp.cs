// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Sokol.App;
using Sokol.Graphics;
using Buffer = Sokol.Graphics.Buffer;

namespace ShaderToy.App
{
    public sealed class ShaderToyApp : Sokol.App.App
    {
        private readonly Pipeline _pipeline;
        private readonly Buffer _indexBuffer;
        private readonly Buffer _vertexBuffer;
        private readonly Shader _shader;

        private FragmentStageParams _uniforms;

        public ShaderToyApp(string shaderToySourceCode)
            : base(new AppDescriptor
            {
                RequestedBackend = GraphicsBackend.OpenGL,
                AllowHighDpi = false
            })
        {
            _vertexBuffer = CreateVertexBuffer();
            _indexBuffer = CreateIndexBuffer();
            _shader = CreateShader(shaderToySourceCode);
            _pipeline = CreatePipeline();
            GraphicsDevice.FreeStrings();

            DrawableSizeChanged += OnDrawableSizeChanged;
        }

        protected override void HandleInput(InputState state)
        {
            var mousePosition = state.MousePosition;
            _uniforms.iMouse.X = mousePosition.X;
            _uniforms.iMouse.Y = mousePosition.Y;
        }

        protected override void Update(AppTime time)
        {
            _uniforms.iTime = time.TotalSeconds;
        }

        protected override void Draw(AppTime time)
        {
            var pass = BeginDefaultPass(Rgba32F.Black);

            var resourceBindings = default(ResourceBindings);
            resourceBindings.VertexBuffer() = _vertexBuffer;
            resourceBindings.IndexBuffer = _indexBuffer;

            pass.ApplyPipeline(_pipeline);
            pass.ApplyBindings(ref resourceBindings);

            pass.ApplyShaderUniforms(ShaderStageType.FragmentStage, ref _uniforms);

            pass.DrawElements(6);

            pass.End();
        }

        private Shader CreateShader(string shaderToySourceCode)
        {
            if (Backend != GraphicsBackend.OpenGL)
            {
                throw new NotSupportedException();
            }

            var shaderDesc = default(ShaderDescriptor);

            ref var uniformBlock = ref shaderDesc.FragmentStage.UniformBlock();
            uniformBlock.Size = Marshal.SizeOf<FragmentStageParams>();

            ref var resolution = ref uniformBlock.Uniform(0);
            resolution.Name = "iResolution";
            resolution.Type = ShaderUniformType.Float3;

            ref var time = ref uniformBlock.Uniform(1);
            time.Name = "iTime";
            time.Type = ShaderUniformType.Float;

            ref var mouse = ref uniformBlock.Uniform(2);
            mouse.Name = "iMouse";
            mouse.Type = ShaderUniformType.Float4;

            // ReSharper disable StringLiteralTypo
            const string vertexStageSourceCode = @"
#version 330
precision mediump float;

layout(location=0) in vec4 position;

void main() {
  gl_Position = position;
}
";

            var fragmentStageSourceCode = @"
#version 330
precision mediump float;

uniform vec3 iResolution;
uniform float iTime;
uniform vec4 iMouse;
out vec4 frag_color;

" + shaderToySourceCode + @"

void main() {
  mainImage(frag_color, gl_FragCoord.xy);
}";
            // ReSharper restore StringLiteralTypo

            shaderDesc.VertexStage.SourceCode = vertexStageSourceCode;
            shaderDesc.FragmentStage.SourceCode = fragmentStageSourceCode;

            return GraphicsDevice.CreateShader(ref shaderDesc);
        }

        private void OnDrawableSizeChanged(Sokol.App.App app, int width, int height)
        {
            var aspectRatio = width / (float)height;
            _uniforms.iResolution.X = width;
            _uniforms.iResolution.Y = height;
            _uniforms.iResolution.Z = aspectRatio;
        }

        private Pipeline CreatePipeline()
        {
            var pipelineDesc = default(PipelineDescriptor);
            pipelineDesc.Shader = _shader;
            pipelineDesc.Layout.Attribute().Format = PipelineVertexAttributeFormat.Float3;
            pipelineDesc.IndexType = PipelineVertexIndexType.UInt16;

            return GraphicsDevice.CreatePipeline(ref pipelineDesc);
        }

        private static Buffer CreateIndexBuffer()
        {
            // ReSharper disable once RedundantCast
            var indices = (Span<ushort>)stackalloc ushort[]
            {
                0, 1, 2,
                0, 2, 3
            };

            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.IndexBuffer
            };

            bufferDesc.SetData(indices);

            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }

        private static Buffer CreateVertexBuffer()
        {
            // ReSharper disable once RedundantCast
            var vertices = (Span<Vector3>)stackalloc Vector3[4];

            vertices[0] = new Vector3(-1.0f, 1.0f, 0.5f);
            vertices[1] = new Vector3(1.0f, 1.0f, 0.5f);
            vertices[2] = new Vector3(1.0f, -1.0f, 0.5f);
            vertices[3] = new Vector3(-1.0f, -1.0f, 0.5f);

            var bufferDesc = new BufferDescriptor
            {
                Usage = ResourceUsage.Immutable,
                Type = BufferType.VertexBuffer
            };

            bufferDesc.SetData(vertices);

            return GraphicsDevice.CreateBuffer(ref bufferDesc);
        }
    }
}
