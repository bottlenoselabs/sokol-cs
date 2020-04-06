// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public static class TraceHooksPInvoke
    {
        // TODO: Document trace hooks.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_install_trace_hooks")]
        public static extern TraceHooks InstallTraceHooks(ref TraceHooks traceHooks);

        // TODO: Document trace hooks.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_push_debug_group")]
        public static extern void PushDebugGroup(IntPtr name);

        // TODO: Document trace hooks.
        [DllImport(Sg.LibraryName, EntryPoint = "sg_pop_debug_group")]
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public static extern void PopDebugGroup();
    }
}
