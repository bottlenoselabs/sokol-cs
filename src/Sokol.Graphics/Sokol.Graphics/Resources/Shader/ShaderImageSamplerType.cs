// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines how a <see cref="Image" /> is sampled in a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ShaderImageSamplerType" /> is blittable to the C `sg_sampler_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ShaderImageSamplerType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="ShaderImageSamplerType" /> is <see cref="ShaderImageSamplerType.Float" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The <see cref="Image "/> will be sampled as floating point numbers.
        /// </summary>
        Float,

        /// <summary>
        ///     The <see cref="Image" /> will be sampled as signed integers.
        /// </summary>
        SignedInteger,

        /// <summary>
        ///     The <see cref="Image" /> will be sampled as unsigned integers.
        /// </summary>
        UnsignedInteger,
    }
}
