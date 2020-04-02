// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The contents of an <see cref="Image" /> as a two-dimensional array of <see cref="SubImageContent" /> members.
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
        private fixed ulong _subImage[16 * (int)sokol_gfx.sg_cube_face.SG_CUBEFACE_NUM * sokol_gfx.SG_MAX_MIPMAPS / 8];

        /// <summary>
        ///     Gets the <see cref="SubImageContent" /> of the <see cref="ImageContent" /> given the specified cube-face
        ///     index and mipmap level index.
        /// </summary>
        /// <param name="cubeFaceIndex">The cube-face index.</param>
        /// <param name="mipMapIndex">The mipmap level index.</param>
        /// <returns>A <see cref="SubImageContent" />.</returns>
        public ref SubImageContent SubImage(CubeFace cubeFaceIndex, int mipMapIndex)
        {
            fixed (ImageContent* imageContent = &this)
            {
                var ptr = (SubImageContent*)&imageContent->_subImage[0];
                var pointerOffset = ((int)cubeFaceIndex * (int)sokol_gfx.sg_cube_face.SG_CUBEFACE_NUM) + mipMapIndex;
                return ref *(ptr + pointerOffset);
            }
        }

        /// <summary>
        ///     Gets the <see cref="SubImageContent" /> of the <see cref="ImageContent" /> given the specified mipmap
        ///     level index.
        /// </summary>
        /// <param name="mipMapIndex">The mipmap level index.</param>
        /// <returns>A <see cref="SubImageContent" />.</returns>
        public ref SubImageContent SubImage(int mipMapIndex)
        {
            fixed (ImageContent* imageContent = &this)
            {
                var ptr = (SubImageContent*)&imageContent->_subImage[0];
                var pointerOffset = mipMapIndex;
                return ref *(ptr + pointerOffset);
            }
        }
    }
}
