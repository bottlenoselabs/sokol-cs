// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     TODO.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsImageInfo
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
        public readonly int SlotCount;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(20)]
        public readonly int ActiveSlotIndex;
    }
}
