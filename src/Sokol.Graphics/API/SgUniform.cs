// /* 
// MIT License
//
// Copyright (c) 2019 Lucas Girouard-Stranks
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//  */
//
// using System;
// using System.Numerics;
// using System.Runtime.CompilerServices;
// using static Sokol.sokol_gfx;
//
// namespace Sokol
// {
//     public sealed class SgUniform
//     {
//         private readonly sg_shader_stage _shaderStage;
//         
//         internal readonly int Size;
//
//         public SgShaderStage ShaderStage { get; }
//         public SgShaderUniformType Type { get; }
//         public int BlockIndex { get; }
//         public string Name { get; }
//
//         public SgUniform(string name, SgShaderStage shaderStage, int blockIndex, SgShaderUniformType type)
//         {
//             if (string.IsNullOrEmpty(name))
//             {
//                 throw new ArgumentNullException(nameof(name));
//             }
//
//             if (blockIndex < 0 || blockIndex >= SG_MAX_SHADERSTAGE_UBS)
//             {
//                 throw new ArgumentOutOfRangeException(nameof(blockIndex));
//             }
//             
//             _shaderStage = shaderStage switch
//             {
//                 SgShaderStage.Fragment => sg_shader_stage.SG_SHADERSTAGE_FS,
//                 SgShaderStage.Vertex => sg_shader_stage.SG_SHADERSTAGE_VS,
//                 _ => throw new ArgumentOutOfRangeException(nameof(shaderStage))
//             };
//             
//             ShaderStage = shaderStage;
//
//             Size = type switch
//             {
//                 SgShaderUniformType.Float => Unsafe.SizeOf<float>(),
//                 SgShaderUniformType.Float2 => Unsafe.SizeOf<Vector2>(),
//                 SgShaderUniformType.Float3 => Unsafe.SizeOf<Vector3>(),
//                 SgShaderUniformType.Float4 => Unsafe.SizeOf<Vector4>(),
//                 SgShaderUniformType.Matrix4 => Unsafe.SizeOf<Matrix4x4>(),
//                 _ => throw new ArgumentOutOfRangeException(nameof(type))
//             };
//
//             Type = type;
//             Name = name;
//             BlockIndex = blockIndex;
//         }
//
//         public unsafe void Apply<T>(ref T value) where T : unmanaged
//         {
//             var size = Unsafe.SizeOf<T>();
//             if (size != Size)
//             {
//                 throw new InvalidOperationException();
//             }
//             
//             var pointer = Unsafe.AsPointer(ref value);
//             sg_apply_uniforms(_shaderStage, BlockIndex, pointer, size);
//         }
//     }
// }