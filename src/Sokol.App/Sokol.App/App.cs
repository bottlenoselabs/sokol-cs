// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sokol.Graphics;
using static SDL2.SDL;

namespace Sokol.App
{
    /// <summary>
    ///     The base class for Sokol.NET applications that use SDL2.
    /// </summary>
    public abstract class App : IDisposable
    {
        [SuppressMessage("ReSharper", "SA1401", Justification = "Internal")]
        internal static App Instance = null!;

        private static int _drawableWidth;
        private static int _drawableHeight;
        private readonly AppLoop _loop;
        private readonly BackendRenderer _renderer;

        /// <summary>
        ///     Gets the main <see cref="AppWindow" />.
        /// </summary>
        /// <value>The main <see cref="AppWindow" />.</value>
        public AppWindow Window { get; }

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
        ///     Occurs when the application is about to close.
        /// </summary>
        public event Action<App>? Closing;

        /// <summary>
        ///     Occurs when a keyboard key is pushed down while the application has focus.
        /// </summary>
        public event Action<App, KeyboardEventData>? KeyDown;

        /// <summary>
        ///     Occurs when a keyboard key is released while the application has focus.
        /// </summary>
        public event Action<App, KeyboardEventData>? KeyUp;

        /// <summary>
        ///     Occurs when the size of the application's framebuffer changes.
        /// </summary>
        public event Action<App, int, int>? DrawableSizeChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class using an optional
        ///     <see cref="AppLoop" />, <see cref="Backend " /> and <see cref="GraphicsDescriptor " />.
        /// </summary>
        /// <param name="backend">The graphics backend.</param>
        /// <param name="loop">The app loop.</param>
        /// <param name="descriptor">The `sokol_gfx` initialize descriptor.</param>
        protected App(
            GraphicsBackend? backend = null,
            AppLoop? loop = null,
            GraphicsDescriptor? descriptor = null)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Another application has already been initialized.");
            }

            Instance = this;

            var (platform, backendUsed) = NativeLibraries.Load(backend);
            Platform = platform;
            Backend = backendUsed;

            SDL_Init(SDL_INIT_VIDEO);
            Window = new AppWindow(string.Empty, 800, 600);

            var desc = descriptor ?? default;
            _renderer = CreateRenderer(Backend, ref desc, Window.Handle);

            GraphicsDevice.Setup(ref desc);

            _loop = loop ?? new FixedTimeStepLoop();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Starts running the <see cref="App" />.
        /// </summary>
        public void Run()
        {
            Window.Show();
            _loop.Run();
            Window.Close();
            Closing?.Invoke(this);
        }

        /// <summary>
        ///     Stops running the <see cref="App" /> at the next frame.
        /// </summary>
        public void Exit()
        {
            _loop.Stop();
        }

        /// <summary>
        ///     Begins and returns the framebuffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.DontCare" /> as the action.
        /// </summary>
        /// <returns>The framebuffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass()
        {
            var passAction = PassAction.DontCare;
            return BeginDefaultPass(ref passAction);
        }

        /// <summary>
        ///     Begins and returns the framebuffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        /// <returns>The framebuffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass(Rgba32F clearColor)
        {
            var passAction = PassAction.Clear(clearColor);
            return BeginDefaultPass(ref passAction);
        }

        /// <summary>
        ///     Begins and returns the framebuffer <see cref="Pass"/> with the specified width, height, and
        ///     <see cref="PassAction" />.
        /// </summary>
        /// <param name="passAction">The framebuffer pass action.</param>
        /// <returns>The framebuffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass([In] ref PassAction passAction)
        {
            return GraphicsDevice.BeginDefaultPass(_drawableWidth, _drawableHeight, ref passAction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void DoInput(InputState state)
        {
            HandleInput(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void DoUpdate(AppTime time)
        {
            Update(time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void DoDraw(AppTime time)
        {
            var (width, height) = _renderer.GetDrawableSize();
            if (_drawableWidth != width || _drawableHeight != height)
            {
                _drawableWidth = width;
                _drawableHeight = height;
                OnDrawableSizeChanged(width, height);
            }

            Draw(time);
            GraphicsDevice.Commit();
            _renderer.Present();
        }

        protected abstract void HandleInput(InputState state);

        protected abstract void Update(AppTime time);

        protected abstract void Draw(AppTime time);

        private static BackendRenderer CreateRenderer(
            GraphicsBackend backend,
            ref GraphicsDescriptor descriptor,
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

        private void ReleaseResources()
        {
            _renderer.Dispose();
            GraphicsDevice.Shutdown();
            Window.Dispose();
            SDL_Quit();
        }

        private void OnDrawableSizeChanged(int width, int height)
        {
            DrawableSizeChanged?.Invoke(this, width, height);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="App" /> class.
        /// </summary>
        ~App()
        {
            ReleaseResources();
        }
    }
}
