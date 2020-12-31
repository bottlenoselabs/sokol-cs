// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different options for how a depth value of a fragment is compared to the depth
    ///     or attachment of a <see cref="GraphicsPass" /> during depth testing. Fragments which fail the test are discarded.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineStencilCompareFunction" /> is blittable to the C `sg_compare_func` enum
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineStencilCompareFunction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineStencilCompareFunction" /> is <see cref="Always" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The stencil test never passes.
        /// </summary>
        Never,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is less
        ///     than the stored value in the stencil attachment.
        /// </summary>
        Less,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is
        ///     equal to the stored value in the stencil attachment.
        /// </summary>
        Equal,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is less
        ///     than or equal to the stored value in the stencil attachment.
        /// </summary>
        LessEqual,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is
        ///     greater than the stored value in the stencil attachment.
        /// </summary>
        Greater,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is not
        ///     equal to the stored value in the stencil attachment.
        /// </summary>
        NotEqual,

        /// <summary>
        ///     The stencil test passes when the value of <see cref="GraphicsPipelineDepthStencilState.StencilReference" /> is
        ///     greater than the stored value in the stencil attachment.
        /// </summary>
        GreaterEqual,

        /// <summary>
        ///     The stencil test always passes.
        /// </summary>
        Always
    }
}
