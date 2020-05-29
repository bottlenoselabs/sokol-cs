// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1602", Justification = "C style.")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "C style.")]
    public enum sg_backend : uint
    {
        SG_BACKEND_GLCORE33 = 0U,
        SG_BACKEND_GLES2 = 1U,
        SG_BACKEND_GLES3 = 2U,
        SG_BACKEND_D3D11 = 3U,
        SG_BACKEND_METAL_IOS = 4U,
        SG_BACKEND_METAL_MACOS = 5U,
        SG_BACKEND_METAL_SIMULATOR = 6U,
        SG_BACKEND_WGPU = 7U,
        SG_BACKEND_DUMMY = 8U
    }
}
