// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the states of a resource.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ResourceUsage" /> is blittable to the C `sg_resource_state` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ResourceState
    {
        /// <summary>
        ///     The starting state of a resource indicating it is unoccupied and can be allocated.
        /// </summary>
        Initial,

        /// <summary>
        ///     The resource is allocated with a given identifier but, currently, is not yet initialized.
        /// </summary>
        Alloc,

        /// <summary>
        ///     The resource is currently valid meaning it was initialized successfully and is now ready to use.
        /// </summary>
        Valid,

        /// <summary>
        ///     The resource failed initialization.
        /// </summary>
        Failed,

        /// <summary>
        ///     The resource with a given identifier doesn't exist.
        /// </summary>
        Invalid
    }
}
