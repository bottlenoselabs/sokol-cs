// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different strategies for how the texel coordinates are interpreted during sampling when the coordinates
    ///     are outside the normal range (<c>0.0f</c> to <c>1.0f</c> inclusively).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsImageSamplerWrap" /> is blittable to the C `sg_wrap` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsImageSamplerWrap
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsImageSamplerWrap" /> is <see cref="GraphicsImageSamplerWrap.Repeat" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Ignore the integer part of a texture coordinate (except for <c>1.0f</c> exactly), creating a repeating pattern.
        /// </summary>
        Repeat,

        /// <summary>
        ///     The texture coordinate is clamped to be a value in the normal range of <c>0.0f</c> to <c>1.0f</c> inclusively.
        /// </summary>
        ClampToEdge,

        /// <summary>
        ///     The texture coordinate will be given a color specified by <see cref="GraphicsImageBorderColor" /> when the
        ///     coordinate is outside the normal range of <c>0.0f</c> to <c>1.0f</c> inclusively. Not supported on all
        ///     <see cref="GraphicsBackend" /> implementations. To check if <see cref="ClampToBorder" /> is supported, inspect
        ///     <see cref="Graphics.Features" /> for the value of <see cref="GraphicsFeatures.ImageClampToBorder" />.
        ///     <see cref="ClampToBorder" /> will fallback to <see cref="ClampToEdge" /> on unsupported
        ///     <see cref="GraphicsBackend" /> implementations.
        /// </summary>
        ClampToBorder,

        /// <summary>
        ///     Same as <see cref="Repeat" /> but the texture coordinate will be mirrored across it's axis when the
        ///     integer part is odd, creating a mirrored repeating pattern.
        /// </summary>
        MirroredRepeat
    }
}
