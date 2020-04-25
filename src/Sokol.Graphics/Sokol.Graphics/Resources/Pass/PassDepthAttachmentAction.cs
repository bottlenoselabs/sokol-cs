// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeInternal

namespace Sokol.Graphics
{
    /// <summary>
    ///     The parameters that configure what happens with a depth render target <see cref="Image" /> at the start of
    ///     a rendering <see cref="Pass" />.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public struct PassDepthAttachmentAction
    {
        /// <summary>
        ///     The <see cref="PassAttachmentAction" />.
        /// </summary>
        [FieldOffset(0)]
        public PassAttachmentAction Action;

        /// <summary>
        ///     The depth to use when the depth render target <see cref="Image" /> is cleared.
        /// </summary>
        [FieldOffset(4)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public float Value;
    }
}
