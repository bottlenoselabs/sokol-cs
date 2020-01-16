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
    public ref struct SgRasterizerState
    {
        public sg_rasterizer_state CStruct;

        public bool AlphaToCoverageEnabled
        {
            readonly get => CStruct.alpha_to_coverage_enabled;
            set => CStruct.alpha_to_coverage_enabled = value;
        }

        public SgCullMode CullMode
        {
            readonly get => (SgCullMode) CStruct.cull_mode;
            set => CStruct.cull_mode = (sg_cull_mode) value;
        }

        public SgFaceWinding FaceWinding
        {
            readonly get => (SgFaceWinding) CStruct.face_winding;
            set => CStruct.face_winding = (sg_face_winding) value;
        }

        public int SampleCount
        {
            readonly get => CStruct.sample_count;
            set => CStruct.sample_count = value;
        }

        public float DepthBias
        {
            readonly get => CStruct.depth_bias;
            set => CStruct.depth_bias = value;
        }

        public float DepthBiasSlopeScale
        {
            readonly get => CStruct.depth_bias_slope_scale;
            set => CStruct.depth_bias_slope_scale = value;
        }
        
        public float DepthBiasClamp
        {
            readonly get => CStruct.depth_bias_clamp;
            set => CStruct.depth_bias_clamp = value;
        }
    }
}