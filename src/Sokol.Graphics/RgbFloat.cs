/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global

namespace Sokol
{
    public struct RgbFloat
    {
        // Basic colors: https://www.w3.org/TR/css-color-3/#html4
        public static readonly RgbFloat Black = 0x000000;
        public static readonly RgbFloat Silver = 0xC0C0C0;
        public static readonly RgbFloat Gray = 0x808080;
        public static readonly RgbFloat White = 0xFFFFFF;
        public static readonly RgbFloat Maroon = 0x800000;
        public static readonly RgbFloat Red = 0xFF0000;
        public static readonly RgbFloat Purple = 0x800080;
        public static readonly RgbFloat Fuchsia = 0xFF00FF;
        public static readonly RgbFloat Green = 0x008000;
        public static readonly RgbFloat Lime = 0x00FF00;
        public static readonly RgbFloat Olive = 0x808000;
        public static readonly RgbFloat Yellow = 0xFFFF00;
        public static readonly RgbFloat Navy = 0x000080;
        public static readonly RgbFloat Blue = 0x0000FF;
        public static readonly RgbFloat Teal = 0x008080;
        public static readonly RgbFloat Aqua = 0x00FFFF;
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbFloat AliceBlue = 0xF0F8FF;
        public static readonly RgbFloat AntiqueWhite = 0xFAEBD7;
        // public static readonly RgbFloat Aqua = 0x00FFFF;
        public static readonly RgbFloat Aquamarine = 0x7FFFD4;
        public static readonly RgbFloat Azure = 0xF0FFFF;
        public static readonly RgbFloat Beige = 0xF5F5DC;
        public static readonly RgbFloat Bisque = 0xFFE4C4;
        // public static readonly RgbFloat Black = 0x000000;
        public static readonly RgbFloat BlanchedAlmond = 0xFFEBCD;
        // public static readonly RgbFloat Blue = 0x0000FF;
        public static readonly RgbFloat BlueViolet = 0x8A2BE2;
        public static readonly RgbFloat Brown = 0xA52A2A;
        public static readonly RgbFloat BurlyWood = 0xDEB887;
        public static readonly RgbFloat CadetBlue = 0x5F9EA0;
        public static readonly RgbFloat Chartreuse = 0x7FFF00;
        public static readonly RgbFloat Chocolate = 0xD2691E;
        public static readonly RgbFloat Coral = 0xFF7F50;
        public static readonly RgbFloat CornflowerBlue = 0x6495ED;
        public static readonly RgbFloat Cornsilk = 0xFFF8DC;
        public static readonly RgbFloat Crimson = 0xDC143C;
        public static readonly RgbFloat Cyan = 0x00FFFF;
        public static readonly RgbFloat DarkBlue = 0x00008B;
        public static readonly RgbFloat DarkCyan = 0x008B8B;
        public static readonly RgbFloat DarkGoldenrod = 0xB8860B;
        public static readonly RgbFloat DarkGray = 0xA9A9A9;
        public static readonly RgbFloat DarkGreen = 0x006400;
        public static readonly RgbFloat DarkGrey = 0xA9A9A9;
        public static readonly RgbFloat DarkKhaki = 0xBDB76B;
        public static readonly RgbFloat DarkMagenta = 0x8B008B;
        public static readonly RgbFloat DarkOliveGreen = 0x556B2F;
        public static readonly RgbFloat DarkOrange = 0xFF8C00;
        public static readonly RgbFloat DarkOrchid = 0x9932CC;
        public static readonly RgbFloat DarkRed = 0x8B0000;
        public static readonly RgbFloat DarkSalmon = 0xE9967A;
        public static readonly RgbFloat DarkSeaGreen = 0x8FBC8F;
        public static readonly RgbFloat DarkStateBlue = 0x483D8B;
        public static readonly RgbFloat DarkStateGray = 0x2F4F4F;
        public static readonly RgbFloat DarkStateGrey = 0x2F4F4F;
        public static readonly RgbFloat DarkTurquoise = 0x00CED1;
        public static readonly RgbFloat DarkViolet = 0x9400D3;
        public static readonly RgbFloat DeepPink = 0xFF1493;
        public static readonly RgbFloat DeepSkyBlue = 0x00BFFF;
        public static readonly RgbFloat DimGray = 0x696969;
        public static readonly RgbFloat DimGrey = 0x696969;
        public static readonly RgbFloat DodgerBlue = 0x1E90FF;
        public static readonly RgbFloat Firebrick = 0xB22222;
        public static readonly RgbFloat FloralWhite = 0xFFFAF0;
        public static readonly RgbFloat ForestGreen = 0x228B22;
        // public static readonly RgbFloat Fuchsia = 0xFF00FF;
        public static readonly RgbFloat Gainsboro = 0xDCDCDC;
        public static readonly RgbFloat GhostWhite = 0xF8F8FF;
        public static readonly RgbFloat Gold = 0xFFD700;
        public static readonly RgbFloat Goldenrod = 0xDAA520;
        // public static readonly RgbFloat Gray = 0x808080;
        // public static readonly RgbFloat Green = 0x008000;
        public static readonly RgbFloat GreenYellow = 0xADFF2F;
        public static readonly RgbFloat Grey = 0x808080;
        public static readonly RgbFloat Honeydew = 0xF0FFF0;
        public static readonly RgbFloat HotPink = 0xFF69B4;
        public static readonly RgbFloat IndianRed = 0xCD5C5C;
        public static readonly RgbFloat Indigo = 0x4B0082;
        public static readonly RgbFloat Ivory = 0xFFFFF0;
        public static readonly RgbFloat Khaki = 0xF0E68C;
        public static readonly RgbFloat Lavender = 0xE6E6FA;
        public static readonly RgbFloat LavenderBlush = 0xFFF0F5;
        public static readonly RgbFloat LawnGreen = 0x7CFC00;
        public static readonly RgbFloat LemonChiffon = 0xFFFACD;
        public static readonly RgbFloat LightBlue = 0xADD8E6;
        public static readonly RgbFloat LightCoral = 0xF08080;
        public static readonly RgbFloat LightCyan = 0xE0FFFF;
        public static readonly RgbFloat LightGoldenrodYellow = 0xFAFAD2;
        public static readonly RgbFloat LightGray = 0xD3D3D3;
        public static readonly RgbFloat LightGreen = 0x90EE90;
        public static readonly RgbFloat LightGrey = 0xD3D3D3;
        public static readonly RgbFloat LightPink = 0xFFB6C1;
        public static readonly RgbFloat LightSalmon = 0xFFA07A;
        public static readonly RgbFloat LightSeaGreen = 0x20B2AA;
        public static readonly RgbFloat LightSkyBlue = 0x87CEFA;
        public static readonly RgbFloat LightSlateGray = 0x778899;
        public static readonly RgbFloat LightSlateGrey = 0x778899;
        public static readonly RgbFloat LightSteelBlue = 0xB0C4DE;
        public static readonly RgbFloat LightYellow = 0xFFFFE0;
        // public static readonly RgbFloat Lime = 0x00FF00;
        public static readonly RgbFloat LimeGreen = 0x32CD32;
        public static readonly RgbFloat Linen = 0xFAF0E6;
        public static readonly RgbFloat Magenta = 0xFF00FF;
        // public static readonly RgbFloat Maroon = 0x800000;
        public static readonly RgbFloat MediumAquamarine = 0x66CDAA;
        public static readonly RgbFloat MediumBlue = 0x0000CD;
        public static readonly RgbFloat MediumOrchid = 0xBA55D3;
        public static readonly RgbFloat MediumPurple = 0x9370DB;
        public static readonly RgbFloat MediumSeaGreen = 0x3CB371;
        public static readonly RgbFloat MediumStateBlue = 0x7B68EE;
        public static readonly RgbFloat MediumSpringGreen = 0x00FA9A;
        public static readonly RgbFloat MediumTurquoise = 0x48D1CC;
        public static readonly RgbFloat MediumVioletRed = 0xC71585;
        public static readonly RgbFloat MidnightBlue = 0x191970;
        public static readonly RgbFloat MintCream = 0xF5FFFA;
        public static readonly RgbFloat MistyRose = 0xFFE4E1;
        public static readonly RgbFloat Moccasin = 0xFFE4B5;
        public static readonly RgbFloat NavajoWhite = 0xFFDEAD;
        // public static readonly RgbFloat Navy = 0x000080;
        public static readonly RgbFloat OldLace = 0xFDF5E6;
        // public static readonly RgbFloat Olive = 0x808000;
        public static readonly RgbFloat OliveDrab = 0x6B8E23;
        public static readonly RgbFloat Orange = 0xFFA500;
        public static readonly RgbFloat Orangered = 0xFF4500;
        public static readonly RgbFloat Orchid = 0xDA70D6;
        public static readonly RgbFloat PaleGoldenrod = 0xEEE8AA;
        public static readonly RgbFloat PaleGreen = 0x98FB98;
        public static readonly RgbFloat PaleTurquoise = 0xAFEEEE;
        public static readonly RgbFloat PaleVioletRed = 0xDB7093;
        public static readonly RgbFloat PapayaWhip = 0xFFEFD5;
        public static readonly RgbFloat PeachPuff = 0xFFDAB9;
        public static readonly RgbFloat Peru = 0xCD853F;
        public static readonly RgbFloat Pink = 0xFFC0CB;
        public static readonly RgbFloat Plum = 0xDDA0DD;
        public static readonly RgbFloat PowderBlue = 0xB0E0E6;
        // public static readonly RgbFloat Purple = 0x800080;
        // public static readonly RgbFloat Red = 0xFF0000;
        public static readonly RgbFloat RosyBrown = 0xBC8F8F;
        public static readonly RgbFloat RoyalBlue = 0x4169E1;
        public static readonly RgbFloat SaddleBrown = 0x8B4513;
        public static readonly RgbFloat Salmon = 0xFA8072;
        public static readonly RgbFloat SandyBrown = 0xF4A460;
        public static readonly RgbFloat SeaGreen = 0x2E8B57;
        public static readonly RgbFloat SeaShell = 0xFFF5EE;
        public static readonly RgbFloat Sienna = 0xA0522D;
        // public static readonly RgbFloat Silver = 0xC0C0C0;
        public static readonly RgbFloat SkyBlue = 0x87CEEB;
        public static readonly RgbFloat SlateBlue = 0x6A5ACD;
        public static readonly RgbFloat SlateGray = 0x708090;
        public static readonly RgbFloat SlateGrey = 0x708090;
        public static readonly RgbFloat Snow = 0xFFFAFA;
        public static readonly RgbFloat SpringGreen = 0x00FF7F;
        public static readonly RgbFloat SteelBlue = 0x4682B4;
        public static readonly RgbFloat Tan = 0xD2B48C;
        // public static readonly RgbFloat Teal = 0x008080;
        public static readonly RgbFloat Thistle = 0xD8BFD8;
        public static readonly RgbFloat Tomato = 0xFF6347;
        public static readonly RgbFloat Turquoise = 0x40E0D0;
        public static readonly RgbFloat Violet = 0xEE82EE;
        public static readonly RgbFloat Wheat = 0xF5DEB3;
        // public static readonly RgbFloat White = 0xFFFFFF;
        public static readonly RgbFloat Whitesmoke = 0xF5F5F5;
        // public static readonly RgbFloat Yellow = 0xFFFF00;
        public static readonly RgbFloat YellowGreen = 0x9ACD32;
        
        public float R;
        public float G;
        public float B;
        
        public RgbFloat(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
        
        public RgbFloat(Vector3 vector4)
        {
            R = vector4.X;
            G = vector4.Y;
            B = vector4.Z;
        }
        
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode());
        }
        
        public static explicit operator RgbFloat(RgbaFloat color)
        {
            return new RgbFloat(color.R, color.G, color.B);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(RgbFloat color)
        {
            return new Vector3(color.R, color.G, color.B);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RgbFloat(Vector3 vector)
        {
            return new RgbFloat(vector);
        }

        public static implicit operator RgbFloat(string hex)
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
        
        public static implicit operator RgbFloat(uint value)
        {
            var r = ((value >> 16) & 0xFF) / 255f;
            var g = ((value >> 8) & 0xFF) / 255f;
            var b = (value & 0xFF) / 255f;
            
            return new RgbFloat(r, g, b);
        }
    }
}