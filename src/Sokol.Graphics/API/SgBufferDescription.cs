using System;
using System.Buffers;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

#pragma warning disable 649

namespace Sokol
{
    public struct SgBufferDescription
    {
        internal sg_buffer_desc desc;
        internal MemoryHandle? DataHandle;
        
        private string _name;
        
        public SgBufferType Type
        {
            get
            {
                if (desc.type == sg_buffer_type._SG_BUFFERTYPE_DEFAULT)
                {
                    desc.type = sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER;
                }
                return (SgBufferType) (desc.type - 1);
            }
            set
            {
                // ReSharper disable once ConvertSwitchStatementToSwitchExpression
                switch (value)
                {
                    case SgBufferType.Index:
                    case SgBufferType.Vertex:
                        desc.type = (sg_buffer_type) (value + 1);
                        break;
                    default:
                        desc.type = sg_buffer_type._SG_BUFFERTYPE_DEFAULT;
                        break;
                }
            }
        }

        public SgBufferUsage Usage
        {
            get
            {
                if (desc.usage == sg_usage._SG_USAGE_DEFAULT)
                {
                    desc.usage = sg_usage.SG_USAGE_IMMUTABLE;
                }

                return (SgBufferUsage) (desc.usage - 1);
            }
            set
            {
                // ReSharper disable once ConvertSwitchStatementToSwitchExpression
                switch (value)
                {
                    case SgBufferUsage.Immutable:
                    case SgBufferUsage.Dynamic:
                    case SgBufferUsage.Stream:
                        desc.usage = (sg_usage) (value + 1);
                        break;
                    default:
                        desc.usage = sg_usage._SG_USAGE_DEFAULT;
                        break;
                }
            }
        }

        public int DataSize
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

        public unsafe IntPtr DataPointer
        {
            get => (IntPtr) desc.content;
            set => desc.content = (void*) value;
        }

        public unsafe string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                
                if (!string.IsNullOrEmpty(_name))
                {
                    Marshal.FreeHGlobal((IntPtr) desc.label);
                }

                desc.label = (byte*) Marshal.StringToHGlobalAnsi(value);
            }
        }

        public unsafe void SetData<T>(Memory<T> data) where T : unmanaged
        {
            DataSize = Marshal.SizeOf<T>() * data.Length;
            DataHandle?.Dispose();
            DataHandle = data.Pin();
            desc.content = DataHandle.Value.Pointer;
        }

        internal unsafe IntPtr NameCPointer => (IntPtr) desc.label;
    }
}