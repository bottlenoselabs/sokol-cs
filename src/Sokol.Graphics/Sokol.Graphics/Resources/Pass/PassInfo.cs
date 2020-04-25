// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.Graphics
{
    // TODO: Add documentation
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct PassInfo
    {
        [FieldOffset(0)]
        public ResourceSlotInfo SlotInfo;
    }
}
