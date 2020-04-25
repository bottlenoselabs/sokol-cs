// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds 1-4 color attachments and 0-1 depth-stencil attachments that a
    ///     <see cref="Pipeline" /> renders to.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To create a <see cref="Pass" /> synchronously, call
    ///         <see cref="GraphicsDevice.CreatePass(ref PassDescriptor)" /> with a specified
    ///         <see cref="PassDescriptor" />. To create a <see cref="Pass" /> asynchronously, call
    ///         <see cref="GraphicsDevice.AllocPass" /> to get an un-initialized <see cref="Pass" /> and then call
    ///         <see cref="Initialize" /> with a specified <see cref="PassDescriptor" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="Pass" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Pass" /> is blittable to the C `sg_pass` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Pass
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="Pass" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document `PassInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public PassInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => PInvoke.sg_query_pass_info(this);
        }

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => PInvoke.sg_query_pass_state(this);
        }

        /// <summary>
        ///     Fill any zero-initialized members of an <see cref="PassDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pass.</param>
        /// <returns>An <see cref="PassDescriptor" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PassDescriptor QueryDefaults([In] ref PassDescriptor descriptor)
        {
            return PInvoke.sg_query_pass_defaults(ref descriptor);
        }

        // TODO: Document manual initialization of a pass
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize([In] ref PassDescriptor descriptor)
        {
            PInvoke.sg_init_pass(this, ref descriptor);
        }

        /// <summary>
        ///     Destroys the <see cref="Pass" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            PInvoke.sg_destroy_pass(this);
        }

        // TODO: Document failing an image.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            PInvoke.sg_fail_pass(this);
        }

        /// <summary>
        ///     Begins the <see cref="Pass" /> with <see cref="PassAction.DontCare" /> as the action.
        /// </summary>
        public void Begin()
        {
            var passAction = PassAction.DontCare;
            Begin(ref passAction);
        }

        /// <summary>
        ///     Begins the <see cref="Pass" /> with <see cref="PassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        public void Begin(Rgba32F clearColor)
        {
            var passAction = PassAction.Clear(clearColor);
            Begin(ref passAction);
        }

        /// <summary>
        ///     Begins the <see cref="Pass" /> with a specified <see cref="PassAction" />.
        /// </summary>
        /// <param name="passAction">The pass action.</param>
        public void Begin([In] ref PassAction passAction)
        {
            PInvoke.sg_begin_pass(this, ref passAction);
        }

        /// <summary>
        ///     Ends the <see cref="Pass" />.
        /// </summary>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void End()
        {
            PInvoke.sg_end_pass();
        }

        /// <summary>
        ///     Sets the specified <see cref="Pipeline" /> as the active render state for the next draw calls.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void ApplyPipeline(Pipeline pipeline)
        {
            PInvoke.sg_apply_pipeline(pipeline);
        }

        /// <summary>
        ///     Sets the specified <see cref="ResourceBindings" /> as the active resources for the next draw
        ///     calls.
        /// </summary>
        /// <param name="bindings">The resource bindings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void ApplyBindings([In] ref ResourceBindings bindings)
        {
            PInvoke.sg_apply_bindings(ref bindings);
        }

        /// <summary>
        ///     Updates the uniforms for a <see cref="Shader" /> given the specified <see cref="ShaderStageType" />,
        ///     the block the uniform belongs to, and the data itself. Must be called after
        ///     <see cref="ApplyPipeline" />.
        /// </summary>
        /// <param name="stage">The shader stage.</param>
        /// <param name="value">The data.</param>
        /// <param name="uniformBlockIndex">The uniform block zero-based index which the uniform belongs to.</param>
        /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public unsafe void ApplyShaderUniforms<T>(ShaderStageType stage, ref T value, int uniformBlockIndex = 0)
            where T : unmanaged
        {
            var dataPointer = (IntPtr)Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            PInvoke.sg_apply_uniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        /// <summary>
        ///     Updates the viewport for the <see cref="Pass" />.
        /// </summary>
        /// <param name="x">The top-left x-coordinate of the viewport.</param>
        /// <param name="y">The top-left y-coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="originIsTopLeft">
        ///     Indicates whether the origin is the top-left (<c>true</c>) or the bottom-left (<c>false</c>).
        /// </param>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void ApplyViewport(int x, int y, int width, int height, bool originIsTopLeft = false)
        {
            PInvoke.sg_apply_viewport(x, y, width, height, originIsTopLeft);
        }

        /// <summary>
        ///     Updates the scissor rectangle for the <see cref="Pass" />.
        /// </summary>
        /// <param name="x">The top-left x-coordinate of the scissor.</param>
        /// <param name="y">The top-left y-coordinate of the scissor.</param>
        /// <param name="width">The width of the scissor.</param>
        /// <param name="height">The height of the scissor.</param>
        /// <param name="originIsTopLeft">
        ///     Indicates whether the origin is the top-left (<c>true</c>) or the bottom-left (<c>false</c>).
        /// </param>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void ApplyScissor(int x, int y, int width, int height, bool originIsTopLeft = false)
        {
            PInvoke.sg_apply_scissor_rect(x, y, width, height, originIsTopLeft);
        }

        /// <summary>
        ///     Queues a command to render primitive elements to the target of <see cref="Pass" />.
        /// </summary>
        /// <param name="elementCount">The number of elements.</param>
        /// <param name="baseElement">The base element index.</param>
        /// <param name="instanceCount">The number of instances.</param>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void DrawElements(int elementCount, int baseElement = 0, int instanceCount = 1)
        {
            PInvoke.sg_draw(baseElement, elementCount, instanceCount);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
