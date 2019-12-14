using System;
using System.Numerics;
using static SDL2.SDL;

namespace Sokol.Samples
{
    public class RendererOpenGL : Renderer
    {
        private readonly IntPtr _contextHandle;
        private readonly IntPtr _windowHandle;
        
        public RendererOpenGL(IntPtr windowHandle, Platform platform)
        {
            _windowHandle = windowHandle;
            
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

            if (platform != Platform.Windows && platform != Platform.Linux)
            {
                return;
            }

            result = glew.glewInit();
            if (result != 0)
            {
                throw new ApplicationException("Failed to initialize GLEW.");
            }
        }

        public override (int width, int height) GetDrawableSize()
        {
            SDL_GL_GetDrawableSize(_windowHandle, out var width, out var height);
            return (width, height);
        }

        public override void Present()
        {
            SDL_GL_SwapWindow(_windowHandle);
        }

        protected override void ReleaseResources()
        {
            if (_contextHandle != IntPtr.Zero)
            {
                SDL_GL_DeleteContext(_contextHandle);   
            }  
        }
    }
}