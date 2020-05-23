// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.WebGPU" /> back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsBackendDescriptorWebGPU" /> is blittable to the C `sg_wgpu_context_desc` struct found
    ///         in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public struct GraphicsBackendDescriptorWebGPU
    {
        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Device;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr RenderViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr ResolveViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr DepthStencilViewCallback;
    }
}
