// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
    ///         <see cref="GraphicsBackendDescriptor" /> is blittable to the C `sg_context_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 8)]
    public struct GraphicsBackendDescriptor
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
        public GraphicsBackendDescriptorOpenGL OpenGL;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.Metal" /> back-end.
        /// </summary>
        [FieldOffset(16)]
        public GraphicsBackendDescriptorMetal Metal;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.Direct3D11" /> back-end.
        /// </summary>
        [FieldOffset(40)]
        public GraphicsBackendDescriptorDirect3D11 Direct3D11;

        /// <summary>
        ///     The parameters used for initializing a <see cref="GraphicsBackend.WebGPU" /> back-end.
        /// </summary>
        [FieldOffset(72)]
        public sokol_gfx.sg_wgpu_context_desc WebGPU;
    }
}
