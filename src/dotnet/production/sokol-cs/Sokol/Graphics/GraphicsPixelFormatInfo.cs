// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Runtime information about a <see cref="GraphicsPixelFormat" />. Result of calling <see cref="Graphics.QueryPixelFormat" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPixelFormatInfo" /> is blittable to the C `sg_pixelformat_info` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 6, Pack = 1)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsPixelFormatInfo
    {
        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> can be sampled in shaders.
        /// </summary>
        [FieldOffset(0)]
        public readonly CBool CanBeSampled;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> can be sampled with filtering.
        /// </summary>
        [FieldOffset(1)]
        public readonly CBool CanBeSampledWithFiltering;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> can be used as a render target.
        /// </summary>
        [FieldOffset(2)]
        public readonly CBool CanBeUsedAsRenderTarget;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> supports alpha-blending.
        /// </summary>
        [FieldOffset(3)]
        public readonly CBool AlphaBlendingIsSupported;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> can be used as a multi-sample anti-aliasing (MSAA) render target.
        /// </summary>
        [FieldOffset(4)]
        public readonly CBool CanBeUsedAsRenderTargetWithMsaa;

        /// <summary>
        ///     The <see cref="GraphicsPixelFormat" /> has depth.
        /// </summary>
        [FieldOffset(5)]
        public readonly CBool IsDepthFormat;
    }
}
