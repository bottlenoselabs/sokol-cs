// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBeInternal

namespace Sokol.Graphics
{
    /// <inheritdoc />
    /// <summary>
    ///     A pixel color value type with 8 bits each for the 4 un-signed integer components: red, green, blue, and
    ///     alpha.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="T:Sokol.Graphics.Rgb8U" /> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="T:Sokol.Graphics.Rgba8U" /> is blittable.
    ///     </para>
    /// </remarks>
    public partial struct Rgba8U : IEquatable<Rgba8U>
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
        ///     The alpha component value.
        /// </summary>
        public byte A;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgba8U" /> structure using specified byte values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="a">The alpha component value.</param>
        public Rgba8U(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgba8U" /> structure using a specified <see cref="Rgb8U" />
        ///     and alpha.
        /// </summary>
        /// <param name="color">The RGB color.</param>
        /// <param name="a">The alpha component value.</param>
        public Rgba8U(Rgb8U color, byte a)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = a;
        }

        /// <inheritdoc />
        public override readonly string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return HashCode.Combine(R, G, B, A);
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }

        /// <inheritdoc />
        public bool Equals(Rgba8U other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Rgba8U other && Equals(other);
        }

        public static bool operator ==(Rgba8U a, Rgba8U b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rgba8U a, Rgba8U b)
        {
            return !(a == b);
        }

        public static Rgba8U operator -(Rgba8U b, Rgba8U a)
        {
            var red = (byte)Math.Max(b.R - a.R, 0);
            var green = (byte)Math.Max(b.G - a.G, 0);
            var blue = (byte)Math.Max(b.B - a.B, 0);
            var alpha = (byte)Math.Max(b.A - a.A, 0);
            return new Rgba8U(red, green, blue, alpha);
        }

        public static Rgba8U operator +(Rgba8U a, Rgba8U b)
        {
            var red = (byte)(a.R + b.R);
            var green = (byte)(a.G + b.G);
            var blue = (byte)(a.B + b.B);
            var alpha = (byte)(a.A + b.A);
            return new Rgba8U(red, green, blue, alpha);
        }

        public static Rgba8U operator *(Rgba8U a, Rgba8U b)
        {
            var red = (byte)(a.R + b.R);
            var green = (byte)(a.G + b.G);
            var blue = (byte)(a.B + b.B);
            var alpha = (byte)(a.A + b.A);
            return new Rgba8U(red, green, blue, alpha);
        }

        public static implicit operator Rgba8U(string value)
        {
            return FromHex(value);
        }

        public static implicit operator Rgba8U(uint value)
        {
            return FromPackedInteger(value);
        }

        private static Rgba8U FromPackedInteger(uint value)
        {
            var r = (byte)((value >> 24) & 0xFF);
            var g = (byte)((value >> 16) & 0xFF);
            var b = (byte)((value >> 8) & 0xFF);
            var a = (byte)(value & 0xFF);

            return new Rgba8U(r, g, b, a);
        }

        private static Rgba8U FromHex(string value)
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
            if (span.Length == 6)
            {
                u = (u << 8) + 0xFF;
            }

            return FromPackedInteger(u);
        }
    }
}
