// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
        ///     Gets the <see cref="GraphicsDescriptor" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsDescriptor" /> of the `sokol_gfx` application.</value>
        public static GraphicsDescriptor Descriptor => QueryDescriptor();

        /// <summary>
        ///     Gets the <see cref="GraphicsBackend" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsBackend" /> of the `sokol_gfx` application.</value>
        public static GraphicsBackend Backend
        {
            get
            {
                var value = PInvoke.sg_query_backend();
                return value switch
                {
                    sg_backend.SG_BACKEND_GLCORE33 => GraphicsBackend.OpenGL,
                    sg_backend.SG_BACKEND_GLES2 => GraphicsBackend.OpenGLES2,
                    sg_backend.SG_BACKEND_GLES3 => GraphicsBackend.OpenGLES3,
                    sg_backend.SG_BACKEND_D3D11 => GraphicsBackend.Direct3D11,
                    sg_backend.SG_BACKEND_METAL_IOS => GraphicsBackend.Metal,
                    sg_backend.SG_BACKEND_METAL_MACOS => GraphicsBackend.Metal,
                    sg_backend.SG_BACKEND_METAL_SIMULATOR => GraphicsBackend.Metal,
                    sg_backend.SG_BACKEND_WGPU => GraphicsBackend.WebGPU,
                    sg_backend.SG_BACKEND_DUMMY => GraphicsBackend.Dummy,
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
        ///     Frees any memory allocated by `sokol.NET` for strings used in descriptors. Only call this method
        ///     after resources are created.
        /// </summary>
        public static void FreeStrings()
        {
            UnmanagedStringMemoryManager.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        ///     Initializes `sokol_gfx` for the life-time of an application. Must be called after a window is created
        ///     and the graphics back-end device or context is created.
        /// </summary>
        /// <param name="desc">The configuration to use for initialize.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Setup([In] ref GraphicsDescriptor desc)
        {
            PInvoke.sg_setup(ref desc);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shutdown()
        {
            PInvoke.sg_shutdown();
        }

        /// <summary>
        ///     Creates a <see cref="Context" />. Must be called once after a GL context has been created and made
        ///     active.
        /// </summary>
        /// <returns>A <see cref="Context" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Context CreateContext()
        {
            return PInvoke.sg_setup_context();
        }

        // TODO: Document allocating a buffer
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer AllocBuffer()
        {
            return PInvoke.sg_alloc_buffer();
        }

        /// <summary>
        ///     Creates a <see cref="Buffer" /> from the specified <see cref="BufferDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating the buffer.</param>
        /// <returns>A <see cref="Buffer" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer CreateBuffer([In] ref BufferDescriptor descriptor)
        {
            return PInvoke.sg_make_buffer(ref descriptor);
        }

        // TODO: Document allocating an image
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image AllocImage()
        {
            return PInvoke.sg_alloc_image();
        }

        /// <summary>
        ///     Creates an <see cref="Image" /> from the specified <see cref="ImageDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating an image.</param>
        /// <returns>An <see cref="Image" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image CreateImage([In] ref ImageDescriptor descriptor)
        {
            return PInvoke.sg_make_image(ref descriptor);
        }

        // TODO: Document allocating a pass
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pass AllocPass()
        {
            return PInvoke.sg_alloc_pass();
        }

        /// <summary>
        ///     Creates a <see cref="Pass" /> from the specified <see cref="PassDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pass.</param>
        /// <returns>A <see cref="Pass" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pass CreatePass([In] ref PassDescriptor descriptor)
        {
            return PInvoke.sg_make_pass(ref descriptor);
        }

        // TODO: Document allocating a pipeline
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pipeline AllocPipeline()
        {
            return PInvoke.sg_alloc_pipeline();
        }

        /// <summary>
        ///     Creates a <see cref="Pipeline" /> from the specified <see cref="PipelineDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pipeline.</param>
        /// <returns>A <see cref="Pipeline" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pipeline CreatePipeline([In] ref PipelineDescriptor descriptor)
        {
            return PInvoke.sg_make_pipeline(ref descriptor);
        }

        // TODO: Document allocating a shader
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shader AllocShader()
        {
            return PInvoke.sg_alloc_shader();
        }

        /// <summary>
        ///     Creates a <see cref="Shader" /> from the specified <see cref="ShaderDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a shader.</param>
        /// <returns>A <see cref="Shader" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shader CreateShader([In] ref ShaderDescriptor descriptor)
        {
            return PInvoke.sg_make_shader(ref descriptor);
        }

        /// <summary>
        ///     Gets a <see cref="bool" /> indicating whether the current state of `sokol_gfx` is initialized or not.
        /// </summary>
        /// <returns>
        ///     <c>true</c> when <see cref="Setup" /> was called successfully and <see cref="Shutdown" /> has not yet been
        ///     called; otherwise <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid()
        {
            return PInvoke.sg_isvalid();
        }

        // TODO: Document calling into underlying 3D API directly.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetStateCache()
        {
            PInvoke.sg_reset_state_cache();
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.DontCare" /> as the action.
        /// </summary>
        /// <param name="width">The width of frame buffer.</param>
        /// <param name="height">The height of the frame buffer.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass(int width, int height)
        {
            var passAction = PassAction.DontCare;
            return BeginDefaultPass(width, height, ref passAction);
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="width">The width of frame buffer.</param>
        /// <param name="height">The height of the frame buffer.</param>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass(int width, int height, Rgba32F clearColor)
        {
            var passAction = PassAction.Clear(clearColor);
            return BeginDefaultPass(width, height, ref passAction);
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction" />.
        /// </summary>
        /// <param name="width">The width of frame buffer.</param>
        /// <param name="height">The height of the frame buffer.</param>
        /// <param name="passAction">The frame buffer pass action.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass(int width, int height, [In] ref PassAction passAction)
        {
            PInvoke.sg_begin_default_pass(ref passAction, width, height);
            return default;
        }

        /// <summary>
        ///     Execute all scheduled rendering operations of the current frame of the `sokol_gfx` application. Marks
        ///     the end of the current frame.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Commit()
        {
            PInvoke.sg_commit();
        }

        /// <summary>
        ///     Gets the <see cref="PixelFormatInfo" /> of a specified <see cref="PixelFormat" />.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>A <see cref="PixelFormatInfo" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PixelFormatInfo QueryPixelFormat(PixelFormat format)
        {
            return PInvoke.sg_query_pixelformat(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GraphicsDescriptor QueryDescriptor()
        {
            return PInvoke.sg_query_desc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GraphicsFeatures QueryFeatures()
        {
            return PInvoke.sg_query_features();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GraphicsLimits QueryLimits()
        {
            return PInvoke.sg_query_limits();
        }
    }
}
