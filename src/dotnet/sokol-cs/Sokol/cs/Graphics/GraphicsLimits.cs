// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Runtime information about hardware limitations for the <see cref="GraphicsBackend" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsLimits" /> is blittable to the C `sg_limits` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    public readonly struct GraphicsLimits
    {
        /// <summary>
        ///     The maximum width or height of a <see cref="GraphicsImageType.Texture2D" /> <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint MaximumImageSize2D;

        /// <summary>
        ///     The maximum width or height of a <see cref="GraphicsImageType.TextureCube" /> <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(4)]
        public readonly uint MaximumImageSizeCube;

        /// <summary>
        ///     The maximum width, height, or depth of a <see cref="GraphicsImageType.Texture3D" /> <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(8)]
        public readonly uint MaximumImageSize3D;

        /// <summary>
        ///     The maximum width or height of each texture in a <see cref="GraphicsImageType.TextureArray" />
        ///     <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(12)]
        public readonly uint MaximumImageSizeArray;

        /// <summary>
        ///     The maximum number of layers for a <see cref="GraphicsImageType.TextureArray" /> <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(16)]
        public readonly uint MaximumImageArrayLayerCount;

        /// <summary>
        ///     The maximum number of supported vertex attribute locations.
        /// </summary>
        [FieldOffset(20)]
        public readonly uint MaximumVertexAttributeCount;
    }
}
