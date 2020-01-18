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

using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 4)]
    public unsafe struct SgPassAction
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] internal fixed int _colors[20 * SG_MAX_COLOR_ATTACHMENTS / 4];
        [FieldOffset(84)] public SgDepthAttachmentAction Depth;
        [FieldOffset(92)] public SgStencilAttachmentAction Stencil;
        [FieldOffset(100)] internal uint _endCanary;
        
        public ref SgColorAttachmentAction Color(int index)
        {
            fixed (SgPassAction* passAction = &this)
            {
                var ptr = (SgColorAttachmentAction*) &passAction->_colors[0];
                return ref *(ptr + index);
            }
        }

        public static SgPassAction Clear(RgbaFloat? color = null)
        {
            var passAction = new SgPassAction();
            ref var color0 = ref passAction.Color(0);
            color0.Action = SgAction.Clear;
            color0.Value = color ?? RgbaFloat.Gray;
            return passAction;
        }
        
        public static SgPassAction DontCare
        {
            get
            {
                var passAction = new SgPassAction();
                for (var i = 0; i < SG_MAX_COLOR_ATTACHMENTS; i++)
                {
                    passAction.Color(i).Action = SgAction.DontCare;
                }

                passAction.Depth.Action = SgAction.DontCare;
                passAction.Stencil.Action = SgAction.DontCare;
                return passAction;   
            }
        }
    }
}