// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedType.Global
// ReSharper disable NotAccessedField.Global

namespace Sokol.App
{
    public readonly struct MouseEvent
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly MouseButton MouseButton;

        // ReSharper disable once MemberCanBePrivate.Global
        public readonly bool Down;

        public MouseEvent(MouseButton button, bool down)
        {
            MouseButton = button;
            Down = down;
        }
    }
}
