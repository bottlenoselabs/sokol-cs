using System;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples
{
    public abstract class App : IDisposable
    {
        private readonly SgDevice _device;
        private readonly Renderer _renderer;
        private bool _isExiting;

        public IntPtr WindowHandle { get; }
        
        public Platform Platform { get; }
        
        public GraphicsBackend GraphicsBackend { get; }

        protected App(SgDeviceDescription? deviceDescription = null)
        {
            SDL_Init(SDL_INIT_VIDEO);
            var platformString = SDL_GetPlatform();
            Platform = GetPlatformFrom(platformString);
            
            var description = deviceDescription ?? new SgDeviceDescription();
            GraphicsBackend = description.GraphicsBackend;
            
            WindowHandle = CreateWindow();
            _renderer = CreateRenderer(WindowHandle, GraphicsBackend, Platform);

            _device = new SgDevice(description);
        }

        private static Platform GetPlatformFrom(string @string)
        {
            return @string switch
            {
                "Windows" => Platform.Windows,
                "Mac OS X" => Platform.macOS,
                "Linux" => Platform.Linux,
                "iOS" => Platform.iOS,
                "Android" => Platform.Android,
                _ => Platform.Unknown
            };
        }

        private IntPtr CreateWindow()
        {
            var windowFlags = SDL_WindowFlags.SDL_WINDOW_HIDDEN | SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
            
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;
            }
            
            var windowHandle = SDL_CreateWindow("", 
                SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 
                800, 600, windowFlags);

            if (windowHandle == IntPtr.Zero)
            {
                throw new ApplicationException("Failed to create a window with SDL2.");
            }

            return windowHandle;
        }
        
        private static Renderer CreateRenderer(IntPtr windowHandle, GraphicsBackend backend, Platform platform)
        {
            if (backend == GraphicsBackend.OpenGL)
            { 
                return new RendererOpenGL(windowHandle, platform);
            }
            
            throw new NotImplementedException();

//            var sysWmInfo = new SDL_SysWMinfo();
//            SDL_GetVersion(out sysWmInfo.version);
//            SDL_GetWindowWMInfo(windowHandle, ref sysWmInfo);
//                
//            switch (sysWmInfo.subsystem)
//            {
//                case SDL_SYSWM_TYPE.SDL_SYSWM_COCOA:
//                    
//                    var window = new NSWindow(sysWmInfo.info.cocoa.window);
//                    _renderer = new RendererMetal(window);
//                    var windowContentSize = window.contentView.frame.size;
//                    width = (uint)windowContentSize.width;
//                    height = (uint)windowContentSize.height;
//                    var contentView = window.contentView;
//                    contentView.wantsLayer = true;
//                    contentView.layer = _metalLayer.NativePtr;
//                    return SwapchainSource.CreateNSWindow(nsWindow);
//                default:
//                    throw new PlatformNotSupportedException("Cannot create a SwapchainSource for " + sysWmInfo.subsystem + ".");
//            }
        }

        public void Run()
        {
            SDL_ShowWindow(WindowHandle);
            
            while (!_isExiting)
            {
                Tick();
            }
            
            Exit();
        }
        
        private void Tick()
        {
            while (SDL_PollEvent(out var e) != 0) 
            {
                switch (e.type) 
                {
                    case SDL_EventType.SDL_QUIT: 
                        _isExiting = true; 
                        break;
                }
            }

            var (width, height) = _renderer.GetDrawableSize();
            Draw(width, height);
            sg_commit();
            _renderer.Present();
        }

        protected abstract void Draw(int drawableWidth, int drawableHeight);

        public void Exit()
        {
            Dispose();
        }

        private void ReleaseResources()
        {
            _device.Dispose();
            _renderer.Dispose();

            if (WindowHandle != IntPtr.Zero)
            {
                SDL_DestroyWindow(WindowHandle);
            }
            
            SDL_Quit();   
        }

        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        ~App()
        {
            ReleaseResources();
        }
    }
}