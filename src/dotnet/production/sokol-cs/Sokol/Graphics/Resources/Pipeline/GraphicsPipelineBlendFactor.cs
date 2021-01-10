// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the types of source and destination parameters used with a <see cref="GraphicsPipelineBlendOperation" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Colors are represented by floating-point numbers, so adding them, subtracting them, and even multiplying
    ///         them are all perfectly valid operations.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineBlendFactor" /> is blittable to the C `sg_blend_factor` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineBlendFactor
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineBlendFactor" /> is <see cref="One" /> for sources and
        ///     <see cref="GraphicsPipelineBlendFactor.Zero" /> for destinations.
        /// </summary>
        Default,

        /// <summary>
        ///     The blend parameter is <c>0.0f</c> for each color component.
        /// </summary>
        Zero,

        /// <summary>
        ///     The blend parameter is <c>1.0f</c> for each color component.
        /// </summary>
        One,

        /// <summary>
        ///     The blend parameter is the source color.
        /// </summary>
        SourceColor,

        /// <summary>
        ///     The blend parameter is one minus the source color.
        /// </summary>
        OneMinusSourceColor,

        /// <summary>
        ///     The blend parameter is the alpha component of the source color.
        /// </summary>
        SourceAlpha,

        /// <summary>
        ///     The blend parameter is one minus the alpha component of the source color.
        /// </summary>
        OneMinusSourceAlpha,

        /// <summary>
        ///     The blend parameter is the destination color.
        /// </summary>
        DestinationColor,

        /// <summary>
        ///     The blend parameter is one minus the destination color.
        /// </summary>
        OneMinusDestinationColor,

        /// <summary>
        ///     The blend parameter is alpha component of the destination color.
        /// </summary>
        DestinationAlpha,

        /// <summary>
        ///     The blend parameter is one minus the alpha component of the destination color.
        /// </summary>
        OneMinusDestinationAlpha,

        /// <summary>
        ///     The blend parameter is the minimum of either, the alpha component of the source color, or one minus the
        ///     alpha component of the destination color.
        /// </summary>
        SourceAlphaSaturated,

        /// <summary>
        ///     The blend parameter is the <see cref="GraphicsPipelineBlendState.BlendColor" />.
        /// </summary>
        BlendColor,

        /// <summary>
        ///     The blend parameter is one minus the <see cref="GraphicsPipelineBlendState.BlendColor" />.
        /// </summary>
        OneMinusBlendColor,

        /// <summary>
        ///     The blend parameter is the alpha component of the <see cref="GraphicsPipelineBlendState.BlendColor" />.
        /// </summary>
        BlendAlpha,

        /// <summary>
        ///     The blend parameter is one minus the alpha component of the <see cref="GraphicsPipelineBlendState.BlendColor" />.
        /// </summary>
        OneMinusBlendAlpha
    }
}
