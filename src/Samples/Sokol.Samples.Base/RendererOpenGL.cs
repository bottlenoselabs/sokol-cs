using System;
using System.Reflection;
using System.Runtime.InteropServices;
using static SDL2.SDL;

namespace Sokol.Samples
{
    public class RendererOpenGL : Renderer
    {
        private readonly IntPtr _contextHandle;

        public override bool VerticalSyncIsEnabled
        {
            get
            {
                var result = SDL_GL_GetSwapInterval();
                return result != 0;
            }
            set
            {
                var result = SDL_GL_SetSwapInterval(value ? 1 : 0);
                if (result != 0)
                {
                    throw new ApplicationException("Failed to set vertical sync.");
                }
            }
        }

        public RendererOpenGL(IntPtr windowHandle)
            : base(windowHandle)
        {
            SetSDL2Attributes();
            
            _contextHandle = SDL_GL_CreateContext(windowHandle);
            if (_contextHandle == IntPtr.Zero)
            {
                throw new ApplicationException("Failed to create OpenGL Core 3.3 context. Are you in a virtual machine without GPU acceleration? Did you forget to update your drivers?");
            }
                
            var result = SDL_GL_MakeCurrent(windowHandle, _contextHandle);
            if (result != 0)
            {
                throw new ApplicationException("Failed to setup OpenGL context with a window.");
            }
            
            NativeLibrary.SetDllImportResolver(typeof(sokol_gfx).Assembly, ResolveLibrary);

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            { 
                return;
            }
            
            NativeLibrary.SetDllImportResolver(typeof(glew).Assembly, ResolveLibrary);

            result = glew.glewInit();
            if (result != 0)
            {
                throw new ApplicationException("Failed to initialize GLEW.");
            }

            // Enable vertical sync by default
            SDL_GL_SetSwapInterval(1);
        }

        private static void SetSDL2Attributes()
        {
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_FLAGS, (int) SDL_GLcontext.SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLEBUFFERS, 1);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLESAMPLES, 4);
        }

        public override (int width, int height) GetDrawableSize()
        {
            SDL_GL_GetDrawableSize(WindowHandle, out var width, out var height);
            return (width, height);
        }

        public override void Present()
        {
            SDL_GL_SwapWindow(WindowHandle);
        }

        protected override void ReleaseResources()
        {
            if (_contextHandle != IntPtr.Zero)
            {
                SDL_GL_DeleteContext(_contextHandle);   
            }  
        }
        
        private static IntPtr ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (libraryName.ToLower())
            {
                case "glew":
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        libraryName = "glew32";   
                    }
                    break;
                }
                case "sokol_gfx":
                    libraryName = "sokol_gfx-opengl";
                    break;
            }

            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }
    }
}