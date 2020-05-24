// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;

namespace CoreGraphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1307", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
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
