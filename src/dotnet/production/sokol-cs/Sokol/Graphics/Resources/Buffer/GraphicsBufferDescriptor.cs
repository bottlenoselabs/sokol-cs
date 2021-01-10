// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Parameters for constructing a <see cref="GraphicsBuffer" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create a
    ///         <see cref="GraphicsBufferDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsBufferDescriptor" /> is blittable to the C `sg_buffer_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 80, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsBufferDescriptor
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     The size of the buffer in bytes. Must be set. Can't be <c>0</c> or negative.
        /// </summary>
        [FieldOffset(4)]
        public uint Size;

        /// <summary>
        ///     The type of buffer. Default is <see cref="GraphicsBufferType.Vertex" />.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsBufferType Type;

        /// <summary>
        ///     The <see cref="GraphicsResourceUsage" /> of the buffer. Default is <see cref="GraphicsResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(12)]
        public GraphicsResourceUsage Usage;

        /// <summary>
        ///     The pointer to the starting address of the block of bytes to copy as the data when initializing the
        ///     <see cref="GraphicsBuffer" />. Default is <see cref="IntPtr.Zero" />, a null pointer, which indicates to not
        ///     copy data when initializing a <see cref="GraphicsBuffer" />. Must not be <see cref="IntPtr.Zero" /> if
        ///     <see cref="Usage" /> is <see cref="GraphicsResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr Data;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(24)]
        internal IntPtr Label;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(32)]
        internal fixed uint _gl_buffers[GraphicsConstants.InflightFramesCount];

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(40)]
        internal fixed ulong _mtl_buffers[GraphicsConstants.InflightFramesCount];

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(56)]
        internal IntPtr _d3d11_buffer;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(64)]
        internal IntPtr _wgpu_buffer;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(72)]
        private readonly uint _end_canary;

        /// <summary>
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields given the pointed memory of the specified
        ///     <see cref="Span{T}" /> that is assumed to be already unmanaged or externally pinned.
        /// </summary>
        /// <param name="data">The block of memory.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void SetData<T>(Span<T> data)
        {
            ref var reference = ref MemoryMarshal.GetReference(data);
            Data = (IntPtr)Unsafe.AsPointer(ref reference);
            Size = (uint)(Marshal.SizeOf<T>() * data.Length);
        }

        /// <summary>
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields given the pointed memory of the specified
        ///     <see cref="Memory{T}" />.
        /// </summary>
        /// <param name="data">The block of memory.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void SetData<T>(Memory<T> data) => SetData(data.Span);
    }
}
