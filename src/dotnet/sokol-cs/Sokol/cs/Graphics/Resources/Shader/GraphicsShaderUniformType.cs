// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the types of a <see cref="GraphicsShader" /> stage global variable (uniform).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between multiple drawing commands.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderUniformType" /> is blittable to the C `sg_uniform_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Same meaning as name of built in type.")]
    public enum GraphicsShaderUniformType
    {
        /// <summary>
        ///     An invalid uniform type. The size of the uniform in bytes is <c>0</c>.
        /// </summary>
        Invalid,

        /// <summary>
        ///     The size the uniform is 4 bytes. Most often the data is a <see cref="Float" />.
        /// </summary>
        Float,

        /// <summary>
        ///     The size the uniform is 8 bytes. Most often the data is two <see cref="Float" />s or a <seealso cref="Vector2" />.
        /// </summary>
        Float2,

        /// <summary>
        ///     The size the uniform is 12 bytes. Most often the data is three <see cref="Float" />s or a <see cref="Vector3" />.
        /// </summary>
        Float3,

        /// <summary>
        ///     The size the uniform is 16 bytes. Most often the data is four <see cref="Float" />s or a <see cref="Vector4" />.
        /// </summary>
        Float4,

        /// <summary>
        ///     The size the uniform is 64 bytes. Most often the data is a <see cref="System.Numerics.Matrix4x4" />.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Matrix name.")]
        Matrix4x4
    }
}
