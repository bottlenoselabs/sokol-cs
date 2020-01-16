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

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    public struct SgBindings
    {
        public sg_bindings CStruct;
        
        public unsafe ref SgBuffer VertexBuffer(int index)
        {
            ref var @ref = ref CStruct.vertex_buffer(index);
            fixed (sg_buffer* ptr = &@ref)
            {
                return ref *(SgBuffer*) ptr;
            }
        }
        
        public unsafe ref int VertexBufferOffset(int index)
        {
            ref var @ref = ref CStruct.vertex_buffer_offset(index);
            fixed (int* ptr = &@ref)
            {
                return ref *ptr;
            }
        }
        
        public unsafe ref SgBuffer IndexBuffer
        {
            get
            {
                ref var @ref = ref CStruct.index_buffer;
                fixed (sg_buffer* ptr = &@ref)
                {
                    return ref *(SgBuffer*) ptr;
                }   
            }
        }
        
        public unsafe ref int IndexBufferOffset
        {
            get
            {
                ref var @ref = ref CStruct.index_buffer_offset;
                fixed (int* ptr = &@ref)
                {
                    return ref *ptr;
                }   
            }
        }
        
        public unsafe ref SgImage VertexImage(int index)
        {
            ref var @ref = ref CStruct.vs_image(index);
            fixed (sg_image* ptr = &@ref)
            {
                return ref *(SgImage*) ptr;
            }
        }
        
        public unsafe ref SgImage FragmentImage(int index)
        {
            ref var @ref = ref CStruct.fs_image(index);
            fixed (sg_image* ptr = &@ref)
            {
                return ref *(SgImage*) ptr;
            }
        }

        public void Apply()
        {
            sg_apply_bindings(ref CStruct);
        }
    }
}