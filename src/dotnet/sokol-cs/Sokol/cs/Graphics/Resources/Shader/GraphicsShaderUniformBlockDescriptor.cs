// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about a set of global variables (uniforms) that is used in a <see cref="GraphicsShader" /> stage.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create a
    ///         <see cref="GraphicsShaderUniformDescriptor" />.
    ///     </para>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between multiple drawing commands.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderUniformBlockDescriptor" /> is only required for <see cref="GraphicsBackend.OpenGL" />,
    ///         <see cref="GraphicsBackend.OpenGLES2" />, and <see cref="GraphicsBackend.OpenGLES3" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderUniformBlockDescriptor" /> is blittable to the C `sg_shader_uniform_block_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public unsafe struct GraphicsShaderUniformBlockDescriptor
    {
        /// <summary>
        ///     The size of the uniform block in bytes.
        /// </summary>
        [FieldOffset(0)]
        public int Size;

        [FieldOffset(8)]
        private fixed ulong _uniforms[16 * GraphicsConstants.MaximumUniformBufferMembers / 8];

        /// <summary>
        ///     Gets a <see cref="GraphicsShaderUniformBlockDescriptor" /> by reference given the specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsShaderUniformBlockDescriptor" /> by reference.</returns>
        public readonly ref GraphicsShaderUniformDescriptor Uniform(int index = 0)
        {
            fixed (GraphicsShaderUniformBlockDescriptor* uniformBlockDescription = &this)
            {
                var ptr = (GraphicsShaderUniformDescriptor*)&uniformBlockDescription->_uniforms[0];
                return ref *(ptr + index);
            }
        }
    }
}
