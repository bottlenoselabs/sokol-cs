// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Sokol.App
{
    public sealed class InputState
    {
        private const int KeyButtonsCount = 256;
        private const int MouseButtonsCount = 13;

        private readonly ButtonState[] _buttonStates = new ButtonState[KeyButtonsCount + MouseButtonsCount];
        private readonly Dictionary<KeyboardKey, bool> _activeKeys = new Dictionary<KeyboardKey, bool>();
        private readonly Dictionary<KeyboardKey, bool> _previousActiveKeys = new Dictionary<KeyboardKey, bool>();

        public KeyboardModifierKeys ModifiersKeys { get; internal set; }

        public Vector2 MousePosition { get; private set; }

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

        internal void HandleKeyboardEvent(KeyboardKey key, bool isDown, KeyboardModifierKeys modifiers)
        {
            ModifiersKeys = modifiers;
            _activeKeys[key] = isDown;
        }

        internal void HandleMouseMotion(int x, int y)
        {
            MousePosition = new Vector2(x, y);
        }

        internal void Update(in TimeSpan elapsedTime)
        {
            foreach (var (key, isDown) in _previousActiveKeys)
            {
                ref var keyButtonState = ref _buttonStates[(int)key];
                ButtonState.Update(ref keyButtonState, isDown, elapsedTime);
            }

            _previousActiveKeys.Clear();

            foreach (var (key, isDown) in _activeKeys)
            {
                ref var keyButtonState = ref _buttonStates[(int)key];
                ButtonState.Update(ref keyButtonState, isDown, elapsedTime);
                _previousActiveKeys.Add(key, isDown);
            }

            _activeKeys.Clear();
        }
    }
}
