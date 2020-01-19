using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

#pragma warning disable 414

namespace Sokol
{
    public unsafe struct AsciiString16
    {
        private readonly int _data0;
        private readonly int _data1;
        private readonly int _data2;
        private readonly int _data3;

        public AsciiString16(string @string)
        {
            _data0 = _data1 = _data2 = _data3 = 0;
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
                var ptr = &thisPtr->_data0;
                return (IntPtr) ptr;
            }
        }

        public static implicit operator AsciiString16(string @string)
        {
            return new AsciiString16(@string);
        }
        
        // NOTE: We need this struct to be immutable for the `in` keyword to pass the struct by reference here!
        //     If the struct is not immutable (is mutable), this will copy the struct by value (default behaviour)! 
        public static implicit operator IntPtr(in AsciiString16 @string)
        {
            return @string.GetPointer();
        }
    }
}