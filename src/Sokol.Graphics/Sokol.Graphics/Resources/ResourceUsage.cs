// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the strategies of how a resource will be updated with data from the CPU to GPU.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Only one update is allowed per frame for each resource. An attempt to update a resource more than once
    ///         a frame will result in a crash of the application.
    ///     </para>
    ///     <para>
    ///         When updating a resource, all the data required for rendering must be included. This means that the
    ///         data can be smaller than the resource size but only if that data is sufficient for rendering.
    ///     </para>
    /// </remarks>
    /// <remarks>
    ///     <para>
    ///         <see cref="ResourceUsage" /> is blittable to the C `sg_usage` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ResourceUsage
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ResourceUsage"/> is <see cref="Immutable"/>.
        /// </summary>
        Default,

        /// <summary>
        ///     The resource will never be updated with new data. Instead, the data of the resource must be provided
        ///     on creation.
        /// </summary>
        Immutable,

        /// <summary>
        ///     The resource will be updated with new data possibly more than once, but not every frame.
        /// </summary>
        Dynamic,

        /// <summary>
        ///     The resource will be updated once every frame.
        /// </summary>
        Stream
    }
}
