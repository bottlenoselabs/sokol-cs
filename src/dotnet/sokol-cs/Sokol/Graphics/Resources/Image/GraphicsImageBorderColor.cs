// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the different border colors that can be used when texels of a <see cref="GraphicsImage" /> are sampled with
    ///     <see cref="GraphicsImageWrap.ClampToBorder" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsImageBorderColor" /> is blittable to the C `sg_border_color` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsImageBorderColor
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsImageBorderColor" /> is <see cref="TransparentBlack" />.
        /// </summary>
        Default,

        /// <summary>
        ///     RGBA #00000000.
        /// </summary>
        TransparentBlack,

        /// <summary>
        ///     RGBA #000000FF.
        /// </summary>
        OpaqueBlack,

        /// <summary>
        ///     RGBA #FFFFFFFF.
        /// </summary>
        OpaqueWhite
    }
}
