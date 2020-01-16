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

using System.Runtime.CompilerServices;
using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Sokol
{
    public ref struct SgVertexLayoutDescription
    {
        public sg_layout_desc CStruct;
        
        public unsafe ref SgVertexBufferLayoutDescription Buffer(int index)
        {
            ref var @ref = ref CStruct.buffer(index);
            fixed (sg_buffer_layout_desc* ptr = &@ref)
            {
                return ref *(SgVertexBufferLayoutDescription*) ptr;
            }
        }
        
        public unsafe ref SgVertexAttributeDescription Attribute(int index)
        {
            ref var @ref = ref CStruct.attr(index);
            fixed (sg_vertex_attr_desc* ptr = &@ref)
            {
                return ref *(SgVertexAttributeDescription*) ptr;
            }
        }
    }
}