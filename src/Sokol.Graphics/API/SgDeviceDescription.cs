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
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable UnassignedField.Global

namespace Sokol
{
    // NOTE:
    // - GetFunctionPointerForDelegate does not accept generics and thus we have to define our own delegates.
    // - UnmanagedFunctionPointer is necessary for iOS AOT.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetPointerDelegate();

    public struct SgDeviceDescription
    {
        public GraphicsBackend GraphicsBackend;
        public int BufferPoolSize;
        public int ImagePoolSize;
        public int ShaderPoolSize;
        public int PipelinePoolSize;
        public int PassPoolSize;
        public int ContextPoolSize;
        public IntPtr MetalDevice;
        public GetPointerDelegate GetMetalRenderPassDescriptor;
        public GetPointerDelegate GetMetalDrawable;

        public void Validate()
        {
            ValidateGraphicsBackend();
            ValidatePoolSizes();
            ValidateMetal();
        }

        private void ValidateMetal()
        {
            if (GraphicsBackend != GraphicsBackend.Metal)
            {
                return;
            }

            if (MetalDevice == IntPtr.Zero)
            {
                throw new InvalidOperationException("A MTLDevice is not provided.");
            }

            if (GetMetalDrawable == null || GetMetalRenderPassDescriptor == null)
            {
                throw new InvalidOperationException("One or both MTL callbacks are null.");
            }
        }

        private void ValidateGraphicsBackend()
        {
            if (GraphicsBackend == GraphicsBackend.Default)
            {
                throw new InvalidOperationException("A graphics backend is not selected.");
            }
            
            // TODO: In .NET 5 there should be a way to check if iOS, Android, etc
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var isDarwin = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            if (isWindows && GraphicsBackend != GraphicsBackend.OpenGL && GraphicsBackend != GraphicsBackend.Direct3D11)
            {
                throw new InvalidOperationException(
                    $"The graphics backend '{GraphicsBackend}' is not available in Windows.");
            }

            if (isDarwin && GraphicsBackend != GraphicsBackend.OpenGL && GraphicsBackend != GraphicsBackend.Metal)
            {
                throw new InvalidOperationException(
                    $"The graphics backend '{GraphicsBackend}' is not available in Darwin.");
            }

            if (isLinux && GraphicsBackend != GraphicsBackend.OpenGL)
            {
                throw new InvalidOperationException($"The graphics backend '{GraphicsBackend}' is not available in Linux.");
            }
        }

        private void ValidatePoolSizes()
        {
            if (BufferPoolSize < 0 || BufferPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(BufferPoolSize));
            }

            if (ImagePoolSize < 0 || ImagePoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(ImagePoolSize));
            }

            if (ShaderPoolSize < 0 || ShaderPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(ShaderPoolSize));
            }

            if (PassPoolSize < 0 || PassPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(PassPoolSize));
            }

            if (ContextPoolSize < 0 || ContextPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(ContextPoolSize));
            }
        }
    }
}