// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Runtime information about available optional features.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Features" /> is blittable to the C `sg_features` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 7, Pack = 1)]
    public readonly struct Features
    {
        /// <summary>
        ///     Hardware instancing is supported.
        /// </summary>
        [FieldOffset(0)]
        public readonly BlittableBoolean Instancing;

        /// <summary>
        ///     Frame-buffer and texture origin is in top left corner.
        /// </summary>
        [FieldOffset(1)]
        public readonly BlittableBoolean OriginTopLeft;

        /// <summary>
        ///     Off-screen render passes can have multiple render targets attached.
        /// </summary>
        [FieldOffset(2)]
        public readonly BlittableBoolean MultipleRenderTargets;

        /// <summary>
        ///     Off-screen render passes support multi-sample anti-aliasing (MSAA).
        /// </summary>
        [FieldOffset(3)]
        public readonly BlittableBoolean MsaaRenderTargets;

        /// <summary>
        ///     Creation of <see cref="ImageType.Texture3D" /> <see cref="Image"/> resources is supported.
        /// </summary>
        [FieldOffset(4)]
        public readonly BlittableBoolean ImageType3D;

        /// <summary>
        ///     Creation of <see cref="ImageType.TextureArray" /> <see cref="Image"/> resources is supported.
        /// </summary>
        [FieldOffset(5)]
        public readonly BlittableBoolean ImageTypeArray;

        /// <summary>
        ///     Border color and clamp-to-border UV-wrap mode is supported.
        /// </summary>
        [FieldOffset(6)]
        public readonly BlittableBoolean ImageClampToBorder;
    }
}
