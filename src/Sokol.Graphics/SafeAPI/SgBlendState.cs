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
    public ref struct SgBlendState
    {
        public sg_blend_state CStruct;

        public bool Enabled
        {
            readonly get => CStruct.enabled;
            set => CStruct.enabled = value;
        }

        public SgBlendFactor SourceFactorRgb
        {
            readonly get => (SgBlendFactor) CStruct.src_factor_rgb;
            set => CStruct.src_factor_rgb = (sg_blend_factor) value;
        }
        
        public SgBlendFactor DestinationFactorRgb
        {
            readonly get => (SgBlendFactor) CStruct.dst_factor_rgb;
            set => CStruct.dst_factor_rgb = (sg_blend_factor) value;
        }

        public SgBlendOperation OperationRgb
        {
            get => (SgBlendOperation) CStruct.op_rgb;
            set => CStruct.op_rgb = (sg_blend_op) value;
        }
        
        public SgBlendFactor SourceFactorAlpha
        {
            readonly get => (SgBlendFactor) CStruct.src_factor_alpha;
            set => CStruct.src_factor_alpha = (sg_blend_factor) value;
        }
        
        public SgBlendFactor DestinationFactorAlpha
        {
            readonly get => (SgBlendFactor) CStruct.dst_factor_alpha;
            set => CStruct.dst_factor_alpha = (sg_blend_factor) value;
        }
        
        public SgBlendOperation OperationAlpha
        {
            readonly get => (SgBlendOperation) CStruct.op_alpha;
            set => CStruct.op_alpha = (sg_blend_op) value;
        }

        public byte ColorWriteMask
        {
            readonly get => CStruct.color_write_mask;
            set => CStruct.color_write_mask = value;
        }

        public int ColorAttachmentCount
        {
            readonly get => CStruct.color_attachment_count;
            set => CStruct.color_attachment_count = value;
        }

        public SgPixelFormat ColorFormat
        {
            readonly get => (SgPixelFormat) CStruct.color_format;
            set => CStruct.color_format = (sg_pixel_format) value;
        }
        
        public SgPixelFormat DepthFormat
        {
            readonly get => (SgPixelFormat) CStruct.depth_format;
            set => CStruct.depth_format = (sg_pixel_format) value;
        }

        public RgbaFloat BlendColor
        {
            readonly get => CStruct.blend_color;
            set => CStruct.blend_color = value;
        }
    }
}