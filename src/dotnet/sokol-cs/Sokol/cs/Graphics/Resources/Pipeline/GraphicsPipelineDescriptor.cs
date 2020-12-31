// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="GraphicsPipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineDescriptor" /> specifies the rendering configuration state used when drawing
    ///         geometry
    ///         with a rendering <see cref="GraphicsPass" />. Includes state about shader stages, vertex attributes, depth
    ///         stencil, blending, and rasterization.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsPipelineDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineDescriptor" /> is blittable to the C `sg_pipeline_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 456, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public struct GraphicsPipelineDescriptor
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     The <see cref="GraphicsPipelineVertexLayoutDescriptor" />.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsPipelineVertexLayoutDescriptor Layout;

        /// <summary>
        ///     The <see cref="GraphicsShader" /> to use.
        /// </summary>
        [FieldOffset(292)]
        public GraphicsShader Shader;

        /// <summary>
        ///     The <see cref="GraphicsPipelineVertexPrimitiveType" />.
        /// </summary>
        [FieldOffset(296)]
        public GraphicsPipelineVertexPrimitiveType PrimitiveType;

        /// <summary>
        ///     The <see cref="GraphicsPipelineVertexIndexType" />.
        /// </summary>
        [FieldOffset(300)]
        public GraphicsPipelineVertexIndexType IndexType;

        /// <summary>
        ///     The <see cref="GraphicsPipelineDepthStencilState" />.
        /// </summary>
        [FieldOffset(304)]
        public GraphicsPipelineDepthStencilState DepthStencil;

        /// <summary>
        ///     The <see cref="GraphicsPipelineBlendState" />.
        /// </summary>
        [FieldOffset(348)]
        public GraphicsPipelineBlendState Blend;

        /// <summary>
        ///     The <see cref="GraphicsPipelineRasterizerState" />.
        /// </summary>
        [FieldOffset(408)]
        public GraphicsPipelineRasterizerState Rasterizer;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(440)]
        private readonly IntPtr _label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(448)]
        private readonly uint _endCanary;
    }
}
