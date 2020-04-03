// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different strategies for how texture coordinates outside the normal range of <c>0</c> to
    ///     <c>1</c>, inclusively, are handled when texels of a texture <see cref="Image" /> are mapped into pixels
    ///     when rendering with a <see cref="Shader" />, also known as sampling.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ClampToBorder" /> is not supported on all <see cref="GraphicsBackend" /> implementations.
    ///         To check if <see cref="ClampToBorder" /> is supported call <see cref="Sg.QueryFeatures" /> and
    ///         inspect the value of <see cref="Features.ImageClampToBorder" />. <see cref="ClampToBorder" /> will
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
        ///     The texture coordinate is clamped between <c>0</c> and <c>1</c>.
        /// </summary>
        ClampToEdge,

        /// <summary>
        ///     The texture coordinate will be given a specified color when it is outside the normal range.
        /// </summary>
        ClampToBorder,

        /// <summary>
        ///     Same as <see cref="Repeat" /> but the texture coordinate will be mirrored across it's axis when the
        ///     integer part is odd.
        /// </summary>
        MirroredRepeat
    }
}
