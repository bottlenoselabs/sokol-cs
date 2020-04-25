// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Sokol.Graphics;

// ReSharper disable NotAccessedField.Global

namespace Samples.Offscreen
{
    public struct Vertex
    {
        public Vector3 Position;
        public Rgba32F Color;
        public Vector2 TextureCoordinate;
    }
}
