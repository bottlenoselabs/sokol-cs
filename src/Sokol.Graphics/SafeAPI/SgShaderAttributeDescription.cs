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
    public struct SgShaderAttributeDescription
    {
        internal sg_shader_attr_desc desc;

        public unsafe IntPtr Name
        {
            get => (IntPtr) desc.name;
            set => desc.name = (byte*) value;
        }

        public unsafe IntPtr SemanticName
        {
            get => (IntPtr) desc.sem_name;
            set => desc.sem_name = (byte*) value;
        }
        
        public int SemanticIndex
        {
            get => desc.sem_index;
            set => desc.sem_index = value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_shader_attr_desc GetCStruct(this ref SgShaderAttributeDescription description) 
        {
            return ref description.desc;
        }
    }
}