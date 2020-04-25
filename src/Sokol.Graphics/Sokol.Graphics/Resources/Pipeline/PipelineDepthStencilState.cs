// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Describes specific configuration of the depth and stencil stages of a <see cref="Pipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A stencil test is a comparison between the <see cref="StencilReference" /> value and a masked value
    ///         stored in a stencil attachment. A value is masked by performing a logical AND operation on it with the
    ///         <see cref="StencilReadMask" /> value. <see cref="StencilWriteMask"/> determines which stencil bits can
    ///         be modified as the result of a <see cref="PipelineStencilOperation" />.
    ///     </para>
    ///     <para>
    ///         The <see cref="DepthCompareFunction" /> property specifies how the depth test is performed. If a
    ///         fragment’s depth value fails the depth test, the fragment is discarded.
    ///         <see cref="PipelineDepthCompareFunction.Less" /> is a commonly used value for
    ///         <see cref="DepthCompareFunction" />, because fragment values that are farther away from the viewer than
    ///         the pixel depth value (a previously written fragment) fail the depth test and are considered occluded by
    ///         the earlier depth value.
    ///     </para>
    ///     <para>
    ///         The <see cref="StencilFront" /> and <see cref="StencilBack" /> properties define two independent stencil
    ///         states: one for front-facing triangle primitives (see <see cref="PipelineTriangleFaceWinding"/>) and the
    ///         other for back-facing triangles primitives, respectively.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineDepthStencilState" /> is blittable to the C `sg_depth_stencil_state` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 44, Pack = 4)]
    public struct PipelineDepthStencilState
    {
        /// <summary>
        ///     The <see cref="PipelineStencilState" /> for front-facing triangle primitives.
        /// </summary>
        [FieldOffset(0)]
        public PipelineStencilState StencilFront;

        /// <summary>
        ///     The <see cref="PipelineStencilState" /> for back-facing triangle primitives.
        /// </summary>
        [FieldOffset(16)]
        public PipelineStencilState StencilBack;

        /// <summary>
        ///     The <see cref="PipelineDepthCompareFunction" /> that is performed between a fragment’s depth value and
        ///     the depth value in the attachment, which determines whether to discard the fragment.
        /// </summary>
        [FieldOffset(32)]
        public PipelineDepthCompareFunction DepthCompareFunction;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether depth values can be written to the depth attachment.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(36)]
        public BlittableBoolean DepthWriteIsEnabled;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether stencil testing is enabled. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(37)]
        public BlittableBoolean StencilIsEnabled;

        /// <summary>
        ///     A bitmask that determines from which bits that stencil comparison tests can read. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(38)]
        public byte StencilReadMask;

        /// <summary>
        ///     A bitmask that determines to which bits that stencil operations can write. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(39)]
        public byte StencilWriteMask;

        /// <summary>
        ///     A stencil reference value used for both front and back stencil comparison tests. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(40)]
        public byte StencilReference;
    }
}
