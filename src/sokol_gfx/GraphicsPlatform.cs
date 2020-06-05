// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     Defines the target platforms available with `sokol_gfx`.
/// </summary>
/// <remarks>
///     <para>
///         Chip support for platforms is based on .NET Core 3.1 and is as follows:
///         <list type="bullet">
///             aa
///             <item>
///                 <description>x64 on Windows, macOS, and Linux</description>
///             </item>
///             <item>
///                 <description>ARM64 on Linux (kernel 4.14+)</description>
///             </item>
///         </list>
///         Chip support for x86 and ARM32 is not supported.
///     </para>
/// </remarks>
[Flags]
[SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
public enum GraphicsPlatform
{
    /// <summary>
    ///     Unknown target platform.
    /// </summary>
    Unknown = 0,

    /// <summary>
    ///     Desktop versions of Windows on 64-bit computing architecture. Includes Windows 7, Windows 8.1, Windows 10,
    ///     and up.
    /// </summary>
    Windows = 1 << 0,

    /// <summary>
    ///     Desktop versions of OSX on 64-bit computing architecture. Includes macOS 10.3 and up.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
    macOS = 1 << 1,

    /// <summary>
    ///     Desktop distributions of Linux on 64-bit computing architecture. Includes, but is not limited to, CentOS,
    ///     Debian, Fedora, and Ubuntu.
    /// </summary>
    Linux = 1 << 2,

    /// <summary>
    ///     Mobile versions of Android on 64-bit computing architecture. Includes Android 5.x and up.
    /// </summary>
    Android = 1 << 3,

    /// <summary>
    ///     Mobile versions of iOS on 64-bit computing architecture. Includes iOS 11.x and up.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
    iOS = 1 << 4,

    // TODO: tvOS, RaspberryPi, WebAssembly, PlayStation4, PlayStationVita, Switch etc
}
