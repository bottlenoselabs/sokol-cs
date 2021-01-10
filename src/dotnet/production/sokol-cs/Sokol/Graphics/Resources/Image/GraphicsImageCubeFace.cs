// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the indices used for cube texture mapping.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsImageCubeFace" /> is blittable to the C `sg_cube_face` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsImageCubeFace
    {
        /// <summary>
        ///     The first cube-face index, +X.
        /// </summary>
        PositiveX,

        /// <summary>
        ///     The second cube-face index, -X.
        /// </summary>
        NegativeX,

        /// <summary>
        ///     The third cube-face index, +Y.
        /// </summary>
        PositiveY,

        /// <summary>
        ///     The fourth cube-face index, -Y.
        /// </summary>
        NegativeY,

        /// <summary>
        ///     The fifth cube-face index, +Z.
        /// </summary>
        PositiveZ,

        /// <summary>
        ///     The sixth cube-face index, -Z.
        /// </summary>
        NegativeZ
    }
}
