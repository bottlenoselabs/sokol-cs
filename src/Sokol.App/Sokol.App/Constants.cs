// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.App
{
    /// <summary>
    ///     Constants of `sokol_app`.
    /// </summary>
    [SuppressMessage("ReSharper", "SA1600", Justification = "Self explanatory.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public constants.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public static class Constants
    {
        public const int MaximumTouchPointsCount = 8;
        public const int MaximumMouseButtonsCount = 3;
        public const int MaximumKeyCodesCount = 512;
    }
}
