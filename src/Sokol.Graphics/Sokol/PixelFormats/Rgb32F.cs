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
    ///     A pixel color value type with 32 bits each for the 3 float components: red, green, and blue.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgb32F"/> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="Rgb32F" /> is blittable. Blittable types are data types in software applications
    ///         which have a unique characteristic. Data are often represented in memory differently in managed and
    ///         unmanaged code in the world of .NET. However, blittable types are defined as having an identical
    ///         presentation in memory for both environments, and can be directly shared. Understanding the difference
    ///         between blittable and non-blittable types can aid in using P/Invoke, a technique for interoperability
    ///         with unmanaged code in .NET applications.
    ///     </para>
    /// </remarks>
    public partial struct Rgb32F
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
        ///     Initializes a new instance of the <see cref="Rgb32F" /> structure using float values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public Rgb32F(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rgb32F" /> structure using a <see cref="Vector3"/>
        ///     interpreted as a RGB pixel color.
        /// </summary>
        /// <param name="vector3">The vector value.</param>
        public Rgb32F(Vector3 vector3)
        {
            R = vector3.X;
            G = vector3.Y;
            B = vector3.Z;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode());
        }

        public static explicit operator Rgb32F(Rgba32F color)
        {
            return new Rgb32F(color.R, color.G, color.B);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Rgb32F color)
        {
            return new Vector3(color.R, color.G, color.B);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Rgb32F(Vector3 vector)
        {
            return new Rgb32F(vector);
        }

        public static implicit operator Rgb32F(string hex)
        {
            var span = hex.AsSpan();
            if (span[0] == '#')
            {
                span = span.Slice(1);
            }

            if (!uint.TryParse(span, NumberStyles.HexNumber, null, out var u))
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{hex}' as an unsigned 32-bit integer.");
            }

            return FromPackedInteger(u);
        }

        public static implicit operator Rgb32F(uint value)
        {
            return FromPackedInteger(value);
        }

        private static Rgb32F FromPackedInteger(uint value)
        {
            var r = ((value >> 16) & 0xFF) / 255f;
            var g = ((value >> 8) & 0xFF) / 255f;
            var b = (value & 0xFF) / 255f;

            return new Rgb32F(r, g, b);
        }
    }
}
