// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different border colors that can be used when texels of a texture <see cref="Image" /> are
    ///     mapped into pixels when rendering with a <see cref="Shader" />, also known as sampling.
    /// </summary>
    public enum ImageBorderColor
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ImageBorderColor" /> is <see cref="TransparentBlack" />.
        /// </summary>
        Default,

        /// <summary>
        ///     RGBA #00000000.
        /// </summary>
        TransparentBlack,

        /// <summary>
        ///     RGBA #000000FF.
        /// </summary>
        OpaqueBlack,

        /// <summary>
        ///     RGBA #FFFFFFFF.
        /// </summary>
        OpaqueWhite
    }
}
