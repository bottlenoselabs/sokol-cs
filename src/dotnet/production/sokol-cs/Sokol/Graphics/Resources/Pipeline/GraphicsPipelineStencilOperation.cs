// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the operations for how a stencil attachment of a <see cref="GraphicsPass" /> is written to when a stencil/depth
    ///     test passes or fails for a fragment.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineStencilOperation" /> is blittable to the C `sg_stencil_op` enum found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineStencilOperation
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineStencilOperation" /> is <see cref="Keep" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Keep the stored stencil value in the attachment.
        /// </summary>
        Keep,

        /// <summary>
        ///     Set the stencil value in the attachment to zero.
        /// </summary>
        Zero,

        /// <summary>
        ///     Replace the stencil value in the attachment with <see cref="GraphicsPipelineDepthStencilState.StencilReference" />.
        /// </summary>
        Replace,

        /// <summary>
        ///     The stencil value in the attachment is incremented by 1 if it is lower than the maximum value of
        ///     <see cref="byte.MaxValue" />.
        /// </summary>
        IncrementClamp,

        /// <summary>
        ///     The stencil value in the attachment is decremented by 1 if it is higher than the minimum value of
        ///     <see cref="byte.MinValue" />.
        /// </summary>
        DecrementClamp,

        /// <summary>
        ///     The stencil value in the attachment is bitwise flipped.
        /// </summary>
        Invert,

        /// <summary>
        ///     Same as <see cref="IncrementClamp" /> except the stencil value is wrapped to <c>0</c> when the maximum value of
        ///     of <c>255</c> is exceeded.
        /// </summary>
        IncrementWrap,

        /// <summary>
        ///     Same as <see cref="DecrementClamp" /> except the stencil value is wrapped to the maximum value of
        ///     <c>255</c> when below <c>0</c>.
        /// </summary>
        DecrementWrap
    }
}
