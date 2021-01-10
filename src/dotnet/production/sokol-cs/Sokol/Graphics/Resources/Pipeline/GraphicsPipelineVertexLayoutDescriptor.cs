// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     The parameters that describe how a vertex is fetched from a vertex <see cref="GraphicsBuffer" /> and unpacked to
    ///     the <see cref="GraphicsShaderStageType" /> stage of a <see cref="GraphicsShader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsPipelineVertexLayoutDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineVertexLayoutDescriptor" /> is blittable to the C `sg_layout_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 288, Pack = 4)]
    public readonly unsafe struct GraphicsPipelineVertexLayoutDescriptor
    {
        [FieldOffset(0)]
        private readonly int _buffers;

        [FieldOffset(96)]
        private readonly int _attrs;

        /// <summary>
        ///     Gets the <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" />, by reference, given the specified index of
        ///     the vertex <see cref="GraphicsBuffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="GraphicsBuffer" />.</param>
        /// <returns>A <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" /> by reference.</returns>
        public ref GraphicsPipelineVertexBufferLayoutDescriptor Buffer(int index)
        {
            fixed (GraphicsPipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var pointer = (GraphicsPipelineVertexBufferLayoutDescriptor*)&layoutDescription->_buffers;
                return ref *(pointer + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsPipelineVertexAttributeDescriptor" />, by reference, given the specified index of the vertex
        ///     <see cref="GraphicsBuffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="GraphicsBuffer" />.</param>
        /// <returns>A <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" /> by reference.</returns>
        public ref GraphicsPipelineVertexAttributeDescriptor Attribute(int index)
        {
            fixed (GraphicsPipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var pointer = (GraphicsPipelineVertexAttributeDescriptor*)&layoutDescription->_attrs;
                return ref *(pointer + index);
            }
        }
    }
}
