// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing a <see cref="GraphicsPass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsPassDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPassDescriptor" /> is blittable to the C `sg_pass_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 80, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    public unsafe struct GraphicsPassDescriptor
    {
        /// <summary>
        ///     The depth stencil <see cref="GraphicsPassAttachmentDescriptor" />.
        /// </summary>
        [FieldOffset(52)]
        public GraphicsPassAttachmentDescriptor DepthStencilAttachment;

        [FieldOffset(4)]
        private fixed int _color_attachments[12 * GraphicsConstants.MaximumColorAttachments / 4];

        // TODO: Trace hooks
        [FieldOffset(64)]
        private readonly byte* _label;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(72)]
        private readonly uint _endCanary;

        /// <summary>
        ///     Gets the color <see cref="GraphicsPassAttachmentDescriptor" />, by reference, given the specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsPassAttachmentDescriptor"/> by reference.</returns>
        public readonly ref GraphicsPassAttachmentDescriptor ColorAttachment(int index = 0)
        {
            fixed (GraphicsPassDescriptor* passDescription = &this)
            {
                var ptr = (GraphicsPassAttachmentDescriptor*)&passDescription->_color_attachments[0];
                return ref *(ptr + index);
            }
        }
    }
}
