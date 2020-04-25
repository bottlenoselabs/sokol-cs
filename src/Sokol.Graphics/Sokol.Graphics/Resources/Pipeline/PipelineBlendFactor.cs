// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of source and destination parameters used with a <see cref="PipelineBlendOperation" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Colors are represented by floating-point numbers, so adding them, subtracting them, and even multiplying
    ///         them are all perfectly valid operations.
    ///     </para>
    ///     <para>
    ///         An example of using <see cref="PipelineBlendOperation" /> and <see cref="PipelineBlendFactor" />
    ///         together to achieve straight blending of source and destination is the following represented
    ///         mathematically: <c>O = S * D</c>, or more explicitly, <c>O = D * S + 0 * D</c>. <c>O</c> is the color to
    ///         persist, <c>S</c> is the source color, and <c>D</c> is the destination color. To achieve this blending
    ///         operation use <see cref="PipelineBlendOperation.Add" /> along with
    ///         <see cref="PipelineBlendFactor.DestinationColor" /> for first parameter to multiply with the source
    ///         color and <see cref="PipelineBlendFactor.Zero" /> for the second parameter to multiply with the
    ///         destination color.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineBlendFactor" /> is blittable to the C `sg_blend_factor` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineBlendFactor
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineBlendFactor" /> is <see cref="One" /> for sources and
        ///     <see cref="PipelineBlendFactor.Zero" /> for destinations.
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
        ///     The blend parameter is the <see cref="PipelineBlendState.BlendColor" />.
        /// </summary>
        BlendColor,

        /// <summary>
        ///     The blend parameter is one minus the <see cref="PipelineBlendState.BlendColor" />.
        /// </summary>
        OneMinusBlendColor,

        /// <summary>
        ///     The blend parameter is the alpha component of the <see cref="PipelineBlendState.BlendColor" />.
        /// </summary>
        BlendAlpha,

        /// <summary>
        ///     The blend parameter is one minus the alpha component of the <see cref="PipelineBlendState.BlendColor" />.
        /// </summary>
        OneMinusBlendAlpha
    }
}
