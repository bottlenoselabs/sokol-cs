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

#pragma warning disable 649

namespace Sokol
{
    public struct SgBufferDescription
    {
        internal sg_buffer_desc desc;

        public SgBufferType Type
        {
            get => (SgBufferType) desc.type;
            set => desc.type = (sg_buffer_type) value;
        }

        public SgUsage Usage
        {
            get => (SgUsage) desc.usage;
            set => desc.usage = (sg_usage) value;
        }

        public int Size
        {
            get => desc.size;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                
                desc.size = value;
            }
        }

        public unsafe IntPtr Content
        {
            get => (IntPtr) desc.content;
            set => desc.content = (void*) value;
        }

        public unsafe IntPtr Label
        {
            get => (IntPtr) desc.label;
            set => desc.label = (byte*) value;
        }
    }

    public static partial class SgSafeExtensions
    {
        public static ref sg_buffer_desc GetCStruct(this ref SgBufferDescription description) 
        {
            return ref description.desc;
        }
    }
}