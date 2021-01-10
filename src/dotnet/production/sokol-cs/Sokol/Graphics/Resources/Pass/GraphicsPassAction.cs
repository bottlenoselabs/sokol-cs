// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     The action that happens at the start of a rendering <see cref="GraphicsPass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPassAction" /> is blittable to the C `sg_pass_action` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsPassAction
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     The <see cref="GraphicsPassDepthAttachmentAction "/> to use.
        /// </summary>
        [FieldOffset(84)]
        public GraphicsPassDepthAttachmentAction Depth;

        /// <summary>
        ///     The <see cref="GraphicsPassStencilAttachmentAction" /> to use.
        /// </summary>
        [FieldOffset(92)]
        public GraphicsPassStencilAttachmentAction Stencil;

        [FieldOffset(4)]
        private readonly int _colors;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(100)]
        private readonly uint _endCanary;

        /// <summary>
        ///     Gets the clear color <see cref="GraphicsPassAction" /> given an optional clear color.
        /// </summary>
        /// <param name="color">The optional clear color.</param>
        /// <returns>A <see cref="GraphicsPassAction" />.</returns>
        public static GraphicsPassAction Clear(Rgba32F? color = null)
        {
            var passAction = default(GraphicsPassAction);
            ref var color0 = ref passAction.Color(0);
            color0.Action = GraphicsPassAttachmentAction.Clear;
            color0.Value = color ?? Rgba32F.Gray;
            return passAction;
        }

        /// <summary>
        ///     Gets the don't care <see cref="GraphicsPassAction " />.
        /// </summary>
        /// <value>
        ///     The don't care <see cref="GraphicsPassAction" />.
        /// </value>
        public static GraphicsPassAction DontCare
        {
            get
            {
                var passAction = default(GraphicsPassAction);
                for (var i = 0; i < GraphicsConstants.MaximumColorAttachments; i++)
                {
                    passAction.Color(i).Action = GraphicsPassAttachmentAction.DontCare;
                }

                passAction.Depth.Action = GraphicsPassAttachmentAction.DontCare;
                passAction.Stencil.Action = GraphicsPassAttachmentAction.DontCare;
                return passAction;
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsPassColorAttachmentAction" />, by reference, to use given a specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsPassColorAttachmentAction" /> by reference.</returns>
        public ref GraphicsPassColorAttachmentAction Color(int index)
        {
            fixed (GraphicsPassAction* passAction = &this)
            {
                var ptr = (GraphicsPassColorAttachmentAction*)&passAction->_colors;
                return ref *(ptr + index);
            }
        }
    }
}
