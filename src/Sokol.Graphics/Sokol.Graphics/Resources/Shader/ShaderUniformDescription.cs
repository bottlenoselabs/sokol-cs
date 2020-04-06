// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about a global variable that is used in either the "per-vertex processing" stage or
    ///     "per-fragment processing" stage. Apart of <see cref="ShaderDescription" />. Only required for
    ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
    ///     <see cref="GraphicsBackend.OpenGLES3" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between drawing commands.
    ///     </para>
    ///     <para>
    ///         Only required for <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
    ///         <see cref="GraphicsBackend.OpenGLES3" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ShaderUniformDescription" /> is blittable to the C `sg_shader_uniform_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8, CharSet = CharSet.Ansi)]
    public struct ShaderUniformDescription
    {
        /// <summary>
        ///     The pointer to a C style string containing the name of the uniform. Must be set for
        ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
        ///     <see cref="GraphicsBackend.OpenGLES3" />. Optional for every other <see cref="GraphicsBackend" />
        ///     implementation.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Name;

        /// <summary>
        ///     The uniform data type.
        /// </summary>
        [FieldOffset(8)]
        public ShaderUniformType Type;

        /// <summary>
        ///     The number of elements in the array.
        /// </summary>
        [FieldOffset(12)]
        public int ArrayCount;
    }
}
