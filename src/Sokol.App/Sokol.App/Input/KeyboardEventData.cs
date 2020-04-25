// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.App
{
    public readonly struct KeyboardEventData
    {
        public readonly KeyboardKey KeyboardKey;
        public readonly bool IsDown;
        public readonly KeyboardModifierKeys Modifiers;

        internal KeyboardEventData(KeyboardKey keyboardKey, bool downState, KeyboardModifierKeys modifiers)
        {
            KeyboardKey = keyboardKey;
            IsDown = downState;
            Modifiers = modifiers;
        }

        public override string ToString()
        {
            return $"{KeyboardKey} {(IsDown ? "Down" : "Up")} [{Modifiers}]";
        }
    }
}
