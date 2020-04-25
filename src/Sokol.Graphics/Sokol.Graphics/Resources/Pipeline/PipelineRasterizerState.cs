// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Describes specific configuration of the rasterization stage of a <see cref="Pipeline" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The rasterizer stage is a fixed function (non-programmable) hardware unit that converts vector
    ///         primitives (points, lines, triangles) into a raster image by performing scan-line conversion. During
    ///         rasterization, vertices are transformed into the homogenous clip-space and clipped. As output,
    ///         per-vertex attributes may be interpolated across the primitive (such as color) and made ready for the
    ///         "per-fragment processing" stage of a <see cref="Shader" />.
    ///     </para>
    ///     <para>
    ///         If a color attachment supports multi-sampling (see <see cref="GraphicsFeatures.MsaaRenderTargets" />),
    ///         you can create multiple samples per fragment, and the following properties determine coverage:
    ///         <list type="bullet">
    ///             <item>
    ///                 <description><see cref="SampleCount" /> is the number of samples for each pixel.</description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     If <see cref="AlphaToCoverageIsEnabled" /> is set to <c>true</c>, then the alpha component
    ///                     fragment output for the active <see cref="Pass"/> resource's color attachments is used to
    ///                     compute a coverage mask that affects the values being written to all attachments (color,
    ///                     depth, and stencil). Default value is <c>false</c>.
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
    ///         For more information, see
    ///         <a
    ///             href="https://docs.microsoft.com/en-ca/windows/win32/dxtecharts/common-techniques-to-improve-shadow-depth-maps?redirectedfrom=MSDN#slope-scale-depth-bias">
    ///             Common Techniques to Improve Shadow Depth Maps
    ///         </a>
    ///         on MSDN.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineRasterizerState" /> is blittable to the C `sg_rasterizer_state` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 28, Pack = 4)]
    public struct PipelineRasterizerState
    {
        /// <summary>
        ///     A <see cref="bool" /> value indicating whether alpha component fragment output for color attachments is
        ///     read and used to compute a sample coverage mask. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(0)]
        public BlittableBoolean AlphaToCoverageIsEnabled;

        /// <summary>
        ///     The <see cref="PipelineTriangleCullMode" />.
        /// </summary>
        [FieldOffset(4)]
        public PipelineTriangleCullMode CullMode;

        /// <summary>
        ///     The <see cref="PipelineTriangleFaceWinding" />.
        /// </summary>
        [FieldOffset(8)]
        public PipelineTriangleFaceWinding FaceWinding;

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
        ///     The bias that scales with the depth gradient of the triangle primitive.
        /// </summary>
        [FieldOffset(20)]
        public float DepthBiasSlopeScale;

        /// <summary>
        ///     The maximum bias value to apply to the fragment.
        /// </summary>
        [FieldOffset(24)]
        public float DepthBiasClamp;
    }
}
