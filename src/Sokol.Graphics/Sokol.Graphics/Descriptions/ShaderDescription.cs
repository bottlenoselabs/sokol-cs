// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ShaderDescription" /> is blittable to the C `sg_shader_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 2968, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct ShaderDescription
    {
        /// <summary>
        ///     The <see cref="ShaderStageDescription" /> for the "per-vertex processing" stage. Must be set.
        /// </summary>
        [FieldOffset(392)]
        public ShaderStageDescription VertexStage;

        /// <summary>
        ///     The <see cref="ShaderStageDescription" /> for the "per-fragment processing" stage. Must be set.
        /// </summary>
        [FieldOffset(1672)]
        public ShaderStageDescription FragmentStage;

        [FieldOffset(8)]
        internal fixed ulong _attributes[24 * sokol_gfx.SG_MAX_VERTEX_ATTRIBUTES / 8];

        // TODO: Trace hooks.
        [FieldOffset(2952)]
        internal IntPtr Label;

        [FieldOffset(0)]
        internal uint _startCanary;

        [FieldOffset(2960)]
        internal uint _endCanary;

        /// <summary>
        ///     Gets the <see cref="ShaderAttributeDescription" /> by reference given the specified index. All vertex
        ///     attributes must be configured for <see cref="GraphicsBackend.OpenGLES2" /> and
        ///     <see cref="GraphicsBackend.Direct3D11" />.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="ShaderAttributeDescription" /> by reference.</returns>
        public ref ShaderAttributeDescription Attribute(int index)
        {
            fixed (ShaderDescription* shaderDescription = &this)
            {
                var ptr = (ShaderAttributeDescription*)&shaderDescription->_attributes[0];
                return ref *(ptr + index);
            }
        }
    }
}
