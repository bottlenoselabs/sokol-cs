// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ShaderToy.App
{
    [SuppressMessage("ReSharper", "SA1307", Justification = "ShaderToy")]
    [SuppressMessage("ReSharper", "SA1305", Justification = "ShaderToy")]
    internal struct FragmentStageParams
    {
        public Vector3 iResolution;
        public float iTime;
        public Vector4 iMouse;
    }
}
