// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     TODO.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsBufferInfo
    {
        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(0)]
        public readonly GraphicsResourceSlotInfo SlotInfo;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(12)]
        public readonly uint UpdateFrameIndex;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(16)]
        public readonly uint AppendFrameIndex;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(20)]
        public readonly int AppendPosition;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(24)]
        public readonly CBool IsAppendOverflowed;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(28)]
        public readonly int SlotCount;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(32)]
        public readonly int ActiveSlotIndex;
    }
}
