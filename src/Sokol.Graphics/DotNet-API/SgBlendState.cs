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

using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 60, Pack = 4)]
    public struct SgBlendState
    {
        [FieldOffset(0)] public BlittableBoolean IsEnabled;
        [FieldOffset(4)] public SgBlendFactor SourceFactorRgb;
        [FieldOffset(8)] public SgBlendFactor DestinationFactorRgb;
        [FieldOffset(12)] public SgBlendOperation OperationRgb;
        [FieldOffset(16)] public SgBlendFactor SourceFactorAlpha;
        [FieldOffset(20)] public SgBlendFactor DestinationFactorAlpha;
        [FieldOffset(24)] public SgBlendOperation OperationAlpha;
        [FieldOffset(28)] public byte ColorWriteMask;
        [FieldOffset(32)] public int ColorAttachmentCount;
        [FieldOffset(36)] public SgPixelFormat ColorFormat;
        [FieldOffset(40)] public SgPixelFormat DepthFormat;
        [FieldOffset(44)] public RgbaFloat BlendColor;
    }
}