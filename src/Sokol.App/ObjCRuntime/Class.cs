// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using static ObjCRuntime.Messaging;

namespace ObjCRuntime
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1311", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    internal readonly unsafe struct Class
    {
        private readonly IntPtr _handle;

        public Class(string name)
        {
            var byteCount = Encoding.UTF8.GetMaxByteCount(name.Length);
            var utf8BytesPtr = stackalloc byte[byteCount];
            fixed (char* namePtr = name)
            {
                Encoding.UTF8.GetBytes(namePtr, name.Length, utf8BytesPtr, byteCount);
            }

            _handle = objc_getClass(utf8BytesPtr);
        }

        public T New<T>()
            where T : unmanaged
        {
            var value = intptr_objc_msgSend(_handle, sel_new);
            return Unsafe.AsRef<T>(&value);
        }

        public static implicit operator IntPtr(Class value)
        {
            return value._handle;
        }

        private static readonly Selector sel_new = "new";

        [DllImport(Constants.ObjCLibrary)]
        private static extern IntPtr objc_getClass(byte* namePtr);
    }
}
