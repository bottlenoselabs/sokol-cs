// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using Sokol.Graphics;
using static SDL2.SDL;

// ReSharper disable UnusedMember.Global
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeInternal

namespace Sokol.App
{
    public sealed class AppWindow : IDisposable
    {
        public IntPtr Handle { get; }

        public uint ID { get; }

        public bool Exists { get; private set; }

        public Vector2 Position { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public event Action<AppWindow>? Closed;

        public event Action<AppWindow>? Closing;

        public event Action<AppWindow>? Resized;

        public event Action<AppWindow>? Moved;

        public event Action<KeyboardEventData>? KeyDown;

        public event Action<KeyboardEventData>? KeyUp;

        public AppWindow(int width, int height, bool allowHighDpi = true)
        {
            var app = App.Instance;
            if (app == null)
            {
                throw new Exception("Application is yet initialized.");
            }

            var windowFlags = SDL_WindowFlags.SDL_WINDOW_SHOWN |
                              SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

            if (allowHighDpi)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
            }

            switch (app.Backend)
            {
                case GraphicsBackend.OpenGL:
                    windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;
                    break;
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.Metal:
                case GraphicsBackend.Direct3D11:
                case GraphicsBackend.Dummy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Handle = SDL_CreateWindow(
                string.Empty,
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

        public void Resize(int width, int height)
        {
            SDL_SetWindowSize(Handle, width, height);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnShown()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnHidden()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnExposed()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnFocused()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnUnfocused()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal void OnMouseEntered()
        {
            // TODO
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
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

            SDL_ShowWindow(Handle);

            SDL_GetWindowPosition(Handle, out var x, out var y);
            OnMoved(x, y);

            SDL_GetWindowSize(Handle, out var width, out var height);
            OnResize(width, height);
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
