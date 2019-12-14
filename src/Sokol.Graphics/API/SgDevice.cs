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
using System.Threading;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public class SgDevice : IDisposable
    {
        private static int _isInitialized;

        private GCHandle _getMetalDeviceGCHandle;
        private GCHandle _getMetalDrawableGCHandle;
        private GCHandle _getMetalRenderPassDescriptorGCHandle;
        private int _isDisposed;

        public SgDevice(ref SgDeviceDescription description)
        {
            Ensure64BitArchitecture();
            EnsureIsNotAlreadyInitialized();
            
            ValidatePoolSizes(ref description);
            var desc = CreateDefaultSgDesc(description);

            if (description.GraphicsBackend == GraphicsBackend.Metal)
            {
                SetupMetal(ref desc, ref description);
            }

            sg_setup(ref desc);
        }

        private static sg_desc CreateDefaultSgDesc(SgDeviceDescription description)
        {
            var desc = new sg_desc
            {
                buffer_pool_size = description.BufferPoolSize == 0
                    ? _SG_DEFAULT_BUFFER_POOL_SIZE
                    : description.BufferPoolSize,
                image_pool_size = description.ImagePoolSize == 0
                    ? _SG_DEFAULT_IMAGE_POOL_SIZE
                    : description.ImagePoolSize,
                shader_pool_size = description.ShaderPoolSize == 0
                    ? _SG_DEFAULT_SHADER_POOL_SIZE
                    : description.ShaderPoolSize,
                pipeline_pool_size = description.PipelinePoolSize == 0
                    ? _SG_DEFAULT_PIPELINE_POOL_SIZE
                    : description.PipelinePoolSize,
                pass_pool_size = description.PassPoolSize == 0
                    ? _SG_DEFAULT_PASS_POOL_SIZE
                    : description.PassPoolSize,
                context_pool_size = description.ContextPoolSize == 0
                    ? _SG_DEFAULT_CONTEXT_POOL_SIZE
                    : description.ContextPoolSize
            };
            return desc;
        }

        private void SetupMetal(ref sg_desc desc, ref SgDeviceDescription description)
        {
            var getMetalDevice = description.GetMetalDevice;
            if (getMetalDevice == null)
            {
                throw new ArgumentNullException(nameof(description.GetMetalDevice));
            }

            var getMetalRenderPassDescriptor = description.GetMetalRenderPassDescriptor;
            if (getMetalRenderPassDescriptor == null)
            {
                throw new ArgumentNullException(nameof(description.GetMetalRenderPassDescriptor));
            }

            var getMetalDrawable = description.GetMetalDrawable;
            if (getMetalDrawable == null)
            {
                throw new ArgumentNullException(nameof(description.GetMetalDrawable));
            }

            unsafe
            {
                desc.mtl_device = (void*) Marshal.GetFunctionPointerForDelegate(getMetalDevice);
                desc.mtl_renderpass_descriptor_cb =
                    (void*) Marshal.GetFunctionPointerForDelegate(getMetalRenderPassDescriptor);
                desc.mtl_drawable_cb = (void*) Marshal.GetFunctionPointerForDelegate(getMetalDrawable);
            }

            // We need to prevent the delegates used above from getting garbage collected
            // NOTE: When creating a function pointer from a delegate, a stub is created. Thus the delegate does not
            //    need to be pinned. The GC is free to move around the delegate as the stub never changes.
            //    When the delegate is claimed by the GC the stub is also removed.
            _getMetalDeviceGCHandle = GCHandle.Alloc(getMetalDevice);
            _getMetalRenderPassDescriptorGCHandle = GCHandle.Alloc(getMetalRenderPassDescriptor);
            _getMetalDrawableGCHandle = GCHandle.Alloc(getMetalDrawable);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private static void Ensure64BitArchitecture()
        {
            var runtimeArchitecture = RuntimeInformation.OSArchitecture;
            if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
            {
                throw new NotSupportedException("32-bit architecture is not supported.");
            }
        }

        private static void EnsureIsNotAlreadyInitialized()
        {
            var isInitialized = Interlocked.CompareExchange(ref _isInitialized, 1, 0);
            if (isInitialized != 0)
            {
                throw new InvalidOperationException("`sg_setup` has already been called.");
            }
        }

        private static void ValidatePoolSizes(ref SgDeviceDescription description)
        {
            if (description.BufferPoolSize < 0 || description.BufferPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(description.BufferPoolSize));
            }

            if (description.ImagePoolSize < 0 || description.ImagePoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(description.ImagePoolSize));
            }

            if (description.ShaderPoolSize < 0 || description.ShaderPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(description.ShaderPoolSize));
            }

            if (description.PassPoolSize < 0 || description.PassPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(description.PassPoolSize));
            }

            if (description.ContextPoolSize < 0 || description.ContextPoolSize >= _SG_MAX_POOL_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(description.ContextPoolSize));
            }
        }

        private void ReleaseUnmanagedResources()
        {
            var isDisposed = Interlocked.CompareExchange(ref _isDisposed, 1, 0);
            if (isDisposed != 0)
            {
                return;
            }

            sg_shutdown();

            if (_getMetalDeviceGCHandle.IsAllocated)
            {
                _getMetalDeviceGCHandle.Free();
            }
            if (_getMetalRenderPassDescriptorGCHandle.IsAllocated)
            {
                _getMetalRenderPassDescriptorGCHandle.Free();
            }
            if (_getMetalDrawableGCHandle.IsAllocated)
            {
                _getMetalDrawableGCHandle.Free();
            }
        }

        ~SgDevice()
        {
            ReleaseUnmanagedResources();
        }
    }
}