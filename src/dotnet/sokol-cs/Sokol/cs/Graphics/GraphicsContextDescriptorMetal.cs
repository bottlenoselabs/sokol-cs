// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.Metal" /> back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptorMetal" /> is blittable to the C `sg_mtl_context_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsContextDescriptorMetal
    {
        /// <summary>
        ///     The pointer to a Metal device. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldevice">`MTLDevice`</a> on Apple's
        ///     developer documentation.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Device;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal render pass descriptor for the current
        ///     frame when rendering to the default framebuffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtlrenderpassdescriptor">`MTLRenderPassDescriptor`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr RenderPassDescriptorCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr RenderPassDescriptorUserDataCallback;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal drawable for the current
        ///     frame when rendering to the default framebuffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldrawable">`MTLDrawable`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr DrawableCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr DrawableUserDataCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(40)]
        public IntPtr UserData;
    }
}
