// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.Metal" /> back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsBackendDescriptorMetal" /> is blittable to the C `sg_mtl_context_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public struct GraphicsBackendDescriptorMetal
    {
        /// <summary>
        ///     The pointer to a Metal device. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldevice">`MTLDevice`</a> on Apple's
        ///     developer documentation.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr MTLDevice;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal render pass descriptor for the current
        ///     frame when rendering to the default frame buffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtlrenderpassdescriptor">`MTLRenderPassDescriptor`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr MTLRenderPassDescriptorCallback;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal drawable for the current
        ///     frame when rendering to the default frame buffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldrawable">`MTLDrawable`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr MTLDrawableCallback;
    }
}
