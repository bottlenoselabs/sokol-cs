// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1602", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    public enum DXGI_ALPHA_MODE
    {
        DXGI_ALPHA_MODE_UNSPECIFIED,
        DXGI_ALPHA_MODE_PREMULTIPLIED,
        DXGI_ALPHA_MODE_STRAIGHT,
        DXGI_ALPHA_MODE_IGNORE,
        DXGI_ALPHA_MODE_FORCE_DWORD
    }
}
