// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe how vertex data is fetched from a <see cref="GraphicsBuffer" /> and mapped into
    ///     the <see cref="GraphicsShaderStageType.Vertex" /> stage of a <see cref="GraphicsShader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create a
    ///         <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineVertexBufferLayoutDescriptor" /> is blittable to the C `sg_buffer_layout_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsPipelineVertexBufferLayoutDescriptor
    {
        /// <summary>
        ///     The stride of each vertex. Default is <c>0</c>. If <c>0</c>, the stride will be automatically computed.
        /// </summary>
        [FieldOffset(0)]
        public int Stride;

        /// <summary>
        ///     The <see cref="PipelineVertexStepFunction" />. Default is <see cref="PipelineVertexStepFunction.PerVertex" />.
        /// </summary>
        [FieldOffset(4)]
        public PipelineVertexStepFunction StepFunction;

        /// <summary>
        ///     The number of instances to draw using the same per-instance data before advancing the
        ///     <see cref="GraphicsBuffer" /> by one element. Default is <c>1</c>. Ignored if <see cref="StepFunction" /> is
        ///     <see cref="PipelineVertexStepFunction.PerVertex" />. If the value is equal to <c>1</c>, new data is
        ///     fetched for every instance; if the value is equal to <c>2</c>, new data is fetched for every two
        ///     instances, and so forth.
        /// </summary>
        [FieldOffset(8)]
        public int StepRate;
    }
}
