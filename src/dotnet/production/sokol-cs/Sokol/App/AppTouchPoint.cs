// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     A touch-point of a touch-screen.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppKeyCode" /> is blittable to the C `sapp_touchpoint` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public readonly struct AppTouchPoint
    {
        /// <summary>
        ///     The unique identifier of the <see cref="AppTouchPoint" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly ulong Identifier;

        /// <summary>
        ///     The <see cref="Vector2" /> position of the <see cref="AppTouchPoint" />.
        /// </summary>
        [FieldOffset(8)]
        public readonly Vector2 Position;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the <see cref="AppTouchPoint" /> has changed or otherwise is a
        ///     new instance.
        /// </summary>
        [FieldOffset(16)]
        public readonly CBool Changed;
    }
}
