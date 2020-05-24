// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Direct3D11
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    public struct DXGI_RATIONAL
    {
        public uint Numerator;
        public uint Denominator;
    }
}
