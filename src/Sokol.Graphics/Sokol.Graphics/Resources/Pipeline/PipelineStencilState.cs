// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Describes how to update the contents of the stencil attachment, based on the results of the stencil test and
    ///     the depth test.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineStencilOperation" /> is blittable to the C `sg_stencil_state` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public struct PipelineStencilState
    {
        /// <summary>
        ///     The <see cref="PipelineStencilOperation" /> that is performed to update the values in the stencil
        ///     attachment when the stencil test fails.
        /// </summary>
        [FieldOffset(0)]
        public PipelineStencilOperation FailOperation;

        /// <summary>
        ///     The <see cref="PipelineStencilOperation" /> that is performed to update the values in the stencil
        ///     attachment when the stencil test passes, but the depth test fails.
        /// </summary>
        [FieldOffset(4)]
        public PipelineStencilOperation DepthFailOperation;

        /// <summary>
        ///     The <see cref="PipelineStencilOperation" /> that is performed to update the values in the stencil
        ///     attachment when both the stencil test and the depth test pass.
        /// </summary>
        [FieldOffset(8)]
        public PipelineStencilOperation PassOperation;

        /// <summary>
        ///     The <see cref="PipelineDepthCompareFunction"/> that is performed between the
        ///     <see cref="PipelineDepthStencilState.StencilReference"/> value and a masked value in the stencil
        ///     attachment.
        /// </summary>
        [FieldOffset(12)]
        public PipelineDepthCompareFunction CompareFunction;
    }
}
