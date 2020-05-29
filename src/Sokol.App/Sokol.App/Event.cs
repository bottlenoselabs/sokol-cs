// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    public unsafe struct Event
    {
        [FieldOffset(0)]
        public ulong FrameCount;

        [FieldOffset(8)]
        public EventType Type;

        [FieldOffset(12)]
        public KeyCode KeyCode;

        [FieldOffset(16)]
        public uint CharCode;

        [FieldOffset(20)]
        public BlittableBoolean KeyRepeat;

        [FieldOffset(24)]
        public uint Modifiers;

        [FieldOffset(28)]
        public MouseButton MouseButton;

        [FieldOffset(32)]
        public Vector2 MousePosition;

        [FieldOffset(40)]
        public Vector2 ScrollPosition;

        [FieldOffset(48)]
        public int TouchesCount;

        [FieldOffset(56)]
        public fixed ulong _touches[192 / 8];

        [FieldOffset(248)]
        public int WindowWidth;

        [FieldOffset(252)]
        public int WindowHeight;

        [FieldOffset(256)]
        public int FramebufferWidth;

        [FieldOffset(260)]
        public int FramebufferHeight;

        public readonly ref TouchPoint Touch(int index = 0)
        {
            fixed (Event* @this = &this)
            {
                var pointer = (TouchPoint*)&@this->_touches[0];
                var pointerOffset = index;
                return ref *(pointer + pointerOffset);
            }
        }
    }
}
