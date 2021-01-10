// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Runtime information about available optional features supported by hardware for the
    ///     <see cref="GraphicsBackend" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Call <see cref="Graphics.Features" /> to get the <see cref="GraphicsFeatures" /> for the current
    ///         <see cref="GraphicsBackend" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsFeatures" /> is blittable to the C `sg_features` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 7, Pack = 1)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsFeatures
    {
        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether hardware instancing is supported.
        /// </summary>
        [FieldOffset(0)]
        public readonly CBool Instancing;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the frame buffer and texture origin is in top left
        ///     corner.
        /// </summary>
        [FieldOffset(1)]
        public readonly CBool OriginTopLeft;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether off-screen render passes can have multiple render
        ///     targets attached.
        /// </summary>
        [FieldOffset(2)]
        public readonly CBool MultipleRenderTargets;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether off-screen render passes support multi-sample
        ///     anti-aliasing (MSAA).
        /// </summary>
        [FieldOffset(3)]
        public readonly CBool MsaaRenderTargets;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the creation of <see cref="GraphicsImageType.Texture3D" />
        ///     <see cref="GraphicsImage" /> resources is supported.
        /// </summary>
        [FieldOffset(4)]
        public readonly CBool ImageType3D;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the creation of <see cref="GraphicsImageType.TextureArray" />
        ///     <see cref="GraphicsImage" /> resources is supported.
        /// </summary>
        [FieldOffset(5)]
        public readonly CBool ImageTypeArray;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether border color and clamp-to-border UV-wrap mode is
        ///     supported.
        /// </summary>
        [FieldOffset(6)]
        public readonly CBool ImageClampToBorder;
    }
}
