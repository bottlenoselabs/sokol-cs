// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global

namespace Sokol.App
{
    public struct ButtonState
    {
        // Brain dump:
        //    - a button can physically be in either of two states: "is down?" or "is up?"
        //    - however, we want to be able to ask the following questions for a button such as:
        //        1. "is down now but previously was up?" (the button enters a pressed state)
        //        2. "is down now and was also previously down?" (the button is pressed)
        //        3. "is up now but was previously down?" (the button enters a released state)
        //        4. "is up now and was also previously up?" (the button is released)
        //    - these questions can be derived from 2 bool variables (do the truth table exercise if you are not sure)
        //        1. `isDown` (`isUp` is the opposite)
        //        2. `wasDown' (`wasUp` is the opposite)
        //    - these states could be expressed as a `bool` but:
        //        1. a bool can take 1-8 bytes in C# (see https://stackoverflow.com/questions/28514373/what-is-the-size-of-a-boolean-in-c-does-it-really-take-4-bytes)
        //        2. we will need a bunch of branching to calculate the states (if conditions)
        //    - to deal with problem 1 we can use a "bit vector" to store the booleans as bits packed in a single byte
        //    - this nicely leads to a solution for problem 2 as we can shift bits to calculate the previous frame
        //    - example:
        //        - '01' is the bit vector for "is down now but was previously up"
        //        - shift the bits left by 1, `10` is the bit vector for "is up now but was previously down"
        //        - set the least sig bit with the current state, `11` is "is down now and was previously down"

        private TimeSpan _downDuration;
        private byte _state;

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public TimeSpan DownDuration
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _downDuration;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _downDuration = value;
        }

        public readonly bool IsDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x1) == 0x1;
        }

        public readonly bool IsUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x1) == 0x0;
        }

        public readonly bool WasDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x2) == 0x1;
        }

        public readonly bool WasUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x2) == 0x0;
        }

        public readonly bool IsPressed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x3) == 0x3;
        }

        public readonly bool IsReleased
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x3) == 0x0;
        }

        public readonly bool HasEnteredPressed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x3) == 0x1;
        }

        public readonly bool HasEnteredReleased
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_state & 0x3) == 0x2;
        }

        internal static void Update(ref ButtonState button, bool isDown, TimeSpan elapsedTime)
        {
            button._state = (byte)(((0x1 & button._state) << 1) | Convert.ToByte(isDown));
            if (isDown)
            {
                button._downDuration += elapsedTime;
            }
            else
            {
                button._downDuration = TimeSpan.Zero;
            }
        }
    }
}
