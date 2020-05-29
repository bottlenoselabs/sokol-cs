// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.App
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void AppCallbackDelegateVoid();
}
