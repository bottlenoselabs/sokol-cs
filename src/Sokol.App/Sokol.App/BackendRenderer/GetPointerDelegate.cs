// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetPointerDelegate();
}
