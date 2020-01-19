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

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Sokol
{
    public enum SgPixelFormat
    {
        Default,
        None,

        R8,
        R8SN,
        R8UI,
        R8SI,

        R16,
        R16SN,
        R16UI,
        R16SI,
        R16F,
        RG8,
        RG8SN,
        RG8UI,
        RG8SI,

        R32UI,
        R32SI,
        R32F,
        RG16,
        RG16SN,
        RG16UI,
        RG16SI,
        RG16F,
        RGBA8,
        RGBA8SN,
        RGBA8UI,
        RGBA8SI,
        BGRA8,
        RGB10A2,
        RG11B10F,

        RG32UI,
        RG32SI,
        RG32F,
        RGBA16,
        RGBA16SN,
        RGBA16UI,
        RGBA16SI,
        RGBA16F,

        RGBA32UI,
        RGBA32SI,
        RGBA32F,

        Depth,
        DepthStencil,

        BC1_RGBA,
        BC2_RGBA,
        BC3_RGBA,
        BC4_R,
        BC4_RSN,
        BC5_RG,
        BC5_RGSN,
        BC6H_RGBF,
        BC6H_RGBUF,
        BC7_RGBA,
        PVRTC_RGB_2BPP,
        PVRTC_RGB_4BPP,
        PVRTC_RGBA_2BPP,
        PVRTC_RGBA_4BPP,
        ETC2_RGB8,
        ETC2_RGB8A1
    }
}