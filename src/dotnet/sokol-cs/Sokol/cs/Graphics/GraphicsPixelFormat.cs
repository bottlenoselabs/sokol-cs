// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the available formats for how information is encoded and organized in a <see cref="GraphicsImage" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The name of a <see cref="GraphicsPixelFormat" /> usually consists of three parts which are read from left to
    ///         right: the components, the bit width per component, and the component data type. A postfix may be added
    ///         to distinguish between formats with similar components and bit widths:
    ///         <list type="bullet">
    ///             <item>
    ///                 <description>No postfix, marking an un-signed normalized integer.</description>
    ///             </item>
    ///             <item>
    ///                 <description>A `SN` postfix, marking a signed normalized integer.</description>
    ///             </item>
    ///             <item>
    ///                 <description>A `UI` postfix, marking an un-signed non-normalized integer.</description>
    ///             </item>
    ///             <item>
    ///                 <description>A `F` postfix, marking a float.</description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         For normalized un-signed integer formats suh as <see cref="RGBA8" />, component values in the range
    ///         <c>[0.0, 1.0]</c> are stored as <c>[0, MAX_UINT]</c>, where <c>MAX_UINT</c> is the greatest un-signed
    ///         integer that can be stored, given the bit size of the component. For normalized signed integer formats
    ///         such as <see cref="RGBA8SN" />, component values in the range <c>[-1.0, 1.0]</c> are stored as
    ///         <c>[MIN_INT, MAX_INT]</c>, where <c>MIN_INT</c> is the greatest negative integer and <c>MAX_INT</c> is
    ///         the greatest positive integer that can be stored, given the bit size of the component.
    ///     </para>
    ///     <para>
    ///         The default <see cref="GraphicsPixelFormat" /> for texture images is <see cref="GraphicsPixelFormat.RGBA8" />.
    ///     </para>
    ///     <para>
    ///         The default <see cref="GraphicsPixelFormat" /> for a <see cref="GraphicsPass" />'s color attachment is
    ///         <see cref="GraphicsBackend" /> dependent. For <see cref="GraphicsBackend.Metal" /> and
    ///         <see cref="GraphicsBackend.Direct3D11" />, it is <see cref="GraphicsPixelFormat.BGRA8" />. For all others, it
    ///         is
    ///         <see cref="GraphicsPixelFormat.RGBA8" />. The reason for this is to allow for more efficient frame flips for
    ///         the
    ///         the default on-screen <see cref="GraphicsPass" />'s color attachment (frame buffer). However, for your own
    ///         offscreen <see cref="GraphicsPass" /> color attachment(s), use any <see cref="GraphicsPixelFormat" /> which is
    ///         convenient.
    ///     </para>
    ///     <para>
    ///         Not every <see cref="GraphicsPixelFormat" /> can be used with every <see cref="GraphicsBackend" />. Call
    ///         <see cref="Graphics.QueryPixelFormat" /> to inspect the capabilities of a given
    ///         <see cref="GraphicsPixelFormat" /> for
    ///         the currently active <see cref="GraphicsBackend" />. When targeting
    ///         <see cref="GraphicsBackend.OpenGLES2" /> or <see cref="GraphicsBackend.OpenGLES3" />, only
    ///         <see cref="GraphicsPixelFormat.R8" /> and <see cref="GraphicsPixelFormat.RGBA8" /> are guaranteed to be safe;
    ///         all others
    ///         must be checked via <see cref="Graphics.QueryPixelFormat" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPixelFormat" /> is blittable to the C `sg_pixel_format` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "Enum members match `sokol_gfx`.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Enum members match `sokol_gfx`.")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "Enum members may have acronyms.")]
    public enum GraphicsPixelFormat
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures.
        /// </summary>
        Default,

        /// <summary>
        ///     A null pixel format, distinguishable from <see cref="Default" />.
        /// </summary>
        None,

        /// <summary>
        ///     A 8-bit color pixel format with one normalized, un-signed, integer component:
        ///     8-bit red.
        /// </summary>
        R8,

        /// <summary>
        ///     A 8-bit color pixel format with one normalized, signed, integer component:
        ///     8-bit red.
        /// </summary>
        R8SN,

        /// <summary>
        ///     A 8-bit color pixel format with one non-normalized, un-signed, integer component:
        ///     8-bit red.
        /// </summary>
        R8UI,

        /// <summary>
        ///     A 8-bit color pixel format with one non-normalized, signed, integer component:
        ///     8-bit red.
        /// </summary>
        R8SI,

        /// <summary>
        ///     A 16-bit color pixel format with one normalized, un-signed, integer component:
        ///     16-bit red.
        /// </summary>
        R16,

        /// <summary>
        ///     A 16-bit color pixel format with one normalized, signed, integer component:
        ///     16-bit red.
        /// </summary>
        R16SN,

        /// <summary>
        ///     A 16-bit color pixel format with one non-normalized, un-signed, integer component:
        ///     16-bit red.
        /// </summary>
        R16UI,

        /// <summary>
        ///     A 16-bit color pixel format with one non-normalized, signed, integer component:
        ///     16-bit red.
        /// </summary>
        R16SI,

        /// <summary>
        ///     A 16-bit color pixel format with a one floating-point component:
        ///     16-bit red.
        /// </summary>
        R16F,

        /// <summary>
        ///     A 16-bit color pixel format with two normalized, un-signed, integer components:
        ///     8-bit red and 8-bit green.
        /// </summary>
        RG8,

        /// <summary>
        ///     A 16-bit color pixel format with two normalized, signed, integer components:
        ///     8-bit red and 8-bit green.
        /// </summary>
        RG8SN,

        /// <summary>
        ///     A 16-bit color pixel format with two non-normalized, un-signed, integer components:
        ///     8-bit red and 8-bit green.
        /// </summary>
        RG8UI,

        /// <summary>
        ///     A 16-bit color pixel format with two non-normalized, signed, integer components:
        ///     8-bit red and 8-bit green.
        /// </summary>
        RG8SI,

        /// <summary>
        ///     A 32-bit color pixel format with one non-normalized, un-signed, integer component:
        ///     32-bit red.
        /// </summary>
        R32UI,

        /// <summary>
        ///     A 32-bit color pixel format with one non-normalized, signed, integer component:
        ///     32-bit red.
        /// </summary>
        R32SI,

        /// <summary>
        ///     A 32-bit color pixel format with one floating-point component:
        ///     32-bit red.
        /// </summary>
        R32F,

        /// <summary>
        ///     A 32-bit color pixel format with two normalized, un-signed, integer components:
        ///     16-bit red and 16-bit green.
        /// </summary>
        RG16,

        /// <summary>
        ///     A 32-bit color pixel format with two normalized, signed, integer components:
        ///     16-bit red and 16-bit green.
        /// </summary>
        RG16SN,

        /// <summary>
        ///     A 32-bit color pixel format with two non-normalized, un-signed, integer components:
        ///     16-bit red and 16-bit green.
        /// </summary>
        RG16UI,

        /// <summary>
        ///     A 32-bit color pixel format with two non-normalized, signed, integer components:
        ///     16-bit red and 16-bit green.
        /// </summary>
        RG16SI,

        /// <summary>
        ///     A 32-bit color pixel format with two floating-point components:
        ///     16-bit red and 16-bit green.
        /// </summary>
        RG16F,

        /// <summary>
        ///     A 32-bit color pixel format with four normalized, un-signed, integer components:
        ///     8-bit red, 8-bit green, 8-bit blue, and 8-bit alpha.
        /// </summary>
        RGBA8,

        /// <summary>
        ///     A 32-bit pixel format with four normalized, signed, integer components:
        ///     8-bit red, 8-bit green, 8-bit blue, and 8-bit alpha.
        /// </summary>
        RGBA8SN,

        /// <summary>
        ///     A 32-bit color pixel format with four non-normalized, un-signed, integer components:
        ///     8-bit red, 8-bit green, 8-bit blue, and 8-bit alpha.
        /// </summary>
        RGBA8UI,

        /// <summary>
        ///     A 32-bit color pixel format with four non-normalized, signed, integer components:
        ///     8-bit red, 8-bit green, 8-bit blue, and 8-bit alpha.
        /// </summary>
        RGBA8SI,

        /// <summary>
        ///     A 32-bit color pixel format with four normalized, un-signed, integer components:
        ///     8-bit blue, 8-bit green, 8-bit red, 8-bit alpha.
        /// </summary>
        BGRA8,

        /// <summary>
        ///     A 32-bit color pixel format with four normalized, un-signed, integer components:
        ///     10-bit red, 10-bit green, 10-bit blue, and 2-bit alpha.
        /// </summary>
        RGB10A2,

        /// <summary>
        ///     A 32-bit color pixel format with four floating-point components:
        ///     11-bit red, 11-bit green, and 10-bit red.
        /// </summary>
        RG11B10F,

        /// <summary>
        ///     A 64-bit color pixel format with two non-normalized, un-signed, integer components:
        ///     32-bit red and 32-bit green.
        /// </summary>
        RG32UI,

        /// <summary>
        ///     A 64-bit color pixel format with two non-normalized, signed, integer components:
        ///     32-bit red and 32-bit green.
        /// </summary>
        RG32SI,

        /// <summary>
        ///     A 64-bit color pixel format with two floating-point components:
        ///     32-bit red and 32-bit green.
        /// </summary>
        RG32F,

        /// <summary>
        ///     A 64-bit color pixel format with four normalized, un-signed, integer components:
        ///     16-bit red, 16-bit green, 16-bit blue, and 16-bit alpha.
        /// </summary>
        RGBA16,

        /// <summary>
        ///     A 64-bit color pixel format with four normalized, signed, integer components:
        ///     16-bit red, 16-bit green, 16-bit blue, and 16-bit alpha.
        /// </summary>
        RGBA16SN,

        /// <summary>
        ///     A 64-bit color pixel format with four non-normalized, un-signed, integer components:
        ///     16-bit red, 16-bit green, 16-bit blue, and 16-bit alpha.
        /// </summary>
        RGBA16UI,

        /// <summary>
        ///     A 64-bit color pixel format with four non-normalized, signed, integer components:
        ///     16-bit red, 16-bit green, 16-bit blue, and 16-bit alpha.
        /// </summary>
        RGBA16SI,

        /// <summary>
        ///     A 64-bit color pixel format with four floating-point components:
        ///     16-bit red, 16-bit green, 16-bit blue, and 16-bit alpha.
        /// </summary>
        RGBA16F,

        /// <summary>
        ///     A 128-bit color pixel format with four non-normalized, un-signed, integer components:
        ///     32-bit red, 32-bit green, 32-bit blue, and 32-bit alpha.
        /// </summary>
        RGBA32UI,

        /// <summary>
        ///     A 128-bit color pixel format with four non-normalized, signed, integer components:
        ///     32-bit red, 32-bit green, 32-bit blue, and 32-bit alpha.
        /// </summary>
        RGBA32SI,

        /// <summary>
        ///     A 128-bit color pixel format with four floating-point components:
        ///     32-bit red, 32-bit green, 32-bit blue, and 32-bit alpha.
        /// </summary>
        RGBA32F,

        /// <summary>
        ///     A 16-bit depth pixel format with one floating-point component:
        ///     16-bit depth.
        /// </summary>
        Depth,

        /// <summary>
        ///     A combined depth and stencil pixel format. The component bits and data type depend on the active
        ///     <see cref="GraphicsBackend" />. For <see cref="GraphicsBackend.Metal" />, it's a 64-bit pixel format
        ///     with two floating-point components: 32-bit depth and 8-bit depth. For all other back-ends, it's a
        ///     32-bit pixel format with two normalized, un-signed, integer components: 24-bit depth and 8-bit depth.
        /// </summary>
        DepthStencil,

        /// <summary>
        ///     <para>
        ///         A lossy compressed 64-bit color pixel format. 16 pixels are encoded using the following scheme: Two
        ///         16-bit RGB color end-points are taken from the original 16 pixels. Each color endpoint is made up of
        ///         the following non-normalized, un-signed, integer components: 5-bit blue, 6-bit green, and 5-bit red.
        ///         These end points are interpolated to make a 4 color map (color palette). A 32-bit lookup table
        ///         stores 16 entries that each reference a color in the map (2-bit per index). Alpha can also be stored
        ///         as on or off (1-bit), but it will reduce the color palette down to 3 colors as one color is reserved
        ///         for transparent black.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC1_RGBA" /> is also known as the texture compression `DXT1` with or
        ///         without alpha.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC1_RGBA" /> is used in practice for color maps, cutout color maps
        ///         (1-bit alpha), or normal maps when memory is tight.
        ///     </para>
        /// </remarks>
        BC1_RGBA,

        /// <summary>
        ///     A lossy compressed 128-bit color pixel format. 16 pixels are encoded using the <see cref="BC1_RGBA" />
        ///     scheme for the RGB components and explicitly 4-bits per pixel for the alpha component.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC2_RGBA" /> is also known as the texture compression `DXT2` or `DXT3`
        ///         depending if the color is pre-multiplied by alpha or not, respectively.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC2_RGBA" /> is used in practice for images with sharp alpha transitions
        ///         between translucent and opaque areas.
        ///     </para>
        /// </remarks>
        BC2_RGBA,

        /// <summary>
        ///     A lossy compressed 128-bit color pixel format. 16 pixels are encoded using the <see cref="BC1_RGBA" />
        ///     scheme for the RGB components and the <see cref="BC4_R" /> scheme for the alpha component.
        /// </summary>
        BC3_RGBA,

        /// <summary>
        ///     <para>
        ///         A lossy compressed 64-bit single color un-signed component pixel format. 16 pixels are encoded using
        ///         the following scheme: Two 8-bit single color end-points are taken from the original 16 pixels.
        ///         These end points are interpolated to make a 8 color map (color gradient). A 32-bit lookup table
        ///         stores 16 entries that each reference a color in the map (3-bit per index).
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC4_R" /> is used in practice for height maps, gloss maps, and any other
        ///         kind of grayscale texture.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC4_R" /> stores the single color component as the red component but this
        ///         does not necessarily mean it is red.
        ///     </para>
        /// </remarks>
        BC4_R,

        /// <summary>
        ///     <para>
        ///         A lossy compressed 64-bit single color signed component pixel format. 16 pixels are encoded using
        ///         the following scheme: Two 8-bit single color end-points are taken from the original 16 pixels.
        ///         These end points are interpolated to make a 8 color map (color gradient). A 32-bit lookup table
        ///         stores 16 entries that each reference a color in the map (3-bit per index).
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC4_R" /> is used in practice for height maps, gloss maps, and any other
        ///         kind of grayscale texture.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC4_R" /> stores the single color component as the red component but
        ///         this does not necessarily mean it is red.
        ///     </para>
        /// </remarks>
        BC4_RSN,

        /// <summary>
        ///     <para>
        ///         A lossy compressed 128-bit two color un-signed components pixel format. 16 pixels are encoded using
        ///         the <see cref="BC4_R" /> scheme for each of the two color components.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC5_RG" /> is used in practice for height maps, gloss maps, and any other
        ///         kind of grayscale texture. Compared to <see cref="BC4_R" />, <see cref="BC5_RG" /> is used in practice
        ///         when the two components are doing independent things and should be kept separate for an increase in
        ///         quality at the price of double the memory size.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC5_RG" /> stores the two color components as the red and green component,
        ///         respectively, but this does not necessarily mean it is red nor green.
        ///     </para>
        /// </remarks>
        BC5_RG,

        /// <summary>
        ///     <para>
        ///         A lossy compressed 128-bit two color signed components pixel format. 16 pixels are encoded using
        ///         the <see cref="BC4_R" /> scheme for each of the two color components.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The pixel format <see cref="BC5_RG" /> is used in practice for height maps, gloss maps, and any other
        ///         kind of grayscale texture. Compared to <see cref="BC4_R" />, <see cref="BC5_RG" /> is used in practice
        ///         when the two components are doing independent things and should be kept separate for an increase in
        ///         quality at the price of double the memory size.
        ///     </para>
        ///     <para>
        ///         The pixel format <see cref="BC5_RG" /> stores the two color components as the red and green component,
        ///         respectively, but this does not necessarily mean it is red nor green.
        ///     </para>
        /// </remarks>
        BC5_RGSN,

        /// <summary>
        /// TODO: http://www.reedbeta.com/blog/understanding-bcn-texture-compression-formats/.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        BC6H_RGBF,

        /// <summary>
        /// TODO: http://www.reedbeta.com/blog/understanding-bcn-texture-compression-formats/.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        BC6H_RGBUF,

        /// <summary>
        /// TODO: http://www.reedbeta.com/blog/understanding-bcn-texture-compression-formats/.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        BC7_RGBA,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        PVRTC_RGB_2BPP,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        PVRTC_RGB_4BPP,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        PVRTC_RGBA_2BPP,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        PVRTC_RGBA_4BPP,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        ETC2_RGB8,

        /// <summary>
        /// TODO.
        /// </summary>
        [SuppressMessage("ReSharper", "CS1591", Justification = "TODO")]
        ETC2_RGB8A1
    }
}
