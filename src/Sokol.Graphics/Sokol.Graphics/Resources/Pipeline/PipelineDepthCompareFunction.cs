// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the options to specify how a sample compare operation should be performed on a depth texture
    ///     <see cref="Image" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineDepthCompareFunction" /> is blittable to the C `sg_compare_func` enum found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineDepthCompareFunction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineDepthCompareFunction" /> is <see cref="Always" />.
        /// </summary>
        Default,

        /// <summary>
        ///     A new value never passes the comparison test.
        /// </summary>
        Never,

        /// <summary>
        ///     A new value passes the comparison test if it is less than the existing value.
        /// </summary>
        Less,

        /// <summary>
        ///     A new value passes the comparison test if it is equal to the existing value.
        /// </summary>
        Equal,

        /// <summary>
        ///     A new value passes the comparison test if it is less than or equal to the existing value.
        /// </summary>
        LessEqual,

        /// <summary>
        ///     A new value passes the comparison test if it is greater than the existing value.
        /// </summary>
        Greater,

        /// <summary>
        ///     A new value passes the comparison test if it is not equal to the existing value.
        /// </summary>
        NotEqual,

        /// <summary>
        ///     A new value passes the comparison test if it is greater than or equal to the existing value.
        /// </summary>
        GreaterEqual,

        /// <summary>
        ///     A new value always passes the comparison test.
        /// </summary>
        Always
    }
}
