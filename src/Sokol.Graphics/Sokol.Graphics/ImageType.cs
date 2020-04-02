// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of <see cref="Image"/> resources.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For <see cref="GraphicsBackend.OpenGLES2" />, <see cref="Texture3D" /> and <see cref="TextureArray" />
    ///         are not always supported. To check if they are supported call <see cref="Sg.QueryFeatures" /> and
    ///         inspect the results for <see cref="Features.ImageType3D" /> and <see cref="Features.ImageTypeArray" />.
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
        ///     The <see cref="Image"/> resource has one sub-image with 2 dimensions: width and height. Mipmaps may
        ///     be used.
        /// </summary>
        Texture2D,

        /// <summary>
        ///     The <see cref="Image"/> resource has six <see cref="Texture2D"/> sub-images which must be square that
        ///     represent the faces of a cube. Mipmaps may be used.
        /// </summary>
        TextureCube,

        /// <summary>
        ///     The <see cref="Image"/> resource has one sub-image with 3 dimensions: width, height, and depth. Mipmaps
        ///     may be used.
        /// </summary>
        Texture3D,

        /// <summary>
        ///     The <see cref="Image"/> resource has one or many sub-images. Mipmaps may be used but each mipmap has
        ///     the same number of levels.
        /// </summary>
        TextureArray
    }
}
