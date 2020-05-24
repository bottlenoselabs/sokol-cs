// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1602", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    public enum DXGI_MODE_SCALING : uint
    {
        DXGI_MODE_SCALING_UNSPECIFIED = 0,
        DXGI_MODE_SCALING_CENTERED = 1,
        DXGI_MODE_SCALING_STRETCHED = 2
    }
}
