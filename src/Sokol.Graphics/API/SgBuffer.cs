/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

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
using System.Buffers;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class SgBuffer : SgResource
    {
        private MemoryHandle? _dataHandle;
        
        public sg_buffer Handle { get; }

        public SgBufferType Type { get; }
        
        public SgBufferUsage Usage { get; }
        
        public int Size { get; }

        protected internal SgBuffer(ref SgBufferDescription description) 
            : base(description.Name, description.NameCPointer)
        {
            Type = description.Type;
            Usage = description.Usage;
            Size = description.DataSize;

            if (Size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(description.DataSize), description.DataSize, null);
            }

            _dataHandle = description.DataHandle;
            if (Usage == SgBufferUsage.Immutable)
            {
                if (!_dataHandle.HasValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(description.DataPointer), description.DataPointer, null);
                }
            }

            Handle = sg_make_buffer(ref description.desc);

            // ReSharper disable once InvertIf
            if (_dataHandle.HasValue && Usage == SgBufferUsage.Immutable)
            {
                _dataHandle.Value.Dispose();
                _dataHandle = null;
            }
        }
        
        protected override void ReleaseUnmanagedResources()
        {
            _dataHandle?.Dispose();
            sg_destroy_buffer(Handle);
            base.ReleaseUnmanagedResources();
        }

        ~SgBuffer()
        {
            ReleaseUnmanagedResources();
        }
        
        public void Update<T>(Memory<T> data) where T : unmanaged
        {
            if (Usage == SgBufferUsage.Immutable)
            {
                throw new InvalidOperationException();
            }
            
            EnsureNotDisposed();

            _dataHandle?.Dispose();
            
            var dataSize = Marshal.SizeOf<T>() * data.Length;
            if (Size != dataSize)
            {
                throw new InvalidOperationException();
            }
            
            var dataHandle = data.Pin();
            _dataHandle = dataHandle;
            
            unsafe
            {
                sg_update_buffer(Handle, dataHandle.Pointer, dataSize);   
            }
        }

        public static implicit operator sg_buffer(SgBuffer buffer)
        {
            return buffer.Handle;
        }
    }
}