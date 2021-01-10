// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines how a texel, or group of texels, within the <see cref="GraphicsImage" /> are fetched (and possibly
    ///     combined) when mapping between texel coordinates (UVW floats from <c>0.0f</c> to <c>1.0f</c> inclusively) and pixel
    ///     coordinates (XYZ positive integers normalized into the same space) is not a 1-1 function.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         There are 3 different cases for texture filtering.
    ///         <list type="bullet">
    ///             <item>
    ///                 <description>
    ///                     More than one pixel for every texel. This is known as up-scaling or magnification. In this case,
    ///                     mipmaps are not used and as such only <see cref="Nearest" /> and <see cref="Linear" /> are
    ///                     applicable options.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     Every texel maps exactly onto one pixel and vice-versa. Filtering does not apply in this
    ///                     case and as such any option of <see cref="GraphicsImageFilter" /> is irrelevant.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     More than one texel for every pixel. This is known as down-scaling or minification. In
    ///                     such a case, mipmaps can be useful and as such all options of
    ///                     <see cref="GraphicsImageFilter" /> are applicable.
    ///                 </description>
    ///             </item>
    ///         </list>
    ///         Filtering can be set for a <see cref="GraphicsImage" /> by changing
    ///         <see cref="GraphicsImageDescriptor.MinificationFilter" /> and
    ///         <see cref="GraphicsImageDescriptor.MagnificationFilter" /> before initialization of a
    ///         <see cref="GraphicsImage" />.
    ///     </para>
    ///     <para>
    ///         Depending on the chosen <see cref="GraphicsImageFilter" />, the results (not necessarily pixels) will
    ///         show a varying degree of blurriness and aliasing compared to their original data. For 2D rendering, severe
    ///         aliasing by using <see cref="Nearest" /> may be desired to get a pixelated retro feel when up-scaling.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsImageFilter" /> is blittable to the C `sg_filter` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsImageFilter
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsImageFilter" /> is <see cref="Nearest" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Returns the texel value that is closest (in Manhattan distance) to the pixel coordinates. Mipmapping is not used.
        /// </summary>
        Nearest,

        /// <summary>
        ///     Returns the weighted average (linear interpolation) of the four texels that are closest (in Manhattan distance) to
        ///     the pixel coordinates. Mipmapping is not used.
        /// </summary>
        Linear,

        /// <summary>
        ///     Same as <see cref="Nearest" /> except with mipmapping. One mipmap is selected based on the size of the texel area
        ///     that most closely matches the size of the pixel area.
        /// </summary>
        NearestMipmapNearest,

        /// <summary>
        ///     Same as <see cref="Linear" /> except with mipmapping. One mipmap is selected based on the size of the texel area
        ///     that most closely matches the size of the pixel area.
        /// </summary>
        NearestMipmapLinear,

        /// <summary>
        ///     Same as <see cref="NearestMipmapNearest" /> except with two mipmaps. The final result is a weighted average (linear
        ///     interpolation) between the two mipmaps.
        /// </summary>
        LinearMipmapNearest,

        /// <summary>
        ///     Same as <see cref="NearestMipmapLinear" /> except with two mipmaps. The final result is a weighted average (linear
        ///     interpolation) between the two mipmaps.
        /// </summary>
        LinearMipmapLinear
    }
}
