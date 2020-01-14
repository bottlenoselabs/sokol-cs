/* 
MIT License

Copyright (c) 2020 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgShader : SgResource<sg_shader>
    {
        public SgShader(ref SgShaderDescription description)
        {
            ref var vs = ref description.VertexShader;
            if (vs.Source == IntPtr.Zero && vs.ByteCode == IntPtr.Zero)
            {
                throw new ArgumentException("No vertex shader code.");
            }

            if (vs.ByteCode != IntPtr.Zero && vs.ByteCodeSize <= 0)
            {
                throw new ArgumentException("Vertex shader byte code is zero or less.");
            }
            
            ref var fs = ref description.FragmentShader;
            if (fs.Source == IntPtr.Zero && fs.ByteCode == IntPtr.Zero)
            {
                throw new ArgumentException("No fragment shader code.");
            }
            
            if (fs.ByteCode != IntPtr.Zero && fs.ByteCodeSize <= 0)
            {
                throw new ArgumentException("Vertex shader byte code is zero or less.");
            }
            
            _handle = sg_make_shader(ref description.desc);
        }

        protected override void ReleaseUnmanagedResources()
        {
            if (_handle.id == 0)
            {
                return;
            }

            sg_destroy_shader(_handle);
            _handle.id = 0;
        }
        
        public static implicit operator sg_shader(SgShader shader)
        {
            return shader._handle;
        }
    }
}