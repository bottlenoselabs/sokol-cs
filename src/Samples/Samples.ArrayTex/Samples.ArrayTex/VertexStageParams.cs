// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace Samples.ArrayTex
{
    public struct VertexStageParams
    {
        public Matrix4x4 MVP;
        public Vector3 Offset0;
        public Vector3 Offset1;
        public Vector3 Offset2;
        public Vector3 Weights;
    }
}
