// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the strategies of how a graphics resource will update data between the CPU and GPU.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Only one update is allowed per frame for each graphics resource. An attempt to update a graphics resource more
    ///         than once a frame will result in a graceful crash of the application.
    ///     </para>
    /// </remarks>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsResourceUsage" /> is blittable to the C `sg_usage` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsResourceUsage
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsResourceUsage" /> is <see cref="Immutable" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The graphics resource will never be updated with new data. Instead, the data of the resource must be provided
        ///     on initialization.
        /// </summary>
        Immutable,

        /// <summary>
        ///     The graphics resource will be updated with new data possibly more than once but not every frame.
        /// </summary>
        Dynamic,

        /// <summary>
        ///     The graphics resource will be updated once every frame.
        /// </summary>
        Stream
    }
}
