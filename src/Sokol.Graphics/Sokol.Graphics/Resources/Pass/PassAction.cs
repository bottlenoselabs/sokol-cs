// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The action that happens at the start of a rendering <see cref="Pass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="PassAction" /> is blittable to the C `sg_pass_action` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 4)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public unsafe struct PassAction
    {
        /// <summary>
        ///     Gets the clear color <see cref="PassAction" /> given an optional clear color.
        /// </summary>
        /// <param name="color">The optional clear color.</param>
        /// <returns>A <see cref="PassAction" />.</returns>
        public static PassAction Clear(Rgba32F? color = null)
        {
            var passAction = default(PassAction);
            ref var color0 = ref passAction.Color();
            color0.Action = PassAttachmentAction.Clear;
            color0.Value = color ?? Rgba32F.Gray;
            return passAction;
        }

        /// <summary>
        ///     Gets the don't care <see cref="PassAction " />.
        /// </summary>
        /// <value>
        ///     The don't care <see cref="PassAction" />.
        /// </value>
        // ReSharper disable once IdentifierTypo
        public static PassAction DontCare
        {
            get
            {
                var passAction = default(PassAction);
                for (var i = 0; i < Constants.MaximumColorAttachments; i++)
                {
                    passAction.Color(i).Action = PassAttachmentAction.DontCare;
                }

                passAction.Depth.Action = PassAttachmentAction.DontCare;
                passAction.Stencil.Action = PassAttachmentAction.DontCare;
                return passAction;
            }
        }

        /// <summary>
        ///     The <see cref="PassDepthAttachmentAction "/> to use.
        /// </summary>
        [FieldOffset(84)]
        // ReSharper disable once MemberCanBePrivate.Global
        public PassDepthAttachmentAction Depth;

        /// <summary>
        ///     The <see cref="PassStencilAttachmentAction" /> to use.
        /// </summary>
        [FieldOffset(92)]
        // ReSharper disable once MemberCanBePrivate.Global
        public PassStencilAttachmentAction Stencil;

        [FieldOffset(4)]
        private fixed int _colors[20 * Constants.MaximumColorAttachments / 4];

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(100)]
        private readonly uint _endCanary;

        /// <summary>
        ///     Gets the <see cref="PassColorAttachmentAction" />, by reference, to use given a specified slot or index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="PassColorAttachmentAction" /> by reference.</returns>
        public ref PassColorAttachmentAction Color(int index = 0)
        {
            fixed (PassAction* passAction = &this)
            {
                var ptr = (PassColorAttachmentAction*)&passAction->_colors[0];
                return ref *(ptr + index);
            }
        }
    }
}
