// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds vertex or index data to be used as input to a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="Buffer" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Buffer" /> is blittable to the C `sg_buffer` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly partial struct Buffer
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="Buffer" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="Buffer" /> has had too much data appended
        ///     to it this frame.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> indicating whether <see cref="Buffer" /> has had too much data appended
        ///     to it this frame.
        /// </value>
        public bool IsOverflown => QueryOverflow(this);

        // TODO: Document `BufferInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public BufferInfo Info => QueryInfo(this);

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State => QueryState(this);

        /// <summary>
        ///     Destroys the <see cref="Buffer" />.
        /// </summary>
        public void Destroy()
        {
            Destroy(this);
        }

        // TODO: Document manual initialization of a buffer.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public void Init([In] ref BufferDescription description)
        {
            Init(this, ref description);
        }

        // TODO: Document failing a buffer.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public void Fail()
        {
            Fail(this);
        }

        /// <summary>
        ///     Overwrites the contents of the <see cref="Buffer" /> by copying memory. The <see cref="Buffer" /> must
        ///     have been created with <see cref="ResourceUsage.Dynamic" /> or <see cref="ResourceUsage.Stream" />.
        /// </summary>
        /// <param name="dataPointer">A pointer to the starting address of a block of bytes to copy data.</param>
        /// <param name="dataSize">The number of bytes to copy.</param>
        public void Update(IntPtr dataPointer, int dataSize)
        {
            Update(this, dataPointer, dataSize);
        }

        /// <summary>
        ///     Overwrites the contents of the <see cref="Buffer" /> by copying memory. The <see cref="Buffer" /> must
        ///     have been created with <see cref="ResourceUsage.Dynamic" /> or <see cref="ResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The region of memory to copy.</param>
        /// <param name="count">The optional amount of elements to copy. Use <c>null</c> to copy all the elements.</param>
        /// <typeparam name="T">The type of elements to copy into the buffer.</typeparam>
        public unsafe void Update<T>(Memory<T> data, int? count = null)
            where T : unmanaged
        {
            var dataHandle = data.Pin();
            var dataLength = count ?? data.Length;
            var dataSize = Marshal.SizeOf<T>() * dataLength;

            Update(this, (IntPtr)dataHandle.Pointer, dataSize);

            dataHandle.Dispose();
        }

        /// <summary>
        ///     Appends to the contents of the <see cref="Buffer" /> by copying memory. The <see cref="Buffer" /> must
        ///     have been created with <see cref="ResourceUsage.Dynamic" /> or <see cref="ResourceUsage.Stream" />.
        /// </summary>
        /// <param name="dataPointer">A pointer to the starting address of a block of bytes to copy data.</param>
        /// <param name="dataSize">The number of bytes to copy.</param>
        /// <returns>
        ///     A byte offset to the start of the written data. This can be applied to
        ///     <see cref="SgBindings.VertexBufferOffset" /> or <see cref="SgBindings.IndexBufferOffset" /> to render
        ///     a portion of the buffer.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The difference between <see cref="Update" /> and <see cref="Append" /> is that,
        ///         <see cref="Append" /> can be called multiple times per frame to append new data to buffer
        ///         incrementally. It can even be interleaved with draw calls referencing the previously appended data.
        ///     </para>
        ///     <para>
        ///         If the application appends more data than can fit into the <see cref="Buffer" />, the
        ///         <see cref="Buffer" /> will go into a overflow state for the rest of the frame. Any draw calls
        ///         attempting to render an overflown buffer will be dropped and a validation error will appear if
        ///         validation is enabled. To manually check if the buffer is in an overflow state, call
        ///         <see cref="QueryState" />.
        ///     </para>
        /// </remarks>
        public int Append(IntPtr dataPointer, int dataSize)
        {
            return Append(this, dataPointer, dataSize);
        }

        /// <summary>
        ///     Appends to the contents of the <see cref="Buffer" /> by copying memory. The <see cref="Buffer" /> must
        ///     have been created with <see cref="ResourceUsage.Dynamic" /> or <see cref="ResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The region of memory to copy.</param>
        /// <param name="count">The optional amount of elements to copy. Use <c>null</c> to copy all the elements.</param>
        /// <typeparam name="T">The type of elements to copy into the buffer.</typeparam>
        /// <returns>
        ///     A byte offset to the start of the written data. This can be applied to
        ///     <see cref="SgBindings.VertexBufferOffset" /> or <see cref="SgBindings.IndexBufferOffset" /> to render
        ///     a portion of the buffer.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The difference between <see cref="Update" /> and <see cref="Append" /> is that,
        ///         <see cref="Append" /> can be called multiple times per frame to append new data to buffer
        ///         incrementally. It can even be interleaved with draw calls referencing the previously appended data.
        ///     </para>
        ///     <para>
        ///         If the application appends more data than can fit into the <see cref="Buffer" />, the
        ///         <see cref="Buffer" /> will go into a overflow state for the rest of the frame. Any draw calls
        ///         attempting to render an overflown buffer will be dropped and a validation error will appear if
        ///         validation is enabled. To manually check if the buffer is in an overflow state, call
        ///         <see cref="QueryState" />.
        ///     </para>
        /// </remarks>
        public unsafe int Append<T>(Memory<T> data, int? count = null)
            where T : unmanaged
        {
            var dataHandle = data.Pin();
            var dataLength = count ?? data.Length;
            var dataSize = Marshal.SizeOf<T>() * dataLength;

            var result = Append(this, (IntPtr)dataHandle.Pointer, dataSize);

            dataHandle.Dispose();

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
