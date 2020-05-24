// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace CoreGraphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1307", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
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

        public override readonly string ToString()
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
