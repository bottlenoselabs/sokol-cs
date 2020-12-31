// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that describes and uses necessary information to render pixels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsPass" /> holds information about a number of texture attachments (each a
    ///         <see cref="GraphicsImage" />) for rendering to the screen (default framebuffer) or to some render target(s)
    ///         (non-default framebuffer) for off-screen rendering. The attachments can include 1 to 4 color textures and an
    ///         optional depth/stencil texture. At render time, a <see cref="GraphicsPass" /> uses a
    ///         <see cref="GraphicsPipeline" />, one or more <see cref="GraphicsBuffer" />(s), shader uniforms (parameters), a
    ///         viewport, and a scissor rectangle to render pixels.
    ///     </para>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsPass" />, call <see cref="Graphics.MakePass" />. To allocate a
    ///         <see cref="GraphicsPass" /> and initialize it later, call <see cref="Graphics.AllocPass" /> to get an
    ///         un-initialized <see cref="GraphicsPass" /> and then call <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsPass" /> must only be used or destroyed with the same active
    ///         <see cref="GraphicsContext" /> that was also active when the resource was initialized.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPass" /> is blittable to the C `sg_pass` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public readonly unsafe struct GraphicsPass
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsPass" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsPassInfo Info => PInvoke.sg_query_pass_info(this);

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsResourceState State => PInvoke.sg_query_pass_state(this);

        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="GraphicsPassDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pass.</param>
        /// <returns>A <see cref="GraphicsPassDescriptor" /> with any zero-initialized members set to default values.</returns>
        public static GraphicsPassDescriptor QueryDefaults([In] ref GraphicsPassDescriptor descriptor) => PInvoke.sg_query_pass_defaults(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <param name="descriptor">.</param>
        public void Initialize([In] ref GraphicsPassDescriptor descriptor) => PInvoke.sg_init_pass(this, ref descriptor);

        /// <summary>
        ///     Destroys the <see cref="GraphicsPass" />.
        /// </summary>
        public void Destroy() => PInvoke.sg_destroy_pass(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Fail() => PInvoke.sg_fail_pass(this);

        /// <summary>
        ///     Begins the <see cref="GraphicsPass" /> with <see cref="GraphicsPassAction.DontCare" /> as the action.
        /// </summary>
        public void Begin()
        {
            var passAction = GraphicsPassAction.DontCare;
            Begin(ref passAction);
        }

        /// <summary>
        ///     Begins the <see cref="GraphicsPass" /> with <see cref="GraphicsPassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        public void Begin(Rgba32F clearColor)
        {
            var passAction = GraphicsPassAction.Clear(clearColor);
            Begin(ref passAction);
        }

        /// <summary>
        ///     Begins the <see cref="GraphicsPass" /> with a specified <see cref="GraphicsPassAction" />.
        /// </summary>
        /// <param name="passAction">The pass action.</param>
        public void Begin([In] ref GraphicsPassAction passAction) => PInvoke.sg_begin_pass(this, ref passAction);

        /// <summary>
        ///     Ends the <see cref="GraphicsPass" />.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void End() => PInvoke.sg_end_pass();

        /// <summary>
        ///     Sets the specified <see cref="GraphicsPipeline" /> for the next calls to <see cref="DrawElements" />.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void ApplyPipeline(GraphicsPipeline pipeline) => PInvoke.sg_apply_pipeline(pipeline);

        /// <summary>
        ///     Sets the specified <see cref="GraphicsResourceBindings" /> for the next calls to <see cref="DrawElements" />.
        /// </summary>
        /// <param name="bindings">The resource bindings.</param>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void ApplyBindings([In] ref GraphicsResourceBindings bindings) => PInvoke.sg_apply_bindings(ref bindings);

        /// <summary>
        ///     Updates the uniforms for a <see cref="GraphicsShader" /> for the next calls to <see cref="DrawElements" /> given
        ///     the specified <see cref="GraphicsShaderStageType" />, the block the uniform belongs to, and the data itself. Must
        ///     be called after
        ///     <see cref="ApplyPipeline" />.
        /// </summary>
        /// <param name="stage">The shader stage.</param>
        /// <param name="value">The data.</param>
        /// <param name="uniformBlockIndex">The uniform block zero-based index which the uniform belongs to.</param>
        /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void ApplyShaderUniforms<T>(GraphicsShaderStageType stage, [In] ref T value, int uniformBlockIndex = 0)
            where T : unmanaged
        {
            var dataPointer = Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            PInvoke.sg_apply_uniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        /// <summary>
        ///     Updates the viewport for the next calls to <see cref="DrawElements" />.
        /// </summary>
        /// <param name="x">The top-left x-coordinate of the viewport.</param>
        /// <param name="y">The top-left y-coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="originIsTopLeft">
        ///     Indicates whether the origin is the top-left (<c>true</c>) or the bottom-left (<c>false</c>).
        /// </param>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void ApplyViewport(int x, int y, int width, int height, bool originIsTopLeft = false) =>
            PInvoke.sg_apply_viewport(x, y, width, height, originIsTopLeft);

        /// <summary>
        ///     Updates the scissor rectangle for the next calls to <see cref="DrawElements" />.
        /// </summary>
        /// <param name="x">The top-left x-coordinate of the scissor.</param>
        /// <param name="y">The top-left y-coordinate of the scissor.</param>
        /// <param name="width">The width of the scissor.</param>
        /// <param name="height">The height of the scissor.</param>
        /// <param name="originIsTopLeft">
        ///     Indicates whether the origin is the top-left (<c>true</c>) or the bottom-left (<c>false</c>).
        /// </param>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void ApplyScissor(int x, int y, int width, int height, bool originIsTopLeft = false) =>
            PInvoke.sg_apply_scissor_rect(x, y, width, height, originIsTopLeft);

        /// <summary>
        ///     Queues a command to render graphic primitives.
        /// </summary>
        /// <param name="elementCount">The number of elements.</param>
        /// <param name="baseElement">The base element index.</param>
        /// <param name="instanceCount">The number of instances.</param>
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "C# API design.")]
        public void DrawElements(int elementCount, int baseElement = 0, int instanceCount = 1) =>
            PInvoke.sg_draw(baseElement, elementCount, instanceCount);

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
