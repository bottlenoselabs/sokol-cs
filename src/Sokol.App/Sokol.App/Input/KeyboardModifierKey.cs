// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Sokol.App
{
    [Flags]
    public enum KeyboardModifierKeys
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
    }
}
