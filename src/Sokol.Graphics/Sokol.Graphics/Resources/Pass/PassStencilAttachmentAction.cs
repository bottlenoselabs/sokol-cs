// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that configure what happens with a stencil render target <see cref="Image" /> at the start of
    ///     a rendering <see cref="Pass" />.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public struct PassStencilAttachmentAction
    {
        /// <summary>
        ///     The <see cref="PassAttachmentAction" />.
        /// </summary>
        [FieldOffset(0)]
        public PassAttachmentAction Action;

        /// <summary>
        ///     The stencil value to use when the stencil render target <see cref="Image" /> is cleared.
        /// </summary>
        [FieldOffset(4)]
        public byte Value;
    }
}
