using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.Samples.Instancing
{
    public class InstancingApplication : App
    {
        private const int MAX_PARTICLES = 512 * 1024;
        private const int NUM_PARTICLES_EMITTED_PER_FRAME = 10;
        private const float FRAME_TIME = 1.0f / 60.0f;
        
        // The interleaved vertex data structure
        private struct Vertex
        {
            public Vector3 Position;
            public RgbaFloat Color;
        }
        
        private SgBuffer _geometryVertexBuffer;
        private SgBuffer _geometryIndexBuffer;
        private SgBuffer _instanceVertexBuffer;
        private SgBindings _bindings;
        private SgShader _shader;
        private SgPipeline _pipeline;
        private SgPassAction _frameBufferPassAction;

        private readonly Random _random = new Random();
        private int _currentParticleCount;
        private float _rotationY;
        private readonly Vector3[] _positions = new Vector3[MAX_PARTICLES];
        private readonly Vector3[] _velocities = new Vector3[MAX_PARTICLES];

        public unsafe InstancingApplication()
        {
            Debug.Assert(Sg.IsValid());
            Debug.Assert(Sg.QueryFeatures().Instancing);

            // use memory from the thread's stack for the static geometry vertices
            var vertices = stackalloc Vertex[6];
            
            const float r = 0.05f;
            // describe the vertices of the quad
            vertices[0].Position = new Vector3(0, -r, 0);
            vertices[0].Color = RgbaFloat.Red;
            vertices[1].Position = new Vector3(r, 0, r);
            vertices[1].Color = RgbaFloat.Green;
            vertices[2].Position = new Vector3(r, 0, -r);
            vertices[2].Color = RgbaFloat.Blue;
            vertices[3].Position = new Vector3(-r, 0, -r); 
            vertices[3].Color = RgbaFloat.Yellow;
            vertices[4].Position = new Vector3(-r, 0, r);
            vertices[4].Color = RgbaFloat.Cyan;
            vertices[5].Position = new Vector3(0, r, 0);
            vertices[5].Color = RgbaFloat.Magenta;

            // describe an immutable vertex buffer for the static geometry
            var geometryVertexBufferDesc = new SgBufferDescription();
            geometryVertexBufferDesc.Usage = SgUsage.Immutable;
            geometryVertexBufferDesc.Type = SgBufferType.Vertex;
            // immutable buffers need to specify the data/size in the description
            geometryVertexBufferDesc.Content = (IntPtr) vertices;
            geometryVertexBufferDesc.Size = Marshal.SizeOf<Vertex>() * 6;

            // create the vertex buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _geometryVertexBuffer = Sg.MakeBuffer(ref geometryVertexBufferDesc);
            
            // use memory from the thread's stack to create the static geometry indices
            var indices = stackalloc ushort[]
            {
                0, 1, 2,    0, 2, 3,    0, 3, 4,    0, 4, 1,
                5, 1, 2,    5, 2, 3,    5, 3, 4,    5, 4, 1
            };
            
            // describe an immutable index buffer for static geometry
            var geometryIndexBufferDesc = new SgBufferDescription();
            geometryIndexBufferDesc.Usage = SgUsage.Immutable;
            geometryIndexBufferDesc.Type = SgBufferType.Index;
            // immutable buffers need to specify the data/size in the description
            geometryIndexBufferDesc.Content = (IntPtr) indices;
            geometryIndexBufferDesc.Size = Marshal.SizeOf<ushort>() * 24;
            
            // create the index buffer resource from the description
            // note: for immutable buffers, this "uploads" the data to the GPU
            _geometryIndexBuffer = Sg.MakeBuffer(ref geometryIndexBufferDesc);
            
            // describe a stream vertex buffer for the instance data
            var instanceVertexBufferDesc = new SgBufferDescription();
            instanceVertexBufferDesc.Usage = SgUsage.Stream;
            instanceVertexBufferDesc.Type = SgBufferType.Vertex;
            instanceVertexBufferDesc.Size = Marshal.SizeOf<Vector3>() * MAX_PARTICLES;
            // create the vertex buffer resource from the description
            _instanceVertexBuffer = Sg.MakeBuffer(ref instanceVertexBufferDesc);
            
            // describe the binding of the buffers (not applied yet!)
            _bindings.VertexBuffer(0) = _geometryVertexBuffer;
            _bindings.VertexBuffer(1) = _instanceVertexBuffer;
            _bindings.IndexBuffer = _geometryIndexBuffer;

            // describe the shader program
            var shaderDesc = new SgShaderDescription();
            shaderDesc.VertexShader.UniformBlock(0).Size = Marshal.SizeOf<Matrix4x4>();
            ref var mvpUniform = ref shaderDesc.VertexShader.UniformBlock(0).Uniform(0);
            mvpUniform.Name = Marshal.StringToHGlobalAnsi("mvp");
            mvpUniform.Type = SgShaderUniformType.Matrix4x4;
            string vertexShaderStageSourceCode;
            string fragmentShaderStageSourceCode;
            // specify shader stage source code for each graphics backend
            if (GraphicsBackend.IsMetal())
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainVert.metal");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/metal/mainFrag.metal");
            }
            else if (GraphicsBackend.IsOpenGL())
            {
                vertexShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.vert");
                fragmentShaderStageSourceCode = File.ReadAllText("assets/shaders/opengl/main.frag");
            }
            else
            {
                throw new NotImplementedException();
            }
            // copy each shader stage source code to unmanaged memory and set it to the shader program desc
            shaderDesc.VertexShader.SourceCode = Marshal.StringToHGlobalAnsi(vertexShaderStageSourceCode);
            shaderDesc.FragmentShader.SourceCode = Marshal.StringToHGlobalAnsi(fragmentShaderStageSourceCode);
            
            // create the shader resource from the description
            _shader = Sg.MakeShader(ref shaderDesc);
            // after creating the shader we can free any allocs we had to make for the shader
            Marshal.FreeHGlobal(shaderDesc.VertexShader.UniformBlock(0).Uniform(0).Name);
            Marshal.FreeHGlobal(shaderDesc.VertexShader.SourceCode);
            Marshal.FreeHGlobal(shaderDesc.FragmentShader.SourceCode);
            
            // describe the render pipeline
            var pipelineDesc = new SgPipelineDescription();
            pipelineDesc.Layout.Buffer(0).Stride = 28;
            pipelineDesc.Layout.Buffer(1).Stride = 12;
            pipelineDesc.Layout.Buffer(1).StepFunction = SgVertexStepFunction.PerInstance;
            ref var positionAttribute = ref pipelineDesc.Layout.Attribute(0);
            positionAttribute.Offset = 0;
            positionAttribute.Format = SgVertexFormat.Float3;
            positionAttribute.BufferIndex = 0;
            ref var colorAttribute = ref pipelineDesc.Layout.Attribute(1);
            colorAttribute.Offset = 12;
            colorAttribute.Format = SgVertexFormat.Float4;
            colorAttribute.BufferIndex = 0;
            ref var attribute2 = ref pipelineDesc.Layout.Attribute(2);
            attribute2.Offset = 0;
            attribute2.Format = SgVertexFormat.Float3;
            attribute2.BufferIndex = 1;
            pipelineDesc.Shader = _shader;
            pipelineDesc.IndexType = SgIndexType.UInt16;
            pipelineDesc.DepthStencil.DepthCompareFunction = SgCompareFunction.LessEqual;
            pipelineDesc.DepthStencil.DepthWriteIsEnabled = true;
            pipelineDesc.Rasterizer.CullMode = SgCullMode.Back;
            // create the pipeline resource from the description
            _pipeline = Sg.MakePipeline(ref pipelineDesc);
            
            // set the frame buffer render pass action
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Grey);
        }
        
        protected override void Draw(int width, int height)
        {
            // emit new particles
            for (var i = 0; i < NUM_PARTICLES_EMITTED_PER_FRAME; i++) 
            {
                if (_currentParticleCount < MAX_PARTICLES) 
                {
                    _positions[_currentParticleCount] = Vector3.Zero;
                    _velocities[_currentParticleCount] = new Vector3(
                        (float)(_random.Next() & 0x7FFF) / 0x7FFF - 0.5f,
                        (float)(_random.Next() & 0x7FFF) / 0x7FFF * 0.5f + 2.0f,
                        (float)(_random.Next() & 0x7FFF) / 0x7FFF - 0.5f);
                    _currentParticleCount++;
                }
                else 
                {
                    break;
                }
            }

            // update particle positions
            for (var i = 0; i < _currentParticleCount; i++) 
            {
                _velocities[i].Y -= 1.0f * FRAME_TIME;
                _positions[i].X += _velocities[i].X * FRAME_TIME;
                _positions[i].Y += _velocities[i].Y * FRAME_TIME;
                _positions[i].Z += _velocities[i].Z * FRAME_TIME;
                // ReSharper disable once InvertIf
                if (_positions[i].Y < -2.0f) 
                {
                    _positions[i].Y = -1.8f;
                    _velocities[i].Y = -_velocities[i].Y;
                    _velocities[i].X *= 0.8f; 
                    _velocities[i].Y *= 0.8f;
                    _velocities[i].Z *= 0.8f;
                }
            }
            
            // update instance data
            Sg.UpdateBuffer(_instanceVertexBuffer, _positions.AsMemory(), _currentParticleCount);

            // create camera projection and view matrix
            var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) (45.0f * Math.PI / 180), (float)width / height,
                0.01f, 50.0f);
            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 1.5f, 12.0f), Vector3.Zero, Vector3.UnitY 
            );
            var viewProjectionMatrix = viewMatrix * projectionMatrix;

            // rotate each particle at the same time and create vertex shader mvp matrix
            _rotationY += 1.0f * 0.020f;
            var rotationMatrixY = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, _rotationY);
            var modelMatrix = rotationMatrixY;
            var modelViewProjectionMatrix = modelMatrix * viewProjectionMatrix;
            
            // begin a framebuffer render pass
            Sg.BeginDefaultPass(ref _frameBufferPassAction, width, height);
            
            // apply the render pipeline and bindings for the render pass
            Sg.ApplyPipeline(_pipeline);
            Sg.ApplyBindings(ref _bindings);
            
            // apply the mvp matrix to the vertex shader
            Sg.ApplyUniforms(SgShaderStageType.VertexShader, 0, ref modelViewProjectionMatrix);
            
            // draw the quad into the target of the render pass
            Sg.Draw(0, 24, _currentParticleCount);
            
            // end framebuffer render pass
            Sg.EndPass();
        }
    }
}