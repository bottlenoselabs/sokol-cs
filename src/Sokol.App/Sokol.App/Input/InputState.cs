// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Sokol.App
{
    public sealed class InputState
    {
        private const int KeyButtonsCount = 256;
        private const int MouseButtonsCount = 13;

        private readonly ButtonState[] _buttonStates = new ButtonState[KeyButtonsCount + MouseButtonsCount];

        public KeyboardModifierKeys ModifiersKeys { get; internal set; }

        internal InputState()
        {
        }

        public ButtonState KeyButton(KeyboardKey key)
        {
            return _buttonStates[(int)key];
        }

        public ButtonState MouseButton(MouseButton mouseButton)
        {
            return _buttonStates[256 + (int)mouseButton];
        }

        internal void UpdateKeyButton(KeyboardKey key, bool isDown, in TimeSpan elapsedTime)
        {
            ButtonState.Update(ref _buttonStates[(int)key], isDown, elapsedTime);
        }
    }
}
