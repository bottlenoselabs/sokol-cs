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

Copyright (c) 2020 Lucas Girouard-Stranks

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
        // Custom colors
        public static readonly RgbaFloat TransparentBlack = 0x00000000;
        public static readonly RgbaFloat Transparent = 0x00000000;
        public static readonly RgbaFloat MonoGameOrange = 0xE73C00FF;

        // Basic colors: https://www.w3.org/TR/css-color-3/#html4
        public static readonly RgbaFloat Black = 0x000000FF;
        public static readonly RgbaFloat Silver = 0xC0C0C0FF;
        public static readonly RgbaFloat Gray = 0x808080FF;
        public static readonly RgbaFloat White = 0xFFFFFFFF;
        public static readonly RgbaFloat Maroon = 0x800000FF;
        public static readonly RgbaFloat Red = 0xFF0000FF;
        public static readonly RgbaFloat Purple = 0x800080FF;
        public static readonly RgbaFloat Fuchsia = 0xFF00FFFF;
        public static readonly RgbaFloat Green = 0x008000FF;
        public static readonly RgbaFloat Lime = 0x00FF00FF;
        public static readonly RgbaFloat Olive = 0x808000FF;
        public static readonly RgbaFloat Yellow = 0xFFFF00FF;
        public static readonly RgbaFloat Navy = 0x000080FF;
        public static readonly RgbaFloat Blue = 0x0000FFFF;
        public static readonly RgbaFloat Teal = 0x008080FF;
        public static readonly RgbaFloat Aqua = 0x00FFFFFF;
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbaFloat AliceBlue = 0xF0F8FFFF;
        public static readonly RgbaFloat AntiqueWhite = 0xFAEBD7FF;
        // public static readonly RgbaFloat Aqua = 0x00FFFFFF;
        public static readonly RgbaFloat Aquamarine = 0x7FFFD4FF;
        public static readonly RgbaFloat Azure = 0xF0FFFFFF;
        public static readonly RgbaFloat Beige = 0xF5F5DCFF;
        public static readonly RgbaFloat Bisque = 0xFFE4C4FF;
        // public static readonly RgbaFloat Black = 0x000000FF;
        public static readonly RgbaFloat BlanchedAlmond = 0xFFEBCDFF;
        // public static readonly RgbaFloat Blue = 0x0000FFFF;
        public static readonly RgbaFloat BlueViolet = 0x8A2BE2FF;
        public static readonly RgbaFloat Brown = 0xA52A2AFF;
        public static readonly RgbaFloat BurlyWood = 0xDEB887FF;
        public static readonly RgbaFloat CadetBlue = 0x5F9EA0FF;
        public static readonly RgbaFloat Chartreuse = 0x7FFF00FF;
        public static readonly RgbaFloat Chocolate = 0xD2691EFF;
        public static readonly RgbaFloat Coral = 0xFF7F50FF;
        public static readonly RgbaFloat CornflowerBlue = 0x6495EDFF;
        public static readonly RgbaFloat Cornsilk = 0xFFF8DCFF;
        public static readonly RgbaFloat Crimson = 0xDC143CFF;
        public static readonly RgbaFloat Cyan = 0x00FFFFFF;
        public static readonly RgbaFloat DarkBlue = 0x00008BFF;
        public static readonly RgbaFloat DarkCyan = 0x008B8BFF;
        public static readonly RgbaFloat DarkGoldenrod = 0xB8860BFF;
        public static readonly RgbaFloat DarkGray = 0xA9A9A9FF;
        public static readonly RgbaFloat DarkGreen = 0x006400FF;
        public static readonly RgbaFloat DarkGrey = 0xA9A9A9FF;
        public static readonly RgbaFloat DarkKhaki = 0xBDB76BFF;
        public static readonly RgbaFloat DarkMagenta = 0x8B008BFF;
        public static readonly RgbaFloat DarkOliveGreen = 0x556B2FFF;
        public static readonly RgbaFloat DarkOrange = 0xFF8C00FF;
        public static readonly RgbaFloat DarkOrchid = 0x9932CCFF;
        public static readonly RgbaFloat DarkRed = 0x8B0000FF;
        public static readonly RgbaFloat DarkSalmon = 0xE9967AFF;
        public static readonly RgbaFloat DarkSeaGreen = 0x8FBC8FFF;
        public static readonly RgbaFloat DarkStateBlue = 0x483D8BFF;
        public static readonly RgbaFloat DarkStateGray = 0x2F4F4FFF;
        public static readonly RgbaFloat DarkStateGrey = 0x2F4F4FFF;
        public static readonly RgbaFloat DarkTurquoise = 0x00CED1FF;
        public static readonly RgbaFloat DarkViolet = 0x9400D3FF;
        public static readonly RgbaFloat DeepPink = 0xFF1493FF;
        public static readonly RgbaFloat DeepSkyBlue = 0x00BFFFFF;
        public static readonly RgbaFloat DimGray = 0x696969FF;
        public static readonly RgbaFloat DimGrey = 0x696969FF;
        public static readonly RgbaFloat DodgerBlue = 0x1E90FFFF;
        public static readonly RgbaFloat Firebrick = 0xB22222FF;
        public static readonly RgbaFloat FloralWhite = 0xFFFAF0FF;
        public static readonly RgbaFloat ForestGreen = 0x228B22FF;
        // public static readonly RgbaFloat Fuchsia = 0xFF00FFFF;
        public static readonly RgbaFloat Gainsboro = 0xDCDCDCFF;
        public static readonly RgbaFloat GhostWhite = 0xF8F8FFFF;
        public static readonly RgbaFloat Gold = 0xFFD700FF;
        public static readonly RgbaFloat Goldenrod = 0xDAA520FF;
        // public static readonly RgbaFloat Gray = 0x808080FF;
        // public static readonly RgbaFloat Green = 0x008000FF;
        public static readonly RgbaFloat GreenYellow = 0xADFF2FFF;
        public static readonly RgbaFloat Grey = 0x808080FF;
        public static readonly RgbaFloat Honeydew = 0xF0FFF0FF;
        public static readonly RgbaFloat HotPink = 0xFF69B4FF;
        public static readonly RgbaFloat IndianRed = 0xCD5C5CFF;
        public static readonly RgbaFloat Indigo = 0x4B0082FF;
        public static readonly RgbaFloat Ivory = 0xFFFFF0FF;
        public static readonly RgbaFloat Khaki = 0xF0E68CFF;
        public static readonly RgbaFloat Lavender = 0xE6E6FAFF;
        public static readonly RgbaFloat LavenderBlush = 0xFFF0F5FF;
        public static readonly RgbaFloat LawnGreen = 0x7CFC00FF;
        public static readonly RgbaFloat LemonChiffon = 0xFFFACDFF;
        public static readonly RgbaFloat LightBlue = 0xADD8E6FF;
        public static readonly RgbaFloat LightCoral = 0xF08080FF;
        public static readonly RgbaFloat LightCyan = 0xE0FFFFFF;
        public static readonly RgbaFloat LightGoldenrodYellow = 0xFAFAD2FF;
        public static readonly RgbaFloat LightGray = 0xD3D3D3FF;
        public static readonly RgbaFloat LightGreen = 0x90EE90FF;
        public static readonly RgbaFloat LightGrey = 0xD3D3D3FF;
        public static readonly RgbaFloat LightPink = 0xFFB6C1FF;
        public static readonly RgbaFloat LightSalmon = 0xFFA07AFF;
        public static readonly RgbaFloat LightSeaGreen = 0x20B2AAFF;
        public static readonly RgbaFloat LightSkyBlue = 0x87CEFAFF;
        public static readonly RgbaFloat LightSlateGray = 0x778899FF;
        public static readonly RgbaFloat LightSlateGrey = 0x778899FF;
        public static readonly RgbaFloat LightSteelBlue = 0xB0C4DEFF;
        public static readonly RgbaFloat LightYellow = 0xFFFFE0FF;
        // public static readonly RgbaFloat Lime = 0x00FF00FF;
        public static readonly RgbaFloat LimeGreen = 0x32CD32FF;
        public static readonly RgbaFloat Linen = 0xFAF0E6FF;
        public static readonly RgbaFloat Magenta = 0xFF00FFFF;
        // public static readonly RgbaFloat Maroon = 0x800000FF;
        public static readonly RgbaFloat MediumAquamarine = 0x66CDAAFF;
        public static readonly RgbaFloat MediumBlue = 0x0000CDFF;
        public static readonly RgbaFloat MediumOrchid = 0xBA55D3FF;
        public static readonly RgbaFloat MediumPurple = 0x9370DBFF;
        public static readonly RgbaFloat MediumSeaGreen = 0x3CB371FF;
        public static readonly RgbaFloat MediumStateBlue = 0x7B68EEFF;
        public static readonly RgbaFloat MediumSpringGreen = 0x00FA9AFF;
        public static readonly RgbaFloat MediumTurquoise = 0x48D1CCFF;
        public static readonly RgbaFloat MediumVioletRed = 0xC71585FF;
        public static readonly RgbaFloat MidnightBlue = 0x191970FF;
        public static readonly RgbaFloat MintCream = 0xF5FFFAFF;
        public static readonly RgbaFloat MistyRose = 0xFFE4E1FF;
        public static readonly RgbaFloat Moccasin = 0xFFE4B5FF;
        public static readonly RgbaFloat NavajoWhite = 0xFFDEADFF;
        // public static readonly RgbaFloat Navy = 0x000080FF;
        public static readonly RgbaFloat OldLace = 0xFDF5E6FF;
        // public static readonly RgbaFloat Olive = 0x808000FF;
        public static readonly RgbaFloat OliveDrab = 0x6B8E23FF;
        public static readonly RgbaFloat Orange = 0xFFA500FF;
        public static readonly RgbaFloat Orangered = 0xFF4500FF;
        public static readonly RgbaFloat Orchid = 0xDA70D6FF;
        public static readonly RgbaFloat PaleGoldenrod = 0xEEE8AAFF;
        public static readonly RgbaFloat PaleGreen = 0x98FB98FF;
        public static readonly RgbaFloat PaleTurquoise = 0xAFEEEEFF;
        public static readonly RgbaFloat PaleVioletRed = 0xDB7093FF;
        public static readonly RgbaFloat PapayaWhip = 0xFFEFD5FF;
        public static readonly RgbaFloat PeachPuff = 0xFFDAB9FF;
        public static readonly RgbaFloat Peru = 0xCD853FFF;
        public static readonly RgbaFloat Pink = 0xFFC0CBFF;
        public static readonly RgbaFloat Plum = 0xDDA0DDFF;
        public static readonly RgbaFloat PowderBlue = 0xB0E0E6FF;
        // public static readonly RgbaFloat Purple = 0x800080FF;
        // public static readonly RgbaFloat Red = 0xFF0000FF;
        public static readonly RgbaFloat RosyBrown = 0xBC8F8FFF;
        public static readonly RgbaFloat RoyalBlue = 0x4169E1FF;
        public static readonly RgbaFloat SaddleBrown = 0x8B4513FF;
        public static readonly RgbaFloat Salmon = 0xFA8072FF;
        public static readonly RgbaFloat SandyBrown = 0xF4A460FF;
        public static readonly RgbaFloat SeaGreen = 0x2E8B57FF;
        public static readonly RgbaFloat SeaShell = 0xFFF5EEFF;
        public static readonly RgbaFloat Sienna = 0xA0522DFF;
        // public static readonly RgbaFloat Silver = 0xC0C0C0FF;
        public static readonly RgbaFloat SkyBlue = 0x87CEEBFF;
        public static readonly RgbaFloat SlateBlue = 0x6A5ACDFF;
        public static readonly RgbaFloat SlateGray = 0x708090FF;
        public static readonly RgbaFloat SlateGrey = 0x708090FF;
        public static readonly RgbaFloat Snow = 0xFFFAFAFF;
        public static readonly RgbaFloat SpringGreen = 0x00FF7FFF;
        public static readonly RgbaFloat SteelBlue = 0x4682B4FF;
        public static readonly RgbaFloat Tan = 0xD2B48CFF;
        // public static readonly RgbaFloat Teal = 0x008080FF;
        public static readonly RgbaFloat Thistle = 0xD8BFD8FF;
        public static readonly RgbaFloat Tomato = 0xFF6347FF;
        public static readonly RgbaFloat Turquoise = 0x40E0D0FF;
        public static readonly RgbaFloat Violet = 0xEE82EEFF;
        public static readonly RgbaFloat Wheat = 0xF5DEB3FF;
        // public static readonly RgbaFloat White = 0xFFFFFFFF;
        public static readonly RgbaFloat Whitesmoke = 0xF5F5F5FF;
        // public static readonly RgbaFloat Yellow = 0xFFFF00FF;
        public static readonly RgbaFloat YellowGreen = 0x9ACD32FF;
        
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

        public static implicit operator RgbaFloat(uint value)
        {
            var r = ((value >> 24) & 0xFF) / 255f;
            var g = ((value >> 16) & 0xFF) / 255f;
            var b = ((value >> 8) & 0xFF) / 255f;
            var a = (value & 0xFF) / 255f;

            return new RgbaFloat(r, g, b, a);
        }
    }
}