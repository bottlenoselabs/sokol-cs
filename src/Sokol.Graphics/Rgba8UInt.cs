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
    public struct Rgba8UInt
    {
        // Custom colors
        public static readonly Rgba8UInt TransparentBlack = 0x00000000;
        public static readonly Rgba8UInt Transparent = 0x00000000;
        public static readonly Rgba8UInt MonoGameOrange = 0xE73C00FF;

        // Basic colors: https://www.w3.org/TR/css-color-3/#html4
        public static readonly Rgba8UInt Black = 0x000000FF;
        public static readonly Rgba8UInt Silver = 0xC0C0C0FF;
        public static readonly Rgba8UInt Gray = 0x808080FF;
        public static readonly Rgba8UInt White = 0xFFFFFFFF;
        public static readonly Rgba8UInt Maroon = 0x800000FF;
        public static readonly Rgba8UInt Red = 0xFF0000FF;
        public static readonly Rgba8UInt Purple = 0x800080FF;
        public static readonly Rgba8UInt Fuchsia = 0xFF00FFFF;
        public static readonly Rgba8UInt Green = 0x008000FF;
        public static readonly Rgba8UInt Lime = 0x00FF00FF;
        public static readonly Rgba8UInt Olive = 0x808000FF;
        public static readonly Rgba8UInt Yellow = 0xFFFF00FF;
        public static readonly Rgba8UInt Navy = 0x000080FF;
        public static readonly Rgba8UInt Blue = 0x0000FFFF;
        public static readonly Rgba8UInt Teal = 0x008080FF;
        public static readonly Rgba8UInt Aqua = 0x00FFFFFF;
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly Rgba8UInt AliceBlue = 0xF0F8FFFF;
        public static readonly Rgba8UInt AntiqueWhite = 0xFAEBD7FF;
        // public static readonly RgbaUInt Aqua = 0x00FFFFFF;
        public static readonly Rgba8UInt Aquamarine = 0x7FFFD4FF;
        public static readonly Rgba8UInt Azure = 0xF0FFFFFF;
        public static readonly Rgba8UInt Beige = 0xF5F5DCFF;
        public static readonly Rgba8UInt Bisque = 0xFFE4C4FF;
        // public static readonly RgbaUInt Black = 0x000000FF;
        public static readonly Rgba8UInt BlanchedAlmond = 0xFFEBCDFF;
        // public static readonly RgbaUInt Blue = 0x0000FFFF;
        public static readonly Rgba8UInt BlueViolet = 0x8A2BE2FF;
        public static readonly Rgba8UInt Brown = 0xA52A2AFF;
        public static readonly Rgba8UInt BurlyWood = 0xDEB887FF;
        public static readonly Rgba8UInt CadetBlue = 0x5F9EA0FF;
        public static readonly Rgba8UInt Chartreuse = 0x7FFF00FF;
        public static readonly Rgba8UInt Chocolate = 0xD2691EFF;
        public static readonly Rgba8UInt Coral = 0xFF7F50FF;
        public static readonly Rgba8UInt CornflowerBlue = 0x6495EDFF;
        public static readonly Rgba8UInt Cornsilk = 0xFFF8DCFF;
        public static readonly Rgba8UInt Crimson = 0xDC143CFF;
        public static readonly Rgba8UInt Cyan = 0x00FFFFFF;
        public static readonly Rgba8UInt DarkBlue = 0x00008BFF;
        public static readonly Rgba8UInt DarkCyan = 0x008B8BFF;
        public static readonly Rgba8UInt DarkGoldenrod = 0xB8860BFF;
        public static readonly Rgba8UInt DarkGray = 0xA9A9A9FF;
        public static readonly Rgba8UInt DarkGreen = 0x006400FF;
        public static readonly Rgba8UInt DarkGrey = 0xA9A9A9FF;
        public static readonly Rgba8UInt DarkKhaki = 0xBDB76BFF;
        public static readonly Rgba8UInt DarkMagenta = 0x8B008BFF;
        public static readonly Rgba8UInt DarkOliveGreen = 0x556B2FFF;
        public static readonly Rgba8UInt DarkOrange = 0xFF8C00FF;
        public static readonly Rgba8UInt DarkOrchid = 0x9932CCFF;
        public static readonly Rgba8UInt DarkRed = 0x8B0000FF;
        public static readonly Rgba8UInt DarkSalmon = 0xE9967AFF;
        public static readonly Rgba8UInt DarkSeaGreen = 0x8FBC8FFF;
        public static readonly Rgba8UInt DarkStateBlue = 0x483D8BFF;
        public static readonly Rgba8UInt DarkStateGray = 0x2F4F4FFF;
        public static readonly Rgba8UInt DarkStateGrey = 0x2F4F4FFF;
        public static readonly Rgba8UInt DarkTurquoise = 0x00CED1FF;
        public static readonly Rgba8UInt DarkViolet = 0x9400D3FF;
        public static readonly Rgba8UInt DeepPink = 0xFF1493FF;
        public static readonly Rgba8UInt DeepSkyBlue = 0x00BFFFFF;
        public static readonly Rgba8UInt DimGray = 0x696969FF;
        public static readonly Rgba8UInt DimGrey = 0x696969FF;
        public static readonly Rgba8UInt DodgerBlue = 0x1E90FFFF;
        public static readonly Rgba8UInt Firebrick = 0xB22222FF;
        public static readonly Rgba8UInt FloralWhite = 0xFFFAF0FF;
        public static readonly Rgba8UInt ForestGreen = 0x228B22FF;
        // public static readonly RgbaUInt Fuchsia = 0xFF00FFFF;
        public static readonly Rgba8UInt Gainsboro = 0xDCDCDCFF;
        public static readonly Rgba8UInt GhostWhite = 0xF8F8FFFF;
        public static readonly Rgba8UInt Gold = 0xFFD700FF;
        public static readonly Rgba8UInt Goldenrod = 0xDAA520FF;
        // public static readonly RgbaUInt Gray = 0x808080FF;
        // public static readonly RgbaUInt Green = 0x008000FF;
        public static readonly Rgba8UInt GreenYellow = 0xADFF2FFF;
        public static readonly Rgba8UInt Grey = 0x808080FF;
        public static readonly Rgba8UInt Honeydew = 0xF0FFF0FF;
        public static readonly Rgba8UInt HotPink = 0xFF69B4FF;
        public static readonly Rgba8UInt IndianRed = 0xCD5C5CFF;
        public static readonly Rgba8UInt Indigo = 0x4B0082FF;
        public static readonly Rgba8UInt Ivory = 0xFFFFF0FF;
        public static readonly Rgba8UInt Khaki = 0xF0E68CFF;
        public static readonly Rgba8UInt Lavender = 0xE6E6FAFF;
        public static readonly Rgba8UInt LavenderBlush = 0xFFF0F5FF;
        public static readonly Rgba8UInt LawnGreen = 0x7CFC00FF;
        public static readonly Rgba8UInt LemonChiffon = 0xFFFACDFF;
        public static readonly Rgba8UInt LightBlue = 0xADD8E6FF;
        public static readonly Rgba8UInt LightCoral = 0xF08080FF;
        public static readonly Rgba8UInt LightCyan = 0xE0FFFFFF;
        public static readonly Rgba8UInt LightGoldenrodYellow = 0xFAFAD2FF;
        public static readonly Rgba8UInt LightGray = 0xD3D3D3FF;
        public static readonly Rgba8UInt LightGreen = 0x90EE90FF;
        public static readonly Rgba8UInt LightGrey = 0xD3D3D3FF;
        public static readonly Rgba8UInt LightPink = 0xFFB6C1FF;
        public static readonly Rgba8UInt LightSalmon = 0xFFA07AFF;
        public static readonly Rgba8UInt LightSeaGreen = 0x20B2AAFF;
        public static readonly Rgba8UInt LightSkyBlue = 0x87CEFAFF;
        public static readonly Rgba8UInt LightSlateGray = 0x778899FF;
        public static readonly Rgba8UInt LightSlateGrey = 0x778899FF;
        public static readonly Rgba8UInt LightSteelBlue = 0xB0C4DEFF;
        public static readonly Rgba8UInt LightYellow = 0xFFFFE0FF;
        // public static readonly RgbaUInt Lime = 0x00FF00FF;
        public static readonly Rgba8UInt LimeGreen = 0x32CD32FF;
        public static readonly Rgba8UInt Linen = 0xFAF0E6FF;
        public static readonly Rgba8UInt Magenta = 0xFF00FFFF;
        // public static readonly RgbaUInt Maroon = 0x800000FF;
        public static readonly Rgba8UInt MediumAquamarine = 0x66CDAAFF;
        public static readonly Rgba8UInt MediumBlue = 0x0000CDFF;
        public static readonly Rgba8UInt MediumOrchid = 0xBA55D3FF;
        public static readonly Rgba8UInt MediumPurple = 0x9370DBFF;
        public static readonly Rgba8UInt MediumSeaGreen = 0x3CB371FF;
        public static readonly Rgba8UInt MediumStateBlue = 0x7B68EEFF;
        public static readonly Rgba8UInt MediumSpringGreen = 0x00FA9AFF;
        public static readonly Rgba8UInt MediumTurquoise = 0x48D1CCFF;
        public static readonly Rgba8UInt MediumVioletRed = 0xC71585FF;
        public static readonly Rgba8UInt MidnightBlue = 0x191970FF;
        public static readonly Rgba8UInt MintCream = 0xF5FFFAFF;
        public static readonly Rgba8UInt MistyRose = 0xFFE4E1FF;
        public static readonly Rgba8UInt Moccasin = 0xFFE4B5FF;
        public static readonly Rgba8UInt NavajoWhite = 0xFFDEADFF;
        // public static readonly RgbaUInt Navy = 0x000080FF;
        public static readonly Rgba8UInt OldLace = 0xFDF5E6FF;
        // public static readonly RgbaUInt Olive = 0x808000FF;
        public static readonly Rgba8UInt OliveDrab = 0x6B8E23FF;
        public static readonly Rgba8UInt Orange = 0xFFA500FF;
        public static readonly Rgba8UInt Orangered = 0xFF4500FF;
        public static readonly Rgba8UInt Orchid = 0xDA70D6FF;
        public static readonly Rgba8UInt PaleGoldenrod = 0xEEE8AAFF;
        public static readonly Rgba8UInt PaleGreen = 0x98FB98FF;
        public static readonly Rgba8UInt PaleTurquoise = 0xAFEEEEFF;
        public static readonly Rgba8UInt PaleVioletRed = 0xDB7093FF;
        public static readonly Rgba8UInt PapayaWhip = 0xFFEFD5FF;
        public static readonly Rgba8UInt PeachPuff = 0xFFDAB9FF;
        public static readonly Rgba8UInt Peru = 0xCD853FFF;
        public static readonly Rgba8UInt Pink = 0xFFC0CBFF;
        public static readonly Rgba8UInt Plum = 0xDDA0DDFF;
        public static readonly Rgba8UInt PowderBlue = 0xB0E0E6FF;
        // public static readonly RgbaUInt Purple = 0x800080FF;
        // public static readonly RgbaUInt Red = 0xFF0000FF;
        public static readonly Rgba8UInt RosyBrown = 0xBC8F8FFF;
        public static readonly Rgba8UInt RoyalBlue = 0x4169E1FF;
        public static readonly Rgba8UInt SaddleBrown = 0x8B4513FF;
        public static readonly Rgba8UInt Salmon = 0xFA8072FF;
        public static readonly Rgba8UInt SandyBrown = 0xF4A460FF;
        public static readonly Rgba8UInt SeaGreen = 0x2E8B57FF;
        public static readonly Rgba8UInt SeaShell = 0xFFF5EEFF;
        public static readonly Rgba8UInt Sienna = 0xA0522DFF;
        // public static readonly RgbaUInt Silver = 0xC0C0C0FF;
        public static readonly Rgba8UInt SkyBlue = 0x87CEEBFF;
        public static readonly Rgba8UInt SlateBlue = 0x6A5ACDFF;
        public static readonly Rgba8UInt SlateGray = 0x708090FF;
        public static readonly Rgba8UInt SlateGrey = 0x708090FF;
        public static readonly Rgba8UInt Snow = 0xFFFAFAFF;
        public static readonly Rgba8UInt SpringGreen = 0x00FF7FFF;
        public static readonly Rgba8UInt SteelBlue = 0x4682B4FF;
        public static readonly Rgba8UInt Tan = 0xD2B48CFF;
        // public static readonly RgbaUInt Teal = 0x008080FF;
        public static readonly Rgba8UInt Thistle = 0xD8BFD8FF;
        public static readonly Rgba8UInt Tomato = 0xFF6347FF;
        public static readonly Rgba8UInt Turquoise = 0x40E0D0FF;
        public static readonly Rgba8UInt Violet = 0xEE82EEFF;
        public static readonly Rgba8UInt Wheat = 0xF5DEB3FF;
        // public static readonly RgbaUInt White = 0xFFFFFFFF;
        public static readonly Rgba8UInt Whitesmoke = 0xF5F5F5FF;
        // public static readonly RgbaUInt Yellow = 0xFFFF00FF;
        public static readonly Rgba8UInt YellowGreen = 0x9ACD32FF;
        
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        
        public Rgba8UInt(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode(), A.GetHashCode());
        }
        
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }
 
        public static implicit operator Rgba8UInt(string hex)
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
                if (span.Length == 6)
                {
                    value = (value << 8) + 0xFF;
                }
            }
            catch
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{hex}' as an unsigned 32-bit integer.");
            }
            
            return value;
        }

        public static implicit operator Rgba8UInt(uint value)
        {
            var r = (byte)((value >> 24) & 0xFF);
            var g = (byte)((value >> 16) & 0xFF);
            var b = (byte)((value >> 8) & 0xFF);
            var a = (byte)(value & 0xFF);

            return new Rgba8UInt(r, g, b, a);
        }
    }
}