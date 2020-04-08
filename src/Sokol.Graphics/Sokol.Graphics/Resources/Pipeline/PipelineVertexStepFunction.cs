// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines whether the pointer of a vertex input stream is advanced "per vertex" or "per instance". Use
    ///     <see cref="PipelineVertexStepFunction" /> to disable or enable instancing.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineVertexStepFunction" /> is blittable to the C `sg_vertex_step` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineVertexStepFunction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineVertexStepFunction" /> is <see cref="PerVertex" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Instancing is disabled.
        /// </summary>
        PerVertex,

        /// <summary>
        ///     Instancing is enabled.
        /// </summary>
        PerInstance
    }
}
