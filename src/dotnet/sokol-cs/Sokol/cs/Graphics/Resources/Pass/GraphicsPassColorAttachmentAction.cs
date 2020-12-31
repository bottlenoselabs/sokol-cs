// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that configure what happens with a color attachment at the start of a rendering
    ///     <see cref="GraphicsPass" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsPassAction" /> is blittable to the C `sg_color_attachment_action` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 20, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public struct GraphicsPassColorAttachmentAction
    {
        /// <summary>
        ///     The <see cref="GraphicsPassAttachmentAction" />.
        /// </summary>
        [FieldOffset(0)]
        public GraphicsPassAttachmentAction Action;

        /// <summary>
        ///     The color to clear the color attachment. Ignored if <see cref="Action" /> is not
        ///     <see cref="GraphicsPassAttachmentAction.Clear" />.
        /// </summary>
        [FieldOffset(4)]
        public Rgba32F Value;
    }
}
