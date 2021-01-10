// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol
{
    /// <summary>
    ///     Defines the index data types of 3D topology primitive vertices which are used by a <see cref="GraphicsPipeline" />
    ///     to fetch index data from an index <see cref="GraphicsBuffer" /> for rasterization.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineVertexIndexType" /> is blittable to the C `sg_index_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Same meaning as name of built in type.")]
    public enum GraphicsPipelineVertexIndexType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineVertexIndexType" /> is <see cref="GraphicsPipelineVertexIndexType.None" />.
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
