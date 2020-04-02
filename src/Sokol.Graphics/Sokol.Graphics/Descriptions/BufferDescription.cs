// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Buffer" />.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 72, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct BufferDescription
    {
        /// <summary>
        ///     The size in byte. Must be set.
        /// </summary>
        [FieldOffset(4)]
        public int Size;

        /// <summary>
        ///     The <see cref="BufferType" />. Default is <see cref="BufferType.Vertex" />.
        /// </summary>
        [FieldOffset(8)]
        public BufferType Type;

        /// <summary>
        ///     The <see cref="ResourceUsage" />. Default is <see cref="ResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(12)]
        public ResourceUsage Usage;

        /// <summary>
        ///     The pointer to the block of bytes to copy as the data when constructing a <see cref="Buffer" />. Default
        ///     is <see cref="IntPtr.Zero" />, a null pointer, which indicates to not copy data when constructing a
        ///     <see cref="Buffer" />. Must not be <see cref="IntPtr.Zero" /> if <see cref="Usage" /> is
        ///     <see cref="ResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr Data;

        // TODO: Trace hooks
        [FieldOffset(24)]
        internal IntPtr Label;

        // TODO: Native 3D Buffers.
        [FieldOffset(32)]
        internal fixed uint _gl_buffers[Sg.InflightFramesCount];

        // TODO: Native 3D Buffers.
        [FieldOffset(40)]
        internal fixed ulong _mtl_buffers[Sg.InflightFramesCount];

        // TODO: Native 3D Buffers.
        [FieldOffset(56)]
        internal IntPtr _d3d11_buffer;

        [FieldOffset(0)]
        internal uint _startCanary;

        [FieldOffset(64)]
        internal uint _end_canary;
    }
}
