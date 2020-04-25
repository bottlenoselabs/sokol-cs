// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.Graphics
{
    // TODO: Add documentation.
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public struct ResourceSlotInfo
    {
        [FieldOffset(0)]
        public ResourceState State;

        [FieldOffset(4)]
        public uint Resource;

        [FieldOffset(8)]
        public uint Context;
    }
}
