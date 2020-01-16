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

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    public ref struct SgImageDescription
    {
        public sg_image_desc CStruct;

        public SgImageType Type
        {
            readonly get => (SgImageType) CStruct.type;
            set => CStruct.type = (sg_image_type) value;
        }

        public bool IsRenderTarget
        {
            readonly get => CStruct.render_target;
            set => CStruct.render_target = value;
        }

        public int Width
        {
            readonly get => CStruct.width;
            set => CStruct.width = value;
        }
        
        public int Height
        {
            readonly get => CStruct.height;
            set => CStruct.height = value;
        }
        
        public int Depth
        {
            readonly get => CStruct.depth;
            set => CStruct.depth = value;
        }
        
        public int Layers
        {
            readonly get => CStruct.layers;
            set => CStruct.layers = value;
        }
        
        public int MipmapsCount
        {
            readonly get => CStruct.num_mipmaps;
            set => CStruct.num_mipmaps = value;
        }

        public SgUsage Usage
        {
            readonly get => (SgUsage) CStruct.usage;
            set => CStruct.usage = (sg_usage) value;
        }

        public SgPixelFormat PixelFormat
        {
            readonly get => (SgPixelFormat) CStruct.pixel_format;
            set => CStruct.pixel_format = (sg_pixel_format) value;
        }

        public int SampleCount
        {
            readonly get => CStruct.sample_count;
            set => CStruct.sample_count = value;
        }

        public SgTextureFilter MinificationFilter
        {
            readonly get => (SgTextureFilter) CStruct.min_filter;
            set => CStruct.min_filter = (sg_filter) value;
        }
        
        public SgTextureFilter MagnificationFilter
        {
            readonly get => (SgTextureFilter) CStruct.mag_filter;
            set => CStruct.mag_filter = (sg_filter) value;
        }

        public SgTextureWrap WrapU
        {
            readonly get => (SgTextureWrap) CStruct.wrap_u;
            set => CStruct.wrap_u = (sg_wrap) value;
        }
        
        public SgTextureWrap WrapV
        {
            readonly get => (SgTextureWrap) CStruct.wrap_v;
            set => CStruct.wrap_v = (sg_wrap) value;
        }
        
        public SgTextureWrap WrapW
        {
            readonly get => (SgTextureWrap) CStruct.wrap_w;
            set => CStruct.wrap_w = (sg_wrap) value;
        }

        public SgTextureBorderColor BorderColor
        {
            readonly get => (SgTextureBorderColor) CStruct.border_color;
            set => CStruct.border_color = (sg_border_color) value;
        }

        public uint MaximumAnisotropy
        {
            readonly get => CStruct.max_anisotropy;
            set => CStruct.max_anisotropy = value;
        }

        public float MinimumLevelOfDetail
        {
            readonly get => CStruct.min_lod;
            set => CStruct.min_lod = value;
        }
        
        public float MaximumLevelOfDetail
        {
            readonly get => CStruct.max_lod;
            set => CStruct.max_lod = value;
        }
        
        public unsafe ref SgImageContent Content
        {
            get
            {
                ref var @ref = ref CStruct.content;
                fixed (sg_image_content* ptr = &@ref)
                {
                    return ref *(SgImageContent*) ptr;
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