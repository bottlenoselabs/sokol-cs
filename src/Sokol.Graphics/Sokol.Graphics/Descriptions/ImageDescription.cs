// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing an <see cref="Image" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ImageDescription" /> is blittable to the C `sg_image_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1664, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct ImageDescription
    {
        /// <summary>
        ///     The <see cref="ImageType" /> of the image. Default is <see cref="ImageType.Texture2D" />.
        /// </summary>
        [FieldOffset(4)]
        public ImageType Type;

        /// <summary>
        ///     The <see cref="bool" /> indicating whether the image is a render target. Default is <c>false</c>. If
        ///     <c>false</c> the image is a texture.
        /// </summary>
        [FieldOffset(8)]
        public BlittableBoolean IsRenderTarget;

        /// <summary>
        ///     The number of texels in the X axis. Must be set. Can't be <c>0</c> or negative.
        /// </summary>
        [FieldOffset(12)]
        public int Width;

        /// <summary>
        ///     The number of texels in the Y axis. Must be set. Can't be <c>0</c> or negative.
        /// </summary>
        [FieldOffset(16)]
        public int Height;

        /// <summary>
        ///     The number of texels in the Z axis. Default is <c>1</c>.
        /// </summary>
        [FieldOffset(20)]
        public int Depth;

        /// <summary>
        ///     The number of texels in the Z axis. Default is <c>1</c>.
        /// </summary>
        [FieldOffset(20)]
        public int Layers;

        /// <summary>
        ///     The number of mipmap levels. Default is <c>1</c>.
        /// </summary>
        [FieldOffset(24)]
        public int MipmapCount;

        /// <summary>
        ///     The <see cref="ResourceUsage" /> of the image. Default is <see cref="ResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(28)]
        public ResourceUsage Usage;

        /// <summary>
        ///     The <see cref="PixelFormat" /> of the image. Default is <see cref="PixelFormat.RGBA8" /> for textures
        ///     and otherwise dependent on the <see cref="GraphicsBackend" /> for render targets.
        /// </summary>
        [FieldOffset(32)]
        public PixelFormat Format;

        /// <summary>
        ///     The number of samples to be used for the render target when multi-sample anti-aliasing (MSAA) is
        ///     available. Does not apply to textures. To check if MSAA is supported call
        ///     <see cref="Sg.QueryFeatures" /> and inspect the value of <see cref="Features.MsaaRenderTargets" />.
        ///     Default is <c>1</c>.
        /// </summary>
        [FieldOffset(36)]
        public int SampleCount;

        /// <summary>
        ///     The <see cref="ImageFilter" /> to use for minification. Default is <see cref="ImageFilter.Nearest" />.
        /// </summary>
        [FieldOffset(40)]
        public ImageFilter MinificationFilter;

        /// <summary>
        ///     The <see cref="ImageFilter" /> to use for magnification. Default is <see cref="ImageFilter.Nearest" />.
        /// </summary>
        [FieldOffset(44)]
        public ImageFilter MagnificationFilter;

        /// <summary>
        ///     The <see cref="ImageWrap" /> to use for the first texture coordinate component.
        /// </summary>
        [FieldOffset(48)]
        public ImageWrap WrapU;

        /// <summary>
        ///     The <see cref="ImageWrap" /> to use for the second texture coordinate component.
        /// </summary>
        [FieldOffset(52)]
        public ImageWrap WrapV;

        /// <summary>
        ///     The <see cref="ImageWrap" /> to use for the third texture coordinate component.
        /// </summary>
        [FieldOffset(56)]
        public ImageWrap WrapW;

        /// <summary>
        ///     The <see cref="ImageBorderColor" /> of the image.
        /// </summary>
        [FieldOffset(60)]
        public ImageBorderColor BorderColor;

        /// <summary>
        ///     The maximum number of texels to sample per pixel when the viewing angle of a texture is oblique. For
        ///     best results, use with mipmapping. Must be between <c>1</c> and <c>16</c>, inclusively. Default is
        ///     <c>1</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Increasing <see cref="MaximumAnisotropy" /> can significantly reduce blurring and preserve detail
        ///         at extreme viewing angles at the cost of rendering performance. Consider allowing the user of the
        ///         application to configure this in the graphical settings.
        ///     </para>
        /// </remarks>
        [FieldOffset(64)]
        public uint MaximumAnisotropy;

        /// <summary>
        ///     The lower end of the mipmap range to clamp access to where <c>0</c> represents the largest and most
        ///     detailed mipmap and any higher level is less detailed. Default is <c>0.0f</c>.
        /// </summary>
        [FieldOffset(68)]
        public float MinimumLevelOfDetail;

        /// <summary>
        ///     The higher end of the mipmap range to clamp access to where <c>0</c> represents the largest and most
        ///     detailed mipmap and any higher level is less detailed. Must be greater than or equal to
        ///     <see cref="MinimumLevelOfDetail" />. Default is <see cref="float.MaxValue" />.
        /// </summary>
        [FieldOffset(72)]
        public float MaximumLevelOfDetail;

        /// <summary>
        ///     The contents of the image.
        /// </summary>
        [FieldOffset(80)]
        public ImageContent Content;

        // TODO: Trace hooks
        [FieldOffset(1616)]
        internal IntPtr Label;

        // TODO: Native 3D textures
        [FieldOffset(1624)]
        internal fixed uint GLTextures[Sg.InflightFramesCount];

        // TODO: Native 3D textures
        [FieldOffset(1632)]
        internal fixed ulong MTLTextures[Sg.InflightFramesCount];

        // TODO: Native 3D textures
        [FieldOffset(1648)]
        internal IntPtr D3D11Texture;

        [FieldOffset(0)]
        internal uint _startCanary;

        [FieldOffset(1656)]
        internal uint _endCanary;
    }
}
