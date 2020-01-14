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

using System.Runtime.CompilerServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public struct SgDepthStencilState
    {
        internal sg_depth_stencil_state state;

        public unsafe ref SgStencilState StencilFront
        {
            get
            {
                ref var stencilState = ref state.stencil_front;
                var pointer = Unsafe.AsPointer(ref stencilState);
                return ref Unsafe.AsRef<SgStencilState>(pointer);
            }
        }
        
        public unsafe ref SgStencilState StencilBack
        {
            get
            {
                ref var stencilState = ref state.stencil_back;
                var pointer = Unsafe.AsPointer(ref stencilState);
                return ref Unsafe.AsRef<SgStencilState>(pointer);
            }
        }

        public SgCompareFunction DepthCompareFunction
        {
            get => (SgCompareFunction) state.depth_compare_func;
            set => state.depth_compare_func = (sg_compare_func) value;
        }

        public bool DepthWriteEnabled
        {
            get => state.depth_write_enabled;
            set => state.depth_write_enabled = value;
        }

        public bool StencilEnabled
        {
            get => state.stencil_enabled;
            set => state.stencil_enabled = value;
        }

        public byte StencilReadMask
        {
            get => state.stencil_read_mask;
            set => state.stencil_read_mask = value;
        }

        public byte StencilWriteMask
        {
            get => state.stencil_write_mask;
            set => state.stencil_write_mask = value;
        }
        
        public byte StencilReference
        {
            get => state.stencil_ref;
            set => state.stencil_ref = value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_depth_stencil_state GetCStruct(this ref SgDepthStencilState state) 
        {
            return ref state.state;
        }
    }
}