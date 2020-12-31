// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of <see cref="GraphicsImage" /> resources.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For <see cref="GraphicsBackend.OpenGLES2" />, <see cref="Texture3D" /> and <see cref="TextureArray" />
    ///         are not always supported. To check if they are supported inspect the result of <see cref="Graphics.Features" />
    ///         for <see cref="GraphicsFeatures.ImageType3D" /> and <see cref="GraphicsFeatures.ImageTypeArray" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsImageType" /> is blittable to the C `sg_image_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsImageType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsImageType" /> is <see cref="GraphicsImageType.Texture2D" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> has one slice with 2 dimensions: width and height. The slice may have mipmap
        ///     levels.
        /// </summary>
        Texture2D,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> has six <see cref="Texture2D" /> slices which must be square that represent
        ///     the faces of a cube. Each face (slice) may have mipmap levels.
        /// </summary>
        TextureCube,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> has one slice with 3 dimensions: width, height, and depth. The slice may have
        ///     mipmap levels.
        /// </summary>
        /// <para>
        ///     <see cref="Texture3D" /> is not always supported. To check if <see cref="Texture3D" /> is supported inspect the
        ///     result of <see cref="Graphics.Features" /> for <see cref="GraphicsFeatures.ImageType3D" />.
        /// </para>
        Texture3D,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> is one or many slices. Each slice may have mipmap levels but the number of mipmap
        ///     levels for each slice must all be the same.
        /// </summary>
        /// <para>
        ///     <see cref="TextureArray" /> is not always supported. To check if <see cref="TextureArray" /> is supported inspect the
        ///     result of <see cref="Graphics.Features" /> for <see cref="GraphicsFeatures.ImageTypeArray" />.
        /// </para>
        TextureArray
    }
}
