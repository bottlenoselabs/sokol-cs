// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public override string ToString()
        {
            return $"{width} x {height}";
        }
    }
}
