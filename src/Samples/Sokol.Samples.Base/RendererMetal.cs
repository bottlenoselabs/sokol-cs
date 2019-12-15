using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Sokol.AppKit;
using Sokol.CoreAnimation;
using Sokol.CoreGraphics;
using Sokol.Metal;
using Sokol.ObjCRuntime;
using static SDL2.SDL;

namespace Sokol.Samples
{
    public class RendererMetal : Renderer
    {
        private static int _isInitialized;
        private static RendererMetal _instance;

        private readonly NSView _nsView;
        private readonly MTLDevice _device;
        private CAMetalDrawable _drawable;
        private CAMetalLayer _metalLayer;
        private int _drawableWidth;
        private int _drawableHeight;
        private readonly bool _isHighDPI;

        public override bool VerticalSyncIsEnabled
        {
            get => _metalLayer.displaySyncEnabled;
            set => _metalLayer.displaySyncEnabled = value;
        }
        
        public RendererMetal(ref SgDeviceDescription deviceDescription, IntPtr windowHandle, NSWindow nsWindow) 
            : base(windowHandle)
        {
            EnsureIsNotAlreadyInitialized();
            
            _instance = this;
            _nsView = nsWindow.contentView;
            _device = MTLDevice.MTLCreateSystemDefaultDevice();
            
            _nsView.wantsLayer = true;

            var windowFlags = SDL_GetWindowFlags(windowHandle);
            _isHighDPI = (windowFlags & (uint) SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI) != 0;

            _metalLayer = CAMetalLayer.New();
            _metalLayer.device = _device;
            _metalLayer.framebufferOnly = true;
            _metalLayer.pixelFormat = MTLPixelFormat.BGRA8Unorm;
            _metalLayer.drawableSize = CalculateDrawableSize();
            _nsView.layer = _metalLayer;

            SetSgDeviceMetalCallbacks(ref deviceDescription);
            
            NativeLibrary.SetDllImportResolver(typeof(sokol_gfx).Assembly, ResolveLibrary);
        }
        
        private static void EnsureIsNotAlreadyInitialized()
        {
            var isInitialized = Interlocked.CompareExchange(ref _isInitialized, 1, 0);
            if (isInitialized != 0)
            {
                throw new InvalidOperationException("Only one `MetalRenderer` can be initialized.");
            }
        }

        private static void SetSgDeviceMetalCallbacks(ref SgDeviceDescription deviceDescription)
        {
            deviceDescription.MetalDevice = GetMTLDevice();
            deviceDescription.GetMetalRenderPassDescriptor = GetMTLRenderPassDescriptor;
            deviceDescription.GetMetalDrawable = GetMTLDrawable;
        }

        private static IntPtr GetMTLDevice()
        {
            return _instance._device;
        }

        private static IntPtr GetMTLRenderPassDescriptor()
        {
            return _instance.GetMetalRenderPassDescriptor();
        }
        
        private static IntPtr GetMTLDrawable()
        {
            return _instance._drawable;
        }

        private IntPtr GetMetalRenderPassDescriptor()
        {
            _metalLayer.drawableSize = CalculateDrawableSize();
            _drawable = _metalLayer.nextDrawable();
            
            var renderPassDescriptor = MTLRenderPassDescriptor.New();
            var colorAttachment = renderPassDescriptor.colorAttachments[0];
            colorAttachment.texture = _drawable.texture;

            return renderPassDescriptor; 
        }

        private CGSize CalculateDrawableSize()
        {
            SDL_GetWindowSize(WindowHandle, out var width, out var height);

            CGSize size;
            if (_isHighDPI)
            {
                var point = _nsView.convertToBacking(new CGPoint(width, height));
                size = new CGSize(point.x, point.y);
            }
            else
            {
                size = new CGSize(width, height);
            }

            _drawableWidth = (int) size.width;
            _drawableHeight = (int) size.height;

            return size;
        }

        public override (int width, int height) GetDrawableSize()
        {
            return (_drawableWidth, _drawableHeight);
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