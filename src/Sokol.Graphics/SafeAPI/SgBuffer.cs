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

// ReSharper disable InconsistentNaming

namespace Sokol
{
    public sealed class SgBuffer : SgResource<sg_buffer>
    {
        public SgBufferType Type { get; }
        
        public SgUsage Usage { get; }
        
        public int Size { get; }

        public SgBuffer(ref SgBufferDescription description)
        {
            Type = description.Type;
            Usage = description.Usage;
            Size = description.Size;

            if (Size <= 0)
            {
                throw new ArgumentException("Buffer size is zero or less.");
            }

            if (Usage == SgUsage.Immutable)
            {
                if (description.Content == IntPtr.Zero || description.Size <= 0)
                {
                    throw new ArgumentException("Immutable buffers need to have the content and the size set in description.");
                }
            }

            _handle = sg_make_buffer(ref description.desc);
        }
        
        ~SgBuffer()
        {
            ReleaseUnmanagedResources();
        }

        protected override void ReleaseUnmanagedResources()
        {
            if (_handle.id == 0)
            {
                return;
            }

            sg_destroy_buffer(_handle);
            _handle.id = 0;
        }
        
        public void Update<T>(Memory<T> data) where T : unmanaged
        {
            if (Usage == SgUsage.Immutable)
            {
                throw new InvalidOperationException("Can not update an immutable buffer.");
            }
            
            EnsureNotDisposed();
            
            var dataSize = Marshal.SizeOf<T>() * data.Length;
            if (Size != dataSize)
            {
                throw new InvalidOperationException("Attempt to update a buffer with wrong data size.");
            }
            
            var dataHandle = data.Pin();

            unsafe
            {
                sg_update_buffer(_handle, dataHandle.Pointer, dataSize);   
            }
            
            dataHandle.Dispose();
        }
        
        public static implicit operator sg_buffer(SgBuffer buffer)
        {
            return buffer._handle;
        }
    }
}