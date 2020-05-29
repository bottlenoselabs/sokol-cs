// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.App
{
    public enum EventType
    {
        Invalid,
        KeyDown,
        KeyUp,
        Char,
        MouseDown,
        MouseUp,
        MouseScroll,
        MouseMove,
        MouseEnter,
        MouseLeave,
        TouchesBegan,
        TouchesMoved,
        TouchesEnded,
        TouchesCancelled,
        Resized,
        // ReSharper disable once IdentifierTypo
        Iconified,
        Restored,
        Suspended,
        Resumed,
        UpdateCursor,
        QuitRequested,
        ClipboardPasted
    }
}
