// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the different color components (RGBA) that are selected when blending a fragment's color with the mapped
    ///     value in the color attachment.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineBlendColorMask" /> is blittable to the C `sg_color_mask` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineBlendColorMask : byte
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineBlendColorMask" /> is <see cref="Rgba" />.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     No color component is blended.
        /// </summary>
        None = 0x10,

        /// <summary>
        ///     Only the red color component is blended.
        /// </summary>
        R = 0x1,

        /// <summary>
        ///     Only the green color component is blended.
        /// </summary>
        G = 0x2,

        /// <summary>
        ///     Only the blue color component is blended.
        /// </summary>
        B = 0x4,

        /// <summary>
        ///     Only the alpha color component is blended.
        /// </summary>
        A = 0x8,

        /// <summary>
        ///     The red, green, and blue color components blended.
        /// </summary>
        Rgb = 0x7,

        /// <summary>
        ///     The red, green, blue, and alpha color components blended.
        /// </summary>
        Rgba = 0xF
    }
}
