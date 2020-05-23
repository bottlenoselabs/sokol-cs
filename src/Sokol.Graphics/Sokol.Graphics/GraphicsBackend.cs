// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the possible 3D graphics API back-ends of `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In `sokol_gfx`, the `sg_backend` struct is highly coupled to the platform for which the 3D graphics API
    ///         is available. Since .NET is multi-platform, this makes it awkward to have an idiomatic C# wrapper.
    ///         Sokol# goes with idea that <see cref="GraphicsBackend"/> defines strictly the available APIs and does
    ///         not know about which platform the API is running on.
    ///     </para>
    /// </remarks>
    public enum GraphicsBackend
    {
        /// <summary>
        ///     The OpenGL Core 3.3 back-end. Used for Windows and Linux applications.
        /// </summary>
        OpenGL = 0,

        /// <summary>
        ///     The OpenGL for Embedded Systems 2.0 back-end. If targeting the web, this value is re-used for the
        ///     WebGL 1.0 back-end. Used mostly for Android and Web applications.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        OpenGLES2 = 1,

        /// <summary>
        ///     The OpenGL for Embedded Systems 3.0 back-end. If targeting the web, this value is re-used for the
        ///     WebGL 2.0 back-end. Used mostly for Android and Web applications.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        OpenGLES3 = 2,

        /// <summary>
        ///     The Direct3D 11 back-end. Used for Windows applications.
        /// </summary>
        Direct3D11 = 3,

        /// <summary>
        ///     The Metal back-end. Used for macOS, iOS, and tvOS applications.
        /// </summary>
        Metal = 4,

        /// <summary>
        ///     The WebGPU back-end. Used for Web applications.
        /// </summary>
        WebGPU = 5,

        /// <summary>
        ///     The dummy back-end. Used for testing and other applications which don't want a real implementation of a
        ///     back-end.
        /// </summary>
        Dummy = 6
    }
}
