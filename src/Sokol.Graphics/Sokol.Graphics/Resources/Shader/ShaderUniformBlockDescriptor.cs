// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about a set of global variables that are used in either the "per-vertex processing"
    ///     stage or "per-fragment processing" stage. Apart of <see cref="ShaderDescriptor" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="ShaderUniformBlockDescriptor" />.
    ///     </para>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between drawing commands.
    ///     </para>
    ///     <para>
    ///         Only required for <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
    ///         <see cref="GraphicsBackend.OpenGLES3" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ShaderUniformBlockDescriptor" /> is blittable to the C `sg_shader_uniform_block_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    public unsafe struct ShaderUniformBlockDescriptor
    {
        /// <summary>
        ///     The size of the uniform block in bytes.
        /// </summary>
        [FieldOffset(0)]
        public int Size;

        [FieldOffset(8)]
        internal fixed ulong _uniforms[16 * sokol_gfx.SG_MAX_UB_MEMBERS / 8];

        /// <summary>
        ///     Gets a <see cref="ShaderUniformBlockDescriptor" /> by reference given the specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="ShaderUniformBlockDescriptor" /> by reference.</returns>
        public ref ShaderUniformDescriptor Uniform(int index)
        {
            fixed (ShaderUniformBlockDescriptor* uniformBlockDescription = &this)
            {
                var ptr = (ShaderUniformDescriptor*)&uniformBlockDescription->_uniforms[0];
                return ref *(ptr + index);
            }
        }
    }
}
