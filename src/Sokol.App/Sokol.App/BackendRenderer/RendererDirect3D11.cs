// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Direct3D11;
using Sokol.Graphics;
using static SDL2.SDL;

namespace Sokol.App
{
    internal sealed class RendererDirect3D11 : BackendRenderer
    {
        public RendererDirect3D11(IntPtr windowHandle, ref GraphicsBackendDescriptor descriptor)
            : base(windowHandle, ref descriptor)
        {
            SDL_GetWindowSize(windowHandle, out var width, out var height);

            SDL2.SDL.D3D11_CreateSwapChain();
            SDL2.SDL.SDL_GetRenderDriverInfo(0, out var x);

            DXGI_SWAP_CHAIN_DESC swapChainDesc;
            swapChainDesc.BufferDesc.Width = (uint)width;
            swapChainDesc.BufferDesc.Height = (uint)height;
            swapChainDesc.BufferDesc.Format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
            swapChainDesc.BufferDesc.RefreshRate.Numerator = 60;
            swapChainDesc.BufferDesc.RefreshRate.Denominator = 1;

            SDL_SysWMinfo windowInfo = default;
            SDL_VERSION(out windowInfo.version);
            SDL_GetWindowWMInfo(windowHandle, ref windowInfo);

            swapChainDesc.OutputWindow = windowInfo.info.win.window;

            swapChainDesc.Windowed = true;
            swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_DISCARD;
            swapChainDesc.BufferCount = 1;
            swapChainDesc.SampleDesc.Count = 1;
            swapChainDesc.BufferUsage = DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT;
            /*
            .OutputWindow = state.hwnd,
                .Windowed = true,
                .SwapEffect = DXGI_SWAP_EFFECT_DISCARD,
                .BufferCount = 1,
                .SampleDesc = {
                .Count = state.sample_count,
                    .Quality = state.sample_count > 1 ? D3D11_STANDARD_MULTISAMPLE_PATTERN : 0
            },
            .BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT
        };*/
        }

        public override bool VerticalSyncIsEnabled { get; set; }

        public override (int width, int height) GetDrawableSize()
        {
            throw new NotImplementedException();
        }

        public override void Present()
        {
            throw new NotImplementedException();
        }

        protected override void ReleaseResources()
        {
            throw new NotImplementedException();
        }
    }
}
