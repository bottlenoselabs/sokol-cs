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
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 2968, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct SgShaderDescription
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(8)] internal fixed ulong _attributes[24 * SG_MAX_VERTEX_ATTRIBUTES / 8];
        [FieldOffset(392)] public SgShaderStageDescription VertexShader;
        [FieldOffset(1672)] public SgShaderStageDescription FragmentShader;
        [FieldOffset(2952)] public IntPtr Label;
        [FieldOffset(2960)] internal uint _endCanary;
        
        public ref SgShaderAttributeDescription Attribute(int index)
        {
            fixed (SgShaderDescription* shaderDescription = &this)
            {
                var ptr = (SgShaderAttributeDescription*) &shaderDescription->_attributes[0];
                return ref *(ptr + index);
            }
        }
    }
}