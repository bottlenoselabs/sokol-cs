// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global
namespace Sokol
{
    /// <summary>
    ///     A color value type with 8 bits each for the 3 components: red, green, and blue.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Rgb8UInt" /> is blittable. Blittable types are data types in software applications
    ///         which have a unique characteristic. Data are often represented in memory differently in managed and
    ///         unmanaged code in the world of .NET. However, blittable types are defined as having an identical
    ///         presentation in memory for both environments, and can be directly shared. Understanding the difference
    ///         between blittable and non-blittable types can aid in using P/Invoke, a technique for interoperability
    ///         with unmanaged code in .NET applications.
    ///     </para>
    /// </remarks>
    public readonly partial struct Rgb8UInt
    {
        /// <summary>
        ///     The red component value.
        /// </summary>
        public readonly byte R;

        /// <summary>
        ///     The green component value.
        /// </summary>
        public readonly byte G;

        /// <summary>
        ///     The blue component value.
        /// </summary>
        public readonly byte B;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rgb8UInt"/> structure using byte values.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public Rgb8UInt(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
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

        public static implicit operator Rgb8UInt(string hex)
        {
            uint value;
            try
            {
                var span = hex.AsSpan();
                if (span[0] == '#')
                {
                    span = span.Slice(1);
                }

                value = uint.Parse(span, NumberStyles.HexNumber);
            }
            catch
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{hex}' as an unsigned 32-bit integer.");
            }

            return value;
        }

        public static implicit operator Rgb8UInt(uint value)
        {
            var r = (byte)((value >> 16) & 0xFF);
            var g = (byte)((value >> 8) & 0xFF);
            var b = (byte)(value & 0xFF);

            return new Rgb8UInt(r, g, b);
        }
    }
}
