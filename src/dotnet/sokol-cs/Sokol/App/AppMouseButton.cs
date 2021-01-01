// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the different buttons of a pointing device.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppKeyCode" /> is blittable to the C `sapp_mousebutton` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum AppMouseButton : uint
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures.
        /// </summary>
        Invalid = 256,

        /// <summary>
        ///     The left mouse button.
        /// </summary>
        Left = 0,

        /// <summary>
        ///     The right mouse button.
        /// </summary>
        Right = 1,

        /// <summary>
        ///     The middle mouse button.
        /// </summary>
        Middle = 2
    }
}
