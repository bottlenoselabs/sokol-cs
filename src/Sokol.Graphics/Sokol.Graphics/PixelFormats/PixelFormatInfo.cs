// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Runtime information about a <see cref="PixelFormat" />. Result of calling
    ///     <see cref="GraphicsDevice.QueryPixelFormat" />.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 6, Pack = 1)]
    public struct PixelFormatInfo
    {
        /// <summary>
        ///     The <see cref="PixelFormat" /> can be sampled in shaders.
        /// </summary>
        [FieldOffset(0)]
        public BlittableBoolean CanBeSampled;

        /// <summary>
        ///     The <see cref="PixelFormat" /> can be sampled with filtering.
        /// </summary>
        [FieldOffset(1)]
        public BlittableBoolean CanBeSampledWithFiltering;

        /// <summary>
        ///     The <see cref="PixelFormat" /> can be used as a render target.
        /// </summary>
        [FieldOffset(2)]
        public BlittableBoolean CanBeUsedAsRenderTarget;

        /// <summary>
        ///     The <see cref="PixelFormat" /> supports alpha-blending.
        /// </summary>
        [FieldOffset(3)]
        public BlittableBoolean AlphaBlendingIsSupported;

        /// <summary>
        ///     The <see cref="PixelFormat" /> can be used as a multi-sample anti-aliasing (MSAA) render target.
        /// </summary>
        [FieldOffset(4)]
        public BlittableBoolean CanBeUsedAsRenderTargetWithMsaa;

        /// <summary>
        ///     The <see cref="PixelFormat" /> has depth.
        /// </summary>
        [FieldOffset(5)]
        public BlittableBoolean IsDepthFormat;
    }
}
