// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe how a vertex attribute data is stored in GPU memory inside a
    ///     <see cref="Buffer" /> and which active <see cref="Buffer" /> the vertex attribute data comes from.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To apply the parameters, get a <see cref="PipelineVertexAttributeDescriptor"/> by reference by calling
    ///         the <see cref="PipelineVertexLayoutDescriptor.Attribute" /> method and set the result before creating a
    ///         <see cref="Pipeline" />.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="PipelineVertexAttributeDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineVertexAttributeDescriptor" /> is blittable to the C `sg_vertex_attr_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct PipelineVertexAttributeDescriptor
    {
        /// <summary>
        ///     The vertex <see cref="Buffer" /> slot or index to use. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(0)]
        public int BufferIndex;

        /// <summary>
        ///     The offset in bytes to this particular attribute of a vertex. Default is <c>0</c>. Can be left as
        ///     <c>0</c> if the vertex layout has no gaps.
        /// </summary>
        [FieldOffset(4)]
        public int Offset;

        /// <summary>
        ///     The <see cref="PipelineVertexAttributeFormat" /> for this particular attribute of a vertex.
        /// </summary>
        [FieldOffset(8)]
        public PipelineVertexAttributeFormat Format;
    }
}
