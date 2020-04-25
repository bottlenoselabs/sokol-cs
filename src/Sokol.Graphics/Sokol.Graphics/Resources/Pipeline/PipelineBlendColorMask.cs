// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different color components that are selected when writing a blended output color to a
    ///     <see cref="Pass"/>'s color attachment.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineBlendColorMask" /> is blittable to the C `sg_color_mask` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineBlendColorMask : byte
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineBlendColorMask" /> is <see cref="Rgba" />.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Any of the color components won't be selected.
        /// </summary>
        None = 0x10,

        /// <summary>
        ///     Only the red color component will be selected.
        /// </summary>
        R = 1 << 0,

        /// <summary>
        ///     Only the green color component will be selected.
        /// </summary>
        G = 1 << 1,

        /// <summary>
        ///     Only the blue color component will be selected.
        /// </summary>
        B = 1 << 2,

        /// <summary>
        ///     Only the alpha color component will be selected.
        /// </summary>
        A = 1 << 3,

        /// <summary>
        ///     The red, green, and blue color components will be selected.
        /// </summary>
        Rgb = 0x7,

        /// <summary>
        ///     The red, green, blue, and alpha color components will be selected.
        /// </summary>
        Rgba = 0xF
    }
}
