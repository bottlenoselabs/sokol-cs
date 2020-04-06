// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of <see cref="Image" /> resources.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For <see cref="GraphicsBackend.OpenGLES2" />, <see cref="Texture3D" /> and <see cref="TextureArray" />
    ///         are not always supported. To check if they are supported call <see cref="GraphicsDevice.QueryFeatures" /> and
    ///         inspect the values for <see cref="GraphicsFeatures.ImageType3D" /> and <see cref="GraphicsFeatures.ImageTypeArray" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ImageType" /> is blittable to the C `sg_image_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ImageType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ImageType" /> is <see cref="ImageType.Texture2D" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The <see cref="Image" /> has one sub-image with 2 dimensions: width and height. The sub-image may have
        ///     mipmap levels.
        /// </summary>
        Texture2D,

        /// <summary>
        ///     The <see cref="Image" /> has six <see cref="Texture2D" /> sub-images which must be square that represent
        ///     the faces of a cube. Each face may have mipmap levels.
        /// </summary>
        TextureCube,

        /// <summary>
        ///     The <see cref="Image" /> has one sub-image with 3 dimensions: width, height, and depth. The sub-image may
        ///     have mipmap levels.
        /// </summary>
        Texture3D,

        /// <summary>
        ///     The <see cref="Image" /> is one or many sub-images. Each sub-image may have mipmap levels but the number
        ///     of mipmap levels for each sub-image must all be the same.
        /// </summary>
        TextureArray
    }
}
