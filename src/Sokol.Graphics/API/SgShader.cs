/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgShader : SgResource
    {
        public sg_shader Handle { get; }
        
        public SgShader(string vertexShaderSourceCode, string fragmentShaderSourceCode, string name = null)
            : this(new SgShaderStageDescription
            {
                SourceCode = vertexShaderSourceCode
            }, new SgShaderStageDescription
            {
                SourceCode = fragmentShaderSourceCode
            }, name)
        {
        }
        
        public unsafe SgShader(
            SgShaderStageDescription vertexShaderDescription, 
            SgShaderStageDescription fragmentShaderDescription, 
            string name = null)
            : base(name)
        {
            if (string.IsNullOrEmpty(vertexShaderDescription.SourceCode))
            {
                throw new ArgumentNullException(nameof(vertexShaderDescription.SourceCode));
            }

            if (string.IsNullOrEmpty(fragmentShaderDescription.SourceCode))
            {
                throw new ArgumentNullException(nameof(fragmentShaderDescription.SourceCode));
            }

            var description = new sg_shader_desc
            {
                label = (char*) CNamePointer
            };

            var globalPointers = new List<IntPtr>();

            var vertexShaderUniformBlocks = vertexShaderDescription.UniformBlocks;
            if (vertexShaderUniformBlocks != null && vertexShaderUniformBlocks.Length > 0)
            {
                var uniformBlocks = vertexShaderUniformBlocks;
                if (uniformBlocks.Length > SG_MAX_SHADERSTAGE_UBS)
                {
                    throw new ArgumentException();
                }

                var descUniformBlocks = description.vs.GetUniformBlocks();

                for (var i = 0; i < uniformBlocks.Length; i++)
                {
                    var uniformBlock = uniformBlocks[i];
                    var uniforms = uniformBlock.Uniforms;
                    if (uniforms.Length > SG_MAX_UB_MEMBERS)
                    {
                        throw new ArgumentException(nameof(vertexShaderDescription));
                    }
                    
                    ref var descUniformBlock = ref descUniformBlocks[i];
                    var descUniforms = descUniformBlock.GetUniforms();
                    var uniformBlockSize = 0;

                    for (var j = 0; j < uniforms.Length; j++)
                    {
                        var uniform = uniforms[j];
                        if (uniform.ShaderStage != SgShaderStage.Vertex)
                        {
                            throw new ArgumentException(nameof(vertexShaderDescription));
                        }

                        uniformBlockSize += uniform.Size; 
                        
                        ref var descUniform = ref descUniforms[j];
                        
                        if (!string.IsNullOrEmpty(uniform.Name))
                        {
                            var cNamePointer = Marshal.StringToHGlobalAnsi(uniform.Name);
                            descUniform.name = (char*) cNamePointer;
                            globalPointers.Add(cNamePointer);
                        }

#pragma warning disable 8509
                        descUniform.type = uniform.Type switch
#pragma warning restore 8509
                        {
                            SgShaderUniformType.Float => sg_uniform_type.SG_UNIFORMTYPE_FLOAT,
                            SgShaderUniformType.Float2 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT2,
                            SgShaderUniformType.Float3 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT3,
                            SgShaderUniformType.Float4 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT4,
                            SgShaderUniformType.Matrix4X4 => sg_uniform_type.SG_UNIFORMTYPE_MAT4
                        };
                    }
                    
                    descUniformBlock.size = uniformBlockSize;
                }
            }
            
            var fragmentShaderUniformBlocks = fragmentShaderDescription.UniformBlocks;
            if (fragmentShaderUniformBlocks != null && fragmentShaderUniformBlocks.Length > 0)
            {
                var uniformBlocks = fragmentShaderUniformBlocks;
                if (uniformBlocks.Length > SG_MAX_SHADERSTAGE_UBS)
                {
                    throw new ArgumentException();
                }

                var descUniformBlocks = description.fs.GetUniformBlocks();

                for (var i = 0; i < uniformBlocks.Length; i++)
                {
                    var uniformBlock = uniformBlocks[i];
                    var uniforms = uniformBlock.Uniforms;
                    if (uniforms.Length > SG_MAX_UB_MEMBERS)
                    {
                        throw new ArgumentException(nameof(vertexShaderDescription));
                    }
                    
                    ref var descUniformBlock = ref descUniformBlocks[i];
                    var uniformBlockSize = 0;
                    var descUniforms = descUniformBlock.GetUniforms();

                    if (uniforms.Length > SG_MAX_UB_MEMBERS)
                    {
                        throw new ArgumentException(nameof(fragmentShaderDescription));
                    }
                    
                    for (var j = 0; j < uniforms.Length; j++)
                    {
                        var uniform = uniforms[j];
                        if (uniform.ShaderStage != SgShaderStage.Fragment)
                        {
                            throw new ArgumentException(nameof(fragmentShaderDescription));
                        }

                        uniformBlockSize += uniform.Size;
                        ref var descUniform = ref descUniforms[j];

                        if (!string.IsNullOrEmpty(uniform.Name))
                        {
                            var cNamePointer = Marshal.StringToHGlobalAnsi(uniform.Name);
                            descUniform.name = (char*) cNamePointer;
                            globalPointers.Add(cNamePointer);
                        }

#pragma warning disable 8509
                        descUniform.type = uniform.Type switch
#pragma warning restore 8509
                        {
                            SgShaderUniformType.Float => sg_uniform_type.SG_UNIFORMTYPE_FLOAT,
                            SgShaderUniformType.Float2 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT2,
                            SgShaderUniformType.Float3 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT3,
                            SgShaderUniformType.Float4 => sg_uniform_type.SG_UNIFORMTYPE_FLOAT4,
                            SgShaderUniformType.Matrix4X4 => sg_uniform_type.SG_UNIFORMTYPE_MAT4
                        };
                    }
                    
                    descUniformBlock.size = uniformBlockSize;
                }
            }
            
            var vertexShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(vertexShaderDescription.SourceCode);
            var fragmentShaderSourceCodeCPointer = Marshal.StringToHGlobalAnsi(fragmentShaderDescription.SourceCode);
            
            description.vs.source = (char*) vertexShaderSourceCodeCPointer;
            description.fs.source = (char*) fragmentShaderSourceCodeCPointer;
            
            Handle = sg_make_shader(ref description);
            
            Marshal.FreeHGlobal(vertexShaderSourceCodeCPointer);
            Marshal.FreeHGlobal(fragmentShaderSourceCodeCPointer);

            foreach (var pointer in globalPointers)
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

        ~SgShader()
        {
            ReleaseUnmanagedResources();
        }
    }
}