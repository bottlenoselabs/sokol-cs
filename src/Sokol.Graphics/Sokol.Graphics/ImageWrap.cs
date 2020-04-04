// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different strategies for how texture coordinates outside the normal range (<c>0.0f</c> to
    ///     <c>1.0f</c> inclusively) are handled when texels of an <see cref="Image" /> are mapped into pixels when
    ///     rendering with a <see cref="Shader" />, also known as sampling.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ClampToBorder" /> is not supported on all <see cref="GraphicsBackend" /> implementations.
    ///         To check if <see cref="ClampToBorder" /> is supported call <see cref="Sg.QueryFeatures" /> and
    ///         inspect the value of <see cref="GraphicsFeatures.ImageClampToBorder" />. <see cref="ClampToBorder" /> will
    ///         fallback to <see cref="ClampToEdge" /> on unsupported <see cref="GraphicsBackend" /> implementations.
    ///     </para>
    ///     <para>
    ///         <see cref="ImageWrap" /> is blittable to the C `sg_wrap` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ImageWrap
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ImageWrap" /> is <see cref="ImageWrap.Repeat" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Ignore the integer part of a texture coordinate, creating a repeating pattern.
        /// </summary>
        Repeat,

        /// <summary>
        ///     The texture coordinate is clamped between <c>0.0f</c> and <c>1.0f</c>.
        /// </summary>
        ClampToEdge,

        /// <summary>
        ///     The texture coordinate will be given a color specified by <see cref="ImageBorderColor" /> when the
        ///     coordinate is outside the normal range.
        /// </summary>
        ClampToBorder,

        /// <summary>
        ///     Same as <see cref="Repeat" /> but the texture coordinate will be mirrored across it's axis when the
        ///     integer part is odd, creating a mirrored repeating pattern.
        /// </summary>
        MirroredRepeat
    }
}
