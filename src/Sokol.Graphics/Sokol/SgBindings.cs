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
using static Sokol.sokol_gfx;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol
{
    [StructLayout(LayoutKind.Explicit, Size = 176, Pack = 4)]
    public unsafe struct SgBindings
    {
        [FieldOffset(0)] internal uint _startCanary;
        [FieldOffset(4)] internal fixed uint _vertexBuffers[SG_MAX_SHADERSTAGE_BUFFERS];
        [FieldOffset(36)] internal fixed int _vertexBufferOffsets[SG_MAX_SHADERSTAGE_BUFFERS];
        [FieldOffset(68)] public Buffer IndexBuffer;
        [FieldOffset(72)] public int IndexBufferOffset;
        [FieldOffset(76)] internal fixed uint _vsImages[SG_MAX_SHADERSTAGE_IMAGES];
        [FieldOffset(124)] internal fixed uint _fsImages[SG_MAX_SHADERSTAGE_IMAGES];
        [FieldOffset(172)] internal uint _endCanary;
        
        public ref Buffer VertexBuffer(int index)
        {
            fixed (SgBindings* bindings = &this)
            {
                var ptr = (Buffer*) &bindings->_vertexBuffers[0];
                return ref *(ptr + index);
            }
        }
            
        public ref int VertexBufferOffset(int index)
        {
            return ref _vertexBufferOffsets[index];
        }

        public ref Image VertexShaderImage(int index)
        {
            fixed (SgBindings* bindings = &this)
            {
                var ptr = (Image*) &bindings->_vsImages[0];
                return ref *(ptr + index);
            }
        }
            
        public ref Image FragmentShaderImage(int index)
        {
            fixed (SgBindings* bindings = &this)
            {
                var ptr = (Image*) &bindings->_fsImages[0];
                return ref *(ptr + index);
            }
        }
    }
}