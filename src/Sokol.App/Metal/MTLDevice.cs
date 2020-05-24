// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Metal
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    public readonly struct MTLDevice
    {
        public readonly IntPtr Handle;

        public MTLDevice(IntPtr handle)
        {
            Handle = handle;
        }

        public static implicit operator IntPtr(MTLDevice value)
        {
            return value.Handle;
        }
    }
}
