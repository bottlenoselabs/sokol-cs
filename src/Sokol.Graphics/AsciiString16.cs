using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Sokol
{
    public unsafe struct AsciiString16
    {
        public fixed byte Data[16];

        public AsciiString16(string @string)
        {
            var stringLength = @string.Length > 16 ? 16 : @string.Length;
            var stringSpan = @string.AsSpan().Slice(0, stringLength);
            var ptr = (void*) AsPointer();
            var dataSpan = new Span<byte>(ptr, 16);
            Encoding.ASCII.GetBytes(stringSpan, dataSpan);
        }

        public IntPtr AsPointer()
        {
            fixed (AsciiString16* thisPtr = &this)
            {
                var ptr = &thisPtr->Data[0];
                return (IntPtr) ptr;
            }
        }

        public static implicit operator AsciiString16(string @string)
        {
            return new AsciiString16(@string);
        }
        
        public static implicit operator IntPtr(AsciiString16 @string)
        {
            return @string.AsPointer();
        }
    }
}