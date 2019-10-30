using System;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgBindings
    {
        private sg_bindings _bindings;

        public SgBindings()
        {
        }

        public unsafe void SetVertexBuffer(SgBuffer buffer, int bufferIndex = 0, int byteOffset = 0)
        {
            if (buffer.Handle.id == 0)
            {
                throw new ArgumentException(nameof(buffer));
            }
            
            if (bufferIndex > SG_MAX_SHADERSTAGE_BUFFERS)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferIndex), bufferIndex, null);
            }
            
            _bindings.vertex_buffers[bufferIndex] = buffer.Handle;
            _bindings.vertex_buffer_offsets[bufferIndex] = byteOffset;
        }

        public void SetIndexBuffer(SgBuffer buffer, int byteOffset = 0)
        {
            if (buffer.Handle.id == 0)
            {
                throw new ArgumentException(nameof(buffer));
            }
            
            _bindings.index_buffer = buffer.Handle;
            _bindings.index_buffer_offset = byteOffset;
        }

        public void Apply()
        {
            sg_apply_bindings(ref _bindings);
        }
    }
}