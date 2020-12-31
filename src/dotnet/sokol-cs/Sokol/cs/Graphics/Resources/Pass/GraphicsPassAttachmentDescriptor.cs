// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that describe an output destination for pixels when rendering a <see cref="GraphicsPass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPassAttachmentDescriptor" /> is used to configure an individual render target
    ///         <see cref="GraphicsImage" /> that the <see cref="GraphicsPass" /> can write into.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsPassAttachmentDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsPassAttachmentDescriptor" /> is blittable to the C `sg_attachment_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    public struct GraphicsPassAttachmentDescriptor
    {
        /// <summary>
        ///     The <see cref="GraphicsImage" /> to render into. Must be set. Must be a render target. Additionally, if this
        ///     <see cref="GraphicsPassAttachmentDescriptor" /> is used for <see cref="GraphicsPassDescriptor.ColorAttachment" />,
        ///     the <see cref="GraphicsImage" /> must have the same dimensions, same sample count, and same
        ///     <see cref="GraphicsPixelFormat" /> as other color attachments.
        /// </summary>
        [FieldOffset(0)]
        public GraphicsImage Image;

        /// <summary>
        ///     The mipmap level of the <see cref="GraphicsImage" /> used for rendering into. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(4)]
        public int Level;

        /// <summary>
        ///     The slice of the <see cref="GraphicsImage" /> used for rendering into. Default is <c>0</c>.
        /// </summary>
        [FieldOffset(8)]
        public int Slice;
    }
}
