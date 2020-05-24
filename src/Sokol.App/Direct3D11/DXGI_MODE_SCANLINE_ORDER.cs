// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1602", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "PInvoke.")]
    public enum DXGI_MODE_SCANLINE_ORDER : uint
    {
        DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED = 0,
        DXGI_MODE_SCANLINE_ORDER_PROGRESSIVE = 1,
        DXGI_MODE_SCANLINE_ORDER_UPPER_FIELD_FIRST = 2,
        DXGI_MODE_SCANLINE_ORDER_LOWER_FIELD_FIRST = 3
    }
}
