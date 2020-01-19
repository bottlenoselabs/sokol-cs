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

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

#pragma warning disable 649

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 72, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct SgBufferDescription
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] public int Size;
        [FieldOffset(8)] public SgBufferType Type;
        [FieldOffset(12)] public SgUsage Usage;
        [FieldOffset(16)] public IntPtr Content;
        [FieldOffset(24)] public IntPtr Label;
        [FieldOffset(32)] internal fixed uint gl_buffers[SG_NUM_INFLIGHT_FRAMES];
        [FieldOffset(40)] internal fixed ulong _mtl_buffers[SG_NUM_INFLIGHT_FRAMES];
        [FieldOffset(56)] internal IntPtr d3d11_buffer;
        [FieldOffset(64)] internal uint _end_canary;
    }
}