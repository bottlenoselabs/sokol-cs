// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace CoreGraphics
{
    public struct CGFloat
    {
        public double value;

        public CGFloat(double value)
        {
            this.value = value;
        }

        public static implicit operator CGFloat(double value)
        {
            return new CGFloat(value);
        }

        public static implicit operator double(CGFloat cgf)
        {
            return cgf.value;
        }

        public override string ToString()
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
