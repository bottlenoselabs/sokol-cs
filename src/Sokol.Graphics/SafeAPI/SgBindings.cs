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
    public struct SgBindings
    {
        // ReSharper disable once InconsistentNaming
        internal sg_bindings _bindings;

        public void Set(ref SgBuffer buffer, int bufferIndex = 0, int bufferOffset = 0)
        {
            if (buffer.Type == SgBufferType.Index && bufferIndex > 0 || bufferIndex > SG_MAX_SHADERSTAGE_BUFFERS)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferIndex), bufferIndex, null);
            }
            
            if (!buffer.IsValid)
            {
                throw new ArgumentException(nameof(buffer));
            }

            if (buffer.Type == SgBufferType.Index)
            {
                _bindings.index_buffer = buffer;
                _bindings.index_buffer_offset = bufferOffset;
            }
            else
            {
                _bindings.vertex_buffer(bufferIndex) = buffer;
                _bindings.vertex_buffer_offset(bufferIndex) = bufferOffset;
            }
        }

        public void Apply()
        {
            sg_apply_bindings(ref _bindings);
        }
    }
    
    public static partial class SgSafeExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static ref sg_bindings GetCStruct(this ref SgBindings bindings) 
        {
            return ref bindings._bindings;
        }
    }
}