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

using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    public struct SgImage
    {
        public sg_image CStruct;

        public bool IsValid => CStruct.id != 0;

        public SgImage(sg_image image)
        {
            CStruct = image;
        }
        
        public SgImage(ref SgImageDescription description)
        {
            CStruct = sg_make_image(ref description.CStruct);
        }
        
        public void Destroy()
        {
            if (CStruct.id == 0)
            {
                return;
            }

            sg_destroy_image(CStruct);
            CStruct.id = 0;
        }
        
        public static implicit operator sg_image(SgImage image)
        {
            return image.CStruct;
        }
        
        public static implicit operator SgImage(sg_image image)
        {
            return new SgImage(image);
        }
    }
}