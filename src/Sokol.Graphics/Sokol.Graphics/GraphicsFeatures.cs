// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Runtime information about available optional features supported by hardware for the
    ///     <see cref="GraphicsBackend" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsFeatures" /> is blittable to the C `sg_features` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 7, Pack = 1)]
    public readonly struct GraphicsFeatures
    {
        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether hardware instancing is supported.
        /// </summary>
        [FieldOffset(0)]
        public readonly BlittableBoolean Instancing;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the framebuffer and texture origin is in top left
        ///     corner.
        /// </summary>
        [FieldOffset(1)]
        public readonly BlittableBoolean OriginTopLeft;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether off-screen render passes can have multiple render
        ///     targets attached.
        /// </summary>
        [FieldOffset(2)]
        public readonly BlittableBoolean MultipleRenderTargets;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether off-screen render passes support multi-sample
        ///     anti-aliasing (MSAA).
        /// </summary>
        [FieldOffset(3)]
        public readonly BlittableBoolean MsaaRenderTargets;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the creation of <see cref="ImageType.Texture3D" />
        ///     <see cref="Image" /> resources is supported.
        /// </summary>
        [FieldOffset(4)]
        public readonly BlittableBoolean ImageType3D;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the creation of <see cref="ImageType.TextureArray" />
        ///     <see cref="Image" /> resources is supported.
        /// </summary>
        [FieldOffset(5)]
        public readonly BlittableBoolean ImageTypeArray;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether border color and clamp-to-border UV-wrap mode is
        ///     supported.
        /// </summary>
        [FieldOffset(6)]
        public readonly BlittableBoolean ImageClampToBorder;
    }
}
