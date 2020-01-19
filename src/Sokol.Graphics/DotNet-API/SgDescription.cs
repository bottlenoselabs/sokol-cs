using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 8)]
    public struct SgDescription
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] public int BufferPoolSize;
        [FieldOffset(8)] public int ImagePoolSize;
        [FieldOffset(12)] public int ShaderPoolSize;
        [FieldOffset(16)] public int PipelinePoolSize;
        [FieldOffset(20)] public int PassPoolSize;
        [FieldOffset(24)] public int ContextPoolSize;
        [FieldOffset(28)] public BlittableBoolean ForceGLES2;
        [FieldOffset(32)] public IntPtr MTLDevice;
        [FieldOffset(40)] public IntPtr MTLRenderPassDescriptorCallback;
        [FieldOffset(48)] public IntPtr MTLDrawableCallback;
        [FieldOffset(56)] public int MTLGlobalUniformBufferSize;
        [FieldOffset(60)] public int MTLSamplerCacheSize;
        [FieldOffset(64)] public IntPtr D3D11Device;
        [FieldOffset(72)] public IntPtr D3D11DeviceContext;
        [FieldOffset(80)] public IntPtr D3D11RenderTargetViewCallback;
        [FieldOffset(88)] public IntPtr D3D11DepthStencilViewCallback;
        [FieldOffset(96)] internal uint _endCanary;
    }
}