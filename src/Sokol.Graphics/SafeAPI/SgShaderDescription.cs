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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public struct SgShaderDescription
    {
        internal sg_shader_desc desc;

        public unsafe ref SgShaderAttributeDescription Attribute(int index)
        {
            ref var layout = ref desc.attr(index);
            var pointer = Unsafe.AsPointer(ref layout);
            return ref Unsafe.AsRef<SgShaderAttributeDescription>(pointer);
        }

        public unsafe ref SgShaderStageDescription VertexShader
        {
            get
            {
                ref var shaderStageDesc = ref desc.vs;
                var pointer = Unsafe.AsPointer(ref shaderStageDesc);
                return ref Unsafe.AsRef<SgShaderStageDescription>(pointer);
            }
        }
        
        public unsafe ref SgShaderStageDescription FragmentShader
        {
            get
            {
                ref var shaderStageDesc = ref desc.fs;
                var pointer = Unsafe.AsPointer(ref shaderStageDesc);
                return ref Unsafe.AsRef<SgShaderStageDescription>(pointer);
            }
        }

        public unsafe IntPtr Label
        {
            get => (IntPtr) desc.label;
            set => desc.label = (byte*) value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_shader_desc GetCStruct(this ref SgShaderDescription description) 
        {
            return ref description.desc;
        }
    }
}