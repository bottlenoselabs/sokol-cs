// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ObjCRuntime
{
    internal readonly unsafe struct Selector
    {
        private readonly IntPtr _handle;

        private Selector(string name)
        {
            var byteCount = Encoding.UTF8.GetMaxByteCount(name.Length);
            var utf8BytesPtr = stackalloc byte[byteCount];
            fixed (char* namePtr = name)
            {
                Encoding.UTF8.GetBytes(namePtr, name.Length, utf8BytesPtr, byteCount);
            }

            _handle = sel_registerName(utf8BytesPtr);
        }

        public static implicit operator Selector(string @string)
        {
            return new Selector(@string);
        }

        public static implicit operator IntPtr(Selector value)
        {
            return value._handle;
        }

        [DllImport(Constants.ObjCLibrary)]
        private static extern IntPtr sel_registerName(byte* namePtr);
    }
}
