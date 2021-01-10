// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
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
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
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
        public CBool ForceGLES2;
    }
}
