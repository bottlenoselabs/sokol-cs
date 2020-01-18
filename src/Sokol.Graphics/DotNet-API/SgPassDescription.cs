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
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 80, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct SgPassDescription
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] internal fixed int _color_attachments[12 * SG_MAX_COLOR_ATTACHMENTS / 4];
        [FieldOffset(52)] public SgAttachmentDescription DepthStencilAttachment;
        [FieldOffset(64)] public byte* Label;
        [FieldOffset(72)] internal uint _endCanary;
        
        public ref SgAttachmentDescription ColorAttachment(int index)
        {
            fixed (SgPassDescription* passDescription = &this)
            {
                var ptr = (SgAttachmentDescription*) &passDescription->_color_attachments[0];
                return ref *(ptr + index);
            }
        }
    }
}