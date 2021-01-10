// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;

namespace Sokol
{
    /// <summary>
    ///     Information about a global variable (uniform) that is used in a <see cref="GraphicsShader" /> stage.
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
    ///         <see cref="GraphicsShaderUniformDescriptor" /> is only required for <see cref="GraphicsBackend.OpenGL" />,
    ///         <see cref="GraphicsBackend.OpenGLES2" />, and <see cref="GraphicsBackend.OpenGLES3" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderUniformDescriptor" /> is blittable to the C `sg_shader_uniform_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsShaderUniformDescriptor
    {
        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(0)]
        public byte* _name;

        /// <summary>
        ///     The uniform data type.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsShaderUniformType Type;

        /// <summary>
        ///     The number of elements in the array.
        /// </summary>
        [FieldOffset(12)]
        public int ArrayCount;

        /// <summary>
        ///     Gets or sets the name of the uniform. Must be set for
        ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
        ///     <see cref="GraphicsBackend.OpenGLES3" />. Optional for every other <see cref="GraphicsBackend" />
        ///     implementation.
        /// </summary>
        /// <value>The string with the name of uniform.</value>
        public string Name
        {
            readonly get => Native.GetStringFromBytePointer(_name);
            set => _name = Native.GetBytePointerFromString(value);
        }
    }
}
