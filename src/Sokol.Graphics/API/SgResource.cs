using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sokol
{
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