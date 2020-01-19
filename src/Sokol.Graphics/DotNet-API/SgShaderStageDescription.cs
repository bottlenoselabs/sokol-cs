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

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

#pragma warning disable 1717

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 1280, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct SgShaderStageDescription
    {
        [FieldOffset(0)] public IntPtr SourceCode;
        [FieldOffset(8)] public IntPtr ByteCode;
        [FieldOffset(16)] public int ByteCodeSize;
        [FieldOffset(24)] public IntPtr EntryFunctionName;
        [FieldOffset(32)] internal fixed ulong _uniformBlocks[264 * SG_MAX_SHADERSTAGE_UBS / 8];
        [FieldOffset(1088)] internal fixed ulong _images[16 * SG_MAX_SHADERSTAGE_IMAGES / 8];
        
        public ref SgShaderUniformBlockDescription UniformBlock(int index)
        {
            fixed (SgShaderStageDescription* shaderStageDescription = &this)
            {
                var ptr = (SgShaderUniformBlockDescription*)&shaderStageDescription->_uniformBlocks[0];
                return ref *(ptr + index);
            }
        }

        public ref SgShaderImageDescription Image(int index)
        {
            fixed (SgShaderStageDescription* shaderStageDescription = &this)
            {
                var ptr = (SgShaderImageDescription*) &shaderStageDescription->_images[0];
                return ref *(ptr + index);
            }
        }
    }
}