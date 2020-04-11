// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Sokol.Graphics;
using static SDL2.SDL;

namespace Sokol.SDL2
{
    /// <summary>
    ///     The base class for Sokol.NET applications that use SDL2.
    /// </summary>
    public abstract class App : IDisposable
    {
        private readonly Renderer _renderer;
        private readonly IntPtr _windowHandle;
        private bool _isExiting;

        /// <summary>
        ///     Gets the current <see cref="GraphicsPlatform" />.
        /// </summary>
        /// <value>The current <see cref="GraphicsPlatform" />.</value>
        public GraphicsPlatform Platform { get; }

        /// <summary>
        ///     Gets the current <see cref="GraphicsBackend" />.
        /// </summary>
        /// <value>The current <see cref="GraphicsBackend" />.</value>
        public GraphicsBackend Backend { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class using an optional
        ///     <see cref="Backend "/> and <see cref="InitializeDescriptor "/>.
        /// </summary>
        /// <param name="graphicsBackend">The graphics backend.</param>
        /// <param name="descriptor">The `sokol_gfx` initialize descriptor.</param>
        protected App(GraphicsBackend? graphicsBackend = null, InitializeDescriptor? descriptor = null)
        {
            var (platform, backend) = NativeLibraries.Load(graphicsBackend);
            Platform = platform;
            Backend = backend;

            SDL_Init(SDL_INIT_VIDEO);
            var desc = descriptor ?? default;

            _windowHandle = CreateWindow(Backend);
            _renderer = CreateRenderer(Backend, ref desc, _windowHandle);

            GraphicsDevice.Setup(ref desc);
        }

        /// <summary>
        ///     Starts running the <see cref="App" />.
        /// </summary>
        public void Run()
        {
            SDL_ShowWindow(_windowHandle);

            while (!_isExiting)
            {
                Tick();
            }
        }

        /// <summary>
        ///     Stops running the <see cref="App" /> at the next frame.
        /// </summary>
        public void Exit()
        {
            _isExiting = true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        // TODO:
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO: Not sure about the parameters for this API...")]
        protected abstract void Draw(int drawableWidth, int drawableHeight);

        private static Renderer CreateRenderer(
            GraphicsBackend backend,
            ref InitializeDescriptor descriptor,
            IntPtr windowHandle)
        {
            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (backend)
            {
                case GraphicsBackend.OpenGL:
                    return new RendererOpenGL(windowHandle);
                case GraphicsBackend.Metal:
                    return new RendererMetal(ref descriptor, windowHandle);
                case GraphicsBackend.Direct3D11:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(backend), backend, null);
            }
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

            var windowHandle = SDL_CreateWindow(
                string.Empty,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                800,
                600,
                windowFlags);

            if (windowHandle == IntPtr.Zero)
            {
                throw new ApplicationException("Failed to create a window with SDL2.");
            }

            return windowHandle;
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
            GraphicsDevice.Commit();
            _renderer.Present();
        }

        private void ReleaseResources()
        {
            GraphicsDevice.Shutdown();

            _renderer.Dispose();

            if (_windowHandle != IntPtr.Zero)
            {
                SDL_DestroyWindow(_windowHandle);
            }

            SDL_Quit();
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="App"/> class.
        /// </summary>
        ~App()
        {
            ReleaseResources();
        }
    }
}
