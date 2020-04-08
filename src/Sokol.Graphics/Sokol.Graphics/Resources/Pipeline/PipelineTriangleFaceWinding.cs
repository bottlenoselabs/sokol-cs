// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different ways the front/back sides of a triangle primitive are determined.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PipelineTriangleFaceWinding" /> is blittable to the C `sg_face_winding` enum found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineTriangleFaceWinding
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default
        ///     <see cref="PipelineTriangleFaceWinding" /> is <see cref="CounterClockwise" />.
        /// </summary>
        Default,

        /// <summary>
        ///     The front side of a triangle is determined by counter-clockwise ordered vertices when facing towards the
        ///     screen. An example of a counter-clockwise face winding triangle is a triangle defined by vertices in the
        ///     following order: top-right, bottom-left, and bottom-right.
        /// </summary>
        CounterClockwise,

        /// <summary>
        ///     The front side of a triangle is determined by clockwise ordered vertices when facing towards the
        ///     screen. An example of a clockwise face winding triangle is a triangle defined by vertices in the
        ///     following order: top-right, bottom-right, and bottom-left.
        /// </summary>
        Clockwise,
    }
}
