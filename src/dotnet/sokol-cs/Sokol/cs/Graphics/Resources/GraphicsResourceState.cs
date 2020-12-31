// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the states of a graphics resource.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsResourceState" /> is blittable to the C `sg_resource_state` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsResourceState
    {
        /// <summary>
        ///     The starting state of a graphics resource indicating it is unoccupied and can be allocated.
        /// </summary>
        Initial,

        /// <summary>
        ///     The graphics resource is allocated with a given identifier but, currently, is not yet initialized.
        /// </summary>
        Alloc,

        /// <summary>
        ///     The graphics resource is currently valid meaning it was initialized successfully and is now ready to use.
        /// </summary>
        Valid,

        /// <summary>
        ///     The graphics resource failed initialization.
        /// </summary>
        Failed,

        /// <summary>
        ///     The graphics resource with a given identifier doesn't exist.
        /// </summary>
        Invalid
    }
}
