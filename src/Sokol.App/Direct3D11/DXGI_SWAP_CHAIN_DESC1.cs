// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1649", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    public struct DXGI_SWAP_CHAIN_DESC1
    {
        public uint Width;
        public uint Height;
        public DXGI_FORMAT Format;
        public BlittableBoolean Stereo;
        public DXGI_SAMPLE_DESC SampleDesc;
        public DXGI_USAGE BufferUsage;
        public uint BufferCount;
        public DXGI_SCALING Scaling;
        public DXGI_SWAP_EFFECT SwapEffect;
        public DXGI_ALPHA_MODE AlphaMode;
        public uint Flags;
    }
}
