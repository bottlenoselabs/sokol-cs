// Original code derived from https://github.com/mellinoe/veldrid/blob/master/src/Veldrid/RgbaFloat.cs

/* The MIT License
 *
 * Copyright (c) 2017 Eric Mellino and Veldrid contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

/* The MIT License
 *
 * Copyright (c) 2019 Lucas Girouard-Stranks
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Sokol
{
    public struct RgbFloat
    {
        public static readonly RgbFloat Red = new RgbFloat(1, 0, 0);
        public static readonly RgbFloat DarkRed = new RgbFloat(0.6f, 0, 0);
        public static readonly RgbFloat Green = new RgbFloat(0, 1, 0);
        public static readonly RgbFloat Blue = new RgbFloat(0, 0, 1);
        public static readonly RgbFloat Yellow = new RgbFloat(1, 1, 0);
        public static readonly RgbFloat Grey = new RgbFloat(.25f, .25f, .25f);
        public static readonly RgbFloat LightGrey = new RgbFloat(.65f, .65f, .65f);
        public static readonly RgbFloat Cyan = new RgbFloat(0, 1, 1);
        public static readonly RgbFloat White = new RgbFloat(1, 1, 1);
        public static readonly RgbFloat CornflowerBlue = new RgbFloat(0.3921f, 0.5843f, 0.9294f);
        public static readonly RgbFloat Clear = new RgbFloat(0, 0, 0);
        public static readonly RgbFloat Black = new RgbFloat(0, 0, 0);
        public static readonly RgbFloat Pink = new RgbFloat(1f, 0.45f, 0.75f);
        public static readonly RgbFloat Orange = new RgbFloat(1f, 0.36f, 0f);

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
    }
}