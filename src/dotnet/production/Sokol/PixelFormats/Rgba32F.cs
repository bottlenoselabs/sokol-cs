// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Sokol
{
    /// <inheritdoc />
    /// <summary>
    ///     A pixel color value type with 32 bits each for the 4 float components: red, green, blue, and alpha.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgba32F" /> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="Rgba32F" /> is blittable.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable value type.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public partial struct Rgba32F : IEquatable<Rgba32F>
    {
        /// <summary>
        ///     The red component value.
        /// </summary>
        public float R;

        /// <summary>
        ///     The green component value.
        /// </summary>
        public float G;

        /// <summary>
        ///     The blue component value.
        /// </summary>
        public float B;

        /// <summary>
        ///     The alpha component value.
        /// </summary>
        public float A;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgba32F" /> structure using float values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="a">The alpha component value.</param>
        public Rgba32F(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgba32F" /> structure using a <see cref="Vector4" />
        ///     interpreted as a RGBA pixel color.
        /// </summary>
        /// <param name="vector4">The vector value.</param>
        public Rgba32F(Vector4 vector4)
        {
            R = vector4.X;
            G = vector4.Y;
            B = vector4.Z;
            A = vector4.W;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgba32F" /> structure using a specified
        ///     <see cref="Rgb32F" /> and alpha.
        /// </summary>
        /// <param name="color">The RGB color.</param>
        /// <param name="a">The alpha component value.</param>
        public Rgba32F(Rgb32F color, byte a)
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
        public bool Equals(Rgba32F other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Rgba32F other && Equals(other);
        }

        public static bool operator ==(Rgba32F a, Rgba32F b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rgba32F a, Rgba32F b)
        {
            return !(a == b);
        }

        public static Rgba32F operator -(Rgba32F b, Rgba32F a)
        {
            var red = Math.Max(b.R - a.R, 0);
            var green = Math.Max(b.G - a.G, 0);
            var blue = Math.Max(b.B - a.B, 0);
            var alpha = Math.Max(b.A - a.A, 0);
            return new Rgba32F(red, green, blue, alpha);
        }

        public static Rgba32F operator +(Rgba32F a, Rgba32F b)
        {
            var red = a.R + b.R;
            var green = a.G + b.G;
            var blue = a.B + b.B;
            var alpha = a.A + b.A;
            return new Rgba32F(red, green, blue, alpha);
        }

        public static Rgba32F operator *(Rgba32F a, Rgba32F b)
        {
            var red = a.R * b.R;
            var green = a.G * b.G;
            var blue = a.B * b.B;
            var alpha = a.A * b.A;
            return new Rgba32F(red, green, blue, alpha);
        }

        public static Rgba32F operator *(float value, Rgba32F color)
        {
            var red = value * color.R;
            var green = value * color.G;
            var blue = value * color.B;
            var alpha = value * color.A;
            return new Rgba32F(red, green, blue, alpha);
        }

        public static Rgba32F operator *(Rgba32F color, float value)
        {
            var red = value * color.R;
            var green = value * color.G;
            var blue = value * color.B;
            var alpha = value * color.A;
            return new Rgba32F(red, green, blue, alpha);
        }

        public static implicit operator Rgba32F(string value)
        {
            return FromHex(value);
        }

        public static implicit operator Rgba32F(uint value)
        {
            return FromPackedInteger(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(Rgba32F color)
        {
            return new Vector4(color.R, color.G, color.B, color.A);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Rgba32F(Vector4 vector)
        {
            return new Rgba32F(vector);
        }

        private static Rgba32F FromPackedInteger(uint value)
        {
            var r = ((value >> 24) & 0xFF) / 255f;
            var g = ((value >> 16) & 0xFF) / 255f;
            var b = ((value >> 8) & 0xFF) / 255f;
            var a = (value & 0xFF) / 255f;

            return new Rgba32F(r, g, b, a);
        }

        private static Rgba32F FromHex(string value)
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
