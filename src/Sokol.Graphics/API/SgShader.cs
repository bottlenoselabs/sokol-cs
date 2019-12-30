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
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using static Sokol.sokol_gfx;
//
// namespace Sokol
// {
//     public sealed class SgShader : SgResource
//     {
//         public sg_shader Handle { get; }
//         
//         public SgShader(string vertexShaderSourceCode, string fragmentShaderSourceCode, string name = null)
//             : this(new SgShaderStageDescription
//             {
//                 SourceCode = vertexShaderSourceCode
//             }, new SgShaderStageDescription
//             {
//                 SourceCode = fragmentShaderSourceCode
//             }, name)
//         {
//         }
//         
//         public unsafe SgShader(
//             SgShaderStageDescription vertexShaderDescription, 
//             SgShaderStageDescription fragmentShaderDescription, 
//             string name = null)
//             : base(name)
//         {
//             if (string.IsNullOrEmpty(vertexShaderDescription.SourceCode))
//             {
//                 throw new ArgumentException("Vertex shader source code is a null or empty string.", 
//                     nameof(vertexShaderDescription.SourceCode));
//             }
//
//             if (string.IsNullOrEmpty(fragmentShaderDescription.SourceCode))
//             {
//                 throw new ArgumentException("Fragment shader source code is a null or empty string.",
//                     nameof(fragmentShaderDescription.SourceCode));
//             }
//
//             var desc = new sg_shader_desc
//             {
//                 label = (char*) CNamePointer
//             };
//             
//             var allocPointers = new List<IntPtr>();
//
//             var vertexShaderUniformBlocks = vertexShaderDescription.UniformBlocks;
//             if (vertexShaderUniformBlocks != null && vertexShaderUniformBlocks.Length > 0)
//             {
//                 DescAddUniformBlocks(SgShaderStage.Vertex, ref desc, vertexShaderUniformBlocks, allocPointers);
//             }
//             
//             var fragmentShaderUniformBlocks = fragmentShaderDescription.UniformBlocks;
//             if (fragmentShaderUniformBlocks != null && fragmentShaderUniformBlocks.Length > 0)
//             {
//                 DescAddUniformBlocks(SgShaderStage.Fragment, ref desc, fragmentShaderUniformBlocks, allocPointers);
//             }
//
//             DescAddSourceCode(ref desc, vertexShaderDescription.SourceCode, fragmentShaderDescription.SourceCode,
//                 allocPointers);
//             DescAddEntry(ref desc, vertexShaderDescription.Entry, fragmentShaderDescription.Entry, 
//                 allocPointers);
//             
//             Handle = sg_make_shader(ref desc);
//
//             foreach (var pointer in allocPointers)
//             {
//                 Marshal.FreeHGlobal(pointer);
//             }
//         }
//
//         private static unsafe void DescAddUniformBlocks(SgShaderStage shaderStage, ref sg_shader_desc desc, 
//             IReadOnlyList<SgUniformBlock> uniformBlocks, ICollection<IntPtr> allocPointers)
//         {
//             if (uniformBlocks.Count > SG_MAX_SHADERSTAGE_UBS)
//             {
//                 throw new ArgumentException($"Too many shader stage uniform blocks; the maximum is ({SG_MAX_SHADERSTAGE_UBS}).", nameof(uniformBlocks));
//             }
//
//             var descUniformBlocks = shaderStage switch
//             {
//                 SgShaderStage.Vertex => desc.vs.GetUniformBlocks(),
//                 SgShaderStage.Fragment => desc.fs.GetUniformBlocks(),
//                 _ => throw new ArgumentOutOfRangeException(nameof(shaderStage), shaderStage, null)
//             };
//
//             for (var blockIndex = 0; blockIndex < uniformBlocks.Count; blockIndex++)
//             {
//                 var uniformBlock = uniformBlocks[blockIndex];
//                 ref var descUniformBlock = ref descUniformBlocks[blockIndex];
//                 AddUniformBlock(ref descUniformBlock, ref uniformBlock, shaderStage, allocPointers, blockIndex);
//             }
//         }
//
//         private static unsafe void AddUniformBlock(ref sg_shader_uniform_block_desc descUniformBlock, 
//             ref SgUniformBlock uniformBlock, 
//             SgShaderStage shaderStage,
//             ICollection<IntPtr> allocPointers,
//             int blockIndex)
//         {
//             var uniforms = uniformBlock.Uniforms;
//             if (uniforms.Length > SG_MAX_UB_MEMBERS)
//             {
//                 throw new ArgumentException($"Too many uniforms; the maximum is ({SG_MAX_UB_MEMBERS}).", nameof(uniformBlock));
//             }
//             
//             var descUniforms = descUniformBlock.GetUniforms();
//             var uniformBlockSize = 0;
//
//             for (var i = 0; i < uniforms.Length; i++)
//             {
//                 var uniform = uniforms[i];
//                 ref var descUniform = ref descUniforms[i];
//                 uniformBlockSize += uniform.Size;
//                 AddUniform(ref descUniform, uniform, shaderStage, blockIndex, allocPointers);
//             }
//
//             descUniformBlock.size = uniformBlockSize;
//         }
//
//         private static unsafe void AddUniform(ref sg_shader_uniform_desc descUniform, SgUniform uniform, SgShaderStage shaderStage, int blockIndex, ICollection<IntPtr> allocPointers)
//         {
//             if (uniform.ShaderStage != shaderStage)
//             {
//                 throw new ArgumentException($"Mismatch uniform shader stage ({uniform.ShaderStage}).", nameof(shaderStage));
//             }
//             
//             if (uniform.BlockIndex != blockIndex)
//             {
//                 throw new ArgumentException($"Mismatch uniform block index ({uniform.BlockIndex}).", nameof(blockIndex));
//             }
//             
//             var nameCPointer = Marshal.StringToHGlobalAnsi(uniform.Name);
//             descUniform.name = (char*) nameCPointer;
//             allocPointers.Add(nameCPointer);
//
// #pragma warning disable 8509
//             descUniform.type = uniform.Type switch
// #pragma warning restore 8509
//             {
//                 SgShaderUniformType.Float => sg_uniform_type.SG_UNIFORMTYPE_FLOAT,
//                 SgShaderUniformType.Float2 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT2,
//                 SgShaderUniformType.Float3 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT3,
//                 SgShaderUniformType.Float4 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT4,
//                 SgShaderUniformType.Matrix4 => sg_uniform_type.SG_UNIFORMTYPE_MAT4
//             };
//         }
//
//         private static void DescAddSourceCode(ref sg_shader_desc desc, 
//             string vertexShaderSourceCode,
//             string fragmentShaderSourceCode, 
//             ICollection<IntPtr> allocPointers)
//         {
//             var vertexShaderSourceCodeCPointer =
//                 Marshal.StringToHGlobalAnsi(vertexShaderSourceCode);
//             allocPointers.Add(vertexShaderSourceCodeCPointer);
//             
//             var fragmentShaderSourceCodeCPointer =
//                 Marshal.StringToHGlobalAnsi(fragmentShaderSourceCode);
//             allocPointers.Add(fragmentShaderSourceCodeCPointer);
//
//             unsafe
//             {
//                 desc.vs.source = (char*) vertexShaderSourceCodeCPointer;
//                 desc.fs.source = (char*) fragmentShaderSourceCodeCPointer;
//             }
//         }
//
//         private static void DescAddEntry(ref sg_shader_desc desc,
//             string vertexShaderEntry,
//             string fragmentShaderEntry,
//             ICollection<IntPtr> allocPointers)
//         {
//             var vertexShaderSourceCodeCPointer = IntPtr.Zero;
//             if (!string.IsNullOrEmpty(vertexShaderEntry))
//             {
//                 vertexShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(vertexShaderEntry);
//                 allocPointers.Add(vertexShaderSourceCodeCPointer);
//             }
//             
//             var fragmentShaderSourceCodeCPointer = IntPtr.Zero;
//             if (!string.IsNullOrEmpty(fragmentShaderEntry))
//             {
//                 fragmentShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(fragmentShaderEntry);
//                 allocPointers.Add(fragmentShaderSourceCodeCPointer);
//             }
//
//             unsafe
//             {
//                 desc.vs.entry = (char*) vertexShaderSourceCodeCPointer;
//                 desc.fs.entry = (char*) fragmentShaderSourceCodeCPointer;
//             }
//         }
//
//         ~SgShader()
//         {
//             ReleaseUnmanagedResources();
//         }
//         
//         public static implicit operator sg_shader(SgShader shader)
//         {
//             return shader.Handle;
//         }
//     }
// }