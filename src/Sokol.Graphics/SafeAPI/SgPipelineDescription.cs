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
using System.Runtime.CompilerServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public struct SgPipelineDescription
    {
        internal sg_pipeline_desc desc;

        public sg_shader Shader
        {
            get => desc.shader;
            set => desc.shader = value;
        }

        public unsafe ref SgVertexLayoutDescription Layout
        {
            get
            {
                ref var layout = ref desc.layout;
                var pointer = Unsafe.AsPointer(ref layout);
                return ref Unsafe.AsRef<SgVertexLayoutDescription>(pointer);
            }
        }

        public SgPrimitiveType PrimitiveType
        {
            get => (SgPrimitiveType) desc.primitive_type;
            set => desc.primitive_type = (sg_primitive_type) value;
        }

        public SgIndexType IndexType
        {
            get => (SgIndexType) desc.index_type;
            set => desc.index_type = (sg_index_type) value;
        }

        public unsafe ref SgDepthStencilState DepthStencil
        {
            get
            {
                ref var depthStencilState = ref desc.depth_stencil;
                var pointer = Unsafe.AsPointer(ref depthStencilState);
                return ref Unsafe.AsRef<SgDepthStencilState>(pointer);
            }
        }
        
        public unsafe ref SgBlendState Blend
        {
            get
            {
                ref var blendState = ref desc.blend;
                var pointer = Unsafe.AsPointer(ref blendState);
                return ref Unsafe.AsRef<SgBlendState>(pointer);
            }
        }

        public unsafe ref SgRasterizerState Rasterizer
        {
            get
            {
                ref var rasterizerState = ref desc.rasterizer;
                var pointer = Unsafe.AsPointer(ref rasterizerState);
                return ref Unsafe.AsRef<SgRasterizerState>(pointer);
            }
        }
        
        public unsafe IntPtr Label
        {
            get => (IntPtr) desc.label;
            set => desc.label = (byte*) value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_pipeline_desc GetCStruct(this ref SgPipelineDescription description) 
        {
            return ref description.desc;
        }
    }
}