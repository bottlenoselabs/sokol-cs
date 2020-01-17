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
    public ref struct SgPipelineDescription
    {
        public sg_pipeline_desc CStruct;

        public SgShader Shader
        {
            readonly get => CStruct.shader;
            set => CStruct.shader = value;
        }

        public unsafe ref SgVertexLayoutDescription Layout
        {
            get
            {
                ref var @ref = ref CStruct.layout;
                fixed (void* ptr = &@ref)
                {
                    return ref *(SgVertexLayoutDescription*) ptr;
                }
            }
        }

        public SgPrimitiveType PrimitiveType
        {
            readonly get => (SgPrimitiveType) CStruct.primitive_type;
            set => CStruct.primitive_type = (sg_primitive_type) value;
        }

        public SgIndexType IndexType
        {
            readonly get => (SgIndexType) CStruct.index_type;
            set => CStruct.index_type = (sg_index_type) value;
        }

        public unsafe ref SgDepthStencilState DepthStencil
        {
            get
            {
                ref var @ref = ref CStruct.depth_stencil;
                fixed (sg_depth_stencil_state* ptr = &@ref)
                {
                    return ref *(SgDepthStencilState*) ptr;
                }
            }
        }
        
        public unsafe ref SgBlendState Blend
        {
            get
            {
                // ref var blendState = ref desc.blend;
                fixed (sg_blend_state* blendState = &CStruct.blend)
                {
                    return ref *(SgBlendState*) blendState;
                }
            }
        }

        public unsafe ref SgRasterizerState Rasterizer
        {
            get
            {
                ref var @ref = ref CStruct.rasterizer;
                fixed (sg_rasterizer_state* ptr = &@ref)
                {
                    return ref *(SgRasterizerState*) ptr;
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