// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol
{
    /// <summary>
    ///     Defines the bit representation for samples of a <see cref="GraphicsImage" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsShaderImageSamplerType" /> is blittable to the C `sg_sampler_type` enum found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Same meaning as name of built in type.")]
    public enum GraphicsShaderImageSamplerType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsShaderImageSamplerType" /> is <see cref="GraphicsShaderImageSamplerType.Float" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The <see cref="GraphicsImage " /> will be sampled as floating point numbers.
        /// </summary>
        Float,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> will be sampled as signed integers.
        /// </summary>
        SignedInteger,

        /// <summary>
        ///     The <see cref="GraphicsImage" /> will be sampled as unsigned integers.
        /// </summary>
        UnsignedInteger
    }
}
