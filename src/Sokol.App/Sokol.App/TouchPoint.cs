// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public struct TouchPoint
    {
        [FieldOffset(0)] /* size = 8, padding = 0 */
        public IntPtr Identifier;

        [FieldOffset(8)] /* size = 8, padding = 0 */
        public Vector2 Position;

        [FieldOffset(16)] /* size = 1, padding = 7 */
        public BlittableBoolean Changed;
    }
}
