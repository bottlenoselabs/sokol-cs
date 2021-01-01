// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     The contents of a <see cref="GraphicsImage" /> as a two-dimensional array of <see cref="GraphicsImageSubContent" />
    ///     members. The first array dimension is for the cube-map faces and the second array dimension is for the mipmap levels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsImageContent" /> is blittable to the C `sg_image_content` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1536, Pack = 8)]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public unsafe struct GraphicsImageContent
    {
        [FieldOffset(0)]
        private fixed ulong _subImage[1536 / 8];

        /// <summary>
        ///     Gets the <see cref="GraphicsImageSubContent" /> by reference given the specified mipmap level and <see cref="GraphicsImageCubeFace" />.
        /// </summary>
        /// <param name="mipMapLevel">The zero-based mipmap level.</param>
        /// <param name="cubeFace">The <see cref="GraphicsImageCubeFace" />.</param>
        /// <returns>A <see cref="GraphicsImageSubContent" />.</returns>
        public readonly ref GraphicsImageSubContent SubImage(int mipMapLevel = 0, GraphicsImageCubeFace cubeFace = GraphicsImageCubeFace.PositiveX)
        {
            fixed (GraphicsImageContent* imageContent = &this)
            {
                var ptr = (GraphicsImageSubContent*)&imageContent->_subImage[0];
                var pointerOffset = ((int)cubeFace * 6) + mipMapLevel;
                return ref *(ptr + pointerOffset);
            }
        }
    }
}
