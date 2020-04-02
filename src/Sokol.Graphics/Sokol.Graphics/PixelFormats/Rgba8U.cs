// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
namespace Sokol.Graphics
{
    /// <summary>
    ///     A pixel color value type with 8 bits each for the 4 un-signed integer components: red, green, blue, and
    ///     alpha.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgb8U" /> is mutable on purpose for easier use when working with the components directly.
    ///     </para>
    ///     <para>
    ///         <see cref="Rgba8U" /> is blittable. Blittable types are data types in software applications
    ///         which have a unique characteristic. Data are often represented in memory differently in managed and
    ///         unmanaged code in the world of .NET. However, blittable types are defined as having an identical
    ///         presentation in memory for both environments, and can be directly shared. Understanding the difference
    ///         between blittable and non-blittable types can aid in using P/Invoke, a technique for interoperability
    ///         with unmanaged code in .NET applications.
    ///     </para>
    /// </remarks>
    public partial struct Rgba8U
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
        ///     Initializes a new instance of the <see cref="Rgba8U" /> structure using byte values.
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

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode(), A.GetHashCode());
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
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
