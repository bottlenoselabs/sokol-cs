// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different ways a fragment's color (source) and the color attachment of a <see cref="GraphicsPass" />
    ///     (destination) are combined before writing being written to the color attachment.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Colors are represented by floating-point numbers, so adding them, subtracting them, and even multiplying
    ///         them are all perfectly valid operations.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineBlendOperation" /> is blittable to the C `sg_blend_op` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineBlendOperation
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineBlendOperation" /> is <see cref="Add" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The source and destination colors are added to each other. Mathematically, the output color is:
        ///     <c>O = s * S + d * D</c>. <c>s</c> and <c>d</c> are the blending parameters for source and destination,
        ///     respectively, and are defined by <see cref="GraphicsPipelineBlendFactor" />. <c>S</c> and <c>D</c> are the
        ///     source and destination colors, respectively.
        /// </summary>
        Add,

        /// <summary>
        ///     The source is subtracted by the destination. Mathematically, the output color is:
        ///     <c>O = s * S - d * D</c>. <c>s</c> and <c>d</c> are the blending parameters for source and destination,
        ///     respectively, and are defined by <see cref="GraphicsPipelineBlendFactor" />. <c>S</c> and <c>D</c> are the
        ///     source and destination colors, respectively.
        /// </summary>
        Subtract,

        /// <summary>
        ///     The destination is subtracted by the source. Mathematically, the output color is:
        ///     <c>O = d * D - s * S</c>. <c>s</c> and <c>d</c> are the blending parameters for source and destination,
        ///     respectively, and are defined by <see cref="GraphicsPipelineBlendFactor" />. <c>S</c> and <c>D</c> are the
        ///     source and destination colors, respectively.
        /// </summary>
        ReverseSubtract
    }
}
