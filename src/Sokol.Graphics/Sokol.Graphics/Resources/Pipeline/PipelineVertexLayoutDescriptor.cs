// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe how a vertex is fetched from an input vertex <see cref="Buffer" /> and unpacked
    ///     to the "per-vertex processing" stage of a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To apply the parameters, set <see cref="PipelineDescriptor.Layout"/> property before creating a
    ///         <see cref="Pipeline" />.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="PipelineVertexLayoutDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineVertexLayoutDescriptor" /> is blittable to the C `sg_layout_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 288, Pack = 4)]
    public unsafe struct PipelineVertexLayoutDescriptor
    {
        [FieldOffset(0)]
        internal fixed int _buffers[12 * sokol_gfx.SG_MAX_SHADERSTAGE_BUFFERS / 4];

        [FieldOffset(96)]
        internal fixed int _attrs[12 * sokol_gfx.SG_MAX_VERTEX_ATTRIBUTES / 4];

        /// <summary>
        ///     Gets the <see cref="PipelineVertexBufferLayoutDescriptor" />, by reference, given the specified slot or
        ///     index of the vertex <see cref="Buffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="Buffer" />.</param>
        /// <returns>A <see cref="PipelineVertexBufferLayoutDescriptor"/> by reference.</returns>
        public ref PipelineVertexBufferLayoutDescriptor Buffer(int index)
        {
            fixed (PipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var ptr = (PipelineVertexBufferLayoutDescriptor*)&layoutDescription->_buffers[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="PipelineVertexAttributeDescriptor" />, by reference, given the specified slot or
        ///     index of the vertex <see cref="Buffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="Buffer" />.</param>
        /// <returns>A <see cref="PipelineVertexBufferLayoutDescriptor"/> by reference.</returns>
        public ref PipelineVertexAttributeDescriptor Attribute(int index)
        {
            fixed (PipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var ptr = (PipelineVertexAttributeDescriptor*)&layoutDescription->_attrs[0];
                return ref *(ptr + index);
            }
        }
    }
}
