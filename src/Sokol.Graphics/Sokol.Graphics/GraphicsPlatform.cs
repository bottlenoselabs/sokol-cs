// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the target platforms available with `Sokol.NET`.
    /// </summary>
    [Flags]
    public enum GraphicsPlatform
    {
        /// <summary>
        ///     Unknown target platform.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Desktop versions of Windows on 64-bit computing architecture (e.g. Windows 7, Windows 8, Windows 10, and up).
        /// </summary>
        Windows = 1 << 0,

        /// <summary>
        ///     Desktop versions of OSX on 64-bit computing architecture (e.g. macOS 10.0 "Cheetah" to macOS 10.15 "Catalina", and up).
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name")]
        macOS = 1 << 1,

        /// <summary>
        ///     Desktop distributions of Linux on 64-bit computing architecture (e.g. CentOS, Debian, Fedora, Ubuntu, etc)
        /// </summary>
        Linux = 1 << 2,

        // TODO: Tizen, Android, iOS, tvOS, RaspberryPi, WebAssembly, PlayStation4, PlayStationVita, Switch etc
    }
}
