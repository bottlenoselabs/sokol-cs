using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Sokol.CoreAnimation;
using Sokol.Metal;
using Sokol.ObjCRuntime;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples
{
    public class RendererMetal : Renderer
    {
        private static int _initializedState;
        private static RendererMetal _instance;
        
        private CAMetalDrawable _drawable;
        private MTLRenderPassDescriptor _renderPassDescriptor;
        private CAMetalLayer _metalLayer;

        private GCHandle _getMetalRenderPassDescriptorGCHandle;
        private GCHandle _getMetalDrawableGCHandle;

        public override bool VerticalSyncIsEnabled
        {
            get => _metalLayer.displaySyncEnabled;
            set => _metalLayer.displaySyncEnabled = value;
        }

        public unsafe RendererMetal(ref sg_desc desc, IntPtr windowHandle) 
            : base(windowHandle)
        {
            EnsureIsNotAlreadyInitialized();
            
            _instance = this;

            // Setup the Metal "swapchain" by creating the SDL_Renderer even though we don't use it.
            // This reduces the PInvoke we need to do in C# and works for macOS, iOS, and tvOS.
            // See the the following function in `SDL_render_mental.m` in SDL code base:
            //     static SDL_Renderer * METAL_CreateRenderer(SDL_Window * window, Uint32 flags)
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
                // framebufferOnly is set to false by SDL for RenderReadPixels but we won't be using SDL renderer API
                framebufferOnly = true,
                // Enable vertical sync by default
                displaySyncEnabled = true
            };
            
            var getMetalRenderPassDescriptor = new GetPointerDelegate(StaticGetMetalRenderPassDescriptor);
            var getMetalDrawable = new GetPointerDelegate(StaticGetMetalDrawable);
            
            _getMetalRenderPassDescriptorGCHandle = GCHandle.Alloc(getMetalRenderPassDescriptor);
            _getMetalDrawableGCHandle = GCHandle.Alloc(getMetalDrawable);

            desc.mtl_device = (void*) _metalLayer.device.Handle;
            desc.mtl_renderpass_descriptor_cb = (void*) Marshal.GetFunctionPointerForDelegate(getMetalRenderPassDescriptor);
            desc.mtl_drawable_cb = (void*) Marshal.GetFunctionPointerForDelegate(getMetalDrawable);
            
            NativeLibrary.SetDllImportResolver(typeof(sokol_gfx).Assembly, ResolveLibrary);
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
        
        private static IntPtr ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            libraryName = libraryName.ToLower() switch
            {
                "sokol_gfx" => "sokol_gfx-metal",
                _ => libraryName
            };

            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }
    }
}