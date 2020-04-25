// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the actions that can be performed at the start of a <see cref="Pass" /> for an attachment.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         If the application renders all the pixels of the render target for a given frame, use the
    ///         <see cref="DontCare" /> action, which allows the GPU to avoid loading the existing contents of the
    ///         render target <see cref="Image" />. Otherwise, use the <see cref="Clear" /> action to clear the previous
    ///         contents of the render target <see cref="Image" />, or the <see cref="Load" /> action to preserve them.
    ///         The <see cref="Clear" /> action also avoids the cost of loading the existing render target
    ///         <see cref="Image" /> contents, but it still incurs the cost of filling the destination with a clear
    ///         color.
    ///     </para>
    ///     <para>
    ///         <see cref="PassAttachmentAction" /> is blittable to the C `sg_action` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PassAttachmentAction
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures. The default is
        ///     <see cref="Clear" /> with a color RGBA #808080FF, depth <c>1.0f</c>, or stencil <c>0.0f</c>.
        /// </summary>
        Default,

        /// <summary>
        ///     A value is written to every pixel in the specified attachment at the start of the rendering
        ///     <see cref="Pass" />.
        /// </summary>
        Clear,

        /// <summary>
        ///     The existing contents of the attachment are preserved at the start of the rendering <see cref="Pass" />.
        /// </summary>
        Load,

        /// <summary>
        ///     Each pixel in the attachment is allowed to take on any value at the start of the rendering
        ///     <see cref="Pass" />.
        /// </summary>
        // ReSharper disable once IdentifierTypo
        DontCare
    }
}
