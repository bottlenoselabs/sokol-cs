// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    public readonly partial struct Buffer
    {
        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="BufferDescription"/> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating a buffer.</param>
        /// <returns>A <see cref="BufferDescription"/> with any zero-initialized members set to default values.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_defaults")]
        public static extern BufferDescription QueryDefaults([In] ref BufferDescription description);

        // TODO: Document allocating a buffer
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_buffer")]
        public static extern Buffer Alloc();

        /// <summary>
        ///     Creates a <see cref="Buffer" /> from the specified <see cref="BufferDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating a buffer.</param>
        /// <returns>A <see cref="Buffer" />.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_buffer")]
        public static extern Buffer Create([In] ref BufferDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_buffer")]
        private static extern void Destroy(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_buffer")]
        private static extern void Init(Buffer buffer, [In] ref BufferDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_buffer")]
        private static extern void Fail(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_update_buffer")]
        private static extern void Update(Buffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_append_buffer")]
        private static extern int Append(Buffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_overflow")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool QueryOverflow(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_state")]
        private static extern ResourceState QueryState(Buffer buffer);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_buffer_info")]
        private static extern BufferInfo QueryInfo(Buffer buffer);
    }
}
