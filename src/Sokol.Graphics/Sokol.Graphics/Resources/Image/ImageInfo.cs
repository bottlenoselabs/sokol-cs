// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    // TODO: Add documentation
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public struct ImageInfo
    {
        [FieldOffset(0)]
        public ResourceSlotInfo SlotInfo;

        [FieldOffset(12)]
        public uint UpdateFrameIndex;

        [FieldOffset(16)]
        public int SlotCount;

        [FieldOffset(20)]
        public int ActiveSlotIndex;
    }
}
