// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different strategies for how texels of a <see cref="Image" /> are mapped into pixels when
    ///     rendering with a <see cref="Shader" />, also known as sampling. Depending on the strategy chosen the pixels
    ///     will show a varying degree of blurriness, detail, and aliasing.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         There are 3 different cases for filtering.
    ///         <list type="bullet">
    ///             <item>
    ///                 <description>Each texel maps onto more than one pixel. This is known as magnification.</description>
    ///             </item>
    ///             <item>
    ///                 <description>Each texel maps exactly onto one pixel. Filtering does not apply in this case.</description>
    ///             </item>
    ///             <item>
    ///                 <description>Each texel maps onto less than one pixel. This is known as minification.</description>
    ///             </item>
    ///         </list>
    ///         Magnification and minification can be set in the <see cref="ImageDescriptor" /> when calling
    ///         <see cref="Image.Create" /> or <see cref="Image.Init" />.
    ///     </para>
    ///     <para>
    ///         A set of optimized images for progressively lower resolutions of an image known as mipmaps can
    ///         significantly increase the detail, reduce aliasing, and in some cases even improve performance when
    ///         filtering an <see cref="Image" /> with minification. Mipmapping is not used for magnification. Using
    ///         mipmapping means selecting between multiple images of different sizes for sampling based on the angle
    ///         and size of the image relative to screen or render target. For more information, see
    ///         <a cref="http://en.wikipedia.org/wiki/Mipmap">https://en.wikipedia.org/wiki/Mipmap</a>.
    ///     </para>
    ///     <para>
    ///         <see cref="ImageFilter" /> is blittable to the C `sg_filter` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ImageFilter
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ImageFilter" /> is <see cref="Nearest" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Returns the value of the texture element that is nearest (in Manhattan distance) to the center of the
        ///     pixel being textured. Mipmapping is not used.
        /// </summary>
        Nearest,

        /// <summary>
        ///     Returns the weighted average of the four texture elements that are closest to the center of the pixel
        ///     being textured. These can include border texture elements, depending on the values of
        ///     <see cref="ImageDescriptor.WrapU" /> and <see cref="ImageDescriptor.WrapV" />, and on the exact
        ///     mapping. Mipmapping is not used.
        /// </summary>
        Linear,

        /// <summary>
        ///     Chooses the mipmap that most closely matches the size of the pixel being textured and uses the texture
        ///     element nearest to the center of the pixel to produce a texture value.
        /// </summary>
        NearestMipmapNearest,

        /// <summary>
        ///     Chooses the mipmap that most closely matches the size of the pixel being textured and uses a weighted
        ///     average of the four texture elements that are closest to the center of the pixel to produce a texture
        ///     value.
        /// </summary>
        NearestMipmapLinear,

        /// <summary>
        ///     Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the
        ///     texture element nearest to the center of the pixel to produce a texture value from each mipmap. The
        ///     final texture value is a weighted average of those two values.
        /// </summary>
        LinearMipmapNearest,

        /// <summary>
        ///     Chooses the two mipmaps that most closely match the size of the pixel being textured and uses a weighted
        ///     average of the four texture elements that are closest to the center of the pixel to produce a texture
        ///     value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        LinearMipmapLinear
    }
}
