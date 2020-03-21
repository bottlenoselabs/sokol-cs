// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace Sokol
{
    /// <summary>
    ///     The static methods of `sokol_gfx`. Abbreviated as `Sg` for "sokol graphics".
    /// </summary>
    public static class Sg
    {
        private const string LibraryName = "sokol_gfx";

        /// <summary>
        ///     Initializes `sokol_gfx` for the life-time of an application. Must be called after a window is created
        ///     and the graphics back-end device or context is created.
        /// </summary>
        /// <param name="desc">The configuration to use for initialize.</param>
        [DllImport(LibraryName, EntryPoint = "sg_setup")]
        public static extern void Setup([In] ref InitializeDescription desc);

        /// <summary>
        ///     Destroys `sokol_gfx` for the life-time of an application. Should be called before an application exits
        ///     which the application previously called <see cref="Setup"/>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you need to destroy a resource before calling <see cref="Shutdown"/>, call one the following:
        ///         <list type="bullet">
        ///             <item>
        ///                 <description><see cref="DestroyBuffer"/></description>
        ///             </item>
        ///             <item>
        ///                 <description><see cref="DestroyImage"/></description>
        ///             </item>
        ///             <item>
        ///                 <description><see cref="DestroyShader"/></description>
        ///             </item>
        ///             <item>
        ///                 <description><see cref="DestroyPipeline"/></description>
        ///             </item>
        ///             <item>
        ///                 <description><see cref="DestroyPass"/></description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        [DllImport(LibraryName, EntryPoint = "sg_shutdown")]
        public static extern void Shutdown();

        [DllImport(LibraryName, EntryPoint = "sg_isvalid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValid();

        [DllImport(LibraryName, EntryPoint = "sg_reset_state_cache")]
        public static extern void ResetStateCache();

        [DllImport(LibraryName, EntryPoint = "sg_install_trace_hooks")]
        public static extern SgTraceHooks InstallTraceHooks(ref SgTraceHooks traceHooks);

        [DllImport(LibraryName, EntryPoint = "sg_push_debug_group")]
        public static extern void PushDebugGroup(IntPtr name);

        [DllImport(LibraryName, EntryPoint = "sg_pop_debug_group")]
        public static extern void PopDebugGroup();

        [DllImport(LibraryName, EntryPoint = "sg_make_buffer")]
        public static extern Buffer MakeBuffer([In] ref SgBufferDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_make_image")]
        public static extern Image MakeImage([In] ref SgImageDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_make_shader")]
        public static extern Shader MakeShader([In] ref SgShaderDescription description);

        public static Shader MakeShader([In] ref SgShaderDescription description, string vertexShaderSourceCode, string fragmentShaderSourceCode)
        {
            var vertexSourceCodePointer = Marshal.StringToHGlobalAnsi(vertexShaderSourceCode);
            var fragmentSourceCodePointer = Marshal.StringToHGlobalAnsi(fragmentShaderSourceCode);

            description.VertexShader.SourceCode = vertexSourceCodePointer;
            description.FragmentShader.SourceCode = fragmentSourceCodePointer;

            var shader = MakeShader(ref description);
            return shader;
        }
        
        [DllImport(LibraryName, EntryPoint = "sg_make_pipeline")]
        public static extern Pipeline MakePipeline([In] ref SgPipelineDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_make_pass")]
        public static extern Pass MakePass([In] ref SgPassDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_destroy_buffer")]
        public static extern void DestroyBuffer(Buffer buffer);

        [DllImport(LibraryName, EntryPoint = "sg_destroy_image")]
        public static extern void DestroyImage(Image image);

        [DllImport(LibraryName, EntryPoint = "sg_destroy_shader")]
        public static extern void DestroyShader(Shader shader);

        [DllImport(LibraryName, EntryPoint = "destroy_pipeline")]
        public static extern void DestroyPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "destroy_pass")]
        public static extern void DestroyPass(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_update_buffer")]
        public static extern void UpdateBuffer(Buffer buffer, IntPtr dataPointer, int dataSize);

        public static unsafe void UpdateBuffer<T>(Buffer buffer, Memory<T> data, int? count = null) where T : unmanaged
        {
            var dataHandle = data.Pin();
            var dataLength = count ?? data.Length;
            var dataSize =  Marshal.SizeOf<T>() * dataLength;
            
            UpdateBuffer(buffer, (IntPtr) dataHandle.Pointer, dataSize);
            
            dataHandle.Dispose();
        }

        [DllImport(LibraryName, EntryPoint = "sg_update_image")]
        public static extern void UpdateImage(Image image, ref SgImageContent data);

        [DllImport(LibraryName, EntryPoint = "sg_append_buffer")]
        public static extern int AppendBuffer(Buffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(LibraryName, EntryPoint = "sg_query_buffer_overflow")]
        public static extern bool QueryBufferOverflow(Buffer buffer);

        [DllImport(LibraryName, EntryPoint = "sg_begin_default_pass")]
        public static extern void BeginDefaultPass([In] ref SgPassAction passAction, int width, int height);

        [DllImport(LibraryName, EntryPoint = "sg_begin_pass")]
        public static extern void BeginPass(Pass pass, [In] ref SgPassAction passAction);

        [DllImport(LibraryName, EntryPoint = "sg_apply_viewport")]
        public static extern void ApplyViewport(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(LibraryName, EntryPoint = "sg_apply_scissor_rect")]
        public static extern void ApplyScissorRectangle(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(LibraryName, EntryPoint = "sg_apply_pipeline")]
        public static extern void ApplyPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_apply_bindings")]
        public static extern void ApplyBindings([In] ref SgBindings bindings);

        [DllImport(LibraryName, EntryPoint = "sg_apply_uniforms")]
        public static extern void ApplyUniforms(SgShaderStageType stage, int uniformBlockIndex, IntPtr dataPointer, int dataSize);

        public static unsafe void ApplyUniforms<T>(SgShaderStageType stage, int uniformBlockIndex, ref T value) where T : unmanaged
        {
            var dataPointer = (IntPtr) Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            ApplyUniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        [DllImport(LibraryName, EntryPoint = "sg_draw")]
        public static extern void Draw(int baseElement, int elementCount, int instanceCount);

        [DllImport(LibraryName, EntryPoint = "sg_end_pass")]
        public static extern void EndPass();

        [DllImport(LibraryName, EntryPoint = "sg_commit")]
        public static extern void Commit();

        [DllImport(LibraryName, EntryPoint = "sg_query_desc")]
        public static extern InitializeDescription QueryDescription();

        [DllImport(LibraryName, EntryPoint = "sg_query_backend")]
        public static extern GraphicsBackend QueryBackend();

        [DllImport(LibraryName, EntryPoint = "sg_query_features")]
        public static extern SgFeatures QueryFeatures();

        [DllImport(LibraryName, EntryPoint = "sg_query_limits")]
        public static extern SgLimits QueryLimits();

        [DllImport(LibraryName, EntryPoint = "sg_query_pixelformat")]
        public static extern SgPixelFormatInfo QueryPixelFormat(PixelFormat format);

        [DllImport(LibraryName, EntryPoint = "sg_query_buffer_state")]
        public static extern SgResourceState QueryBufferState(Buffer buffer);

        [DllImport(LibraryName, EntryPoint = "sg_query_image_state")]
        public static extern SgResourceState QueryImageState(Image image);

        [DllImport(LibraryName, EntryPoint = "sg_query_shader_state")]
        public static extern SgResourceState QueryShaderState(Shader shader);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_state")]
        public static extern SgResourceState QueryPipelineState(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_state")]
        public static extern SgResourceState QueryPassState(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_query_buffer_info")]
        public static extern SgBufferInfo QueryBufferInfo(Buffer buffer);

        [DllImport(LibraryName, EntryPoint = "sg_query_image_info")]
        public static extern SgImageInfo QueryImageInfo(Image image);

        [DllImport(LibraryName, EntryPoint = "sg_query_shader_info")]
        public static extern SgShaderInfo QueryShaderInfo(Shader shader);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_info")]
        public static extern SgPipelineInfo QueryPipelineInfo(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_info")]
        public static extern SgPassInfo QueryPassInfo(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_query_buffer_defaults")]
        public static extern SgBufferDescription QueryBufferDefaults([In] ref SgBufferDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_query_image_defaults")]
        public static extern SgImageDescription QueryImageDefaults([In] ref SgImageDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_query_shader_defaults")]
        public static extern SgShaderDescription QueryShaderDefaults([In] ref SgShaderDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_query_pipeline_defaults")]
        public static extern SgPipelineDescription QueryPipelineDefaults([In] ref SgPipelineDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_query_pass_defaults")]
        public static extern SgPassDescription QueryPassDefaults([In] ref SgPassDescription description);

        [DllImport(LibraryName, EntryPoint = "sg_alloc_buffer")]
        public static extern Buffer AllocBuffer();

        [DllImport(LibraryName, EntryPoint = "sg_alloc_image")]
        public static extern Image AllocImage();

        [DllImport(LibraryName, EntryPoint = "sg_alloc_shader")]
        public static extern Shader AllocShader();

        [DllImport(LibraryName, EntryPoint = "sg_alloc_pipeline")]
        public static extern Pipeline AllocPipeline();

        [DllImport(LibraryName, EntryPoint = "sg_alloc_pass")]
        public static extern Pass AllocPass();

        [DllImport(LibraryName, EntryPoint = "sg_init_buffer")]
        public static extern void InitBuffer(Buffer buffer, [In] ref SgBufferDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_init_image")]
        public static extern void InitImage(Image image, [In] ref SgImageDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_init_shader")]
        public static extern void InitShader(Shader shader, [In] ref SgShaderDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_init_pipeline")]
        public static extern void InitPipeline(Pipeline pipeline, [In] ref SgPipelineDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_init_pass")]
        public static extern void InitPass(Pass pass, [In] ref SgPassDescription desc);

        [DllImport(LibraryName, EntryPoint = "sg_fail_buffer")]
        public static extern void FailBuffer(Buffer buffer);

        [DllImport(LibraryName, EntryPoint = "sg_fail_image")]
        public static extern void FailImage(Image image);

        [DllImport(LibraryName, EntryPoint = "sg_fail_shader")]
        public static extern void FailShader(Shader shader);

        [DllImport(LibraryName, EntryPoint = "sg_fail_pipeline")]
        public static extern void FailPipeline(Pipeline pipeline);

        [DllImport(LibraryName, EntryPoint = "sg_fail_pass")]
        public static extern void FailPass(Pass pass);

        [DllImport(LibraryName, EntryPoint = "sg_setup_context")]
        public static extern Context SetupContext();

        [DllImport(LibraryName, EntryPoint = "sg_activate_context")]
        public static extern void ActivateContext(Context context);

        [DllImport(LibraryName, EntryPoint = "sg_discard_context")]
        public static extern void DiscardContext(Context context);
    }
}