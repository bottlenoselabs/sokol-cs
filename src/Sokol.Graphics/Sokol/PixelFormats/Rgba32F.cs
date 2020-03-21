// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace
namespace Sokol
{
    /// <summary>
    ///     A pixel color value type with 32 bits each for the 4 float components: red, green, blue, and alpha.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgba32F" /> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="Rgba32F" /> is blittable. Blittable types are data types in software applications
    ///         which have a unique characteristic. Data are often represented in memory differently in managed and
    ///         unmanaged code in the world of .NET. However, blittable types are defined as having an identical
    ///         presentation in memory for both environments, and can be directly shared. Understanding the difference
    ///         between blittable and non-blittable types can aid in using P/Invoke, a technique for interoperability
    ///         with unmanaged code in .NET applications.
    ///     </para>
    /// </remarks>
    public partial struct Rgba32F
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

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode(), A.GetHashCode());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
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
