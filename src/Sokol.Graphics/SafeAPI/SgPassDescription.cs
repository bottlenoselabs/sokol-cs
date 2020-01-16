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
using static Sokol.sokol_gfx;

namespace Sokol
{
    public struct SgPassDescription
    {
        public sg_pass_desc CStruct;
        
        public unsafe ref SgAttachmentDescription ColorAttachment(int index)
        {
            ref var @ref = ref CStruct.color_attachment(index);
            fixed (sg_attachment_desc* ptr = &@ref)
            {
                return ref *(SgAttachmentDescription*) ptr;
            }
        }
        
        public unsafe ref SgAttachmentDescription DepthStencilAttachment
        {
            get
            {
                ref var @ref = ref CStruct.depth_stencil_attachment;
                fixed (sg_attachment_desc* ptr = &@ref)
                {
                    return ref *(SgAttachmentDescription*) ptr;
                }   
            }
        }
        
        public unsafe IntPtr Label
        {
            readonly get => (IntPtr) CStruct.label;
            set => CStruct.label = (byte*) value;
        }
    }
}