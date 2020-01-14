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
    public struct SgBlendState
    {
        internal sg_blend_state state;

        public bool Enabled
        {
            get => state.enabled;
            set => state.enabled = value;
        }

        public SgBlendFactor SourceFactorRgb
        {
            get => (SgBlendFactor) state.src_factor_rgb;
            set => state.src_factor_rgb = (sg_blend_factor) value;
        }
        
        public SgBlendFactor DestinationFactorRgb
        {
            get => (SgBlendFactor) state.dst_factor_rgb;
            set => state.dst_factor_rgb = (sg_blend_factor) value;
        }

        public SgBlendOperation OperationRgb
        {
            get => (SgBlendOperation) state.op_rgb;
            set => state.op_rgb = (sg_blend_op) value;
        }
        
        public SgBlendFactor SourceFactorAlpha
        {
            get => (SgBlendFactor) state.src_factor_alpha;
            set => state.src_factor_alpha = (sg_blend_factor) value;
        }
        
        public SgBlendFactor DestinationFactorAlpha
        {
            get => (SgBlendFactor) state.dst_factor_alpha;
            set => state.dst_factor_alpha = (sg_blend_factor) value;
        }
        
        public SgBlendOperation OperationAlpha
        {
            get => (SgBlendOperation) state.op_alpha;
            set => state.op_alpha = (sg_blend_op) value;
        }

        public byte ColorWriteMask
        {
            get => state.color_write_mask;
            set => state.color_write_mask = value;
        }

        public int ColorAttachmentCount
        {
            get => state.color_attachment_count;
            set => state.color_attachment_count = value;
        }

        public SgPixelFormat ColorFormat
        {
            get => (SgPixelFormat) state.color_format;
            set => state.color_format = (sg_pixel_format) value;
        }
        
        public SgPixelFormat DepthFormat
        {
            get => (SgPixelFormat) state.depth_format;
            set => state.depth_format = (sg_pixel_format) value;
        }

        public RgbaFloat BlendColor
        {
            get => state.blend_color;
            set => state.blend_color = value;
        }
    }
    
    public static partial class SgSafeExtensions
    {
        public static ref sg_blend_state GetCStruct(this ref SgBlendState state) 
        {
            return ref state.state;
        }
    }
}