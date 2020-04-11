// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.SDL2
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetPointerDelegate();
}
