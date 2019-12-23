/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

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
using System.Runtime.InteropServices;
using System.Threading;

namespace Sokol
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class SgResource : IDisposable
    {
        private int _disposedState;
        
        protected readonly IntPtr CNamePointer;

        public bool IsDisposed => _disposedState != 0;

        public string Name { get; }

        public SgResource(string name)
        {
            Name = name;

            if (!string.IsNullOrEmpty(name))
            {
                CNamePointer = Marshal.StringToHGlobalAnsi(Name);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureNotDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(Name);
            }
        }

        protected virtual void ReleaseUnmanagedResources()
        {
            Marshal.FreeHGlobal(CNamePointer);
        }

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