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
    ///         <see cref="GraphicsPipelineDepthCompareFunction" /> is blittable to the C `sg_compare_func` enum
    ///         found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineDepthCompareFunction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineDepthCompareFunction" /> is <see cref="Always" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The depth test never passes.
        /// </summary>
        Never,

        /// <summary>
        ///     The depth  test passes when the fragment's depth value is less than the stored value in the
        ///     depth attachment.
        /// </summary>
        Less,

        /// <summary>
        ///     The depth test passes when the fragment's depth value is equal to the stored value in the depth attachment.
        /// </summary>
        Equal,

        /// <summary>
        ///     The depth test passes when the fragment's depth value is less than or equal to the stored value in the depth
        ///     attachment.
        /// </summary>
        LessEqual,

        /// <summary>
        ///     The depth test passes when the fragment's depth value is greater than the stored value in the depth attachment.
        /// </summary>
        Greater,

        /// <summary>
        ///     The depth test passes when the fragment's depth value is not equal to the stored value in the depth attachment.
        /// </summary>
        NotEqual,

        /// <summary>
        ///     The depth test passes when the fragment's depth value is greater than or equal to the stored value in the depth
        ///     attachment.
        /// </summary>
        GreaterEqual,

        /// <summary>
        ///     The depth test always passes.
        /// </summary>
        Always
    }
}
