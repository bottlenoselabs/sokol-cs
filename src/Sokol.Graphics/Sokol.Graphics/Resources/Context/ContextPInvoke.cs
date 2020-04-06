// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "Internal usage")]
    public static class ContextPInvoke
    {
        [DllImport(Sg.LibraryName, EntryPoint = "sg_setup_context")]
        public static extern Context Create();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_activate_context")]
        public static extern void Activate(Context context);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_discard_context")]
        public static extern void Destroy(Context context);
    }
}
