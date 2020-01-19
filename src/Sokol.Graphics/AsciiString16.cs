using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 1)]
    public unsafe struct AsciiString16
    {
        [FieldOffset(0)] private readonly byte Data;

        public AsciiString16(string @string)
        {
            Data = 0;
            var stringLength = @string.Length > 16 ? 16 : @string.Length;
            var stringSpan = @string.AsSpan().Slice(0, stringLength);
            var ptr = (void*) GetPointer();
            var dataSpan = new Span<byte>(ptr, 16);
            Encoding.ASCII.GetBytes(stringSpan, dataSpan);
        }

        private readonly IntPtr GetPointer()
        {
            fixed (AsciiString16* thisPtr = &this)
            {
                var ptr = &thisPtr->Data;
                return (IntPtr) ptr;
            }
        }

        public static implicit operator AsciiString16(string @string)
        {
            return new AsciiString16(@string);
        }
        
        // NOTE: We need this struct to be immutable for the `in` keyword to pass the struct by reference here!
        public static implicit operator IntPtr(in AsciiString16 @string)
        {
            return @string.GetPointer();
        }
    }
}