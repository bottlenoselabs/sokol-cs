// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace Sokol.Graphics
{
    /// <summary>
    ///     The static methods of `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For practicality, it's recommended to import the module with all the bindings like so:
    ///         <c>using static Sokol.Graphics.GraphicsDevice;</c>.
    ///     </para>
    /// </remarks>
    public static class GraphicsDevice
    {
        /// <summary>
        ///     Gets the <see cref="InitializeDescriptor" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="InitializeDescriptor" /> of the `sokol_gfx` application.</value>
        public static InitializeDescriptor Descriptor => QueryDescriptor();

        /// <summary>
        ///     Gets the <see cref="GraphicsBackend" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsBackend" /> of the `sokol_gfx` application.</value>
        public static GraphicsBackend Backend
        {
            get
            {
                var value = QueryBackend();
                return value switch
                {
                    sokol_gfx.sg_backend.SG_BACKEND_GLCORE33 => GraphicsBackend.OpenGL,
                    sokol_gfx.sg_backend.SG_BACKEND_GLES2 => GraphicsBackend.OpenGLES2,
                    sokol_gfx.sg_backend.SG_BACKEND_GLES3 => GraphicsBackend.OpenGLES3,
                    sokol_gfx.sg_backend.SG_BACKEND_D3D11 => GraphicsBackend.Direct3D11,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_IOS => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_MACOS => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_SIMULATOR => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_DUMMY => GraphicsBackend.Dummy,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsFeatures" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsFeatures" /> of the `sokol_gfx` application.</value>
        public static GraphicsFeatures Features => QueryFeatures();

        /// <summary>
        ///     Gets the <see cref="GraphicsLimits" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsLimits" /> of the `sokol_gfx` application.</value>
        public static GraphicsLimits Limits => QueryLimits();

        /// <summary>
        ///     Initializes `sokol_gfx` for the life-time of an application. Must be called after a window is created
        ///     and the graphics back-end device or context is created.
        /// </summary>
        /// <param name="desc">The configuration to use for initialize.</param>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_setup")]
        public static extern void Setup([In] ref InitializeDescriptor desc);

        /// <summary>
        ///     Destroys `sokol_gfx` for the life-time of an application. Should be called before an application exits
        ///     which the application previously called <see cref="Setup" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you need to destroy a resource before calling <see cref="Shutdown" />, call one the following:
        ///         <list type="bullet">
        ///             <item>
        ///                 <description>
        ///                     <see cref="Buffer.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="Image.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="Shader.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="Pipeline.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="Pass.Destroy" />
        ///                 </description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_shutdown")]
        public static extern void Shutdown();

        /// <summary>
        ///     Gets a <see cref="bool" /> indicating whether the current state of `sokol_gfx` is initialized or not.
        /// </summary>
        /// <returns>
        ///     <c>true</c> when <see cref="Setup" /> was called successfully and <see cref="Shutdown" /> has not yet been
        ///     called; otherwise <c>false</c>.
        /// </returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_isvalid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValid();

        // TODO: Document calling into underlying 3D API directly.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_reset_state_cache")]
        public static extern void ResetStateCache();

        /// <summary>
        ///     Execute all scheduled rendering operations of the current frame of the `sokol_gfx` application. Marks
        ///     the end of the current frame.
        /// </summary>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_commit")]
        public static extern void Commit();

        /// <summary>
        ///     Gets the <see cref="PixelFormatInfo" /> of a specified <see cref="PixelFormat" />.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>A <see cref="PixelFormatInfo" />.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_pixelformat")]
        public static extern PixelFormatInfo QueryPixelFormat(PixelFormat format);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_desc")]
        private static extern InitializeDescriptor QueryDescriptor();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_backend")]
        private static extern sokol_gfx.sg_backend QueryBackend();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_features")]
        private static extern GraphicsFeatures QueryFeatures();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_limits")]
        private static extern GraphicsLimits QueryLimits();
    }
}
