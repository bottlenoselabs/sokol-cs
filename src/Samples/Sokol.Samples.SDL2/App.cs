using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.glew;
using static Sokol.sokol_gfx;

namespace Sokol.Samples
{
    public abstract class App : IDisposable
    {
        private readonly IntPtr _deviceHandle;
        private bool _isExiting;
        private readonly bool _debugSokol;

        public IntPtr WindowHandle { get; }
        
        public Platform Platform { get; }
        
        public GraphicsBackend GraphicsBackend { get; }

        protected App(bool debugSokol = false)
        {
            if (RuntimeInformationHelper.Is32BitArchitecture)
            {
                throw new NotSupportedException("32-bit architecture is not supported.");
            }
            
            _debugSokol = debugSokol;

            DllMap.Register(typeof(App).Assembly);
            DllMap.Register(typeof(sokol_gfx).Assembly);
            
            DllMap.Configure(OperatingSystem.Windows, "SDL2", "SDL2.dll");
            DllMap.Configure(OperatingSystem.Darwin, "SDL2", "libSDL2-2.0.0.dylib");
            DllMap.Configure(OperatingSystem.Linux, "SDL2", "libSDL2-2.0.so.0");
            
            DllMap.Configure(OperatingSystem.Windows, "glew", "glew32.dll");
            DllMap.Configure(OperatingSystem.Linux, "glew", "libGLEW.2.1.0.dylib");
            DllMap.Configure(OperatingSystem.Linux, "glew", "libGLEW-2.1.0.so.0");
            
            DllMap.Configure(OperatingSystem.Windows, "sokol_gfx", "sokol_gfx.dll");
            DllMap.Configure(OperatingSystem.Darwin, "sokol_gfx", "libsokol_gfx.dylib");
            DllMap.Configure(OperatingSystem.Linux, "sokol_gfx", "libsokol_gfx.so");

            SDL_Init(SDL_INIT_VIDEO);
            Platform = PlatformHelper.RuntimePlatform;
            GraphicsBackend = GraphicsBackendHelper.GetDefaultGraphicsBackendFor(Platform);
            
            SetSdl2Attributes();
            CreateWindow(out var windowHandle);
            WindowHandle = windowHandle;
            CreateGraphicsDevice(out var deviceHandle);
            _deviceHandle = deviceHandle;
            
            var desc = new sg_desc {
                //mtl_device = (void*) _metalLayer.device,
                //mtl_renderpass_descriptor_cb = (void*) getRenderPassDescriptorCallback,
                //mtl_drawable_cb = (void*) getMetalDrawableCallback
            };
            sg_setup(ref desc);
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
        }

        private void CreateGraphicsDevice(out IntPtr deviceHandle)
        {
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                deviceHandle = SDL_GL_CreateContext(WindowHandle);
                SDL_GL_MakeCurrent(WindowHandle, deviceHandle);

                if (Platform == Platform.Windows || Platform == Platform.Linux)
                {
                    glewInit();   
                }
            }
            else
            {
                deviceHandle = IntPtr.Zero;
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
            sg_shutdown();

            Dispose();
        }

        private void ReleaseUnmanagedResources()
        {
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                SDL_GL_DeleteContext(_deviceHandle);
            }

            SDL_DestroyWindow(WindowHandle);
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

        private IntPtr ResolveLibraryHandle(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            var runtimeArchitecture = RuntimeInformation.OSArchitecture;
            if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
            {
                throw new NotSupportedException("32-bit architecture is not supported.");
            }
            
            var platformString = Platform.ToString().ToLower();
            var graphicsBackendString = GraphicsBackend.ToString().ToLower();
            var configuration = _debugSokol ? "debug" : "release";
            var libPath = Path.Combine(AppContext.BaseDirectory, 
                "lib", 
                platformString, 
                graphicsBackendString, 
                configuration);

            string filePath;
            switch (Platform)
            {
                case Platform.Windows:
                    filePath = Path.Combine(libPath, "sokol_gfx.dll");
                    break;
                case Platform.macOS:
                    filePath = Path.Combine(libPath, "libsokol_gfx.dylib");
                    break;
                default:
                    throw new NotImplementedException();
            }

            var handle = NativeLibrary.Load(filePath, assembly, null);
            return handle;
        }
    }
}