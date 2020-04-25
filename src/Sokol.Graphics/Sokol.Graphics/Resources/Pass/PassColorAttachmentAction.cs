// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that configure what happens with a color attachment at the start of a rendering
    ///     <see cref="Pass" />.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 20, Pack = 4)]
    public struct PassColorAttachmentAction
    {
        /// <summary>
        ///     The <see cref="PassAttachmentAction" />.
        /// </summary>
        [FieldOffset(0)]
        public PassAttachmentAction Action;

        /// <summary>
        ///     The color to use when the color render target <see cref="Image" /> is cleared.
        /// </summary>
        [FieldOffset(4)]
        public Rgba32F Value;
    }
}
