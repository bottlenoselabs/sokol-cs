// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;

namespace Sokol.Graphics
{
    public abstract partial class BackendRenderer : IDisposable
    {
        private int _disposedState;

        public IntPtr Window { get; }

        public abstract bool VerticalSyncIsEnabled { get; set; }

        protected BackendRenderer(IntPtr window, ref GraphicsBackendDescriptor descriptor)
        {
            if (window == IntPtr.Zero)
            {
                throw new ArgumentException("The Window handle is invalid.", nameof(window));
            }

            Window = window;
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

        ~BackendRenderer()
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
