// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using static ObjCRuntime.Messaging;

// ReSharper disable InconsistentNaming

namespace ObjCRuntime
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1311", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    public static class NSObject
    {
        public static void release(IntPtr receiver)
        {
            void_objc_msgSend(receiver, sel_release);
        }

        private static readonly Selector sel_release = "release";
    }
}
