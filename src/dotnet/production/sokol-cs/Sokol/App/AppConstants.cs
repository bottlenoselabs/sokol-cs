// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Constants of `sokol_app`. Changes to these constants are only observed from C#; changes to these constants
    ///     here are not observed in the C library.
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        ///     The maximum number of touch points.
        /// </summary>
        public const int MaximumTouchPointsCount = 8;

        /// <summary>
        ///     The maximum number of mouse buttons.
        /// </summary>
        public const int MaximumMouseButtonsCount = 3;

        /// <summary>
        ///     The maximum number of key codes.
        /// </summary>
        public const int MaximumKeyCodesCount = 512;
    }
}
