// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.OpenGL" />,
    ///     <see cref="GraphicsBackend.OpenGLES2" />, or <see cref="GraphicsBackend.OpenGLES3" /> back-end with
    ///     `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptorOpenGL" /> is blittable to the C `sg_gl_context_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct GraphicsContextDescriptorOpenGL
    {
        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the <see cref="GraphicsBackend.OpenGLES3" /> back-end
        ///     will fallback to <see cref="GraphicsBackend.OpenGLES2" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         It is useful to set as <c>true</c> when a browser doesn't support a WebGL 2.0 context,
        ///         allowing `sokol_gfx` to fall back to using a WebGL 1.0 context.
        ///     </para>
        /// </remarks>
        [FieldOffset(0)]
        public BlittableBoolean ForceGLES2;
    }
}
