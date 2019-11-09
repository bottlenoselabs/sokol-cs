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

// NOTE: .NET Core might supply this; see https://github.com/dotnet/corefx/issues/2315#issuecomment-526943838

namespace Sokol
{
    public struct RgbaFloat
    {
        //TODO: Add more names from https://www.w3.org/TR/css-color-3/#svg-color
        public static readonly RgbaFloat Red = new RgbaFloat(1, 0, 0, 1);
        public static readonly RgbaFloat DarkRed = new RgbaFloat(0.6f, 0, 0, 1);
        public static readonly RgbaFloat Green = new RgbaFloat(0, 1, 0, 1);
        public static readonly RgbaFloat Blue = new RgbaFloat(0, 0, 1, 1);
        public static readonly RgbaFloat Yellow = new RgbaFloat(1, 1, 0, 1);
        public static readonly RgbaFloat Grey = new RgbaFloat(.25f, .25f, .25f, 1);
        public static readonly RgbaFloat LightGrey = new RgbaFloat(.65f, .65f, .65f, 1);
        public static readonly RgbaFloat Cyan = new RgbaFloat(0, 1, 1, 1);
        public static readonly RgbaFloat White = new RgbaFloat(1, 1, 1, 1);
        public static readonly RgbaFloat CornflowerBlue = new RgbaFloat(0.3921f, 0.5843f, 0.9294f, 1);
        public static readonly RgbaFloat Clear = new RgbaFloat(0, 0, 0, 0);
        public static readonly RgbaFloat Black = new RgbaFloat(0, 0, 0, 1);
        public static readonly RgbaFloat Pink = new RgbaFloat(1f, 0.45f, 0.75f, 1);
        public static readonly RgbaFloat Orange = new RgbaFloat(1f, 0.36f, 0f, 1);

        
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
    }
}