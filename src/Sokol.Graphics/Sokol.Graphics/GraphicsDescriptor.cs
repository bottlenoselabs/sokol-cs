// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsDescriptor" /> is blittable to the C `sg_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 8)]
    public struct GraphicsDescriptor
    {
        /// <summary>
        ///     The maximum number of available <see cref="Buffer" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(4)]
        public int BufferPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Image" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(8)]
        public int ImagePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Shader" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(12)]
        public int ShaderPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Pipeline" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(16)]
        public int PipelinePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Pass" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(20)]
        public int PassPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Context" /> instances for the life-time of a `sokol_gfx`
        ///     application.
        /// </summary>
        [FieldOffset(24)]
        public int ContextPoolSize;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the <see cref="GraphicsBackend.OpenGLES3" /> backend will
        ///     fallback to <see cref="GraphicsBackend.OpenGLES2" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         It is useful to set as <c>true</c> when a browser doesn't support a WebGL 2.0 context,
        ///         allowing `sokol_gfx` to fall back to using a WebGL 1.0 context.
        ///     </para>
        /// </remarks>
        [FieldOffset(28)]
        public BlittableBoolean ForceGLES2;

        /// <summary>
        ///     The pointer to a Metal device. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldevice">`MTLDevice`</a> on Apple's
        ///     developer documentation.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr MTLDevice;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal render pass descriptor for the current
        ///     frame when rendering to the default frame buffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtlrenderpassdescriptor">`MTLRenderPassDescriptor`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(40)]
        public IntPtr MTLRenderPassDescriptorCallback;

        /// <summary>
        ///     The pointer to a C style callback function to obtain a Metal drawable for the current
        ///     frame when rendering to the default frame buffer. For more information see
        ///     <a href="https://developer.apple.com/documentation/metal/mtldrawable">`MTLDrawable`</a>
        ///     on Apple's developer documentation.
        /// </summary>
        [FieldOffset(48)]
        public IntPtr MTLDrawableCallback;

        /// <summary>
        ///     The size in bytes of the global uniform buffer. This must be big enough to hold all uniform block
        ///     updates for a single frame, the default value is 4 MiB (4 * 1024 * 1024).
        /// </summary>
        [FieldOffset(56)]
        public int MTLGlobalUniformBufferSize;

        /// <summary>
        ///     The number of slots in the sampler cache. The Metal back-end will share texture samplers with the
        ///     same state in this cache. The default value is 64.
        /// </summary>
        [FieldOffset(60)]
        public int MTLSamplerCacheSize;

        /// <summary>
        ///     The pointer to a D3D11 device. For more information see
        ///     <a href="https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device">`ID3DDevice`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(64)]
        public IntPtr D3D11Device;

        /// <summary>
        ///     The pointer to a D3D11 device context. For more information see
        ///     <a href="https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11devicecontext">`ID3DDeviceContext`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(72)]
        public IntPtr D3D11DeviceContext;

        /// <summary>
        ///     The pointer to a C style callback function to obtain the render target sub-resources of the default
        ///     frame buffer. For more information see
        ///     <a href="https://docs.microsoft.com/en-ca/windows/win32/api/d3d11/nn-d3d11-id3d11rendertargetview">`ID3D11RenderTargetView`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(80)]
        public IntPtr D3D11RenderTargetViewCallback;

        /// <summary>
        ///     The pointer to a C style callback function to obtain an accessor to a texture resource for depth-stencil
        ///     testing. For more information see
        ///     <a href="https://docs.microsoft.com/en-ca/windows/win32/api/d3d11/nn-d3d11-id3d11depthstencilview">`ID3D11DepthStencilView`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(88)]
        public IntPtr D3D11DepthStencilViewCallback;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        internal uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(96)]
        internal uint _endCanary;
    }
}
