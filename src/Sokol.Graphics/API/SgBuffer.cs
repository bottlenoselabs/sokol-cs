using System;
using System.Buffers;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgBuffer : SgResource
    {
        private MemoryHandle? _dataHandle;

        public sg_buffer Handle { get; private set; }

        public int Size { get; }

        public SgBufferType Type { get; }

        public SgBufferUsage Usage { get; }

        public SgBuffer(SgBufferType type, SgBufferUsage usage, int size, string name = null)
            : base(name)
        {
            Type = type;
            Size = size;
            Usage = usage;

            if (usage != SgBufferUsage.Immutable)
            {
                CreateNonImmutableBuffer();
            }
        }

        private void CreateNonImmutableBuffer()
        {
            CreateDescription(out var description);
            Handle = sg_make_buffer(ref description);
        }

        private void CreateImmutableBuffer<T>(Memory<T> data)
        {
            var dataSize = Marshal.SizeOf<T>() * data.Length;
            if (Size != dataSize)
            {
                throw new InvalidOperationException();
            }

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

        public void Update<T>(Memory<T> data) where T : struct
        {
            EnsureNotDisposed();
            
            if (Handle.id == 0)
            {
                CreateImmutableBuffer(data);
                return;
            }

            _dataHandle?.Dispose();
            var dataHandle = data.Pin();
            _dataHandle = dataHandle;
            
            var dataSize = Marshal.SizeOf<T>() * data.Length;
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