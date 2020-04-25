// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using CoreAnimation;
using Metal;
using ObjCRuntime;
using Sokol.Graphics;
using static SDL2.SDL;

namespace Sokol.App
{
    internal sealed class RendererMetal : BackendRenderer
    {
        private static int _initializedState;
        private static RendererMetal _instance = null!;

        private CAMetalDrawable _drawable;
        private GCHandle _getMetalDrawableGCHandle;

        private GCHandle _getMetalRenderPassDescriptorGCHandle;
        private readonly CAMetalLayer _metalLayer;
        private MTLRenderPassDescriptor _renderPassDescriptor;

        public override bool VerticalSyncIsEnabled
        {
            get => _metalLayer.displaySyncEnabled;
            set => _metalLayer.displaySyncEnabled = value;
        }

        public RendererMetal(ref GraphicsDescriptor descriptor, IntPtr windowHandle)
            : base(windowHandle)
        {
            EnsureIsNotAlreadyInitialized();

            _instance = this;

            // TODO: THIS WILL BRAKE IN 2.0.12+: Use SDL_Metal_GetLayer and SDL_Metal_GetDrawableSize instead
            // Setup the Metal "swapchain" by creating the SDL_Renderer even though we don't use it
            // This reduces the PInvoke we need to do in C# and works for macOS, iOS, and tvOS
            SDL_SetHint("SDL_HINT_RENDER_DRIVER", "metal");
            const SDL_RendererFlags sdlRendererFlags = SDL_RendererFlags.SDL_RENDERER_ACCELERATED;
            var sdlRendererHandle = SDL_CreateRenderer(windowHandle, -1, sdlRendererFlags);
            var metalLayerHandle = SDL_RenderGetMetalLayer(sdlRendererHandle);
            SDL_DestroyRenderer(sdlRendererHandle);

            if (metalLayerHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("CAMetalLayer is a null pointer.");
            }

            _metalLayer = new CAMetalLayer(metalLayerHandle)
            {
                framebufferOnly = true,
                displaySyncEnabled = true
            };

            var getMetalRenderPassDescriptor = new GetPointerDelegate(StaticGetMetalRenderPassDescriptor);
            var getMetalDrawable = new GetPointerDelegate(StaticGetMetalDrawable);

            _getMetalRenderPassDescriptorGCHandle = GCHandle.Alloc(getMetalRenderPassDescriptor);
            _getMetalDrawableGCHandle = GCHandle.Alloc(getMetalDrawable);

            descriptor.MTLDevice = _metalLayer.device.Handle;
            descriptor.MTLRenderPassDescriptorCallback =
                Marshal.GetFunctionPointerForDelegate(getMetalRenderPassDescriptor);
            descriptor.MTLDrawableCallback = Marshal.GetFunctionPointerForDelegate(getMetalDrawable);
        }

        public override (int width, int height) GetDrawableSize()
        {
            var drawableSize = _metalLayer.drawableSize;
            var width = (int)drawableSize.width;
            var height = (int)drawableSize.height;
            return (width, height);
        }

        public override void Present()
        {
            // Presenting the drawable is done for us in sg_commit
        }

        protected override void ReleaseResources()
        {
            if (_drawable.Handle != IntPtr.Zero)
            {
                NSObject.release(_drawable);
            }

            if (_metalLayer.Handle != IntPtr.Zero)
            {
                NSObject.release(_metalLayer.Handle);
            }

            if (_getMetalRenderPassDescriptorGCHandle.IsAllocated)
            {
                _getMetalRenderPassDescriptorGCHandle.Free();
            }

            if (_getMetalDrawableGCHandle.IsAllocated)
            {
                _getMetalDrawableGCHandle.Free();
            }
        }

        private static void EnsureIsNotAlreadyInitialized()
        {
            var state = Interlocked.CompareExchange(ref _initializedState, 1, 0);
            if (state != 0)
            {
                throw new InvalidOperationException($"Only one `{nameof(RendererMetal)}` can be initialized.");
            }
        }

        private static IntPtr StaticGetMetalRenderPassDescriptor()
        {
            // This callback is invoked by sokol_gfx native library in `sg_begin_default_pass()`
            return _instance.GetMetalRenderPassDescriptor();
        }

        private static IntPtr StaticGetMetalDrawable()
        {
            // This callback is invoked by sokol_gfx native library in `sg_end_pass()` for the default pass
            return _instance._drawable;
        }

        private IntPtr GetMetalRenderPassDescriptor()
        {
            if (_drawable.Handle != IntPtr.Zero)
            {
                NSObject.release(_drawable);
            }

            _drawable = _metalLayer.nextDrawable();

            if (_renderPassDescriptor != IntPtr.Zero)
            {
                NSObject.release(_renderPassDescriptor);
            }

            _renderPassDescriptor = MTLRenderPassDescriptor.New();

            var colorAttachment = _renderPassDescriptor.colorAttachments[0];
            colorAttachment.texture = _drawable.texture;

            return _renderPassDescriptor;
        }
    }
}
