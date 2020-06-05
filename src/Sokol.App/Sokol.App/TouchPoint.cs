// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    /// <summary>
    ///     A touch-point of a touch-screen device.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public readonly struct TouchPoint
    {
        /// <summary>
        ///     The unique identifier of the <see cref="TouchPoint" />.
        /// </summary>
        [FieldOffset(0)] /* size = 8, padding = 0 */
        public readonly IntPtr Identifier;

        /// <summary>
        ///     The <see cref="Vector2" /> position of the <see cref="TouchPoint" />.
        /// </summary>
        [FieldOffset(8)] /* size = 8, padding = 0 */
        public readonly Vector2 Position;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the <see cref="TouchPoint" /> has changed state or is
        ///     a new instance.
        /// </summary>
        [FieldOffset(16)] /* size = 1, padding = 7 */
        public readonly BlittableBoolean Changed;
    }
}
