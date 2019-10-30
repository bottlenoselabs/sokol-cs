using System;
using System.Buffers;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public abstract class SgBuffer : SgResource
    {
        public sg_buffer Handle { get; protected set; }

        public int Size { get; }

        public SgBufferType Type { get; }

        public SgBufferUsage Usage { get; }
        
        protected SgBuffer(SgBufferType type, SgBufferUsage usage, int size, string name = null) 
            : base(name)
        {
            Type = type;
            Size = size;
            Usage = usage;
        }
    }
    
    public sealed class SgBuffer<T> : SgBuffer where T : struct
    {
        private MemoryHandle? _dataHandle;

        public SgBuffer(SgBufferType type, SgBufferUsage usage, Memory<T> data, string name = null)
            : base(type, usage, Marshal.SizeOf<T>() * data.Length, name)
        {
            CreateDescription(out var description);
            
            var dataHandle = data.Pin();
            unsafe
            {
                description.content = dataHandle.Pointer;   
            }
            _dataHandle = dataHandle;
            
            Handle = sg_make_buffer(ref description);
        }
        
        private void CreateDescription(out sg_buffer_desc description)
        {
            var result = new sg_buffer_desc
            {
                size = Size,
                type = Type switch
                {
                    SgBufferType.Vertex => sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER,
                    SgBufferType.Index => sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER,
                    _ => throw new InvalidOperationException()
                },
                usage = Usage switch
                {
                    SgBufferUsage.Immutable => sg_usage.SG_USAGE_IMMUTABLE,
                    SgBufferUsage.Dynamic => sg_usage.SG_USAGE_DYNAMIC,
                    SgBufferUsage.Stream => sg_usage.SG_USAGE_STREAM,
                    _ => throw new InvalidOperationException()
                }
            };
        
            unsafe
            {
                result.label = (char*) CNamePointer;
            }

            description = result;
        }

        public void Update(Memory<T> data)
        {
            EnsureNotDisposed();
            
            if (Usage == SgBufferUsage.Immutable)
            {
                throw new InvalidOperationException();
            }

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