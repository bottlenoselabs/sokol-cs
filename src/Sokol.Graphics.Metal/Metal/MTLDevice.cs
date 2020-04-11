// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Metal
{
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
