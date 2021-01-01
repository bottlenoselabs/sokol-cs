// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol
{
    /// <summary>
    ///     Defines the actions that can be performed at the start of a <see cref="GraphicsPass" /> for a color/depth/stencil
    ///     attachment of the screen or some render target <see cref="GraphicsImage" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPassAttachmentAction" /> is blittable to the C `sg_action` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum GraphicsPassAttachmentAction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default is
        ///     <see cref="Clear" /> with a color RGBA #808080FF, depth <c>1.0f</c>, or stencil <c>0.0f</c>.
        /// </summary>
        Default,

        /// <summary>
        ///     A user defined value is written to every pixel in the attachment at the start of the <see cref="GraphicsPass" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When using a <see cref="GraphicsPassAttachmentAction" /> as a loading action, if the
        ///         <see cref="GraphicsPass" /> does not need to know about the previous contents of the attachment and renders to
        ///         some of the pixels of the attachment, use the <see cref="Clear" /> action for correct results.
        ///     </para>
        /// </remarks>
        Clear,

        /// <summary>
        ///     The existing contents of the attachment are preserved at the start of the <see cref="GraphicsPass" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When using a <see cref="GraphicsPassAttachmentAction" /> as a loading action, if the
        ///         <see cref="GraphicsPass" /> does need to know about the previous contents of the attachment and renders to some
        ///         or all of the pixels for the attachment, use the <see cref="Load" /> action for correct results.
        ///     </para>
        /// </remarks>
        Load,

        /// <summary>
        ///     Each pixel in the attachment is allowed to take on any value at the start of the <see cref="GraphicsPass" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When using a <see cref="GraphicsPassAttachmentAction" /> as a loading action, if the
        ///         <see cref="GraphicsPass" /> does not need to know about the previous contents of the attachment and renders to
        ///         all the pixels of the attachment, use the <see cref="DontCare" /> action for best performance. This is because
        ///         in such a scenario the previous contents of the attachment before being rendered to are irrelevant and as such
        ///         do not need to be cleared or preserved.
        ///     </para>
        /// </remarks>
        [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "Identifier without proper english punctuation.")]
        DontCare
    }
}
