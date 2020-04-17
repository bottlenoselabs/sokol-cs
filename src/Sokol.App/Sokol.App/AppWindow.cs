// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using Sokol.Graphics;
using static SDL2.SDL;

namespace Sokol.App
{
    public sealed class AppWindow : IDisposable
    {
        private bool _shouldClose;

        public IntPtr Handle { get; }

        public uint ID { get; }

        public bool Exists { get; private set; }

        public Vector2 Position { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public event Action Created;

        public event Action<AppWindow> Closed;

        public event Action<AppWindow> Closing;

        public event Action<AppWindow> Resized;

        public event Action<AppWindow> Moved;

        public event Action<KeyboardEventData> KeyDown;

        public event Action<KeyboardEventData> KeyUp;

        public AppWindow(string title, int width, int height)
        {
            var app = App.Instance;
            if (app == null)
            {
                throw new Exception("Application is yet initialized.");
            }

            var windowFlags = SDL_WindowFlags.SDL_WINDOW_SHOWN |
                              SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                              SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
            if (app.Backend == GraphicsBackend.OpenGL)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;
            }
            else if (app.Backend == GraphicsBackend.Metal)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
            }

            Handle = SDL_CreateWindow(
                title,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                width,
                height,
                windowFlags);

            if (Handle == IntPtr.Zero)
            {
                throw new ApplicationException("Failed to create a SDL2 window.");
            }

            ID = SDL_GetWindowID(Handle);

            if ((windowFlags & SDL_WindowFlags.SDL_WINDOW_SHOWN) == SDL_WindowFlags.SDL_WINDOW_SHOWN)
            {
                SDL_ShowWindow(Handle);
            }

            OnCreated();
        }

        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        public void Show()
        {
            SDL_ShowWindow(Handle);
        }

        public void Close()
        {
            OnClosing();
            OnClosed();
        }

        internal void OnShown()
        {
            // TODO
        }

        internal void OnHidden()
        {
            // TODO
        }

        internal void OnExposed()
        {
            // TODO
        }

        internal void OnFocused()
        {
            // TODO
        }

        internal void OnUnfocused()
        {
            // TODO
        }

        internal void OnMouseEntered()
        {
            // TODO
        }

        internal void OnMouseLeft()
        {
            // TODO
        }

        internal void OnMoved(int newX, int newY)
        {
            Position = new Vector2(newX, newY);
            Moved?.Invoke(this);
        }

        internal void OnResize(int newWidth, int newHeight)
        {
            Width = newWidth;
            Height = newHeight;
            Resized?.Invoke(this);
        }

        internal void OnKeyDown(KeyboardKey key, KeyboardModifierKeys modifiers)
        {
            var keyEvent = new KeyboardEventData(key, true, modifiers);
            KeyDown?.Invoke(keyEvent);
        }

        internal void OnKeyUp(KeyboardKey key, KeyboardModifierKeys modifiers)
        {
            var keyEvent = new KeyboardEventData(key, false, modifiers);
            KeyUp?.Invoke(keyEvent);
        }

        private void OnClosing()
        {
            Closing?.Invoke(this);
            SDL_DestroyWindow(Handle);
            Exists = false;
        }

        private void OnClosed()
        {
            Closed?.Invoke(this);
        }

        private void OnCreated()
        {
            Exists = true;

            SDL_GetWindowPosition(Handle, out var x, out var y);
            OnMoved(x, y);

            SDL_GetWindowSize(Handle, out var width, out var height);
            OnResize(width, height);

            Created?.Invoke();
        }

        private void ReleaseResources()
        {
            SDL_DestroyWindow(Handle);
        }

        ~AppWindow()
        {
            ReleaseResources();
        }
    }
}
