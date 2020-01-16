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
    public struct SgPass
    {
        public sg_pass CStruct;

        public bool IsValid => CStruct.id != 0;

        public SgPass(sg_pass pass)
        {
            CStruct = pass;
        }
        
        public SgPass(ref SgPassDescription description)
        {
            CStruct = sg_make_pass(ref description.CStruct);
        }

        public void Begin(ref sg_pass_action action)
        {
            sg_begin_pass(CStruct, ref action);
        }

        public void End()
        {
            sg_end_pass();
        }

        public void Destroy()
        {
            if (CStruct.id == 0)
            {
                return;
            }

            sg_destroy_pass(CStruct);
            CStruct.id = 0;
        }
        
        public static implicit operator sg_pass(SgPass pass)
        {
            return pass.CStruct;
        }
        
        public static implicit operator SgPass(sg_pass pass)
        {
            return new SgPass(pass);
        }
    }
}