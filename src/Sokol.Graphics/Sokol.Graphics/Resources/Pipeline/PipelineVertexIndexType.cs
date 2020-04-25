// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the index data types of 3D topology primitive vertices which are used by a <see cref="Pipeline" />
    ///     to fetch index data from an index <see cref="Buffer"/> for rasterization.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineVertexIndexType" /> is blittable to the C `sg_index_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineVertexIndexType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineVertexIndexType" /> is <see cref="PipelineVertexIndexType.None" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Indexing of vertex data is not used.
        /// </summary>
        None,

        /// <summary>
        ///     Each vertex index is a un-signed 16-bit integer.
        /// </summary>
        UInt16,

        /// <summary>
        ///     Each vertex index is a un-signed 32-bit integer.
        /// </summary>
        UInt32
    }
}
