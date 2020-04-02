// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Pointer to and the size of the data for an <see cref="Image" /> layer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public struct SubImageContent
    {
        /// <summary>
        ///     The memory address to the starting address of the block of bytes as data.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Pointer;

        /// <summary>
        ///     The size of the block of bytes as data.
        /// </summary>
        [FieldOffset(8)]
        public int Size;
    }
}
