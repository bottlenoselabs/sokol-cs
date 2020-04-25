// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="Pass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="PassDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="PassDescriptor" /> is blittable to the C `sg_pass_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 80, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct PassDescriptor
    {
        /// <summary>
        ///     The depth stencil <see cref="PassAttachmentDescriptor" />.
        /// </summary>
        [FieldOffset(52)]
        public PassAttachmentDescriptor DepthStencilAttachment;

        [FieldOffset(4)]
        internal fixed int _color_attachments[12 * sokol_gfx.SG_MAX_COLOR_ATTACHMENTS / 4];

        // TODO: Trace hooks
        [FieldOffset(64)]
        internal byte* Label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        internal uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(72)]
        internal uint _endCanary;

        /// <summary>
        ///     Gets the color <see cref="PassAttachmentDescriptor" />, by reference, given the specified slot or index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="PassAttachmentDescriptor"/> by reference.</returns>
        public ref PassAttachmentDescriptor ColorAttachment(int index)
        {
            fixed (PassDescriptor* passDescription = &this)
            {
                var ptr = (PassAttachmentDescriptor*)&passDescription->_color_attachments[0];
                return ref *(ptr + index);
            }
        }
    }
}
