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
        public sg_buffer Handle { get; protected set; }

        public SgBufferType Type { get; }
        
        public SgBufferUsage Usage { get; }
        
        public int Size { get; }

        protected internal SgBuffer(SgBufferType type, SgBufferUsage usage, int size, string name) 
            : base(name)
        {
            if (type != SgBufferType.Index && type != SgBufferType.Vertex)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            if (usage != SgBufferUsage.Dynamic && usage != SgBufferUsage.Immutable && usage != SgBufferUsage.Stream)
            {
                throw new ArgumentOutOfRangeException(nameof(usage));
            }
            
            if (size == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            
            Type = type;
            Usage = usage;
            Size = size;
        }
        
        public static implicit operator sg_buffer(SgBuffer buffer)
        {
            return buffer.Handle;
        }
    }
    
    public sealed class SgBuffer<T> : SgBuffer where T : struct
    {
        private MemoryHandle? _dataHandle;

        public SgBuffer(SgBufferType type, SgBufferUsage usage, int size, string name = null)
            : base(type, usage, size, name)
        {
            var desc = new sg_buffer_desc
            {
                size = size,
#pragma warning disable 8509
                type = type switch
#pragma warning restore 8509
                {
                    SgBufferType.Vertex => sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER,
                    SgBufferType.Index => sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER
                },
#pragma warning disable 8509
                usage = usage switch
#pragma warning restore 8509
                {
                    SgBufferUsage.Immutable => sg_usage.SG_USAGE_IMMUTABLE,
                    SgBufferUsage.Dynamic => sg_usage.SG_USAGE_DYNAMIC,
                    SgBufferUsage.Stream => sg_usage.SG_USAGE_STREAM
                }
            };

            unsafe
            {
                desc.label = (char*) CNamePointer;
            }

            Handle = sg_make_buffer(ref desc);
        }

        public SgBuffer(SgBufferType type, SgBufferUsage usage, Memory<T> data, string name = null)
            : base(type, usage, Marshal.SizeOf<T>() * data.Length, name)
        {
            var desc = new sg_buffer_desc
            {
                size = Size,
#pragma warning disable 8509
                type = type switch
#pragma warning restore 8509
                {
                    SgBufferType.Vertex => sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER,
                    SgBufferType.Index => sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER,
                },
#pragma warning disable 8509
                usage = usage switch
#pragma warning restore 8509
                {
                    SgBufferUsage.Immutable => sg_usage.SG_USAGE_IMMUTABLE,
                    SgBufferUsage.Dynamic => sg_usage.SG_USAGE_DYNAMIC,
                    SgBufferUsage.Stream => sg_usage.SG_USAGE_STREAM
                }
            };
            
            var dataHandle = data.Pin();
            unsafe
            {
                desc.label = (char*) CNamePointer;
                desc.content = dataHandle.Pointer;   
            }
            
            _dataHandle = dataHandle;
            
            Handle = sg_make_buffer(ref desc);
        }

        public void Update(Memory<T> data)
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
    }
}