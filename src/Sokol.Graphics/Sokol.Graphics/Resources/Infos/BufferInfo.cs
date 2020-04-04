// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    // TODO: Add documentation
    [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public struct BufferInfo
    {
        [FieldOffset(0)]
        public ResourceSlotInfo SlotInfo;

        [FieldOffset(12)]
        public uint UpdateFrameIndex;

        [FieldOffset(16)]
        public uint AppendFrameIndex;

        [FieldOffset(20)]
        public int AppendPosition;

        [FieldOffset(24)]
        public BlittableBoolean IsAppendOverflowed;

        [FieldOffset(28)]
        public int SlotCount;

        [FieldOffset(32)]
        public int ActiveSlotIndex;
    }
}
