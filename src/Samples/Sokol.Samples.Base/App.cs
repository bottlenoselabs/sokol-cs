using System;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.Samples
{
    public abstract class App : IDisposable
    {
        private readonly Renderer _renderer;
        private bool _isExiting;
        private readonly IntPtr _windowHandle;

        protected App(GraphicsBackend? graphicsBackend = null, sg_desc? desc = null)
        {
            Ensure64BitArchitecture();
            SDL_Init(SDL_INIT_VIDEO);
            GraphicsBackend = graphicsBackend ?? GetDefaultGraphicsBackend();
            var description = desc ?? new sg_desc();

            _windowHandle = CreateWindow(GraphicsBackend);
            _renderer = CreateRenderer(GraphicsBackend, ref description, _windowHandle);
            
            sg_setup(ref description);
        }

        public GraphicsBackend GraphicsBackend { get; }

        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }
        
        private static GraphicsBackend GetDefaultGraphicsBackend()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //TODO: Use DirectX
                return GraphicsBackend.OpenGL;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return GraphicsBackend.Metal;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GraphicsBackend.OpenGL;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return GraphicsBackend.OpenGL;
            }

            return GraphicsBackend.OpenGL;
        }
        
        private static void Ensure64BitArchitecture()
        {
            var runtimeArchitecture = RuntimeInformation.OSArchitecture;
            if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
            {
                throw new NotSupportedException("32-bit architecture is not supported.");
            }
        }
        
        private static Renderer CreateRenderer(GraphicsBackend graphicsBackend, ref sg_desc deviceDescription, IntPtr windowHandle)
        {
            return graphicsBackend switch
            {
                GraphicsBackend.OpenGL => new RendererOpenGL(windowHandle),
                GraphicsBackend.Metal => new RendererMetal(ref deviceDescription, windowHandle),
                GraphicsBackend.OpenGLES2 => throw new NotImplementedException(),
                GraphicsBackend.OpenGLES3 => throw new NotImplementedException(),
                GraphicsBackend.Direct3D11 => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(graphicsBackend), graphicsBackend, null),
            };
        }

        private static IntPtr CreateWindow(GraphicsBackend graphicsBackend)
        {
            var windowFlags = SDL_WindowFlags.SDL_WINDOW_HIDDEN |
                              SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                              SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

            if (graphicsBackend == GraphicsBackend.OpenGL)
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

        public void Run()
        {
            SDL_ShowWindow(_windowHandle);

            while (!_isExiting)
            {
                Tick();
            }
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
            _isExiting = true;
        }

        private void ReleaseResources()
        {
            sg_shutdown();
            
            _renderer.Dispose();

            if (_windowHandle != IntPtr.Zero)
            {
                SDL_DestroyWindow(_windowHandle);
            }

            SDL_Quit();
        }

        ~App()
        {
            ReleaseResources();
        }
    }
}