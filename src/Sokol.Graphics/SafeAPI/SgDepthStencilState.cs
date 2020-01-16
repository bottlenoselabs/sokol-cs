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
    public ref struct SgDepthStencilState
    {
        public sg_depth_stencil_state CStruct;

        public unsafe ref SgStencilState StencilFront
        {
            get
            {
                ref var @ref = ref CStruct.stencil_front;
                fixed (sg_stencil_state* ptr = &@ref)
                {
                    return ref *(SgStencilState*) ptr;
                }
            }
        }
        
        public unsafe ref SgStencilState StencilBack
        {
            get
            {
                ref var @ref = ref CStruct.stencil_back;
                fixed (sg_stencil_state* ptr = &@ref)
                {
                    return ref *(SgStencilState*) ptr;
                }
            }
        }

        public SgCompareFunction DepthCompareFunction
        {
            readonly get => (SgCompareFunction) CStruct.depth_compare_func;
            set => CStruct.depth_compare_func = (sg_compare_func) value;
        }

        public bool DepthWriteEnabled
        {
            readonly get => CStruct.depth_write_enabled;
            set => CStruct.depth_write_enabled = value;
        }

        public bool StencilEnabled
        {
            readonly get => CStruct.stencil_enabled;
            set => CStruct.stencil_enabled = value;
        }

        public byte StencilReadMask
        {
            readonly get => CStruct.stencil_read_mask;
            set => CStruct.stencil_read_mask = value;
        }

        public byte StencilWriteMask
        {
            readonly get => CStruct.stencil_write_mask;
            set => CStruct.stencil_write_mask = value;
        }
        
        public byte StencilReference
        {
            readonly get => CStruct.stencil_ref;
            set => CStruct.stencil_ref = value;
        }
    }
}