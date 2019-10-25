using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public abstract class App : IDisposable
    {
        private readonly IntPtr _deviceHandle;
        
        public IntPtr WindowHandle { get; }

        public Platform Platform { get; }

        public GraphicsBackend GraphicsBackend { get; }

        protected App()
        {
            SDL_Init(SDL_INIT_VIDEO);

            var platformString = SDL_GetPlatform();
            Platform = PlatformHelper.GetPlatformFrom(platformString);
            GraphicsBackend = GraphicsBackendHelper.GetDefaultGraphicsBackendFor(Platform);
            GraphicsBackend = GraphicsBackend.OpenGL;

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

            var windowFlags = SDL_WindowFlags.SDL_WINDOW_HIDDEN;
            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;
            }
            else if (GraphicsBackend == GraphicsBackend.Metal)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
            }
            
            WindowHandle = SDL_CreateWindow("", 
                SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 
                800, 600, windowFlags);

            if (GraphicsBackend == GraphicsBackend.OpenGL)
            {
                _deviceHandle = SDL_GL_CreateContext(WindowHandle);
                SDL_GL_MakeCurrent(WindowHandle, _deviceHandle);
            }
            
            NativeLibrary.SetDllImportResolver(typeof(sokol_gfx).Assembly, ResolveLibraryHandle);
            
            var desc = new sg_desc {
                //mtl_device = (void*) _metalLayer.device,
                //mtl_renderpass_descriptor_cb = (void*) getRenderPassDescriptorCallback,
                //mtl_drawable_cb = (void*) getMetalDrawableCallback
            };
            sg_setup(ref desc);
        }

        public void Run()
        {
            SDL_ShowWindow(WindowHandle);
            
            var quit = false;
            while (!quit)
            {
                while (SDL_PollEvent(out var e) != 0) 
                {
                    switch (e.type) 
                    {
                        case SDL_EventType.SDL_QUIT: 
                            quit = true; 
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
        }

        protected abstract void Draw();

        public void Exit()
        {
            Dispose();
        }

        private void ReleaseUnmanagedResources()
        {
            sg_shutdown();
            
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
            var platformString = Platform.ToString().ToLower();
            var graphicsBackendString = GraphicsBackend.ToString().ToLower();
            var libPath = Path.Combine(AppContext.BaseDirectory, "lib", platformString, graphicsBackendString);

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