// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing a <see cref="GraphicsBackend.Direct3D11" /> back-end with `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsContextDescriptorDirect3D11" /> is blittable to the C `sg_d3d11_context_desc` struct
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 56, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsContextDescriptorDirect3D11
    {
        /// <summary>
        ///     The pointer to a D3D11 device. For more information see
        ///     <a href="https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device">`ID3DDevice`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Device;

        /// <summary>
        ///     The pointer to a D3D11 device context. For more information see
        ///     <a href="https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11devicecontext">`ID3DDeviceContext`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr DeviceContext;

        /// <summary>
        ///     The pointer to a C style callback function to obtain the render target sub-resources of the default
        ///     frame buffer. For more information see
        ///     <a href="https://docs.microsoft.com/en-ca/windows/win32/api/d3d11/nn-d3d11-id3d11rendertargetview">`ID3D11RenderTargetView`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr RenderTargetViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr RenderTargetViewUserDataCallback;

        /// <summary>
        ///     The pointer to a C style callback function to obtain an accessor to a texture resource for depth-stencil
        ///     testing. For more information see
        ///     <a href="https://docs.microsoft.com/en-ca/windows/win32/api/d3d11/nn-d3d11-id3d11depthstencilview">`ID3D11DepthStencilView`</a>
        ///     on Microsoft's developer documentation.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr DepthStencilViewCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(40)]
        public IntPtr DepthStencilViewUserDataCallback;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(48)]
        public IntPtr UserData;
    }
}
