// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="GraphicsShader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsShaderDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderDescriptor" /> is blittable to the C `sg_shader_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 2984, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsShaderDescriptor
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        [FieldOffset(8)]
        private fixed ulong _attributes[24 * GraphicsConstants.MaximumVertexAttributes / 8];

        /// <summary>
        ///     The <see cref="GraphicsShaderStageDescriptor" /> for the <see cref="GraphicsShaderStageType.Vertex" /> stage. Must be set.
        /// </summary>
        [FieldOffset(392)]
        public GraphicsShaderStageDescriptor VertexStage;

        /// <summary>
        ///     The <see cref="GraphicsShaderStageDescriptor" /> for the <see cref="GraphicsShaderStageType.Fragment" /> stage. Must be
        ///     set.
        /// </summary>
        [FieldOffset(1680)]
        public GraphicsShaderStageDescriptor FragmentStage;

        /// <summary>
        /// TODO.
        /// </summary>
        [FieldOffset(2968)]
        private readonly IntPtr _label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(2976)]
        private readonly uint _endCanary;

        /// <summary>
        ///     Gets the <see cref="GraphicsShaderVertexAttributeDescriptor" /> by reference given the specified index. All vertex
        ///     attributes must be configured for <see cref="GraphicsBackend.OpenGLES2" /> and
        ///     <see cref="GraphicsBackend.Direct3D11" />.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsShaderVertexAttributeDescriptor" /> by reference.</returns>
        public readonly ref GraphicsShaderVertexAttributeDescriptor Attribute(int index = 0)
        {
            fixed (GraphicsShaderDescriptor* shaderDescription = &this)
            {
                var ptr = (GraphicsShaderVertexAttributeDescriptor*)&shaderDescription->_attributes[0];
                return ref *(ptr + index);
            }
        }
    }
}
