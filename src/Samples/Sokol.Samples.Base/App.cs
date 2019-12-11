using System;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples
{
    public abstract class App : IDisposable
    {
        private readonly SgDevice _device;
        private readonly IntPtr _contextHandle;
        private bool _isExiting;

        public IntPtr WindowHandle { get; }
        
        public Platform Platform { get; }
        
        public GraphicsBackend GraphicsBackend { get; }

        protected App(SgDeviceDescription? deviceDescription = null)
        {
            SDL_Init(SDL_INIT_VIDEO);
            Platform = PlatformHelper.RuntimePlatform;
            GraphicsBackend = GetDefaultGraphicsBackendFor(Platform);
            
            SetSdl2Attributes();
            CreateWindow(out var windowHandle);
            WindowHandle = windowHandle;
            CreateGraphicsDevice(out var deviceHandle);
            _contextHandle = deviceHandle;

            var deviceDescription1 = deviceDescription ?? new SgDeviceDescription();
            _device = new SgDevice(deviceDescription1);
        }
        
        internal static GraphicsBackend GetDefaultGraphicsBackendFor(Platform platform)
        {
            return platform switch
            {
                Platform.Windows => GraphicsBackend.OpenGL, //TODO: Use Direct3D11 instead
                Platform.macOS => GraphicsBackend.OpenGL, //TODO: Use Metal instead
                Platform.Linux => GraphicsBackend.OpenGL,
                Platform.iOS => GraphicsBackend.Metal,
                Platform.Android => GraphicsBackend.OpenGLES3,
                _ => GraphicsBackend.OpenGL
            };
        }   

        private void SetSdl2Attributes()
        {
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_FLAGS, (int) SDL_GLcontext.SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);
                SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
                SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
                SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
                SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
            }
            else if (GraphicsBackend == GraphicsBackend.Metal)
            {
                SDL_SetHint(SDL_HINT_RENDER_DRIVER, "metal");
            }
        }

        private void CreateWindow(out IntPtr windowHandle)
        {
            var windowFlags = SDL_WindowFlags.SDL_WINDOW_HIDDEN;
            
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;
            }
            else if (GraphicsBackend == GraphicsBackend.Metal)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
            }
            
            windowHandle = SDL_CreateWindow("", 
                SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 
                800, 600, windowFlags);

            if (windowHandle == IntPtr.Zero)
            {
                throw new ApplicationException("Failed to create a window with SDL2.");
            }
        }

        private void CreateGraphicsDevice(out IntPtr deviceHandle)
        {
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                deviceHandle = SDL_GL_CreateContext(WindowHandle);
                
                if (deviceHandle == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to create OpenGL Core 3.3 context. Did you forget to update your drivers?");
                }
                
                SDL_GL_MakeCurrent(WindowHandle, deviceHandle);
                if (Platform == Platform.Windows || Platform == Platform.Linux)
                {
                    glew.glewInit();   
                }
            }
            else
            {
                throw new NotImplementedException();
            }
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
                
            Draw();
                
            sg_commit();
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                SDL_GL_SwapWindow(WindowHandle);   
            }
        }

        protected abstract void Draw();

        public void Exit()
        {
            Dispose();
        }

        private void ReleaseUnmanagedResources()
        {
            _device.Dispose();
            
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                if (_contextHandle != IntPtr.Zero)
                {
                    SDL_GL_DeleteContext(_contextHandle);   
                }
            }

            if (WindowHandle != IntPtr.Zero)
            {
                SDL_DestroyWindow(WindowHandle);
            }
            
            SDL_Quit();   
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~App()
        {
            ReleaseUnmanagedResources();
        }
    }
}