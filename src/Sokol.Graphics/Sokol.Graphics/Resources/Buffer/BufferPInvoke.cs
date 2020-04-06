// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "Internal usage")]
    public static class BufferPInvoke
    {
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_defaults")]
        public static extern BufferDescription QueryDefaults([In] ref BufferDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_buffer")]
        public static extern Buffer Alloc();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_buffer")]
        public static extern Buffer Create([In] ref BufferDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_buffer")]
        public static extern void Init(Buffer buffer, [In] ref BufferDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_buffer")]
        public static extern void Destroy(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_buffer")]
        public static extern void Fail(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_update_buffer")]
        public static extern void Update(Buffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_append_buffer")]
        public static extern int Append(Buffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_overflow")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool QueryOverflow(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_state")]
        public static extern ResourceState QueryState(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_info")]
        public static extern BufferInfo QueryInfo(Buffer buffer);
    }
}
