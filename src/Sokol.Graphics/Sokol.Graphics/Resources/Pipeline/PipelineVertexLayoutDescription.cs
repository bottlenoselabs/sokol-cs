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
    ///         To apply the parameters, set <see cref="PipelineDescription.Layout"/> property before creating a
    ///         <see cref="Pipeline" />.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="PipelineVertexLayoutDescription" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineVertexLayoutDescription" /> is blittable to the C `sg_layout_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 288, Pack = 4)]
    public unsafe struct PipelineVertexLayoutDescription
    {
        [FieldOffset(0)]
        internal fixed int _buffers[12 * sokol_gfx.SG_MAX_SHADERSTAGE_BUFFERS / 4];

        [FieldOffset(96)]
        internal fixed int _attrs[12 * sokol_gfx.SG_MAX_VERTEX_ATTRIBUTES / 4];

        /// <summary>
        ///     Gets the <see cref="PipelineVertexBufferLayoutDescription" />, by reference, given the specified slot or
        ///     index of the vertex <see cref="Buffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="Buffer" />.</param>
        /// <returns>A <see cref="PipelineVertexBufferLayoutDescription"/> by reference.</returns>
        public ref PipelineVertexBufferLayoutDescription Buffer(int index)
        {
            fixed (PipelineVertexLayoutDescription* layoutDescription = &this)
            {
                var ptr = (PipelineVertexBufferLayoutDescription*)&layoutDescription->_buffers[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="PipelineVertexAttributeDescription" />, by reference, given the specified slot or
        ///     index of the vertex <see cref="Buffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="Buffer" />.</param>
        /// <returns>A <see cref="PipelineVertexBufferLayoutDescription"/> by reference.</returns>
        public ref PipelineVertexAttributeDescription Attribute(int index)
        {
            fixed (PipelineVertexLayoutDescription* layoutDescription = &this)
            {
                var ptr = (PipelineVertexAttributeDescription*)&layoutDescription->_attrs[0];
                return ref *(ptr + index);
            }
        }
    }
}
