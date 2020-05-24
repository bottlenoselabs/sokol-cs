// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1602", Justification = "PInvoke.")]
    public enum DXGI_SWAP_EFFECT
    {
        DXGI_SWAP_EFFECT_DISCARD,
        DXGI_SWAP_EFFECT_SEQUENTIAL,
        DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL,
        DXGI_SWAP_EFFECT_FLIP_DISCARD
    }
}
