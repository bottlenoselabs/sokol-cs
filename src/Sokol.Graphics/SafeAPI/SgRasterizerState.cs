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

namespace Sokol
{
    public struct SgRasterizerState
    {
        internal sg_rasterizer_state state;

        public bool AlphaToCoverageEnabled
        {
            get => state.alpha_to_coverage_enabled;
            set => state.alpha_to_coverage_enabled = value;
        }

        public SgCullMode CullMode
        {
            get => (SgCullMode) state.cull_mode;
            set => state.cull_mode = (sg_cull_mode) value;
        }

        public SgFaceWinding FaceWinding
        {
            get => (SgFaceWinding) state.face_winding;
            set => state.face_winding = (sg_face_winding) value;
        }

        public int SampleCount
        {
            get => state.sample_count;
            set => state.sample_count = value;
        }

        public float DepthBias
        {
            get => state.depth_bias;
            set => state.depth_bias = value;
        }

        public float DepthBiasSlopeScale
        {
            get => state.depth_bias_slope_scale;
            set => state.depth_bias_slope_scale = value;
        }
        
        public float DepthBiasClamp
        {
            get => state.depth_bias_clamp;
            set => state.depth_bias_clamp = value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_rasterizer_state GetCStruct(this ref SgRasterizerState state) 
        {
            return ref state.state;
        }
    }
}