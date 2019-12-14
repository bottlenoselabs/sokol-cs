using System;
using AppKit;
using Metal;
using CoreGraphics;
using CoreAnimation;

namespace Sokol.Samples
{
    public class RendererMetal : Renderer
    {
        private static MTLDevice _device;

        private CAMetalLayer _metalLayer;
        private readonly int _drawableWidth;
        private readonly int _drawableHeight;

        public override bool VerticalSyncIsEnabled
        {
            get => _metalLayer.displaySyncEnabled;
            set => _metalLayer.displaySyncEnabled = value;
        }
        
        public RendererMetal(ref SgDeviceDescription deviceDescription, IntPtr windowHandle, NSWindow nsWindow) 
            : base(windowHandle, Platform.macOS)
        {
            _device = MTLDevice.MTLCreateSystemDefaultDevice();

            var nsView = nsWindow.contentView;
            nsView.wantsLayer = true;
            
            var windowContentSize = nsView.frame.size;
            _drawableWidth = (int)windowContentSize.width;
            _drawableHeight = (int)windowContentSize.height;
            
            _metalLayer = CAMetalLayer.New();
            _metalLayer.device = _device;
            _metalLayer.framebufferOnly = true;
            _metalLayer.pixelFormat = MTLPixelFormat.BGRA8Unorm_sRGB;
            _metalLayer.drawableSize = new CGSize(_drawableWidth, _drawableHeight);
            nsView.layer = _metalLayer;
            
            SetSgDeviceMetalCallbacks(ref deviceDescription);
        }

        private static void SetSgDeviceMetalCallbacks(ref SgDeviceDescription deviceDescription)
        {
            deviceDescription.GetMetalDevice = GetMetalDevice;
            deviceDescription.GetMetalRenderPassDescriptor = GetMetalRenderPassDescriptor;
            deviceDescription.GetMetalDrawable = GetMetalDrawable;
        }

        private static IntPtr GetMetalDevice()
        {
            return _device;
        }

        private static IntPtr GetMetalRenderPassDescriptor()
        {
            return IntPtr.Zero;
        }
        
        private static IntPtr GetMetalDrawable()
        {
            return IntPtr.Zero;
        }

        public override (int width, int height) GetDrawableSize()
        {
            return (_drawableWidth, _drawableHeight);
        }

        public override void Present()
        {
            
        }

        protected override void ReleaseResources()
        {
            
        }
    }
}