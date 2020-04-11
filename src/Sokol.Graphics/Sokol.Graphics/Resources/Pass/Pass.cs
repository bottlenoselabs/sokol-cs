// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
    public struct Pass
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="Pass" />.
        /// </summary>
        [FieldOffset(0)]
        public uint Identifier;

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
        ///     Begins the <see cref="Pass" />.
        /// </summary>
        /// <param name="passAction">The pass action.</param>
        public void Begin([In] ref PassAction passAction)
        {
            PInvoke.sg_begin_pass(this, ref passAction);
        }

        /// <summary>
        ///     Ends the <see cref="Pass" />.
        /// </summary>
        public void End()
        {
            PInvoke.sg_end_pass();
        }

        /// <summary>
        ///     Sets the specified <see cref="Pipeline" /> as the active render state for the next draw calls.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Apply(Pipeline pipeline)
        {
            PInvoke.sg_apply_pipeline(pipeline);
        }

        /// <summary>
        ///     Sets the specified <see cref="ResourceBindings" /> as the active resources for the next draw
        ///     calls.
        /// </summary>
        /// <param name="bindings">The resource bindings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Apply([In] ref ResourceBindings bindings)
        {
            PInvoke.sg_apply_bindings(ref bindings);
        }

        /// <summary>
        ///     Queues a command to render geometry which will be scheduled by the active <see cref="Pipeline" />.
        /// </summary>
        /// <param name="baseElement">The base element index.</param>
        /// <param name="elementCount">The number of elements.</param>
        /// <param name="instanceCount">The number of instances.</param>
        public void Draw(int baseElement, int elementCount, int instanceCount = 1)
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
