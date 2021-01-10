// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     <para>
    ///         A GPU resource that configures options for a how a GPU converts 2D or 3D geometry into pixels that can be
    ///         displayed on screen or saved off-screen to a <see cref="GraphicsImage" />.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsPipeline" /> processes drawing commands of geometric data as input and outputs data into
    ///         texture (<see cref="GraphicsImage" />) attachments of a <see cref="GraphicsPass" />. This includes
    ///         rasterization (such as multisampling), visibility, and blending. The <see cref="GraphicsPipeline" /> has
    ///         several steps that happen linearly but in parallel for each XY coordinate of the output. Some stages can be
    ///         programmed using a <see cref="GraphicsShader" />. Some other stages have configurable behavior such as
    ///         <see cref="GraphicsPipelineDepthStencilState" />, <see cref="GraphicsPipelineRasterizerState" />, and
    ///         <see cref="GraphicsPipelineBlendState" />. Other stages are hidden from the perspective of the developer as
    ///         they can't be programmed or configured with `sokol_gfx`.
    ///     </para>
    ///     <para>
    ///         With rasterization, lines and triangles are the building blocks for modeling any 2D and 3D geometry
    ///         (called 3D models). These lines and triangles are made up of points (called vertices) where each
    ///         point is traditionally given attributes such as a position, a color, a normal vector, or
    ///         texture coordinates. This vertex data is the input to a <see cref="GraphicsShader" /> where two programmable
    ///         stages can customize rasterization. The output a <see cref="GraphicsShader" /> are the fragments which may be
    ///         discarded or written to the attachments of a <see cref="GraphicsPass" />.
    ///     </para>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsPipeline" />, call <see cref="Graphics.MakePipeline" />. To
    ///         allocate a <see cref="GraphicsPipeline" /> and initialize it later, call <see cref="Graphics.AllocPipeline" />
    ///         to get an un-initialized <see cref="GraphicsPipeline" /> and then call <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         To activate a <see cref="GraphicsPipeline" /> during a <see cref="GraphicsPass" /> with all it's state, and by
    ///         consequence deactivate any other active <see cref="GraphicsPipeline" />, call
    ///         <see cref="GraphicsPass.ApplyPipeline" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsPipeline" /> must only be used or destroyed with the same active
    ///         <see cref="GraphicsContext" /> that was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPipeline" /> is blittable to the C `sg_pipeline` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsPipeline
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsPipeline" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsPipelineInfo Info => GraphicsPInvoke.sg_query_pipeline_info(this);

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsResourceState State => GraphicsPInvoke.sg_query_pipeline_state(this);

        /// <summary>
        ///     Fill any zero-initialized members of an <see cref="GraphicsPipelineDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating an image.</param>
        /// <returns>An <see cref="GraphicsPipelineDescriptor" /> with any zero-initialized members set to default values.</returns>
        public static GraphicsPipelineDescriptor QueryDefaults([In] ref GraphicsPipelineDescriptor descriptor) =>
            GraphicsPInvoke.sg_query_pipeline_defaults(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <param name="descriptor">.</param>
        public void Initialize([In] ref GraphicsPipelineDescriptor descriptor) => GraphicsPInvoke.sg_init_pipeline(this, ref descriptor);

        /// <summary>
        ///     Destroys the <see cref="GraphicsPipeline" />.
        /// </summary>
        public void Destroy() => GraphicsPInvoke.sg_destroy_pipeline(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Fail() => GraphicsPInvoke.sg_fail_pipeline(this);

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
