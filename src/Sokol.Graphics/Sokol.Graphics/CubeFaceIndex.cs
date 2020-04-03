// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the indices used for cube texture mapping.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="CubeFaceIndex" /> is blittable to the C `sg_cube_face` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum CubeFaceIndex
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
