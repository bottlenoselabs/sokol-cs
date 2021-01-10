// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     A GPU resource that holds vertex or vertex-index data to be used by a <see cref="GraphicsPipeline" /> to input data
    ///     to the <see cref="GraphicsShaderStageType.Vertex" /> stage of a <see cref="GraphicsShader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsBuffer" />, call <see cref="Graphics.MakeBuffer" />. To
    ///         allocate a <see cref="GraphicsBuffer" /> and initialize it later, call <see cref="Graphics.MakeBuffer" /> to get
    ///         an un-initialized <see cref="GraphicsBuffer" /> and then call <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsBuffer" /> must only be used or destroyed with the same active
    ///         <see cref="GraphicsContext" /> that was also active when the resource was initialized.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsBuffer" /> is blittable to the C `sg_buffer` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsBuffer
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsBuffer" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="GraphicsBuffer" /> has had too much data appended
        ///     to it this frame.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the <see cref="GraphicsBuffer" /> has had too much data appended
        ///     to it this frame; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverflown => GraphicsPInvoke.sg_query_buffer_overflow(this);

        /// <summary>
        ///    Gets TODO.
        /// </summary>
        public GraphicsBufferInfo Info => GraphicsPInvoke.sg_query_buffer_info(this);

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsResourceState State => GraphicsPInvoke.sg_query_buffer_state(this);

        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="GraphicsBufferDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a <see cref="GraphicsBuffer" />.</param>
        /// <returns>A <see cref="GraphicsBufferDescriptor" /> with any zero-initialized members set to default values.</returns>
        public static GraphicsBufferDescriptor QueryDefaults(ref GraphicsBufferDescriptor descriptor) =>
            GraphicsPInvoke.sg_query_buffer_defaults(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <param name="descriptor">.</param>
        public void Initialize(ref GraphicsBufferDescriptor descriptor) => GraphicsPInvoke.sg_init_buffer(this, ref descriptor);

        /// <summary>
        ///     Destroys the <see cref="GraphicsBuffer" />.
        /// </summary>
        public void Destroy() => GraphicsPInvoke.sg_destroy_buffer(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Fail() => GraphicsPInvoke.sg_fail_buffer(this);

        /// <summary>
        ///     Overwrites the contents of the <see cref="GraphicsBuffer" /> by copying the specified pointed memory. The
        ///     <see cref="GraphicsBuffer" /> must have been created with <see cref="GraphicsResourceUsage.Dynamic" /> or
        ///     <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="dataPointer">A pointer to the starting address of a block of bytes to copy data.</param>
        /// <param name="dataSize">The number of bytes to copy.</param>
        public void Update(IntPtr dataPointer, int dataSize) => GraphicsPInvoke.sg_update_buffer(this, dataPointer, dataSize);

        /// <summary>
        ///     Overwrites the contents of the <see cref="GraphicsBuffer" /> by copying the pointed memory in the specified
        ///     <see cref="Span{T}" /> that is unmanaged or externally pinned. The <see cref="GraphicsBuffer" /> must have been
        ///     created with <see cref="GraphicsResourceUsage.Dynamic" /> or <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The block of memory to copy.</param>
        /// <typeparam name="T">The type of elements to copy into the buffer.</typeparam>
        public unsafe void Update<T>(Span<T> data)
            where T : unmanaged
        {
            ref var dataReference = ref MemoryMarshal.GetReference(data);
            var dataPointer = (IntPtr)Unsafe.AsPointer(ref dataReference);
            var dataSize = Marshal.SizeOf<T>() * data.Length;

            GraphicsPInvoke.sg_update_buffer(this, dataPointer, dataSize);
        }

        /// <summary>
        ///     Appends to the contents of the <see cref="GraphicsBuffer" /> by copying the specified pointed memory. The
        ///     <see cref="GraphicsBuffer" /> must have been created with <see cref="GraphicsResourceUsage.Dynamic" /> or
        ///     <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="dataPointer">A pointer to the starting address of a block of bytes to copy data.</param>
        /// <param name="dataSize">The number of bytes to copy.</param>
        /// <returns>
        ///     A byte offset to the start of the written data. This can be applied to
        ///     <see cref="GraphicsResourceBindings.VertexBufferOffset" /> or
        ///     <see cref="GraphicsResourceBindings.IndexBufferOffset" /> to render
        ///     a portion of the buffer.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The difference between <see cref="Update" /> and <see cref="Append" /> is that,
        ///         <see cref="Append" /> can be called multiple times per frame to append new data to buffer
        ///         incrementally. It can even be interleaved with draw calls referencing the previously appended data.
        ///     </para>
        ///     <para>
        ///         If the application appends more data than can fit into the <see cref="GraphicsBuffer" />, the
        ///         <see cref="GraphicsBuffer" /> will go into a overflow state for the rest of the frame. Any
        ///         <see cref="GraphicsPass.DrawElements" /> attempting to render an overflown buffer will be dropped and a
        ///         validation error will appear if validation is enabled. To manually check if the buffer is in an overflow state,
        ///         inspect the results of <see cref="State" />.
        ///     </para>
        /// </remarks>
        public int Append(IntPtr dataPointer, int dataSize) => GraphicsPInvoke.sg_append_buffer(this, dataPointer, dataSize);

        /// <summary>
        ///     Appends to the contents of the <see cref="GraphicsBuffer" /> by copying the pointed memory in the specified
        ///     <see cref="Span{T}" /> that is unmanaged or externally pinned. The <see cref="GraphicsBuffer" /> must have been
        ///     created with <see cref="GraphicsResourceUsage.Dynamic" /> or <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The region of memory to copy.</param>
        /// <typeparam name="T">The type of elements to copy into the buffer.</typeparam>
        /// <returns>
        ///     A byte offset to the start of the written data. This can be applied to
        ///     <see cref="GraphicsResourceBindings.VertexBufferOffset" /> or
        ///     <see cref="GraphicsResourceBindings.IndexBufferOffset" /> to render
        ///     a portion of the buffer.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The difference between <see cref="Update" /> and <see cref="Append" /> is that,
        ///         <see cref="Append" /> can be called multiple times per frame to append new data to buffer
        ///         incrementally. It can even be interleaved with draw calls referencing the previously appended data.
        ///     </para>
        ///     <para>
        ///         If the application appends more data than can fit into the <see cref="GraphicsBuffer" />, the
        ///         <see cref="GraphicsBuffer" /> will go into a overflow state for the rest of the frame. Any
        ///         <see cref="GraphicsPass.DrawElements" /> attempting to render an overflown buffer will be dropped and a
        ///         validation error will appear if validation is enabled. To manually check if the buffer is in an overflow state,
        ///         inspect the results of <see cref="State" />.
        ///     </para>
        /// </remarks>
        public unsafe int Append<T>(Span<T> data)
            where T : unmanaged
        {
            ref var dataReference = ref MemoryMarshal.GetReference(data);
            var dataPointer = (IntPtr)Unsafe.AsPointer(ref dataReference);
            var dataSize = Marshal.SizeOf<T>() * data.Length;

            return GraphicsPInvoke.sg_append_buffer(this, dataPointer, dataSize);
        }

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
