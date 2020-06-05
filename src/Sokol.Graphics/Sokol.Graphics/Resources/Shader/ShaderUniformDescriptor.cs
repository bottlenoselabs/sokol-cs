// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about a global variable that is used in either the "per-vertex processing" stage or
    ///     "per-fragment processing" stage. Apart of <see cref="ShaderDescriptor" />. Only required for
    ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
    ///     <see cref="GraphicsBackend.OpenGLES3" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="ShaderUniformDescriptor" />.
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
    ///         <see cref="ShaderUniformDescriptor" /> is blittable to the C `sg_shader_uniform_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8, CharSet = CharSet.Ansi)]
    public struct ShaderUniformDescriptor
    {
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

        [FieldOffset(0)]
        private IntPtr _name;

        /// <summary>
        ///     Gets or sets the name of the uniform. Must be set for
        ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
        ///     <see cref="GraphicsBackend.OpenGLES3" />. Optional for every other <see cref="GraphicsBackend" />
        ///     implementation.
        /// </summary>
        /// <value>The string with the name of uniform.</value>
        public string Name
        {
            readonly get => UnmanagedStringMemoryManager.GetString(_name);
            set => _name = UnmanagedStringMemoryManager.SetString(value);
        }
    }
}
