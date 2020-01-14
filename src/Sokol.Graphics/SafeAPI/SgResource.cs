/* 
MIT License

Copyright (c) 2020 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

// ReSharper disable UnassignedField.Global
// ReSharper disable InconsistentNaming

namespace Sokol
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class SgResource<T> : IDisposable where T : IEquatable<uint>
    {
        protected T _handle;
        private int _disposedState;
        
        public T Handle => _handle;

        public bool IsValid => !_handle.Equals(0U);

        public bool IsDisposed => _disposedState != 0;

        protected internal SgResource()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureNotDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected abstract void ReleaseUnmanagedResources();

        public void Dispose()
        {
            var disposedState = Interlocked.CompareExchange(ref _disposedState, 1, 0);
            if (disposedState != 0)
            {
                return;
            }

            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}