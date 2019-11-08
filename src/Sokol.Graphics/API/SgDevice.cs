using System;
using System.Runtime.InteropServices;
using System.Threading;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public class SgDevice : IDisposable
    {
        private static int _isInitialized;
        private int _isDisposed;

        public SgDevice(SgDeviceDescription description)
        {
            var runtimeArchitecture = RuntimeInformation.OSArchitecture;
            if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
            {
                throw new NotSupportedException("32-bit architecture is not supported.");
            }
            
            var isInitialized = Interlocked.CompareExchange(ref _isInitialized, 1, 0);
            if (isInitialized != 0)
            {
                throw new InvalidOperationException("`sg_setup` has already been called.");
            }
            
            var desc = new sg_desc();

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

            desc.buffer_pool_size = description.BufferPoolSize == 0
                ? _SG_DEFAULT_BUFFER_POOL_SIZE
                : description.BufferPoolSize;

            desc.image_pool_size = description.ImagePoolSize == 0
                ? _SG_DEFAULT_IMAGE_POOL_SIZE
                : description.ImagePoolSize;
            
            desc.shader_pool_size = description.ShaderPoolSize == 0
                ? _SG_DEFAULT_SHADER_POOL_SIZE
                : description.ShaderPoolSize;
            
            desc.pipeline_pool_size = description.PipelinePoolSize == 0
                ? _SG_DEFAULT_PIPELINE_POOL_SIZE
                : description.PipelinePoolSize;
            
            desc.pipeline_pool_size = description.PassPoolSize == 0
                ? _SG_DEFAULT_PASS_POOL_SIZE
                : description.PassPoolSize;
            
            desc.context_pool_size = description.ContextPoolSize == 0
                ? _SG_DEFAULT_CONTEXT_POOL_SIZE
                : description.ContextPoolSize;
            
            sg_setup(ref desc);
        }

        private void ReleaseUnmanagedResources()
        {
            var isDisposed = Interlocked.CompareExchange(ref _isDisposed, 1, 0);
            if (isDisposed != 0)
            {
                return;
            }
            
            sg_shutdown();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~SgDevice()
        {
            ReleaseUnmanagedResources();
        }
    }
}