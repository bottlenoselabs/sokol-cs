// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe how a vertex attribute data is encoded inside a <see cref="GraphicsBuffer" /> and
    ///     which active <see cref="GraphicsBuffer" /> the vertex attribute data comes from.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsPipelineVertexAttributeDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineVertexAttributeDescriptor" /> is blittable to the C `sg_vertex_attr_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsPipelineVertexAttributeDescriptor
    {
        /// <summary>
        ///     The vertex <see cref="GraphicsBuffer" /> index to use. Default is <c>0</c>.
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
