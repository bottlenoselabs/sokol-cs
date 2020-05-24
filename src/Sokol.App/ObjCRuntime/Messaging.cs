// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CoreGraphics;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace ObjCRuntime
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    internal static unsafe class Messaging
    {
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_bool(IntPtr receiver, Selector selector, BlittableBoolean arg1);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_intptr(IntPtr receiver, Selector selector, IntPtr arg1);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_intptr_nsuinteger(
            IntPtr receiver,
            Selector selector,
            IntPtr arg1,
            UIntPtr arg2);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern BlittableBoolean bool_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern IntPtr intptr_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern IntPtr intptr_objc_msgSend_nsuinteger(IntPtr receiver, Selector selector, UIntPtr arg1);

        public static T t_objc_msgSend<T>(IntPtr receiver, Selector selector)
            where T : unmanaged
        {
            var value = intptr_objc_msgSend(receiver, selector);
            return Unsafe.AsRef<T>(&value);
        }

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern CGSize cgsize_objc_msgSend(IntPtr receiver, Selector selector);
    }
}
