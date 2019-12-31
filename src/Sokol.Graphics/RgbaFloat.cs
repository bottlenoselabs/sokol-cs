// Original code derived from https://github.com/mellinoe/veldrid/blob/master/src/Veldrid/RgbaFloat.cs

/* 
MIT License

Copyright (c) 2017 Eric Mellino and Veldrid contributors

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

// NOTE: .NET Core might supply this; see https://github.com/dotnet/corefx/issues/2315#issuecomment-526943838

namespace Sokol
{
    public struct RgbaFloat
    {
        // Basic colors: https://www.w3.org/TR/css-color-3/#html4
        public static readonly RgbaFloat Black = 0x000000;
        public static readonly RgbaFloat Silver = 0xC0C0C0;
        public static readonly RgbaFloat Gray = 0x808080;
        public static readonly RgbaFloat White = 0xFFFFFF;
        public static readonly RgbaFloat Maroon = 0x800000;
        public static readonly RgbaFloat Red = 0xFF0000;
        public static readonly RgbaFloat Purple = 0x800080;
        public static readonly RgbaFloat Fuchsia = 0xFF00FF;
        public static readonly RgbaFloat Green = 0x008000;
        public static readonly RgbaFloat Lime = 0x00FF00;
        public static readonly RgbaFloat Olive = 0x808000;
        public static readonly RgbaFloat Yellow = 0xFFFF00;
        public static readonly RgbaFloat Navy = 0x000080;
        public static readonly RgbaFloat Blue = 0x0000FF;
        public static readonly RgbaFloat Teal = 0x008080;
        public static readonly RgbaFloat Aqua = 0x00FFFF;
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbaFloat AliceBlue = 0xF0F8FF;
        public static readonly RgbaFloat AntiqueWhite = 0xFAEBD7;
        // public static readonly RgbaFloat Aqua = 0x00FFFF;
        public static readonly RgbaFloat Aquamarine = 0x7FFFD4;
        public static readonly RgbaFloat Azure = 0xF0FFFF;
        public static readonly RgbaFloat Beige = 0xF5F5DC;
        public static readonly RgbaFloat Bisque = 0xFFE4C4;
        // public static readonly RgbaFloat Black = 0x000000;
        public static readonly RgbaFloat BlanchedAlmond = 0xFFEBCD;
        // public static readonly RgbaFloat Blue = 0x0000FF;
        public static readonly RgbaFloat BlueViolet = 0x8A2BE2;
        public static readonly RgbaFloat Brown = 0xA52A2A;
        public static readonly RgbaFloat BurlyWood = 0xDEB887;
        public static readonly RgbaFloat CadetBlue = 0x5F9EA0;
        public static readonly RgbaFloat Chartreuse = 0x7FFF00;
        public static readonly RgbaFloat Chocolate = 0xD2691E;
        public static readonly RgbaFloat Coral = 0xFF7F50;
        public static readonly RgbaFloat CornflowerBlue = 0x6495ED;
        public static readonly RgbaFloat Cornsilk = 0xFFF8DC;
        public static readonly RgbaFloat Crimson = 0xDC143C;
        public static readonly RgbaFloat Cyan = 0x00FFFF;
        public static readonly RgbaFloat DarkBlue = 0x00008B;
        public static readonly RgbaFloat DarkCyan = 0x008B8B;
        public static readonly RgbaFloat DarkGoldenrod = 0xB8860B;
        public static readonly RgbaFloat DarkGray = 0xA9A9A9;
        public static readonly RgbaFloat DarkGreen = 0x006400;
        public static readonly RgbaFloat DarkGrey = 0xA9A9A9;
        public static readonly RgbaFloat DarkKhaki = 0xBDB76B;
        public static readonly RgbaFloat DarkMagenta = 0x8B008B;
        public static readonly RgbaFloat DarkOliveGreen = 0x556B2F;
        public static readonly RgbaFloat DarkOrange = 0xFF8C00;
        public static readonly RgbaFloat DarkOrchid = 0x9932CC;
        public static readonly RgbaFloat DarkRed = 0x8B0000;
        public static readonly RgbaFloat DarkSalmon = 0xE9967A;
        public static readonly RgbaFloat DarkSeaGreen = 0x8FBC8F;
        public static readonly RgbaFloat DarkStateBlue = 0x483D8B;
        public static readonly RgbaFloat DarkStateGray = 0x2F4F4F;
        public static readonly RgbaFloat DarkStateGrey = 0x2F4F4F;
        public static readonly RgbaFloat DarkTurquoise = 0x00CED1;
        public static readonly RgbaFloat DarkViolet = 0x9400D3;
        public static readonly RgbaFloat DeepPink = 0xFF1493;
        public static readonly RgbaFloat DeepSkyBlue = 0x00BFFF;
        public static readonly RgbaFloat DimGray = 0x696969;
        public static readonly RgbaFloat DimGrey = 0x696969;
        public static readonly RgbaFloat DodgerBlue = 0x1E90FF;
        public static readonly RgbaFloat Firebrick = 0xB22222;
        public static readonly RgbaFloat FloralWhite = 0xFFFAF0;
        public static readonly RgbaFloat ForestGreen = 0x228B22;
        // public static readonly RgbaFloat Fuchsia = 0xFF00FF;
        public static readonly RgbaFloat Gainsboro = 0xDCDCDC;
        public static readonly RgbaFloat GhostWhite = 0xF8F8FF;
        public static readonly RgbaFloat Gold = 0xFFD700;
        public static readonly RgbaFloat Goldenrod = 0xDAA520;
        // public static readonly RgbaFloat Gray = 0x808080;
        // public static readonly RgbaFloat Green = 0x008000;
        public static readonly RgbaFloat GreenYellow = 0xADFF2F;
        public static readonly RgbaFloat Grey = 0x808080;
        public static readonly RgbaFloat Honeydew = 0xF0FFF0;
        public static readonly RgbaFloat HotPink = 0xFF69B4;
        public static readonly RgbaFloat IndianRed = 0xCD5C5C;
        public static readonly RgbaFloat Indigo = 0x4B0082;
        public static readonly RgbaFloat Ivory = 0xFFFFF0;
        public static readonly RgbaFloat Khaki = 0xF0E68C;
        public static readonly RgbaFloat Lavender = 0xE6E6FA;
        public static readonly RgbaFloat LavenderBlush = 0xFFF0F5;
        public static readonly RgbaFloat LawnGreen = 0x7CFC00;
        public static readonly RgbaFloat LemonChiffon = 0xFFFACD;
        public static readonly RgbaFloat LightBlue = 0xADD8E6;
        public static readonly RgbaFloat LightCoral = 0xF08080;
        public static readonly RgbaFloat LightCyan = 0xE0FFFF;
        public static readonly RgbaFloat LightGoldenrodYellow = 0xFAFAD2;
        public static readonly RgbaFloat LightGray = 0xD3D3D3;
        public static readonly RgbaFloat LightGreen = 0x90EE90;
        public static readonly RgbaFloat LightGrey = 0xD3D3D3;
        public static readonly RgbaFloat LightPink = 0xFFB6C1;
        public static readonly RgbaFloat LightSalmon = 0xFFA07A;
        public static readonly RgbaFloat LightSeaGreen = 0x20B2AA;
        public static readonly RgbaFloat LightSkyBlue = 0x87CEFA;
        public static readonly RgbaFloat LightSlateGray = 0x778899;
        public static readonly RgbaFloat LightSlateGrey = 0x778899;
        public static readonly RgbaFloat LightSteelBlue = 0xB0C4DE;
        public static readonly RgbaFloat LightYellow = 0xFFFFE0;
        // public static readonly RgbaFloat Lime = 0x00FF00;
        public static readonly RgbaFloat LimeGreen = 0x32CD32;
        public static readonly RgbaFloat Linen = 0xFAF0E6;
        public static readonly RgbaFloat Magenta = 0xFF00FF;
        // public static readonly RgbaFloat Maroon = 0x800000;
        public static readonly RgbaFloat MediumAquamarine = 0x66CDAA;
        public static readonly RgbaFloat MediumBlue = 0x0000CD;
        public static readonly RgbaFloat MediumOrchid = 0xBA55D3;
        public static readonly RgbaFloat MediumPurple = 0x9370DB;
        public static readonly RgbaFloat MediumSeaGreen = 0x3CB371;
        public static readonly RgbaFloat MediumStateBlue = 0x7B68EE;
        public static readonly RgbaFloat MediumSpringGreen = 0x00FA9A;
        public static readonly RgbaFloat MediumTurquoise = 0x48D1CC;
        public static readonly RgbaFloat MediumVioletRed = 0xC71585;
        public static readonly RgbaFloat MidnightBlue = 0x191970;
        public static readonly RgbaFloat MintCream = 0xF5FFFA;
        public static readonly RgbaFloat MistyRose = 0xFFE4E1;
        public static readonly RgbaFloat Moccasin = 0xFFE4B5;
        public static readonly RgbaFloat NavajoWhite = 0xFFDEAD;
        // public static readonly RgbaFloat Navy = 0x000080;
        public static readonly RgbaFloat OldLace = 0xFDF5E6;
        // public static readonly RgbaFloat Olive = 0x808000;
        public static readonly RgbaFloat OliveDrab = 0x6B8E23;
        public static readonly RgbaFloat Orange = 0xFFA500;
        public static readonly RgbaFloat Orangered = 0xFF4500;
        public static readonly RgbaFloat Orchid = 0xDA70D6;
        public static readonly RgbaFloat PaleGoldenrod = 0xEEE8AA;
        public static readonly RgbaFloat PaleGreen = 0x98FB98;
        public static readonly RgbaFloat PaleTurquoise = 0xAFEEEE;
        public static readonly RgbaFloat PaleVioletRed = 0xDB7093;
        public static readonly RgbaFloat PapayaWhip = 0xFFEFD5;
        public static readonly RgbaFloat PeachPuff = 0xFFDAB9;
        public static readonly RgbaFloat Peru = 0xCD853F;
        public static readonly RgbaFloat Pink = 0xFFC0CB;
        public static readonly RgbaFloat Plum = 0xDDA0DD;
        public static readonly RgbaFloat PowderBlue = 0xB0E0E6;
        // public static readonly RgbaFloat Purple = 0x800080;
        // public static readonly RgbaFloat Red = 0xFF0000;
        public static readonly RgbaFloat RosyBrown = 0xBC8F8F;
        public static readonly RgbaFloat RoyalBlue = 0x4169E1;
        public static readonly RgbaFloat SaddleBrown = 0x8B4513;
        public static readonly RgbaFloat Salmon = 0xFA8072;
        public static readonly RgbaFloat SandyBrown = 0xF4A460;
        public static readonly RgbaFloat SeaGreen = 0x2E8B57;
        public static readonly RgbaFloat SeaShell = 0xFFF5EE;
        public static readonly RgbaFloat Sienna = 0xA0522D;
        // public static readonly RgbaFloat Silver = 0xC0C0C0;
        public static readonly RgbaFloat SkyBlue = 0x87CEEB;
        public static readonly RgbaFloat SlateBlue = 0x6A5ACD;
        public static readonly RgbaFloat SlateGray = 0x708090;
        public static readonly RgbaFloat SlateGrey = 0x708090;
        public static readonly RgbaFloat Snow = 0xFFFAFA;
        public static readonly RgbaFloat SpringGreen = 0x00FF7F;
        public static readonly RgbaFloat SteelBlue = 0x4682B4;
        public static readonly RgbaFloat Tan = 0xD2B48C;
        // public static readonly RgbaFloat Teal = 0x008080;
        public static readonly RgbaFloat Thistle = 0xD8BFD8;
        public static readonly RgbaFloat Tomato = 0xFF6347;
        public static readonly RgbaFloat Turquoise = 0x40E0D0;
        public static readonly RgbaFloat Violet = 0xEE82EE;
        public static readonly RgbaFloat Wheat = 0xF5DEB3;
        // public static readonly RgbaFloat White = 0xFFFFFF;
        public static readonly RgbaFloat Whitesmoke = 0xF5F5F5;
        // public static readonly RgbaFloat Yellow = 0xFFFF00;
        public static readonly RgbaFloat YellowGreen = 0x9ACD32;
        
        public float R;
        public float G;
        public float B;
        public float A;
        
        public RgbaFloat(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public RgbaFloat(Vector4 vector4)
        {
            R = vector4.X;
            G = vector4.Y;
            B = vector4.Z;
            A = vector4.W;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(RgbaFloat color)
        {
            return new Vector4(color.R, color.G, color.B, color.A);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RgbaFloat(Vector4 vector)
        {
            return new RgbaFloat(vector);
        }
        
        public static implicit operator RgbaFloat(string hex)
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

        public static implicit operator RgbaFloat(uint value)
        {
            float r, g, b, a;
            if (value > 0xFFFFFF)
            {
                r = ((value >> 24) & 0xFF) / 255f;
                g = ((value >> 16) & 0xFF) / 255f;
                b = ((value >> 8) & 0xFF) / 255f;
                a = (value & 0xFF) / 255f;
            }
            else
            {
                r = ((value >> 16) & 0xFF) / 255f;
                g = ((value >> 8) & 0xFF) / 255f;
                b = (value & 0xFF) / 255f;
                a = 1f;
            }

            return new RgbaFloat(r, g, b, a);
        }
    }
}