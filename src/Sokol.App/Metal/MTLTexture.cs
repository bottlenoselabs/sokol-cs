// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Metal
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MTLTexture
    {
        private readonly IntPtr _handle;

        public static implicit operator IntPtr(MTLTexture value)
        {
            return value._handle;
        }
    }
}
