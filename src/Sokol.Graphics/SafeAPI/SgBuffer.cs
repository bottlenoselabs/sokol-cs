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
// ReSharper disable InconsistentNaming

namespace Sokol
{
    public struct SgBuffer
    {
        public sg_buffer CStruct;

        public bool IsValid => CStruct.id != 0;

        public SgBuffer(sg_buffer buffer)
        {
            CStruct = buffer;
        }

        public SgBuffer(ref SgBufferDescription description)
        {
            if (description.Size <= 0)
            {
                throw new ArgumentException("Buffer size is zero or less.");
            }

            if (description.Usage == SgUsage.Immutable)
            {
                if (description.Content == IntPtr.Zero || description.Size <= 0)
                {
                    throw new ArgumentException("Immutable buffers need to have the content and the size set in description.");
                }
            }

            CStruct = sg_make_buffer(ref description.CStruct);
        }
        
        public void Update<T>(Memory<T> data) where T : unmanaged
        {
            var dataHandle = data.Pin();
            var dataSize = Marshal.SizeOf<T>() * data.Length;
            
            unsafe
            {
                sg_update_buffer(CStruct, dataHandle.Pointer, dataSize);   
            }
            
            dataHandle.Dispose();
        }
        
        public void Destroy()
        {
            if (CStruct.id == 0)
            {
                return;
            }

            sg_destroy_buffer(CStruct);
            CStruct.id = 0;
        }
        
        public static implicit operator sg_buffer(SgBuffer buffer)
        {
            return buffer.CStruct;
        }
        
        public static implicit operator SgBuffer(sg_buffer buffer)
        {
            return new SgBuffer(buffer);
        }
    }
}