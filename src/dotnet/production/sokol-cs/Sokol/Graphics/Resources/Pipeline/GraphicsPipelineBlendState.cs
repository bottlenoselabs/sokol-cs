// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Describes the blend stage of a <see cref="GraphicsPipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsPipelineBlendState" /> is used to configure how a fragment's color is blended with the
    ///         stored mapped value in the color attachment of the <see cref="GraphicsPass" /> and how that resulting color is
    ///         written to the attachment. The blend stage happens after the programmable
    ///         <see cref="GraphicsShaderStageType.Fragment" /> stage of a <see cref="GraphicsPipeline" />.
    ///     </para>
    ///     <para>
    ///         The <see cref="ColorFormat" /> property must be specified. This property is used to specify what
    ///         <see cref="GraphicsPixelFormat" /> is written to the color attachment of the <see cref="GraphicsPass" />.
    ///     </para>
    ///     <para>
    ///         Blend operations determine how a source fragment is combined with a destination value in a
    ///         color attachment to determine the pixel value to be written. The following properties define whether
    ///         and how blending is performed:
    ///         <list type="bullet">
    ///             <item>
    ///                 <description>
    ///                     The <see cref="IsEnabled" /> value enables color blending. Default is <c>false</c>.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     The <see cref="ColorWriteMask" /> value identifies which color components are blended.
    ///                     The default value is <see cref="GraphicsPipelineBlendColorMask.Rgba" />, which allows all color
    ///                     components to be blended.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     The <see cref="SourceFactorRgb" />, <see cref="SourceFactorAlpha" />,
    ///                     <see cref="DestinationFactorRgb" />, and <see cref="DestinationFactorAlpha" /> values
    ///                     assign the source and destination blend factors. The default value for
    ///                     <see cref="SourceFactorRgb" /> and <see cref="SourceFactorAlpha" /> is
    ///                     <see cref="GraphicsPipelineBlendFactor.One" />. The default value for
    ///                     <see cref="DestinationFactorRgb" /> and <see cref="DestinationFactorAlpha" /> is
    ///                     <see cref="GraphicsPipelineBlendFactor.Zero" />.
    ///                 </description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineBlendState" /> is blittable to the C `sg_blend_state` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 60, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    public struct GraphicsPipelineBlendState
    {
        /// <summary>
        ///     A <see cref="bool" /> value indicating whether blending of the fragment's color and the mapped value in the color
        ///     attachment is enabled. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(0)]
        public CBool IsEnabled;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendFactor" /> used with the source color to calculate the red, green, blue
        ///     components of the blended output color.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsPipelineBlendFactor SourceFactorRgb;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendFactor" /> used with the destination color to calculate the red, green,
        ///     blue components of the blended output color.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsPipelineBlendFactor DestinationFactorRgb;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendOperation" /> used to calculate the red, green, blue
        ///     components of the blended output color.
        /// </summary>
        [FieldOffset(12)]
        public GraphicsPipelineBlendOperation OperationRgb;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendFactor" /> used with the source color to calculate the alpha component of
        ///     the blended output color.
        /// </summary>
        [FieldOffset(16)]
        public GraphicsPipelineBlendFactor SourceFactorAlpha;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendFactor" /> used with the destination color to calculate the alpha component
        ///     of the blended output color.
        /// </summary>
        [FieldOffset(20)]
        public GraphicsPipelineBlendFactor DestinationFactorAlpha;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendOperation" /> used to calculate the alpha component of the blended output
        ///     color.
        /// </summary>
        [FieldOffset(24)]
        public GraphicsPipelineBlendOperation OperationAlpha;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendColorMask" />.
        /// </summary>
        [FieldOffset(28)]
        public GraphicsPipelineBlendColorMask ColorWriteMask;

        /// <summary>
        ///     The number of color attachments. Default is <c>1</c>. <c>0</c> is the same as <c>1</c>.
        /// </summary>
        [FieldOffset(32)]
        public uint ColorAttachmentCount;

        /// <summary>
        ///     The color <see cref="GraphicsPixelFormat" /> of the color attachments. Must be specified.
        /// </summary>
        [FieldOffset(36)]
        public GraphicsPixelFormat ColorFormat;

        /// <summary>
        ///     The depth <see cref="GraphicsPixelFormat" /> of the depth attachment.
        /// </summary>
        [FieldOffset(40)]
        public GraphicsPixelFormat DepthFormat;

        /// <summary>
        ///     The color used with <see cref="GraphicsPipelineBlendFactor.BlendColor" />,
        ///     <see cref="GraphicsPipelineBlendFactor.OneMinusBlendColor" />,
        ///     <see cref="GraphicsPipelineBlendFactor.BlendAlpha" />,
        ///     and <see cref="GraphicsPipelineBlendFactor.OneMinusBlendAlpha" />.
        /// </summary>
        [FieldOffset(44)]
        public Rgba32F BlendColor;
    }
}
