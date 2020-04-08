// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe how vertex data is fetched from a <see cref="Buffer" /> and mapped into
    ///     the "per-vertex processing" stage of a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To apply the parameters, get a <see cref="PipelineVertexBufferLayoutDescription" /> by reference by
    ///         calling the <see cref="PipelineVertexLayoutDescription.Buffer" /> method and set the result before
    ///         creating a <see cref="Pipeline" />.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create a
    ///         <see cref="PipelineVertexBufferLayoutDescription" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineVertexBufferLayoutDescription" /> is blittable to the C `sg_vertex_attr_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct PipelineVertexBufferLayoutDescription
    {
        /// <summary>
        ///     The stride of each vertex. Default is <c>0</c>. If <c>0</c>, the stride will be automatically computed.
        /// </summary>
        [FieldOffset(0)]
        public int Stride;

        /// <summary>
        ///     The <see cref="PipelineVertexStepFunction" />.
        /// </summary>
        [FieldOffset(4)]
        public PipelineVertexStepFunction StepFunction;

        /// <summary>
        ///     The number of instances to draw using the same per-instance data before advancing the
        ///     <see cref="Buffer" /> by one element. Default is <c>1</c>. Ignored if <see cref="StepFunction" /> is
        ///     <see cref="PipelineVertexStepFunction.PerVertex" />. If the value is equal to <c>1</c>, new data is
        ///     fetched for every instance; if the value is equal to <c>2</c>, new data is fetched for every two
        ///     instances, and so forth.
        /// </summary>
        [FieldOffset(8)]
        public int StepRate;
    }
}
