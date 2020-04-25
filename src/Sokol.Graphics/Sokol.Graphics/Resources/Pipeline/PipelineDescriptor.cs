// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Pipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineDescriptor" /> specifies the rendering configuration state used when drawing geometry
    ///         with a rendering <see cref="Pass" />. Includes state about shader stages, vertex attributes, depth
    ///         stencil, blending, and rasterization.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="PipelineDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineDescriptor" /> is blittable to the C `sg_pipeline_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 456, Pack = 8, CharSet = CharSet.Ansi)]
    public struct PipelineDescriptor
    {
        /// <summary>
        ///     The <see cref="PipelineVertexLayoutDescriptor" />.
        /// </summary>
        [FieldOffset(4)]
        public PipelineVertexLayoutDescriptor Layout;

        /// <summary>
        ///     The <see cref="Shader" /> resource.
        /// </summary>
        [FieldOffset(292)]
        public Shader Shader;

        /// <summary>
        ///     The <see cref="PipelineVertexPrimitiveType" />.
        /// </summary>
        [FieldOffset(296)]
        public PipelineVertexPrimitiveType PrimitiveType;

        /// <summary>
        ///     The <see cref="PipelineVertexIndexType" />.
        /// </summary>
        [FieldOffset(300)]
        public PipelineVertexIndexType IndexType;

        /// <summary>
        ///     The <see cref="PipelineDepthStencilState" />.
        /// </summary>
        [FieldOffset(304)]
        public PipelineDepthStencilState DepthStencil;

        /// <summary>
        ///     The <see cref="PipelineBlendState" />.
        /// </summary>
        [FieldOffset(348)]
        public PipelineBlendState Blend;

        /// <summary>
        ///     The <see cref="PipelineRasterizerState" />.
        /// </summary>
        [FieldOffset(408)]
        public PipelineRasterizerState Rasterizer;

        // TODO: Trace hooks
        [FieldOffset(440)]
        private readonly IntPtr _label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(448)]
        private readonly uint _endCanary;
    }
}
