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
    ///     The static methods of `sokol_gfx`. Abbreviated as `Sg` for "sokol graphics".
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For practicality, it's recommended to import the module with all the bindings like so:
    ///         <c>using static Sokol.Sg;</c>.
    ///     </para>
    /// </remarks>
    public static class Sg
    {
        [SuppressMessage("ReSharper", "SA1600", Justification = "Library name.")]
        internal const string LibraryName = "sokol_gfx";

        /// <summary>
        ///     Gets the <see cref="InitializeDescription" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="InitializeDescription" /> of the `sokol_gfx` application.</value>
        public static InitializeDescription Description => QueryDescription();

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
        [DllImport(LibraryName, EntryPoint = "sg_setup")]
        public static extern void Setup([In] ref InitializeDescription desc);

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
        ///                     <see cref="DestroyPipeline" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="DestroyPass" />
        ///                 </description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        [DllImport(LibraryName, EntryPoint = "sg_shutdown")]
        public static extern void Shutdown();

        /// <summary>
        ///     Gets a <see cref="bool" /> indicating whether the current state of `sokol_gfx` is initialized or not.
        /// </summary>
        /// <returns>
        ///     <c>true</c> when <see cref="Setup" /> was called successfully and <see cref="Shutdown" /> has not yet been
        ///     called; otherwise <c>false</c>.
        /// </returns>
        [DllImport(LibraryName, EntryPoint = "sg_isvalid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValid();

        // TODO: Document calling into underlying 3D API directly.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(LibraryName, EntryPoint = "sg_reset_state_cache")]
        public static extern void ResetStateCache();

        // TODO: Document trace hooks.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(LibraryName, EntryPoint = "sg_install_trace_hooks")]
        public static extern SgTraceHooks InstallTraceHooks(ref SgTraceHooks traceHooks);

        // TODO: Document trace hooks.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(LibraryName, EntryPoint = "sg_push_debug_group")]
        public static extern void PushDebugGroup(IntPtr name);

        // TODO: Document trace hooks.
        [DllImport(LibraryName, EntryPoint = "sg_pop_debug_group")]
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public static extern void PopDebugGroup();

        [DllImport(LibraryName, EntryPoint = "sg_make_pipeline")]
        public static extern Pipeline MakePipeline([In] ref SgPipelineDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_make_pass")]
        public static extern Pass MakePass([In] ref SgPassDescription description);

        [DllImport(LibraryName, EntryPoint = "destroy_pipeline")]
        public static extern void DestroyPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "destroy_pass")]
        public static extern void DestroyPass(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_begin_default_pass")]
        public static extern void BeginDefaultPass([In] ref PassAction passAction, int width, int height);

        [DllImport(LibraryName, EntryPoint = "sg_begin_pass")]
        public static extern void BeginPass(Pass pass, [In] ref PassAction passAction);

        [DllImport(LibraryName, EntryPoint = "sg_apply_viewport")]
        public static extern void ApplyViewport(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(LibraryName, EntryPoint = "sg_apply_scissor_rect")]
        public static extern void
            ApplyScissorRectangle(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(LibraryName, EntryPoint = "sg_apply_pipeline")]
        public static extern void ApplyPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_apply_bindings")]
        public static extern void ApplyBindings([In] ref SgBindings bindings);

        [DllImport(LibraryName, EntryPoint = "sg_apply_uniforms")]
        public static extern void ApplyUniforms(SgShaderStageType stage, int uniformBlockIndex, IntPtr dataPointer,
            int dataSize);

        public static unsafe void ApplyUniforms<T>(SgShaderStageType stage, int uniformBlockIndex, ref T value)
            where T : unmanaged
        {
            var dataPointer = (IntPtr)Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            ApplyUniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        [DllImport(LibraryName, EntryPoint = "sg_draw")]
        public static extern void Draw(int baseElement, int elementCount, int instanceCount);

        [DllImport(LibraryName, EntryPoint = "sg_end_pass")]
        public static extern void EndPass();

        [DllImport(LibraryName, EntryPoint = "sg_commit")]
        public static extern void Commit();

        /// <summary>
        ///     Gets the <see cref="PixelFormatInfo" /> of a specified <see cref="PixelFormat" />.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>A <see cref="PixelFormatInfo" />.</returns>
        [DllImport(LibraryName, EntryPoint = "sg_query_pixelformat")]
        public static extern PixelFormatInfo QueryPixelFormat(PixelFormat format);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_state")]
        public static extern ResourceState QueryPipelineState(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_state")]
        public static extern ResourceState QueryPassState(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_info")]
        public static extern SgPipelineInfo QueryPipelineInfo(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_info")]
        public static extern SgPassInfo QueryPassInfo(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_defaults")]
        public static extern SgPipelineDescription QueryPipelineDefaults([In] ref SgPipelineDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_defaults")]
        public static extern SgPassDescription QueryPassDefaults([In] ref SgPassDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_alloc_pipeline")]
        public static extern Pipeline AllocPipeline();

        [DllImport(LibraryName, EntryPoint = "sg_alloc_pass")]
        public static extern Pass AllocPass();

        [DllImport(LibraryName, EntryPoint = "sg_init_pipeline")]
        public static extern void InitPipeline(Pipeline pipeline, [In] ref SgPipelineDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_init_pass")]
        public static extern void InitPass(Pass pass, [In] ref SgPassDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_fail_pipeline")]
        public static extern void FailPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_fail_pass")]
        public static extern void FailPass(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_query_desc")]
        private static extern InitializeDescription QueryDescription();

        [DllImport(LibraryName, EntryPoint = "sg_query_backend")]
        private static extern sokol_gfx.sg_backend QueryBackend();

        [DllImport(LibraryName, EntryPoint = "sg_query_features")]
        private static extern GraphicsFeatures QueryFeatures();

        [DllImport(LibraryName, EntryPoint = "sg_query_limits")]
        private static extern GraphicsLimits QueryLimits();
    }
}
