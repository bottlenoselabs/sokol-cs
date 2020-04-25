// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     A pixel color value type with 8 bits each for the 3 un-signed integer components: red, green, and blue.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgb8U" /> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="Rgb8U" /> is blittable.
    ///     </para>
    /// </remarks>
    public partial struct Rgb8U : IEquatable<Rgb8U>
    {
        /// <summary>
        ///     The red component value.
        /// </summary>
        public byte R;

        /// <summary>
        ///     The green component value.
        /// </summary>
        public byte G;

        /// <summary>
        ///     The blue component value.
        /// </summary>
        public byte B;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgb8U" /> structure using byte values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public Rgb8U(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return HashCode.Combine(R, G, B);
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }

        /// <inheritdoc/>
        public bool Equals(Rgb8U other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Rgb8U other && Equals(other);
        }

        public static bool operator ==(Rgb8U a, Rgb8U b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rgb8U a, Rgb8U b)
        {
            return !(a == b);
        }

        public static Rgb8U operator -(Rgb8U b, Rgb8U a)
        {
            var red = (byte)Math.Max(b.R - a.R, 0);
            var green = (byte)Math.Max(b.G - a.G, 0);
            var blue = (byte)Math.Max(b.B - a.B, 0);
            return new Rgb8U(red, green, blue);
        }

        public static Rgb8U operator +(Rgb8U a, Rgb8U b)
        {
            var red = (byte)(a.R + b.R);
            var green = (byte)(a.G + b.G);
            var blue = (byte)(a.B + b.B);
            return new Rgb8U(red, green, blue);
        }

        public static implicit operator Rgb8U(string value)
        {
            return FromHex(value);
        }

        public static implicit operator Rgb8U(uint value)
        {
            return FromPackedInteger(value);
        }

        private static Rgb8U FromPackedInteger(uint value)
        {
            var r = (byte)((value >> 16) & 0xFF);
            var g = (byte)((value >> 8) & 0xFF);
            var b = (byte)(value & 0xFF);

            return new Rgb8U(r, g, b);
        }

        private static Rgb8U FromHex(string value)
        {
            var span = value.AsSpan();
            if (span[0] == '#')
            {
                span = span.Slice(1);
            }

            if (!uint.TryParse(span, NumberStyles.HexNumber, null, out var u))
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{value}' as an unsigned 32-bit integer.");
            }

            u = uint.Parse(span, NumberStyles.HexNumber);

            return FromPackedInteger(u);
        }
    }
}
