// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
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
    public unsafe struct GraphicsPipelineVertexLayoutDescriptor
    {
        [FieldOffset(0)]
        private fixed int _buffers[12 * GraphicsConstants.MaximumShaderStageBuffers / 4];

        [FieldOffset(96)]
        private fixed int _attrs[12 * GraphicsConstants.MaximumVertexAttributes / 4];

        /// <summary>
        ///     Gets the <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" />, by reference, given the specified index of
        ///     the vertex <see cref="GraphicsBuffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="GraphicsBuffer" />.</param>
        /// <returns>A <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" /> by reference.</returns>
        public readonly ref GraphicsPipelineVertexBufferLayoutDescriptor Buffer(int index = 0)
        {
            fixed (GraphicsPipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var ptr = (GraphicsPipelineVertexBufferLayoutDescriptor*)&layoutDescription->_buffers[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsPipelineVertexAttributeDescriptor" />, by reference, given the specified index of the vertex
        ///     <see cref="GraphicsBuffer" />.
        /// </summary>
        /// <param name="index">The zero-based index of the vertex <see cref="GraphicsBuffer" />.</param>
        /// <returns>A <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" /> by reference.</returns>
        public readonly ref GraphicsPipelineVertexAttributeDescriptor Attribute(int index = 0)
        {
            fixed (GraphicsPipelineVertexLayoutDescriptor* layoutDescription = &this)
            {
                var ptr = (GraphicsPipelineVertexAttributeDescriptor*)&layoutDescription->_attrs[0];
                return ref *(ptr + index);
            }
        }
    }
}
