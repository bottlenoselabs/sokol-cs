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
using System.Runtime.CompilerServices;

// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global

namespace Sokol
{
    public struct Rgb8UInt
    {
        // Custom colors
        public static readonly Rgb8UInt TransparentBlack = 0x000000;
        public static readonly Rgb8UInt Transparent = 0x000000;
        public static readonly Rgb8UInt MonoGameOrange = 0xE73C00;
        
        // Basic colors: https://www.w3.org/TR/css-color-3/#html4
        public static readonly Rgb8UInt Black = 0x000000;
        public static readonly Rgb8UInt Silver = 0xC0C0C0;
        public static readonly Rgb8UInt Gray = 0x808080;
        public static readonly Rgb8UInt White = 0xFFFFFF;
        public static readonly Rgb8UInt Maroon = 0x800000;
        public static readonly Rgb8UInt Red = 0xFF0000;
        public static readonly Rgb8UInt Purple = 0x800080;
        public static readonly Rgb8UInt Fuchsia = 0xFF00FF;
        public static readonly Rgb8UInt Green = 0x008000;
        public static readonly Rgb8UInt Lime = 0x00FF00;
        public static readonly Rgb8UInt Olive = 0x808000;
        public static readonly Rgb8UInt Yellow = 0xFFFF00;
        public static readonly Rgb8UInt Navy = 0x000080;
        public static readonly Rgb8UInt Blue = 0x0000FF;
        public static readonly Rgb8UInt Teal = 0x008080;
        public static readonly Rgb8UInt Aqua = 0x00FFFF;
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly Rgb8UInt AliceBlue = 0xF0F8FF;
        public static readonly Rgb8UInt AntiqueWhite = 0xFAEBD7;
        // public static readonly RgbUInt Aqua = 0x00FFFF;
        public static readonly Rgb8UInt Aquamarine = 0x7FFFD4;
        public static readonly Rgb8UInt Azure = 0xF0FFFF;
        public static readonly Rgb8UInt Beige = 0xF5F5DC;
        public static readonly Rgb8UInt Bisque = 0xFFE4C4;
        // public static readonly RgbUInt Black = 0x000000;
        public static readonly Rgb8UInt BlanchedAlmond = 0xFFEBCD;
        // public static readonly RgbUInt Blue = 0x0000FF;
        public static readonly Rgb8UInt BlueViolet = 0x8A2BE2;
        public static readonly Rgb8UInt Brown = 0xA52A2A;
        public static readonly Rgb8UInt BurlyWood = 0xDEB887;
        public static readonly Rgb8UInt CadetBlue = 0x5F9EA0;
        public static readonly Rgb8UInt Chartreuse = 0x7FFF00;
        public static readonly Rgb8UInt Chocolate = 0xD2691E;
        public static readonly Rgb8UInt Coral = 0xFF7F50;
        public static readonly Rgb8UInt CornflowerBlue = 0x6495ED;
        public static readonly Rgb8UInt Cornsilk = 0xFFF8DC;
        public static readonly Rgb8UInt Crimson = 0xDC143C;
        public static readonly Rgb8UInt Cyan = 0x00FFFF;
        public static readonly Rgb8UInt DarkBlue = 0x00008B;
        public static readonly Rgb8UInt DarkCyan = 0x008B8B;
        public static readonly Rgb8UInt DarkGoldenrod = 0xB8860B;
        public static readonly Rgb8UInt DarkGray = 0xA9A9A9;
        public static readonly Rgb8UInt DarkGreen = 0x006400;
        public static readonly Rgb8UInt DarkGrey = 0xA9A9A9;
        public static readonly Rgb8UInt DarkKhaki = 0xBDB76B;
        public static readonly Rgb8UInt DarkMagenta = 0x8B008B;
        public static readonly Rgb8UInt DarkOliveGreen = 0x556B2F;
        public static readonly Rgb8UInt DarkOrange = 0xFF8C00;
        public static readonly Rgb8UInt DarkOrchid = 0x9932CC;
        public static readonly Rgb8UInt DarkRed = 0x8B0000;
        public static readonly Rgb8UInt DarkSalmon = 0xE9967A;
        public static readonly Rgb8UInt DarkSeaGreen = 0x8FBC8F;
        public static readonly Rgb8UInt DarkStateBlue = 0x483D8B;
        public static readonly Rgb8UInt DarkStateGray = 0x2F4F4F;
        public static readonly Rgb8UInt DarkStateGrey = 0x2F4F4F;
        public static readonly Rgb8UInt DarkTurquoise = 0x00CED1;
        public static readonly Rgb8UInt DarkViolet = 0x9400D3;
        public static readonly Rgb8UInt DeepPink = 0xFF1493;
        public static readonly Rgb8UInt DeepSkyBlue = 0x00BFFF;
        public static readonly Rgb8UInt DimGray = 0x696969;
        public static readonly Rgb8UInt DimGrey = 0x696969;
        public static readonly Rgb8UInt DodgerBlue = 0x1E90FF;
        public static readonly Rgb8UInt Firebrick = 0xB22222;
        public static readonly Rgb8UInt FloralWhite = 0xFFFAF0;
        public static readonly Rgb8UInt ForestGreen = 0x228B22;
        // public static readonly RgbUInt Fuchsia = 0xFF00FF;
        public static readonly Rgb8UInt Gainsboro = 0xDCDCDC;
        public static readonly Rgb8UInt GhostWhite = 0xF8F8FF;
        public static readonly Rgb8UInt Gold = 0xFFD700;
        public static readonly Rgb8UInt Goldenrod = 0xDAA520;
        // public static readonly RgbUInt Gray = 0x808080;
        // public static readonly RgbUInt Green = 0x008000;
        public static readonly Rgb8UInt GreenYellow = 0xADFF2F;
        public static readonly Rgb8UInt Grey = 0x808080;
        public static readonly Rgb8UInt Honeydew = 0xF0FFF0;
        public static readonly Rgb8UInt HotPink = 0xFF69B4;
        public static readonly Rgb8UInt IndianRed = 0xCD5C5C;
        public static readonly Rgb8UInt Indigo = 0x4B0082;
        public static readonly Rgb8UInt Ivory = 0xFFFFF0;
        public static readonly Rgb8UInt Khaki = 0xF0E68C;
        public static readonly Rgb8UInt Lavender = 0xE6E6FA;
        public static readonly Rgb8UInt LavenderBlush = 0xFFF0F5;
        public static readonly Rgb8UInt LawnGreen = 0x7CFC00;
        public static readonly Rgb8UInt LemonChiffon = 0xFFFACD;
        public static readonly Rgb8UInt LightBlue = 0xADD8E6;
        public static readonly Rgb8UInt LightCoral = 0xF08080;
        public static readonly Rgb8UInt LightCyan = 0xE0FFFF;
        public static readonly Rgb8UInt LightGoldenrodYellow = 0xFAFAD2;
        public static readonly Rgb8UInt LightGray = 0xD3D3D3;
        public static readonly Rgb8UInt LightGreen = 0x90EE90;
        public static readonly Rgb8UInt LightGrey = 0xD3D3D3;
        public static readonly Rgb8UInt LightPink = 0xFFB6C1;
        public static readonly Rgb8UInt LightSalmon = 0xFFA07A;
        public static readonly Rgb8UInt LightSeaGreen = 0x20B2AA;
        public static readonly Rgb8UInt LightSkyBlue = 0x87CEFA;
        public static readonly Rgb8UInt LightSlateGray = 0x778899;
        public static readonly Rgb8UInt LightSlateGrey = 0x778899;
        public static readonly Rgb8UInt LightSteelBlue = 0xB0C4DE;
        public static readonly Rgb8UInt LightYellow = 0xFFFFE0;
        // public static readonly RgbUInt Lime = 0x00FF00;
        public static readonly Rgb8UInt LimeGreen = 0x32CD32;
        public static readonly Rgb8UInt Linen = 0xFAF0E6;
        public static readonly Rgb8UInt Magenta = 0xFF00FF;
        // public static readonly RgbUInt Maroon = 0x800000;
        public static readonly Rgb8UInt MediumAquamarine = 0x66CDAA;
        public static readonly Rgb8UInt MediumBlue = 0x0000CD;
        public static readonly Rgb8UInt MediumOrchid = 0xBA55D3;
        public static readonly Rgb8UInt MediumPurple = 0x9370DB;
        public static readonly Rgb8UInt MediumSeaGreen = 0x3CB371;
        public static readonly Rgb8UInt MediumStateBlue = 0x7B68EE;
        public static readonly Rgb8UInt MediumSpringGreen = 0x00FA9A;
        public static readonly Rgb8UInt MediumTurquoise = 0x48D1CC;
        public static readonly Rgb8UInt MediumVioletRed = 0xC71585;
        public static readonly Rgb8UInt MidnightBlue = 0x191970;
        public static readonly Rgb8UInt MintCream = 0xF5FFFA;
        public static readonly Rgb8UInt MistyRose = 0xFFE4E1;
        public static readonly Rgb8UInt Moccasin = 0xFFE4B5;
        public static readonly Rgb8UInt NavajoWhite = 0xFFDEAD;
        // public static readonly RgbUInt Navy = 0x000080;
        public static readonly Rgb8UInt OldLace = 0xFDF5E6;
        // public static readonly RgbUInt Olive = 0x808000;
        public static readonly Rgb8UInt OliveDrab = 0x6B8E23;
        public static readonly Rgb8UInt Orange = 0xFFA500;
        public static readonly Rgb8UInt Orangered = 0xFF4500;
        public static readonly Rgb8UInt Orchid = 0xDA70D6;
        public static readonly Rgb8UInt PaleGoldenrod = 0xEEE8AA;
        public static readonly Rgb8UInt PaleGreen = 0x98FB98;
        public static readonly Rgb8UInt PaleTurquoise = 0xAFEEEE;
        public static readonly Rgb8UInt PaleVioletRed = 0xDB7093;
        public static readonly Rgb8UInt PapayaWhip = 0xFFEFD5;
        public static readonly Rgb8UInt PeachPuff = 0xFFDAB9;
        public static readonly Rgb8UInt Peru = 0xCD853F;
        public static readonly Rgb8UInt Pink = 0xFFC0CB;
        public static readonly Rgb8UInt Plum = 0xDDA0DD;
        public static readonly Rgb8UInt PowderBlue = 0xB0E0E6;
        // public static readonly RgbUInt Purple = 0x800080;
        // public static readonly RgbUInt Red = 0xFF0000;
        public static readonly Rgb8UInt RosyBrown = 0xBC8F8F;
        public static readonly Rgb8UInt RoyalBlue = 0x4169E1;
        public static readonly Rgb8UInt SaddleBrown = 0x8B4513;
        public static readonly Rgb8UInt Salmon = 0xFA8072;
        public static readonly Rgb8UInt SandyBrown = 0xF4A460;
        public static readonly Rgb8UInt SeaGreen = 0x2E8B57;
        public static readonly Rgb8UInt SeaShell = 0xFFF5EE;
        public static readonly Rgb8UInt Sienna = 0xA0522D;
        // public static readonly RgbUInt Silver = 0xC0C0C0;
        public static readonly Rgb8UInt SkyBlue = 0x87CEEB;
        public static readonly Rgb8UInt SlateBlue = 0x6A5ACD;
        public static readonly Rgb8UInt SlateGray = 0x708090;
        public static readonly Rgb8UInt SlateGrey = 0x708090;
        public static readonly Rgb8UInt Snow = 0xFFFAFA;
        public static readonly Rgb8UInt SpringGreen = 0x00FF7F;
        public static readonly Rgb8UInt SteelBlue = 0x4682B4;
        public static readonly Rgb8UInt Tan = 0xD2B48C;
        // public static readonly RgbUInt Teal = 0x008080;
        public static readonly Rgb8UInt Thistle = 0xD8BFD8;
        public static readonly Rgb8UInt Tomato = 0xFF6347;
        public static readonly Rgb8UInt Turquoise = 0x40E0D0;
        public static readonly Rgb8UInt Violet = 0xEE82EE;
        public static readonly Rgb8UInt Wheat = 0xF5DEB3;
        // public static readonly RgbUInt White = 0xFFFFFF;
        public static readonly Rgb8UInt Whitesmoke = 0xF5F5F5;
        // public static readonly RgbUInt Yellow = 0xFFFF00;
        public static readonly Rgb8UInt YellowGreen = 0x9ACD32;
        
        public byte R;
        public byte G;
        public byte B;
        
        public Rgb8UInt(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
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