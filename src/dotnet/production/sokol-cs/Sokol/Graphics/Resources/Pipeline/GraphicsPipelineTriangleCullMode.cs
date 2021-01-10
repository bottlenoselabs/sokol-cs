// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the different types of triangle face culling modes used by a <see cref="GraphicsPipeline" /> during
    ///     rasterization of triangles.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPipelineTriangleCullMode" /> can be used as an optimization to render less triangles which
    ///         are normally never seen. The face of a triangle is determined by it's order of vertices compared to the
    ///         window space. Assuming a triangle is defined by clock-wise vertices (see
    ///         <see cref="GraphicsPipelineTriangleFaceWinding.Clockwise" />), back facing triangles are usually hidden behind
    ///         front facing triangles when rendering a 3D model. In such a situation, rendering the back facing
    ///         triangles is unnecessary and the rendering can be optimized by using
    ///         <see cref="GraphicsPipelineTriangleCullMode.Back" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineTriangleCullMode" /> is blittable to the C `sg_cull_mode` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPipelineTriangleCullMode
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="GraphicsPipelineTriangleCullMode" /> is <see cref="None" />.
        /// </summary>
        Default,

        /// <summary>
        ///     All triangles are rendered regardless of their facing orientation towards the screen.
        /// </summary>
        None,

        /// <summary>
        ///     Triangles which have their front side facing towards the screen are not rendered.
        /// </summary>
        Front,

        /// <summary>
        ///     Triangles which have their back side facing towards the screen are not rendered.
        /// </summary>
        Back
    }
}
