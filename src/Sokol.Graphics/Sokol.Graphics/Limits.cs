// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Runtime information about hardware limitations.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Limits" /> is blittable to the C `sg_limits` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public struct Limits
    {
        /// <summary>
        ///     The maximum width or height of an <see cref="ImageType.Texture2D" /> <see cref="Image" />.
        /// </summary>
        [FieldOffset(0)]
        public uint MaximumImageSize2D;

        /// <summary>
        ///     The maximum width or height of a <see cref="ImageType.TextureCube" /> <see cref="Image" />.
        /// </summary>
        [FieldOffset(4)]
        public uint MaximumImageSizeCube;

        /// <summary>
        ///     The maximum width, height, or depth of a <see cref="ImageType.Texture3D" /> <see cref="Image" />.
        /// </summary>
        [FieldOffset(8)]
        public uint MaximumImageSize3D;

        /// <summary>
        ///     The maximum width or height of each texture in a <see cref="ImageType.TextureArray" />
        ///     <see cref="Image" />.
        /// </summary>
        [FieldOffset(12)]
        public uint MaximumImageSizeArray;

        /// <summary>
        ///     The maximum number of layers for a <see cref="ImageType.TextureArray" /> <see cref="Image" />.
        /// </summary>
        [FieldOffset(16)]
        public uint MaximumImageArrayLayerCount;

        /// <summary>
        ///     The maximum number of supported vertex attribute locations.
        /// </summary>
        [FieldOffset(20)]
        public uint MaximumVertexAttributeCount;
    }
}
