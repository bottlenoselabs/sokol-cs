// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static ObjCRuntime.Messaging;

// ReSharper disable InconsistentNaming

namespace ObjCRuntime
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1311", Justification = "PInvoke.")]
    internal static class NSArray
    {
        public static unsafe T objectAtIndexedSubscript<T>(IntPtr receiver, UIntPtr index)
            where T : unmanaged
        {
            var value = intptr_objc_msgSend_nsuinteger(receiver, sel_objectAtIndexedSubscript, index);
            return Unsafe.AsRef<T>(&value);
        }

        public static void setObjectAtIndexedSubscript(IntPtr receiver, IntPtr value, UIntPtr index)
        {
            void_objc_msgSend_intptr_nsuinteger(receiver, sel_setObjectAtIndexedSubscript, value, index);
        }

        private static readonly Selector sel_objectAtIndexedSubscript = "objectAtIndexedSubscript:";
        private static readonly Selector sel_setObjectAtIndexedSubscript = "setObject:atIndexedSubscript:";
    }
}
