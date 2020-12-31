// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using lithiumtoast.NativeTools;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the possible 3D graphics API back-ends of `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In `sokol_gfx`, the `sg_backend` struct is highly coupled to the platform for which the 3D graphics API
    ///         is available. Since .NET is multi-platform, this makes it awkward for idiomatic C# bindings.
    ///         <see cref="GraphicsBackend" /> defines strictly the available APIs and does not know the platform exactly.
    ///         To check what the current platform is, use the <see cref="lithiumtoast.NativeTools.Native.RuntimePlatform" /> property.
    ///     </para>
    /// </remarks>
    public enum GraphicsBackend
    {
        /// <summary>
        ///     The OpenGL Core 3.3 back-end. Used for <see cref="NativeRuntimePlatform.Linux" /> applications.
        /// </summary>
        OpenGL = 0,

        /// <summary>
        ///     The OpenGL for Embedded Systems 2.0 back-end. If targeting <see cref="NativeRuntimePlatform.Web" />, this value is
        ///     re-used for the WebGL 1.0 back-end. Used for <see cref="NativeRuntimePlatform.Android" /> and
        ///     <see cref="NativeRuntimePlatform.Web" /> applications.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        OpenGLES2 = 1,

        /// <summary>
        ///     The OpenGL for Embedded Systems 3.0 back-end. If targeting <see cref="NativeRuntimePlatform.Web" />, this value is
        ///     re-used for the
        ///     WebGL 2.0 back-end. Used for <see cref="NativeRuntimePlatform.Android" /> and <see cref="NativeRuntimePlatform.Web" />
        ///     applications.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        OpenGLES3 = 2,

        /// <summary>
        ///     The Direct3D 11 back-end. Used for <see cref="NativeRuntimePlatform.Windows" /> applications.
        /// </summary>
        Direct3D11 = 3,

        /// <summary>
        ///     The Metal back-end. Used for <see cref="NativeRuntimePlatform.macOS" />, <see cref="NativeRuntimePlatform.iOS" />, and
        ///     <see cref="NativeRuntimePlatform.tvOS" /> applications.
        /// </summary>
        Metal = 4,

        /// <summary>
        ///     The WebGPU back-end. Used for <see cref="NativeRuntimePlatform.Web" /> applications.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        WebGPU = 5,

        /// <summary>
        ///     The dummy back-end. Used for testing and other applications which don't want or need a real implementation of a
        ///     graphics back-end.
        /// </summary>
        Dummy = 6
    }
}
