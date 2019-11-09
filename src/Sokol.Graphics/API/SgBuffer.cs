using System;
using System.Buffers;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public abstract class SgBuffer : SgResource
    {
        internal sg_buffer Handle;
     
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