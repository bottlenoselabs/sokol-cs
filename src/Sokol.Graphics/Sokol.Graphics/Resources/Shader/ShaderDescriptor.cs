// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="ShaderDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ShaderDescriptor" /> is blittable to the C `sg_shader_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 2968, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct ShaderDescriptor
    {
        /// <summary>
        ///     The <see cref="ShaderStageDescriptor" /> for the "per-vertex processing" stage. Must be set.
        /// </summary>
        [FieldOffset(392)]
        public ShaderStageDescriptor VertexStage;

        /// <summary>
        ///     The <see cref="ShaderStageDescriptor" /> for the "per-fragment processing" stage. Must be set.
        /// </summary>
        [FieldOffset(1672)]
        public ShaderStageDescriptor FragmentStage;

        [FieldOffset(8)]
        internal fixed ulong _attributes[24 * sokol_gfx.SG_MAX_VERTEX_ATTRIBUTES / 8];

        // TODO: Trace hooks.
        [FieldOffset(2952)]
        internal IntPtr Label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        internal uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(2960)]
        internal uint _endCanary;

        /// <summary>
        ///     Gets the <see cref="ShaderVertexAttributeDescriptor" /> by reference given the specified index. All vertex
        ///     attributes must be configured for <see cref="GraphicsBackend.OpenGLES2" /> and
        ///     <see cref="GraphicsBackend.Direct3D11" />.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="ShaderVertexAttributeDescriptor" /> by reference.</returns>
        public ref ShaderVertexAttributeDescriptor Attribute(int index)
        {
            fixed (ShaderDescriptor* shaderDescription = &this)
            {
                var ptr = (ShaderVertexAttributeDescriptor*)&shaderDescription->_attributes[0];
                return ref *(ptr + index);
            }
        }
    }
}
