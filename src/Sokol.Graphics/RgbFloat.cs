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
        public static readonly RgbFloat Black = "#000000";
        public static readonly RgbFloat Silver = "#C0C0C0";
        public static readonly RgbFloat Gray = "#808080";
        public static readonly RgbFloat White = "#FFFFFF";
        public static readonly RgbFloat Maroon = "#800000";
        public static readonly RgbFloat Red = "#FF0000";
        public static readonly RgbFloat Purple = "#800080";
        public static readonly RgbFloat Fuchsia = "#FF00FF";
        public static readonly RgbFloat Green = "#008000";
        public static readonly RgbFloat Lime = "#00FF00";
        public static readonly RgbFloat Olive = "#808000";
        public static readonly RgbFloat Yellow = "#FFFF00";
        public static readonly RgbFloat Navy = "#000080";
        public static readonly RgbFloat Blue = "#0000FF";
        public static readonly RgbFloat Teal = "#008080";
        public static readonly RgbFloat Aqua = "#00FFFF";
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbFloat AliceBlue = "#F0F8FF";
        public static readonly RgbFloat AntiqueWhite = "#FAEBD7";
        // public static readonly RgbFloat Aqua = "#00FFFF";
        public static readonly RgbFloat Aquamarine = "#7FFFD4";
        public static readonly RgbFloat Azure = "#F0FFFF";
        public static readonly RgbFloat Beige = "#F5F5DC";
        public static readonly RgbFloat Bisque = "#FFE4C4";
        // public static readonly RgbFloat Black = "#000000";
        public static readonly RgbFloat BlanchedAlmond = "#FFEBCD";
        // public static readonly RgbFloat Blue = "#0000FF";
        public static readonly RgbFloat BlueViolet = "#8A2BE2";
        public static readonly RgbFloat Brown = "#A52A2A";
        public static readonly RgbFloat BurlyWood = "#DEB887";
        public static readonly RgbFloat CadetBlue = "#5F9EA0";
        public static readonly RgbFloat Chartreuse = "#7FFF00";
        public static readonly RgbFloat Chocolate = "#D2691E";
        public static readonly RgbFloat Coral = "#FF7F50";
        public static readonly RgbFloat CornflowerBlue = "#6495ED";
        public static readonly RgbFloat Cornsilk = "#FFF8DC";
        public static readonly RgbFloat Crimson = "#DC143C";
        public static readonly RgbFloat Cyan = "#00FFFF";
        public static readonly RgbFloat DarkBlue = "#00008B";
        public static readonly RgbFloat DarkCyan = "#008B8B";
        public static readonly RgbFloat DarkGoldenrod = "#B8860B";
        public static readonly RgbFloat DarkGray = "#A9A9A9";
        public static readonly RgbFloat DarkGreen = "#006400";
        public static readonly RgbFloat DarkGrey = "#A9A9A9";
        public static readonly RgbFloat DarkKhaki = "#BDB76B";
        public static readonly RgbFloat DarkMagenta = "#8B008B";
        public static readonly RgbFloat DarkOliveGreen = "#556B2F";
        public static readonly RgbFloat DarkOrange = "#FF8C00";
        public static readonly RgbFloat DarkOrchid = "#9932CC";
        public static readonly RgbFloat DarkRed = "#8B0000";
        public static readonly RgbFloat DarkSalmon = "#E9967A";
        public static readonly RgbFloat DarkSeaGreen = "#8FBC8F";
        public static readonly RgbFloat DarkStateBlue = "#483D8B";
        public static readonly RgbFloat DarkStateGray = "#2F4F4F";
        public static readonly RgbFloat DarkStateGrey = "#2F4F4F";
        public static readonly RgbFloat DarkTurquoise = "#00CED1";
        public static readonly RgbFloat DarkViolet = "#9400D3";
        public static readonly RgbFloat DeepPink = "#FF1493";
        public static readonly RgbFloat DeepSkyBlue = "#00BFFF";
        public static readonly RgbFloat DimGray = "#696969";
        public static readonly RgbFloat DimGrey = "#696969";
        public static readonly RgbFloat DodgerBlue = "#1E90FF";
        public static readonly RgbFloat Firebrick = "#B22222";
        public static readonly RgbFloat FloralWhite = "#FFFAF0";
        public static readonly RgbFloat ForestGreen = "#228B22";
        // public static readonly RgbFloat Fuchsia = "#FF00FF";
        public static readonly RgbFloat Gainsboro = "#DCDCDC";
        public static readonly RgbFloat GhostWhite = "#F8F8FF";
        public static readonly RgbFloat Gold = "#FFD700";
        public static readonly RgbFloat Goldenrod = "#DAA520";
        // public static readonly RgbFloat Gray = "#808080";
        // public static readonly RgbFloat Green = "#008000";
        public static readonly RgbFloat GreenYellow = "#ADFF2F";
        public static readonly RgbFloat Grey = "#808080";
        public static readonly RgbFloat Honeydew = "#F0FFF0";
        public static readonly RgbFloat HotPink = "#FF69B4";
        public static readonly RgbFloat IndianRed = "#CD5C5C";
        public static readonly RgbFloat Indigo = "#4B0082";
        public static readonly RgbFloat Ivory = "#FFFFF0";
        public static readonly RgbFloat Khaki = "#F0E68C";
        public static readonly RgbFloat Lavender = "#E6E6FA";
        public static readonly RgbFloat LavenderBlush = "#FFF0F5";
        public static readonly RgbFloat LawnGreen = "#7CFC00";
        public static readonly RgbFloat LemonChiffon = "#FFFACD";
        public static readonly RgbFloat LightBlue = "#ADD8E6";
        public static readonly RgbFloat LightCoral = "#F08080";
        public static readonly RgbFloat LightCyan = "#E0FFFF";
        public static readonly RgbFloat LightGoldenrodYellow = "#FAFAD2";
        public static readonly RgbFloat LightGray = "#D3D3D3";
        public static readonly RgbFloat LightGreen = "#90EE90";
        public static readonly RgbFloat LightGrey = "#D3D3D3";
        public static readonly RgbFloat LightPink = "#FFB6C1";
        public static readonly RgbFloat LightSalmon = "#FFA07A";
        public static readonly RgbFloat LightSeaGreen = "#20B2AA";
        public static readonly RgbFloat LightSkyBlue = "#87CEFA";
        public static readonly RgbFloat LightSlateGray = "#778899";
        public static readonly RgbFloat LightSlateGrey = "#778899";
        public static readonly RgbFloat LightSteelBlue = "#B0C4DE";
        public static readonly RgbFloat LightYellow = "#FFFFE0";
        // public static readonly RgbFloat Lime = "#00FF00";
        public static readonly RgbFloat LimeGreen = "#32CD32";
        public static readonly RgbFloat Linen = "#FAF0E6";
        public static readonly RgbFloat Magenta = "#FF00FF";
        // public static readonly RgbFloat Maroon = "#800000";
        public static readonly RgbFloat MediumAquamarine = "#66CDAA";
        public static readonly RgbFloat MediumBlue = "#0000CD";
        public static readonly RgbFloat MediumOrchid = "#BA55D3";
        public static readonly RgbFloat MediumPurple = "#9370DB";
        public static readonly RgbFloat MediumSeaGreen = "#3CB371";
        public static readonly RgbFloat MediumStateBlue = "#7B68EE";
        public static readonly RgbFloat MediumSpringGreen = "#00FA9A";
        public static readonly RgbFloat MediumTurquoise = "#48D1CC";
        public static readonly RgbFloat MediumVioletRed = "#C71585";
        public static readonly RgbFloat MidnightBlue = "#191970";
        public static readonly RgbFloat MintCream = "#F5FFFA";
        public static readonly RgbFloat MistyRose = "#FFE4E1";
        public static readonly RgbFloat Moccasin = "#FFE4B5";
        public static readonly RgbFloat NavajoWhite = "#FFDEAD";
        // public static readonly RgbFloat Navy = "#000080";
        public static readonly RgbFloat OldLace = "#FDF5E6";
        // public static readonly RgbFloat Olive = "#808000";
        public static readonly RgbFloat OliveDrab = "#6B8E23";
        public static readonly RgbFloat Orange = "#FFA500";
        public static readonly RgbFloat Orangered = "#FF4500";
        public static readonly RgbFloat Orchid = "#DA70D6";
        public static readonly RgbFloat PaleGoldenrod = "#EEE8AA";
        public static readonly RgbFloat PaleGreen = "#98FB98";
        public static readonly RgbFloat PaleTurquoise = "#AFEEEE";
        public static readonly RgbFloat PaleVioletRed = "#DB7093";
        public static readonly RgbFloat PapayaWhip = "#FFEFD5";
        public static readonly RgbFloat PeachPuff = "#FFDAB9";
        public static readonly RgbFloat Peru = "#CD853F";
        public static readonly RgbFloat Pink = "#FFC0CB";
        public static readonly RgbFloat Plum = "#DDA0DD";
        public static readonly RgbFloat PowderBlue = "#B0E0E6";
        // public static readonly RgbFloat Purple = "#800080";
        // public static readonly RgbFloat Red = "#FF0000";
        public static readonly RgbFloat PosyBrown = "#BC8F8F";
        public static readonly RgbFloat RoyalBlue = "#4169E1";
        public static readonly RgbFloat SaddleBrown = "#8B4513";
        public static readonly RgbFloat Salmon = "#FA8072";
        public static readonly RgbFloat SandyBrown = "#F4A460";
        public static readonly RgbFloat SeaGreen = "#2E8B57";
        public static readonly RgbFloat SeaShell = "#FFF5EE";
        public static readonly RgbFloat Sienna = "#A0522D";
        // public static readonly RgbFloat Silver = "#C0C0C0";
        public static readonly RgbFloat SkyBlue = "#87CEEB";
        public static readonly RgbFloat SlateBlue = "#6A5ACD";
        public static readonly RgbFloat SlateGray = "#708090";
        public static readonly RgbFloat SlateGrey = "#708090";
        public static readonly RgbFloat Snow = "#FFFAFA";
        public static readonly RgbFloat SpringGreen = "#00FF7F";
        public static readonly RgbFloat SteelBlue = "#4682B4";
        public static readonly RgbFloat Tan = "#D2B48C";
        // public static readonly RgbFloat Teal = "#008080";
        public static readonly RgbFloat Thistle = "#D8BFD8";
        public static readonly RgbFloat Tomato = "#FF6347";
        public static readonly RgbFloat Turquoise = "#40E0D0";
        public static readonly RgbFloat Violet = "#EE82EE";
        public static readonly RgbFloat Wheat = "#F5DEB3";
        // public static readonly RgbFloat White = "#FFFFFF";
        public static readonly RgbFloat Whitesmoke = "#F5F5F5";
        // public static readonly RgbFloat Yellow = "#FFFF00";
        public static readonly RgbFloat YellowGreen = "#9ACD32";
        
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
            if (hex[0] != '#' || hex.Length != 7 || !uint.TryParse(hex.AsSpan(1), out var value))
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{hex}'.");
            }

            var r = ((value >> 16) & 0xFF) / 255d;
            var g = ((value >> 8) & 0xFF) / 255d;
            var b = (value & 0xFF) / 255d;
            
            return new RgbFloat((float)r, (float)g, (float)b);
        }
    }
}