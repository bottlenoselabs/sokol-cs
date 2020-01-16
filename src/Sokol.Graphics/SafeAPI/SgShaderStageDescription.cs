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
// ReSharper disable ValueParameterNotUsed
// ReSharper disable UnusedMember.Global

#pragma warning disable 1717

namespace Sokol
{
    public ref struct SgShaderStageDescription
    {
        public sg_shader_stage_desc CStruct;

        public unsafe IntPtr Source
        {
            readonly get => (IntPtr) CStruct.source;
            set => CStruct.source = (byte*) value;
        }

        public unsafe IntPtr ByteCode
        {
            readonly get => (IntPtr) CStruct.byte_code;
            set => CStruct.byte_code = CStruct.byte_code;
        }

        public int ByteCodeSize
        {
            readonly get => CStruct.byte_code_size;
            set => CStruct.byte_code_size = value;
        }

        public unsafe IntPtr Entry
        {
            readonly get => (IntPtr) CStruct.entry;
            set => CStruct.entry = (byte*) value;
        }
        
        public unsafe ref SgShaderUniformBlockDescription UniformBlock(int index)
        {
            ref var @ref = ref CStruct.uniformBlock(index);
            fixed (sg_shader_uniform_block_desc* ptr = &@ref)
            {
                return ref *(SgShaderUniformBlockDescription*) ptr;
            }
        }
    }
}