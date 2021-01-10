// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     Describes specific configuration of the rasterization stage of a <see cref="GraphicsPipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The rasterizer stage is a fixed function (non-programmable) hardware unit that converts vector
    ///         primitives (points, lines, triangles) into a raster image by performing scan-line conversion. During
    ///         rasterization, vertices are transformed into the homogenous clip-space and clipped. Attributes used in a
    ///         <see cref="GraphicsShaderStageType.Vertex" /> stage may be interpolated across the primitive (such as color)
    ///         and made ready for the <see cref="GraphicsShaderStageType.Fragment" /> stage. The rasterization stage happens
    ///         after the <see cref="GraphicsShaderStageType.Vertex" /> stage and before the
    ///         <see cref="GraphicsShaderStageType.Fragment" /> stage.
    ///     </para>
    ///     <para>
    ///         If a color attachment supports multi-sampling (see <see cref="GraphicsFeatures.MsaaRenderTargets" />),
    ///         multiple samples per fragment can be created, and the following fields are relevant:
    ///         <list type="bullet">
    ///             <item>
    ///                 <description><see cref="SampleCount" /> is the number of samples for each pixel.</description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     If <see cref="AlphaToCoverageIsEnabled" /> is set to <c>true</c>, the fragment's color alpha
    ///                     component is used to compute a coverage mask that affects the values being written to all
    ///                     attachments (color, depth, and stencil). Default value is <c>false</c>.
    ///                 </description>
    ///             </item>
    ///         </list>
    ///         To determine a final coverage mask, a logical AND is performed on the resulting coverage mask with the
    ///         masks from the rasterizer and fragment shader.
    ///     </para>
    ///     <para>
    ///         Polygons (triangle primitives) that are coplanar in 3D space can be made to appear as if they are not
    ///         coplanar by adding a z-bias (or depth bias) to each polygon. This is a technique commonly used to
    ///         improve and fine-tune shadow maps, avoid shadow acne (unintentional self-shadowing), and avoid other
    ///         depth artifacts that are caused by coplanar polygons. An application can help ensure that
    ///         coplanar polygons are rendered properly by adjusting <see cref="DepthBias" />,
    ///         <see cref="DepthBiasSlopeScale" />, and <see cref="DepthBiasClamp" /> to appropriate values.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipelineRasterizerState" /> is blittable to the C `sg_rasterizer_state` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 28, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public struct GraphicsPipelineRasterizerState
    {
        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(0)]
        public CBool AlphaToCoverageIsEnabled;

        /// <summary>
        ///     The <see cref="GraphicsPipelineTriangleCullMode" />.
        /// </summary>
        [FieldOffset(4)]
        public GraphicsPipelineTriangleCullMode CullMode;

        /// <summary>
        ///     The <see cref="GraphicsPipelineTriangleFaceWinding" />.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsPipelineTriangleFaceWinding FaceWinding;

        /// <summary>
        ///     The number of samples for each pixel.
        /// </summary>
        [FieldOffset(12)]
        public int SampleCount;

        /// <summary>
        ///     The constant z-bias applied to all fragments.
        /// </summary>
        [FieldOffset(16)]
        public float DepthBias;

        /// <summary>
        ///     The z-bias that scales with the gradient of the triangle.
        /// </summary>
        [FieldOffset(20)]
        public float DepthBiasSlopeScale;

        /// <summary>
        ///     The maximum z-bias value to apply to all fragments.
        /// </summary>
        [FieldOffset(24)]
        public float DepthBiasClamp;
    }
}
