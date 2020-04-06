// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of a <see cref="Shader" /> stage parameter.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ShaderUniformType" /> is blittable to the C `sg_uniform_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum ShaderUniformType
    {
        /// <summary>
        ///     An invalid uniform type. The size of the uniform in bytes is <c>0</c>.
        /// </summary>
        Invalid,

        /// <summary>
        ///     The size the uniform is 4 bytes. Most often the data is a <see cref="Float"/>.
        /// </summary>
        Float,

        /// <summary>
        ///     The size the uniform is 8 bytes. Most often the data is a <see cref="Float"/> x2.
        /// </summary>
        Float2,

        /// <summary>
        ///     The size the uniform is 12 bytes. Most often the data is a <see cref="Float"/> x3.
        /// </summary>
        Float3,

        /// <summary>
        ///     The size the uniform is 16 bytes. Most often the data is a <see cref="Float"/> x4.
        /// </summary>
        Float4,

        /// <summary>
        ///     The size the uniform is 64 bytes. Most often the data is a <see cref="Float"/> 4x4.
        /// </summary>
        Matrix4x4
    }
}
