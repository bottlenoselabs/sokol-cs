// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a 3D graphics API back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptor" /> is blittable to the C `sg_context_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 8)]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public struct GraphicsContextDescriptor
    {
        /// <summary>
        ///     The color <see cref="PixelFormat" /> of the frame buffer.
        /// </summary>
        /// <remarks>
        ///     <para>The frame buffer must be created outside `sokol_gfx` before initializing.</para>
        /// </remarks>
        [FieldOffset(0)]
        public PixelFormat ColorFormat;

        /// <summary>
        ///     The depth <see cref="PixelFormat" /> of the frame buffer.
        /// </summary>
        /// <remarks>
        ///     <para>The frame buffer must be created outside `sokol_gfx` before initializing.</para>
        /// </remarks>
        [FieldOffset(4)]
        public PixelFormat DepthFormat;

        /// <summary>
        ///     The number of samples used for the frame buffer when multi-sample anti-aliasing (MSAA) is enabled.
        /// </summary>
        /// <remarks>
        ///     <para>The frame buffer must be created outside `sokol_gfx` before initializing.</para>
        /// </remarks>
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
        [FieldOffset(40)]
        public GraphicsContextDescriptorDirect3D11 Direct3D11;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.WebGPU" /> back-end.
        /// </summary>
        [FieldOffset(72)]
        public GraphicsContextDescriptorWebGPU WebGPU;
    }
}
