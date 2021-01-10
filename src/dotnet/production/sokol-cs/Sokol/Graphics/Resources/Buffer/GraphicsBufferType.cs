// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines how the data in <see cref="GraphicsBuffer" /> is used.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsBufferType" /> is blittable to the C `sg_buffer_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsBufferType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsBufferType" /> is <see cref="Vertex" />.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     The data is used for vertices or instances.
        /// </summary>
        Vertex = 1,

        /// <summary>
        ///     The data is used for indices.
        /// </summary>
        Index = 2
    }
}
