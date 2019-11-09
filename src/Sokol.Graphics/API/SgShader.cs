using System;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgShader : SgResource
    {
        internal readonly sg_shader Handle;

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

            var vertexShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(vertexShaderSourceCode);
            var fragmentShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(fragmentShaderSourceCode);

            var description = new sg_shader_desc
            {
                label = (char*) CNamePointer,
                vs =
                {
                    source = (char*) vertexShaderSourceCodeCPointer
                },
                fs =
                {
                    source = (char*) fragmentShaderSourceCodeCPointer
                }
            };

            Handle = sg_make_shader(ref description);
            
            Marshal.FreeHGlobal(vertexShaderSourceCodeCPointer);
            Marshal.FreeHGlobal(fragmentShaderSourceCodeCPointer);
        }

        ~SgShader()
        {
            ReleaseUnmanagedResources();
        }
    }
}