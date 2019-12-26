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
        public static readonly RgbaFloat Black = "#000000";
        public static readonly RgbaFloat Silver = "#C0C0C0";
        public static readonly RgbaFloat Gray = "#808080";
        public static readonly RgbaFloat White = "#FFFFFF";
        public static readonly RgbaFloat Maroon = "#800000";
        public static readonly RgbaFloat Red = "#FF0000";
        public static readonly RgbaFloat Purple = "#800080";
        public static readonly RgbaFloat Fuchsia = "#FF00FF";
        public static readonly RgbaFloat Green = "#008000";
        public static readonly RgbaFloat Lime = "#00FF00";
        public static readonly RgbaFloat Olive = "#808000";
        public static readonly RgbaFloat Yellow = "#FFFF00";
        public static readonly RgbaFloat Navy = "#000080";
        public static readonly RgbaFloat Blue = "#0000FF";
        public static readonly RgbaFloat Teal = "#008080";
        public static readonly RgbaFloat Aqua = "#00FFFF";
        
        // Extended colors: https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbaFloat AliceBlue = "#F0F8FF";
        public static readonly RgbaFloat AntiqueWhite = "#FAEBD7";
        // public static readonly RgbaFloat Aqua = "#00FFFF";
        public static readonly RgbaFloat Aquamarine = "#7FFFD4";
        public static readonly RgbaFloat Azure = "#F0FFFF";
        public static readonly RgbaFloat Beige = "#F5F5DC";
        public static readonly RgbaFloat Bisque = "#FFE4C4";
        // public static readonly RgbaFloat Black = "#000000";
        public static readonly RgbaFloat BlanchedAlmond = "#FFEBCD";
        // public static readonly RgbaFloat Blue = "#0000FF";
        public static readonly RgbaFloat BlueViolet = "#8A2BE2";
        public static readonly RgbaFloat Brown = "#A52A2A";
        public static readonly RgbaFloat BurlyWood = "#DEB887";
        public static readonly RgbaFloat CadetBlue = "#5F9EA0";
        public static readonly RgbaFloat Chartreuse = "#7FFF00";
        public static readonly RgbaFloat Chocolate = "#D2691E";
        public static readonly RgbaFloat Coral = "#FF7F50";
        public static readonly RgbaFloat CornflowerBlue = "#6495ED";
        public static readonly RgbaFloat Cornsilk = "#FFF8DC";
        public static readonly RgbaFloat Crimson = "#DC143C";
        public static readonly RgbaFloat Cyan = "#00FFFF";
        public static readonly RgbaFloat DarkBlue = "#00008B";
        public static readonly RgbaFloat DarkCyan = "#008B8B";
        public static readonly RgbaFloat DarkGoldenrod = "#B8860B";
        public static readonly RgbaFloat DarkGray = "#A9A9A9";
        public static readonly RgbaFloat DarkGreen = "#006400";
        public static readonly RgbaFloat DarkGrey = "#A9A9A9";
        public static readonly RgbaFloat DarkKhaki = "#BDB76B";
        public static readonly RgbaFloat DarkMagenta = "#8B008B";
        public static readonly RgbaFloat DarkOliveGreen = "#556B2F";
        public static readonly RgbaFloat DarkOrange = "#FF8C00";
        public static readonly RgbaFloat DarkOrchid = "#9932CC";
        public static readonly RgbaFloat DarkRed = "#8B0000";
        public static readonly RgbaFloat DarkSalmon = "#E9967A";
        public static readonly RgbaFloat DarkSeaGreen = "#8FBC8F";
        public static readonly RgbaFloat DarkStateBlue = "#483D8B";
        public static readonly RgbaFloat DarkStateGray = "#2F4F4F";
        public static readonly RgbaFloat DarkStateGrey = "#2F4F4F";
        public static readonly RgbaFloat DarkTurquoise = "#00CED1";
        public static readonly RgbaFloat DarkViolet = "#9400D3";
        public static readonly RgbaFloat DeepPink = "#FF1493";
        public static readonly RgbaFloat DeepSkyBlue = "#00BFFF";
        public static readonly RgbaFloat DimGray = "#696969";
        public static readonly RgbaFloat DimGrey = "#696969";
        public static readonly RgbaFloat DodgerBlue = "#1E90FF";
        public static readonly RgbaFloat Firebrick = "#B22222";
        public static readonly RgbaFloat FloralWhite = "#FFFAF0";
        public static readonly RgbaFloat ForestGreen = "#228B22";
        // public static readonly RgbaFloat Fuchsia = "#FF00FF";
        public static readonly RgbaFloat Gainsboro = "#DCDCDC";
        public static readonly RgbaFloat GhostWhite = "#F8F8FF";
        public static readonly RgbaFloat Gold = "#FFD700";
        public static readonly RgbaFloat Goldenrod = "#DAA520";
        // public static readonly RgbaFloat Gray = "#808080";
        // public static readonly RgbaFloat Green = "#008000";
        public static readonly RgbaFloat GreenYellow = "#ADFF2F";
        public static readonly RgbaFloat Grey = "#808080";
        public static readonly RgbaFloat Honeydew = "#F0FFF0";
        public static readonly RgbaFloat HotPink = "#FF69B4";
        public static readonly RgbaFloat IndianRed = "#CD5C5C";
        public static readonly RgbaFloat Indigo = "#4B0082";
        public static readonly RgbaFloat Ivory = "#FFFFF0";
        public static readonly RgbaFloat Khaki = "#F0E68C";
        public static readonly RgbaFloat Lavender = "#E6E6FA";
        public static readonly RgbaFloat LavenderBlush = "#FFF0F5";
        public static readonly RgbaFloat LawnGreen = "#7CFC00";
        public static readonly RgbaFloat LemonChiffon = "#FFFACD";
        public static readonly RgbaFloat LightBlue = "#ADD8E6";
        public static readonly RgbaFloat LightCoral = "#F08080";
        public static readonly RgbaFloat LightCyan = "#E0FFFF";
        public static readonly RgbaFloat LightGoldenrodYellow = "#FAFAD2";
        public static readonly RgbaFloat LightGray = "#D3D3D3";
        public static readonly RgbaFloat LightGreen = "#90EE90";
        public static readonly RgbaFloat LightGrey = "#D3D3D3";
        public static readonly RgbaFloat LightPink = "#FFB6C1";
        public static readonly RgbaFloat LightSalmon = "#FFA07A";
        public static readonly RgbaFloat LightSeaGreen = "#20B2AA";
        public static readonly RgbaFloat LightSkyBlue = "#87CEFA";
        public static readonly RgbaFloat LightSlateGray = "#778899";
        public static readonly RgbaFloat LightSlateGrey = "#778899";
        public static readonly RgbaFloat LightSteelBlue = "#B0C4DE";
        public static readonly RgbaFloat LightYellow = "#FFFFE0";
        // public static readonly RgbaFloat Lime = "#00FF00";
        public static readonly RgbaFloat LimeGreen = "#32CD32";
        public static readonly RgbaFloat Linen = "#FAF0E6";
        public static readonly RgbaFloat Magenta = "#FF00FF";
        // public static readonly RgbaFloat Maroon = "#800000";
        public static readonly RgbaFloat MediumAquamarine = "#66CDAA";
        public static readonly RgbaFloat MediumBlue = "#0000CD";
        public static readonly RgbaFloat MediumOrchid = "#BA55D3";
        public static readonly RgbaFloat MediumPurple = "#9370DB";
        public static readonly RgbaFloat MediumSeaGreen = "#3CB371";
        public static readonly RgbaFloat MediumStateBlue = "#7B68EE";
        public static readonly RgbaFloat MediumSpringGreen = "#00FA9A";
        public static readonly RgbaFloat MediumTurquoise = "#48D1CC";
        public static readonly RgbaFloat MediumVioletRed = "#C71585";
        public static readonly RgbaFloat MidnightBlue = "#191970";
        public static readonly RgbaFloat MintCream = "#F5FFFA";
        public static readonly RgbaFloat MistyRose = "#FFE4E1";
        public static readonly RgbaFloat Moccasin = "#FFE4B5";
        public static readonly RgbaFloat NavajoWhite = "#FFDEAD";
        // public static readonly RgbaFloat Navy = "#000080";
        public static readonly RgbaFloat OldLace = "#FDF5E6";
        // public static readonly RgbaFloat Olive = "#808000";
        public static readonly RgbaFloat OliveDrab = "#6B8E23";
        public static readonly RgbaFloat Orange = "#FFA500";
        public static readonly RgbaFloat Orangered = "#FF4500";
        public static readonly RgbaFloat Orchid = "#DA70D6";
        public static readonly RgbaFloat PaleGoldenrod = "#EEE8AA";
        public static readonly RgbaFloat PaleGreen = "#98FB98";
        public static readonly RgbaFloat PaleTurquoise = "#AFEEEE";
        public static readonly RgbaFloat PaleVioletRed = "#DB7093";
        public static readonly RgbaFloat PapayaWhip = "#FFEFD5";
        public static readonly RgbaFloat PeachPuff = "#FFDAB9";
        public static readonly RgbaFloat Peru = "#CD853F";
        public static readonly RgbaFloat Pink = "#FFC0CB";
        public static readonly RgbaFloat Plum = "#DDA0DD";
        public static readonly RgbaFloat PowderBlue = "#B0E0E6";
        // public static readonly RgbaFloat Purple = "#800080";
        // public static readonly RgbaFloat Red = "#FF0000";
        public static readonly RgbaFloat RosyBrown = "#BC8F8F";
        public static readonly RgbaFloat RoyalBlue = "#4169E1";
        public static readonly RgbaFloat SaddleBrown = "#8B4513";
        public static readonly RgbaFloat Salmon = "#FA8072";
        public static readonly RgbaFloat SandyBrown = "#F4A460";
        public static readonly RgbaFloat SeaGreen = "#2E8B57";
        public static readonly RgbaFloat SeaShell = "#FFF5EE";
        public static readonly RgbaFloat Sienna = "#A0522D";
        // public static readonly RgbaFloat Silver = "#C0C0C0";
        public static readonly RgbaFloat SkyBlue = "#87CEEB";
        public static readonly RgbaFloat SlateBlue = "#6A5ACD";
        public static readonly RgbaFloat SlateGray = "#708090";
        public static readonly RgbaFloat SlateGrey = "#708090";
        public static readonly RgbaFloat Snow = "#FFFAFA";
        public static readonly RgbaFloat SpringGreen = "#00FF7F";
        public static readonly RgbaFloat SteelBlue = "#4682B4";
        public static readonly RgbaFloat Tan = "#D2B48C";
        // public static readonly RgbaFloat Teal = "#008080";
        public static readonly RgbaFloat Thistle = "#D8BFD8";
        public static readonly RgbaFloat Tomato = "#FF6347";
        public static readonly RgbaFloat Turquoise = "#40E0D0";
        public static readonly RgbaFloat Violet = "#EE82EE";
        public static readonly RgbaFloat Wheat = "#F5DEB3";
        // public static readonly RgbaFloat White = "#FFFFFF";
        public static readonly RgbaFloat Whitesmoke = "#F5F5F5";
        // public static readonly RgbaFloat Yellow = "#FFFF00";
        public static readonly RgbaFloat YellowGreen = "#9ACD32";
        
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
            if (hex[0] != '#' || hex.Length != 7 || !uint.TryParse(hex.AsSpan(1), out var value))
            {
                throw new ArgumentException($"Failed to parse the hex rgb '{hex}'.");
            }

            var r = ((value >> 16) & 0xFF) / 255d;
            var g = ((value >> 8) & 0xFF) / 255d;
            var b = (value & 0xFF) / 255d;
            
            return new RgbaFloat((float)r, (float)g, (float)b, 1);
        }
    }
}