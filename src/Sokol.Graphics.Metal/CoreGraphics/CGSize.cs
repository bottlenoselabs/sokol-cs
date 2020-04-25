// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace CoreGraphics
{
    public struct CGSize
    {
        public CGFloat width;
        public CGFloat height;

        public CGSize(CGFloat width, CGFloat height)
        {
            this.width = width;
            this.height = height;
        }

        public override readonly string ToString()
        {
            return $"{width} x {height}";
        }
    }
}
