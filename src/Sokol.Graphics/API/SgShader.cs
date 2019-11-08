using System;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgShader : SgResource
    {
        public sg_shader Handle { get; }

        public unsafe SgShader(string vertexShaderSourceCode, string fragmentShaderSourceCode, string name = null)
            : base(name)
        {
            if (vertexShaderSourceCode == null)
            {
                throw new ArgumentNullException(nameof(vertexShaderSourceCode));
            }

            if (fragmentShaderSourceCode == null)
            {
                throw new ArgumentNullException(nameof(vertexShaderSourceCode));
            }

            var description = new sg_shader_desc
            {
                label = (char*) CNamePointer,
                fs = {source = (char*) Marshal.StringToHGlobalAnsi(fragmentShaderSourceCode)},
                vs = {source = (char*) Marshal.StringToHGlobalAnsi(vertexShaderSourceCode)}
            };

            Handle = sg_make_shader(ref description);
        }

        protected override void ReleaseUnmanagedResources()
        {
            //TODO: free attribute names, and shader source code
            base.ReleaseUnmanagedResources();
        }

        ~SgShader()
        {
            ReleaseUnmanagedResources();
        }
    }
}