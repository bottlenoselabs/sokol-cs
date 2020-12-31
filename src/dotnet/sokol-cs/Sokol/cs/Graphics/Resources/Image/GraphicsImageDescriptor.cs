// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="GraphicsImage" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create a
    ///         <see cref="GraphicsImageDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsImageDescriptor" /> is blittable to the C `sg_image_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1688, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsImageDescriptor
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     The <see cref="GraphicsImageType" /> of the image. Default is <see cref="GraphicsImageType.Texture2D" />.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsImageType Type;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the image is a render target. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(8)]
        public CBool IsRenderTarget;

        /// <summary>
        ///     The number of texels in the X axis. Must be set. Can't be <c>0</c>.
        /// </summary>
        [FieldOffset(12)]
        public uint Width;

        /// <summary>
        ///     The number of texels in the Y axis. Must be set. Can't be <c>0</c>.
        /// </summary>
        [FieldOffset(16)]
        public uint Height;

        /// <summary>
        ///     The number of texels in the Z axis for <see cref="GraphicsImageType.Texture3D" /> or the number of layers for
        ///     <see cref="GraphicsImageType.TextureArray" />. Default is <c>1</c>. <c>0</c> is the same as <c>1</c>.
        /// </summary>
        [FieldOffset(20)]
        public uint SliceCount;

        /// <summary>
        ///     The number of mipmap levels available when sampling. Default is <c>1</c>. <c>0</c> is the same as <c>1</c>. If
        ///     <see cref="MipmapCount" /> is set be larger than <c>1</c>, also set <see cref="Content" /> with the pixel data for
        ///     the mipmaps.
        /// </summary>
        [FieldOffset(24)]
        public uint MipmapCount;

        /// <summary>
        ///     The <see cref="GraphicsResourceUsage" /> of the image. Default is <see cref="GraphicsResourceUsage.Immutable" />.
        /// </summary>
        [FieldOffset(28)]
        public GraphicsResourceUsage Usage;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> of the image's pixel data. Default is
        ///     <see cref="GraphicsPixelFormat.RGBA8" /> for textures and <see cref="GraphicsBackend" /> dependent for render
        ///     targets.
        /// </summary>
        [FieldOffset(32)]
        public GraphicsPixelFormat Format;

        /// <summary>
        ///     The number of samples to use when multi-sample anti-aliasing (MSAA) is available. Default is <c>1</c>. <c>0</c> is
        ///     the same as <c>1</c>. Only applies to render targets. To check if MSAA is supported, inspect
        ///     <see cref="Graphics.Features" /> for the value of <see cref="GraphicsFeatures.MsaaRenderTargets" />.
        /// </summary>
        [FieldOffset(36)]
        public int SampleCount;

        /// <summary>
        ///     The <see cref="GraphicsImageSamplerFilter" /> to use for minification. Default is
        ///     <see cref="GraphicsImageSamplerFilter.Nearest" />.
        /// </summary>
        [FieldOffset(40)]
        public GraphicsImageSamplerFilter MinificationFilter;

        /// <summary>
        ///     The <see cref="GraphicsImageSamplerFilter" /> to use for magnification. Default is
        ///     <see cref="GraphicsImageSamplerFilter.Nearest" />.
        /// </summary>
        [FieldOffset(44)]
        public GraphicsImageSamplerFilter MagnificationFilter;

        /// <summary>
        ///     The <see cref="GraphicsImageSamplerWrap" /> to use for the first texel coordinate component. Default is
        ///     <see cref="GraphicsImageSamplerWrap.Repeat" />.
        /// </summary>
        [FieldOffset(48)]
        public GraphicsImageSamplerWrap WrapU;

        /// <summary>
        ///     The <see cref="GraphicsImageSamplerWrap" /> to use for the second texel coordinate component. Default is
        ///     <see cref="GraphicsImageSamplerWrap.Repeat" />.
        /// </summary>
        [FieldOffset(52)]
        public GraphicsImageSamplerWrap WrapV;

        /// <summary>
        ///     The <see cref="GraphicsImageSamplerWrap" /> to use for the third texel coordinate component. Default is
        ///     <see cref="GraphicsImageSamplerWrap.Repeat" />.
        /// </summary>
        [FieldOffset(56)]
        public GraphicsImageSamplerWrap WrapW;

        /// <summary>
        ///     The <see cref="GraphicsImageBorderColor" /> of the image. Only used when <see cref="WrapU" />,
        ///     <see cref="WrapV" /> , or <see cref="WrapW" /> are <see cref="GraphicsImageSamplerWrap.ClampToBorder" />.
        /// </summary>
        [FieldOffset(60)]
        public GraphicsImageBorderColor BorderColor;

        /// <summary>
        ///     The maximum number of texels to sample per pixel when the viewing angle of a texture is oblique. For
        ///     best results, use with mipmapping. Can't be zero. Can't be greater than <c>16</c>. Default is <c>1</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Increasing <see cref="MaximumAnisotropy" /> can significantly reduce blurring and preserve detail
        ///         at extreme viewing angles at the cost of rendering performance. Consider allowing the user of the
        ///         application to configure this in the graphical settings of the application.
        ///     </para>
        /// </remarks>
        [FieldOffset(64)]
        public uint MaximumAnisotropy;

        /// <summary>
        ///     The highest resolution mipmap (lowest mipmap level) used when sampling. <c>0.0f</c> is the lowest mipmap level.
        ///     <see cref="float.MaxValue" /> is the highest mipmap level. Default is <c>0.0f</c>.
        /// </summary>
        [FieldOffset(68)]
        public float MinimumLevelOfDetail;

        /// <summary>
        ///     The lowest resolution mipmap (highest mipmap level) used when sampling. <c>0.0f</c> is the lowest mipmap level.
        ///     <see cref="float.MaxValue" /> is the highest mipmap level. Default is <see cref="float.MaxValue" />.
        /// </summary>
        [FieldOffset(72)]
        public float MaximumLevelOfDetail;

        /// <summary>
        ///     The pixel data.
        /// </summary>
        [FieldOffset(80)]
        public GraphicsImageContent Content;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1616)]
        public IntPtr Label;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1624)]
        public fixed uint GLTextures[GraphicsConstants.InflightFramesCount];

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1632)]
        public uint GLTextureTarget;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1640)]
        public fixed ulong MTLTextures[GraphicsConstants.InflightFramesCount];

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1656)]
        public IntPtr D3D11Texture;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1664)]
        public IntPtr D3D11ShaderResourceView;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(1672)]
        public IntPtr WebGPUTexture;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(1680)]
        private readonly uint _endCanary;
    }
}
