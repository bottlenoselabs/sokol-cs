// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.App
{
    public struct MouseEvent
    {
        public readonly MouseButton MouseButton;
        public readonly bool Down;

        public MouseEvent(MouseButton button, bool down)
        {
            MouseButton = button;
            Down = down;
        }
    }
}
