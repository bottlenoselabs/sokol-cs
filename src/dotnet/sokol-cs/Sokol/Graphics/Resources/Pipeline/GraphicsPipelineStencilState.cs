// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Describes some of the stencil test stage for a <see cref="GraphicsPipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineStencilState" /> is blittable to the C `sg_stencil_state` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsPipelineStencilState
    {
        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilOperation" /> that is used to write to the stencil attachment when the
        ///     stencil test fails.
        /// </summary>
        [FieldOffset(0)]
        public GraphicsPipelineStencilOperation FailOperation;

        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilOperation" /> that is used to write to the stencil attachment when the
        ///     stencil test passes, but the depth test fails.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsPipelineStencilOperation DepthFailOperation;

        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilOperation" /> that is used to write to the stencil attachment when both the
        ///     stencil test and the depth test pass.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsPipelineStencilOperation PassOperation;

        /// <summary>
        ///     The <see cref="GraphicsPipelineStencilCompareFunction" /> that is used for stencil testing.
        /// </summary>
        [FieldOffset(12)]
        public GraphicsPipelineStencilCompareFunction CompareFunction;
    }
}
