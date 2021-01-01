// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.WebGPU" /> back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptorWebGPU" /> is blittable to the C `sg_wgpu_context_desc` struct found
    ///         in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 64, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsContextDescriptorWebGPU
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
        public IntPtr RenderViewUserdataCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr ResolveViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr ResolveViewUserDataCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(40)]
        public IntPtr DepthStencilViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(48)]
        public IntPtr DepthStencilViewUserDataCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(56)]
        public IntPtr UserData;
    }
}
