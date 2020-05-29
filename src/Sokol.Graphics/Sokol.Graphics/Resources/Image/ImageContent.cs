// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeInternal

namespace Sokol.Graphics
{
    /// <summary>
    ///     The contents of an <see cref="Image" /> as a two-dimensional array of <see cref="ImageSubContent" /> members.
    ///     The first array dimension is the cube-map faces and the second array dimension is the mipmap levels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ImageContent" /> is blittable to the C `sg_image_content` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1536, Pack = 8)]
    public unsafe struct ImageContent
    {
        [FieldOffset(0)]
        private fixed ulong _subImage[16 * 6 * 16 / 8];

        /// <summary>
        ///     Gets the <see cref="ImageSubContent" /> by reference given the specified cube-face index and mipmap
        ///     level index.
        /// </summary>
        /// <param name="cubeFaceIndex">The cube-face index.</param>
        /// <param name="mipMapIndex">The zero-based mipmap level index.</param>
        /// <returns>A <see cref="ImageSubContent" />.</returns>
        public readonly ref ImageSubContent SubImage(ImageCubeFaceIndex cubeFaceIndex, int mipMapIndex = 0)
        {
            fixed (ImageContent* imageContent = &this)
            {
                var ptr = (ImageSubContent*)&imageContent->_subImage[0];
                var pointerOffset = ((int)cubeFaceIndex * 6) + mipMapIndex;
                return ref *(ptr + pointerOffset);
            }
        }

        /// <summary>
        ///     Gets the <see cref="ImageSubContent" /> by reference given the specified mipmap level index.
        /// </summary>
        /// <param name="mipMapIndex">The zero-based mipmap level index.</param>
        /// <returns>A <see cref="ImageSubContent" />.</returns>
        public readonly ref ImageSubContent SubImage(int mipMapIndex = 0)
        {
            fixed (ImageContent* imageContent = &this)
            {
                var ptr = (ImageSubContent*)&imageContent->_subImage[0];
                var pointerOffset = mipMapIndex;
                return ref *(ptr + pointerOffset);
            }
        }
    }
}
