// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace Sokol.App
{
    public struct ButtonState
    {
        private const int PressedStateIsDownMask = 0x1;
        private const int PressedStateIsDownValue = 0x1;
        private const int PressedStateIsUpMask = 0x1;
        private const int PressedStateIsUpValue = 0x0;
        private const int PressedStateIsPressedMask = 0x3;
        private const int PressedStateIsPressedValue = 0x1;
        private const int PressedStateWasDownMask = 0x2;
        private const int PressedStateWasDownValue = 0x1;
        private const int PressedStateWasUpMask = 0x2;
        private const int PressedStateWasUpValue = 0x0;

        private TimeSpan _downDuration;
        private byte _pressedState;

        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public TimeSpan DownDuration
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _downDuration;
            set => _downDuration = value;
        }

        public bool IsDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_pressedState & PressedStateIsDownMask) == PressedStateIsDownValue;
        }

        public bool IsUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_pressedState & PressedStateIsUpMask) == PressedStateIsUpValue;
        }

        public bool IsPressed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_pressedState & PressedStateIsPressedMask) == PressedStateIsPressedValue;
        }

        public bool WasDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_pressedState & PressedStateWasDownMask) == PressedStateWasDownValue;
        }

        public bool WasUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_pressedState & PressedStateWasUpMask) == PressedStateWasUpValue;
        }

        internal static void Update(ref ButtonState buttonState, bool isDown, TimeSpan elapsedTime)
        {
            var intIsDown = Convert.ToInt32(isDown);
            var newPressedState = buttonState._pressedState << 1 | intIsDown;
            buttonState._pressedState = (byte)newPressedState;
            if (isDown)
            {
                buttonState.DownDuration += elapsedTime;
            }
            else
            {
                buttonState.DownDuration = TimeSpan.Zero;
            }
        }
    }
}
