// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Buffer" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="BufferDescription" /> is blittable to the C `sg_buffer_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 72, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct BufferDescription
    {
        /// <summary>
        ///     The size of the buffer in bytes. Must be set. Can't be <c>0</c> or negative.
        /// </summary>
        [FieldOffset(4)]
        public int Size;

        /// <summary>
        ///     The <see cref="BufferType" /> of the buffer. Default is <see cref="BufferType.Vertex" />.
        /// </summary>
        [FieldOffset(8)]
        public BufferType Type;

        /// <summary>
        ///     The <see cref="ResourceUsage" /> of the buffer. Default is <see cref="ResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(12)]
        public ResourceUsage Usage;

        /// <summary>
        ///     The pointer to the starting address of the block of bytes to copy as the data when constructing a
        ///     <see cref="Buffer" />. Default is <see cref="IntPtr.Zero" />, a null pointer, which indicates to not
        ///     copy data when constructing a <see cref="Buffer" />. Must not be <see cref="IntPtr.Zero" /> if
        ///     <see cref="Usage" /> is <see cref="ResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr Data;

        // TODO: Trace hooks.
        [FieldOffset(24)]
        internal IntPtr Label;

        // TODO: Native 3D Buffers.
        [FieldOffset(32)]
        internal fixed uint _gl_buffers[sokol_gfx.SG_NUM_INFLIGHT_FRAMES];

        // TODO: Native 3D Buffers.
        [FieldOffset(40)]
        internal fixed ulong _mtl_buffers[sokol_gfx.SG_NUM_INFLIGHT_FRAMES];

        // TODO: Native 3D Buffers.
        [FieldOffset(56)]
        internal IntPtr _d3d11_buffer;

        [FieldOffset(0)]
        internal uint _startCanary;

        [FieldOffset(64)]
        internal uint _end_canary;
    }
}
