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
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 1664, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct SgImageDescription
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] public SgImageType Type;
        [FieldOffset(8)] public BlittableBoolean IsRenderTarget;
        [FieldOffset(12)] public int Width;
        [FieldOffset(16)] public int Height;
        [FieldOffset(20)] public int Depth;
        [FieldOffset(20)] public int Layers;
        [FieldOffset(24)] public int MipmapCount;
        [FieldOffset(28)] public SgUsage Usage;
        [FieldOffset(32)] public SgPixelFormat PixelFormat;
        [FieldOffset(36)] public int SampleCount;
        [FieldOffset(40)] public SgTextureFilter MinificationFilter;
        [FieldOffset(44)] public SgTextureFilter MagnificationFilter;
        [FieldOffset(48)] public SgTextureWrap WrapU;
        [FieldOffset(52)] public SgTextureWrap WrapV;
        [FieldOffset(56)] public SgTextureWrap WrapW;
        [FieldOffset(60)] public SgTextureBorderColor BorderColor;
        [FieldOffset(64)] public uint MaximumAnisotropy;
        [FieldOffset(68)] public float MinimumLevelOfDetail;
        [FieldOffset(72)] public float MaximumLevelOfDetail;
        [FieldOffset(80)] public SgImageContent Content;
        [FieldOffset(1616)] public IntPtr Label;
        [FieldOffset(1624)] internal fixed uint GLTextures[SG_NUM_INFLIGHT_FRAMES];
        [FieldOffset(1632)] internal fixed ulong MTLTextures[SG_NUM_INFLIGHT_FRAMES];
        [FieldOffset(1648)] internal IntPtr D3D11Texture;
        [FieldOffset(1656)] internal uint _endCanary;
    }
}