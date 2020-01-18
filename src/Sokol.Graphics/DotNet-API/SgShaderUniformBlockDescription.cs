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

using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    public unsafe struct SgShaderUniformBlockDescription
    {
        [FieldOffset(0)] public int Size;
        [FieldOffset(8)] public fixed ulong _uniforms[16 * SG_MAX_UB_MEMBERS / 8];

        public ref SgShaderUniformDescription Uniform(int index)
        {
            fixed (SgShaderUniformBlockDescription* uniformBlockDescription = &this)
            {
                var ptr = (SgShaderUniformDescription*) &uniformBlockDescription->_uniforms[0];
                return ref * (ptr + index);
            }
        }
    }
}