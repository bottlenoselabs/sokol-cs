// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the operations performed on a currently stored stencil value when a comparison test passes or fails.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineStencilOperation" /> is blittable to the C `sg_stencil_op` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineStencilOperation
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineStencilOperation" /> is <see cref="Keep" />.
        /// </summary>
        Default,

        /// <summary>
        ///     Keep the current stencil value.
        /// </summary>
        Keep,

        /// <summary>
        ///     Set the stencil value to zero.
        /// </summary>
        Zero,

        /// <summary>
        ///     Replace the stencil value with the stencil reference value, which is set by the
        /// </summary>
        Replace,

        /// <summary>
        ///     If the current stencil value is not the maximum representable value, increase the stencil value by one.
        ///     Otherwise, if the current stencil value is the maximum representable value, do not change the stencil
        ///     value.
        /// </summary>
        IncrementClamp,

        /// <summary>
        ///     If the current stencil value is not zero, decrease the stencil value by one. Otherwise, if the current
        ///     stencil value is zero, do not change the stencil value.
        /// </summary>
        DecrementClamp,

        /// <summary>
        ///     Perform a logical bitwise invert operation on the current stencil value.
        /// </summary>
        Invert,

        /// <summary>
        ///     If the current stencil value is not the maximum representable value, increase the stencil value by one.
        ///     Otherwise, if the current stencil value is the maximum representable value, set the stencil value to
        ///     zero.
        /// </summary>
        IncrementWrap,

        /// <summary>
        ///     If the current stencil value is not zero, decrease the stencil value by one. Otherwise, if the current
        ///     stencil value is zero, set the stencil value to the maximum representable value.
        /// </summary>
        DecrementWrap
    }
}
