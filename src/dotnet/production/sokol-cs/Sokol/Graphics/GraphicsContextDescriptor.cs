// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Parameters for initializing a 3D graphics API back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptor" /> is blittable to the C `sg_context_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 184, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    public struct GraphicsContextDescriptor
    {
        /// <summary>
        ///     The color <see cref="GraphicsPixelFormat" /> of the framebuffer.
        /// </summary>
        [FieldOffset(0)]
        public GraphicsPixelFormat ColorFormat;

        /// <summary>
        ///     The depth <see cref="GraphicsPixelFormat" /> of the framebuffer.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsPixelFormat DepthFormat;

        /// <summary>
        ///     The number of samples used for the framebuffer when multi-sample anti-aliasing (MSAA) is enabled.
        /// </summary>
        [FieldOffset(8)]
        public int SampleCount;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.OpenGL" />,
        ///     <see cref="GraphicsBackend.OpenGLES2" />, or <see cref="GraphicsBackend.OpenGLES3" /> back-end.
        /// </summary>
        [FieldOffset(12)]
        public GraphicsContextDescriptorOpenGL GL;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.Metal" /> back-end.
        /// </summary>
        [FieldOffset(16)]
        public GraphicsContextDescriptorMetal Metal;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.Direct3D11" /> back-end.
        /// </summary>
        [FieldOffset(64)]
        public GraphicsContextDescriptorDirect3D11 Direct3D11;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.WebGPU" /> back-end.
        /// </summary>
        [FieldOffset(120)]
        public GraphicsContextDescriptorWebGPU WebGPU;
    }
}
