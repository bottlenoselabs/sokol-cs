// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Describes the depth/stencil test stage of a <see cref="GraphicsPipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsPipelineStencilState" /> is used to enable or disable writing to the attachments of
    ///         a <see cref="GraphicsPass" /> on a per-pixel basis. The depth/stencil test stage usually happens after the
    ///         programmable <see cref="GraphicsShaderStageType.Fragment" /> stage of a <see cref="GraphicsPipeline" />.
    ///         However in some cases hardware vendors can optimize for early depth/stencil testing which instead take place
    ///         after the programmable <see cref="GraphicsShaderStageType.Vertex" /> stage. In this case significant
    ///         performance can be observed because fragments that would fail depth/stencil test can be discarded before they
    ///         are even generated and processed by the <see cref="GraphicsShaderStageType.Fragment" /> stage. In any case,
    ///         stencil testing happens before depth testing.
    ///     </para>
    ///     <para>
    ///         A stencil test conditionally discards a fragment (pixel data: color + depth + stencil) based on the comparison
    ///         between the <see cref="StencilReference" /> value and the value in the stencil attachment of a
    ///         <see cref="GraphicsPass" />. To enable stencil testing set <see cref="StencilIsEnabled" /> to <c>true</c>
    ///         (default is <c>false</c>). When reading a value from the stencil attachment, the value is masked using a
    ///         bitwise AND operation with the value of <see cref="StencilReadMask" />. When writing a value to the stencil
    ///         attachment, the value is masked using a bitwise AND operation with the value of
    ///         <see cref="StencilWriteMask" />. Additionally, a <see cref="GraphicsPipelineStencilState" /> can be configured
    ///         differently for front-facing triangles via <see cref="StencilFront" /> and back-facing triangles via
    ///         <see cref="StencilBack" />. If the difference between front and back-facing triangles does not matter set both
    ///         to the desired values. The value of <see cref="GraphicsPipelineStencilState.CompareFunction" /> determines how
    ///         the stencil test can fail/pass. The values of <see cref="GraphicsPipelineStencilState.FailOperation" />,
    ///         <see cref="GraphicsPipelineStencilState.DepthFailOperation" />, and
    ///         <see cref="GraphicsPipelineStencilState.PassOperation" /> determine how the stencil attachment is written when
    ///         stencil test fails/passes.
    ///     </para>
    ///     <para>
    ///         A depth test conditionally discards a fragment (pixel data: color + depth + stencil) based on the comparison
    ///         between the fragment's depth value and the value in the depth attachment of a
    ///         <see cref="GraphicsPass" />. Depth testing with `sokol_gfx` is always enabled, however writing to the depth
    ///         attachment is disabled by default. To enable writing to the depth attachment set
    ///         <see cref="DepthWriteIsEnabled" /> to <c>true</c>. The <see cref="DepthCompareFunction" /> property specifies
    ///         how the depth test can fail/pass.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineDepthStencilState" /> is blittable to the C `sg_depth_stencil_state` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 44, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsPipelineDepthStencilState
    {
        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilState" /> for front-facing triangles.
        /// </summary>
        [FieldOffset(0)]
        public GraphicsPipelineStencilState StencilFront;

        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilState" /> for back-facing triangles.
        /// </summary>
        [FieldOffset(16)]
        public GraphicsPipelineStencilState StencilBack;

        /// <summary>
        ///     The <see cref="GraphicsPipelineDepthCompareFunction" /> to use for depth testing.
        /// </summary>
        [FieldOffset(32)]
        public GraphicsPipelineDepthCompareFunction DepthCompareFunction;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the depth attachment is written to.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(36)]
        public CBool DepthWriteIsEnabled;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether stencil testing is enabled. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(37)]
        public CBool StencilIsEnabled;

        /// <summary>
        ///     A bitmask that determines which bits the stencil test uses when reading from the stencil attachment for a fragment.
        ///     Default is <c>0x00</c>.
        /// </summary>
        [FieldOffset(38)]
        public byte StencilReadMask;

        /// <summary>
        ///     A bitmask that determines which bits the stencil test uses when writing to the stencil attachment for a
        ///     fragment. Default is <c>0x00</c>.
        /// </summary>
        [FieldOffset(39)]
        public byte StencilWriteMask;

        /// <summary>
        ///     The stencil value used for stencil tests. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(40)]
        public byte StencilReference;
    }
}
