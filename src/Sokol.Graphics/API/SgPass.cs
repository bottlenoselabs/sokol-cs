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

using static Sokol.sokol_gfx;

namespace Sokol
{
    public class SgPass : SgResource
    {
        public sg_pass Handle { get; }

        public SgPass(string name = null)
            : base(name)
        {
            /*
             *     SOKOL_ASSERT(from && to);
    *to = *from;
    for (int i = 0; i < SG_MAX_COLOR_ATTACHMENTS; i++) {
        if (to->colors[i].action  == _SG_ACTION_DEFAULT) {
            to->colors[i].action = SG_ACTION_CLEAR;
            to->colors[i].val[0] = SG_DEFAULT_CLEAR_RED;
            to->colors[i].val[1] = SG_DEFAULT_CLEAR_GREEN;
            to->colors[i].val[2] = SG_DEFAULT_CLEAR_BLUE;
            to->colors[i].val[3] = SG_DEFAULT_CLEAR_ALPHA;
        }
    }
    if (to->depth.action == _SG_ACTION_DEFAULT) {
        to->depth.action = SG_ACTION_CLEAR;
        to->depth.val = SG_DEFAULT_CLEAR_DEPTH;
    }
    if (to->stencil.action == _SG_ACTION_DEFAULT) {
        to->stencil.action = SG_ACTION_CLEAR;
        to->stencil.val = SG_DEFAULT_CLEAR_STENCIL;
    }
}
             */
        }
        
        public static implicit operator sg_pass(SgPass pass)
        {
            return pass.Handle;
        }
    }
}