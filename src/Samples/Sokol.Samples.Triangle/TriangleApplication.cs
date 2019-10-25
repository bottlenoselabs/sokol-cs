using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.Triangle
{
    public class TriangleApplication : App
    {
        private static sg_pipeline _pipeline;
        private static sg_bindings _bindings;
        
        public unsafe TriangleApplication()
        {
            var vertices = stackalloc float[21 * 4];
            
            // v1
            vertices[0] = 0.0f;
            vertices[1] = 0.5f;
            vertices[2] = 0.5f;
            
            // c1
            vertices[3] = 1.0f;
            vertices[4] = 0.0f;
            vertices[5] = 0.0f;
            vertices[6] = 1.0f;
            
            // v2
            vertices[7] = 0.5f;
            vertices[8] = -0.5f;
            vertices[9] = 0.5f;
            
            // c2
            vertices[10] = 0.0f;
            vertices[11] = 1.0f;
            vertices[12] = 0.0f;
            vertices[13] = 1.0f;
            
            // v3
            vertices[14] = -0.5f;
            vertices[15] = -0.5f;
            vertices[16] = 0.5f;
            
            // c3
            vertices[17] = 0.0f;
            vertices[18] = 0.0f;
            vertices[29] = 1.0f;
            vertices[20] = 1.0f;
            
            var vertexBufferDesc = new sg_buffer_desc {size = 21 * 4, content = vertices};
            var vertexBuffer = sg_make_buffer(&vertexBufferDesc);
            _bindings = new sg_bindings();
            _bindings.vertex_buffers[0] = vertexBuffer.id;

            var shaderDesc = new sg_shader_desc();
            var shaderAttrs = shaderDesc.GetAttrs();
            shaderAttrs[0].name = (char*) Marshal.StringToHGlobalAnsi("position");
            shaderAttrs[1].name = (char*) Marshal.StringToHGlobalAnsi("color0");

            var vertexShaderSourceCode = @"
#version 330
in vec4 position;
in vec4 color0;
out vec4 color;
void main() {
  gl_Position = position;
  color = color0;
}
"; 
            
            shaderDesc.vs.source = (char*) Marshal.StringToHGlobalAnsi(vertexShaderSourceCode);

            var fragmentShaderSourceCode = @"
#version 330
in vec4 color;
out vec4 frag_color;
void main() {
  frag_color = color;
}
"; 

            shaderDesc.fs.source = (char*) Marshal.StringToHGlobalAnsi(fragmentShaderSourceCode);
            var shader = sg_make_shader(&shaderDesc);

            var pipelineDesc = new sg_pipeline_desc()
            {
                shader = shader
            };
            var pipelineAttrs = pipelineDesc.layout.GetAttrs();
            pipelineAttrs[0] = new sg_vertex_attr_desc()
            {
                format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3,
            };
            pipelineAttrs[1] = new sg_vertex_attr_desc()
            {
                format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4
            };
            
            _pipeline = sg_make_pipeline(&pipelineDesc);
        }

        protected override void Draw()
        {
            SDL_GL_GetDrawableSize(WindowHandle, out var width, out var height);
            var passAction = new sg_pass_action();
            sg_begin_default_pass(ref passAction, width, height);
            sg_apply_pipeline(_pipeline);
            sg_apply_bindings(ref _bindings);
            sg_draw(0, 3, 1);
            sg_end_pass();
        }
    }
}