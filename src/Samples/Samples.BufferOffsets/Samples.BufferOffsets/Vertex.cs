// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Sokol.Graphics;

// ReSharper disable NotAccessedField.Global

namespace Samples.BufferOffsets
{
    // The interleaved vertex data structure
    internal struct Vertex
    {
        public Vector2 Position;
        public Rgb32F Color;
    }
}
