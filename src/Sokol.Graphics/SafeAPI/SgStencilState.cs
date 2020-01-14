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
    public struct SgStencilState
    {
        internal sg_stencil_state state;

        public SgStencilOperation FailOperation
        {
            get => (SgStencilOperation) state.fail_op;
            set => state.fail_op = (sg_stencil_op) value;
        }
        
        public SgStencilOperation DepthFailOperation
        {
            get => (SgStencilOperation) state.depth_fail_op;
            set => state.fail_op = (sg_stencil_op) value;
        }
        
        public SgStencilOperation PassOperation
        {
            get => (SgStencilOperation) state.pass_op;
            set => state.pass_op = (sg_stencil_op) value;
        }

        public SgCompareFunction CompareFunction
        {
            get => (SgCompareFunction) state.compare_func;
            set => state.compare_func = (sg_compare_func) value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_stencil_state GetCStruct(this ref SgStencilState state) 
        {
            return ref state.state;
        }
    }
}