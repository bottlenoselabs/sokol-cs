// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;

namespace Sokol.SDL2
{
    internal abstract class Renderer : IDisposable
    {
        private int _disposedState;

        public IntPtr WindowHandle { get; }

        public abstract bool VerticalSyncIsEnabled { get; set; }

        protected Renderer(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                throw new ArgumentException("Window handle is invalid.", nameof(windowHandle));
            }

            WindowHandle = windowHandle;
        }

        public void Dispose()
        {
            var disposedState = Interlocked.CompareExchange(ref _disposedState, 1, 0);
            if (disposedState != 0)
            {
                return;
            }

            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        public abstract (int width, int height) GetDrawableSize();

        public abstract void Present();

        protected abstract void ReleaseResources();

        ~Renderer()
        {
            var disposedState = Interlocked.CompareExchange(ref _disposedState, 1, 0);
            if (disposedState != 0)
            {
                return;
            }

            ReleaseResources();
        }
    }
}
