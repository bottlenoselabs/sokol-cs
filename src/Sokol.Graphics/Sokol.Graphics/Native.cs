// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using static NativeLibrary;
using static Sokol.Graphics.PInvoke;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Native utility methods for working with `sokol_gfx`.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public static class Native
    {
        private static IntPtr _libraryHandle;

        /// <summary>
        ///     Loads the native library `sokol_gfx` and sets up the function pointers given a specified file path to
        ///     the `sokol_gfx` native library.
        /// </summary>
        /// <param name="libraryPath">The library path to `sokol_gfx`.</param>
        public static void LoadApi(string libraryPath)
        {
            GraphicsHelper.EnsureIs64BitArchitecture();
            _libraryHandle = LoadLibrary(libraryPath);

            sg_setup = GetLibraryFunction<d_sg_setup>(_libraryHandle);
            sg_shutdown = GetLibraryFunction<d_sg_shutdown>(_libraryHandle);
            sg_isvalid = GetLibraryFunction<d_sg_isvalid>(_libraryHandle);
            sg_reset_state_cache = GetLibraryFunction<d_sg_reset_state_cache>(_libraryHandle);
            sg_install_trace_hooks = GetLibraryFunction<d_sg_install_trace_hooks>(_libraryHandle);
            sg_push_debug_group = GetLibraryFunction<d_sg_push_debug_group>(_libraryHandle);
            sg_pop_debug_group = GetLibraryFunction<d_sg_pop_debug_group>(_libraryHandle);
            sg_make_buffer = GetLibraryFunction<d_sg_make_buffer>(_libraryHandle);
            sg_make_image = GetLibraryFunction<d_sg_make_image>(_libraryHandle);
            sg_make_shader = GetLibraryFunction<d_sg_make_shader>(_libraryHandle);
            sg_make_pipeline = GetLibraryFunction<d_sg_make_pipeline>(_libraryHandle);
            sg_make_pass = GetLibraryFunction<d_sg_make_pass>(_libraryHandle);
            sg_destroy_buffer = GetLibraryFunction<d_sg_destroy_buffer>(_libraryHandle);
            sg_destroy_image = GetLibraryFunction<d_sg_destroy_image>(_libraryHandle);
            sg_destroy_shader = GetLibraryFunction<d_sg_destroy_shader>(_libraryHandle);
            sg_destroy_pipeline = GetLibraryFunction<d_sg_destroy_pipeline>(_libraryHandle);
            sg_destroy_pass = GetLibraryFunction<d_sg_destroy_pass>(_libraryHandle);
            sg_update_buffer = GetLibraryFunction<d_sg_update_buffer>(_libraryHandle);
            sg_update_image = GetLibraryFunction<d_sg_update_image>(_libraryHandle);
            sg_append_buffer = GetLibraryFunction<d_sg_append_buffer>(_libraryHandle);
            sg_query_buffer_overflow = GetLibraryFunction<d_sg_query_buffer_overflow>(_libraryHandle);
            sg_begin_default_pass = GetLibraryFunction<d_sg_begin_default_pass>(_libraryHandle);
            sg_begin_pass = GetLibraryFunction<d_sg_begin_pass>(_libraryHandle);
            sg_apply_viewport = GetLibraryFunction<d_sg_apply_viewport>(_libraryHandle);
            sg_apply_scissor_rect = GetLibraryFunction<d_sg_apply_scissor_rect>(_libraryHandle);
            sg_apply_pipeline = GetLibraryFunction<d_sg_apply_pipeline>(_libraryHandle);
            sg_apply_bindings = GetLibraryFunction<d_sg_apply_bindings>(_libraryHandle);
            sg_apply_uniforms = GetLibraryFunction<d_sg_apply_uniforms>(_libraryHandle);
            sg_draw = GetLibraryFunction<d_sg_draw>(_libraryHandle);
            sg_end_pass = GetLibraryFunction<d_sg_end_pass>(_libraryHandle);
            sg_commit = GetLibraryFunction<d_sg_commit>(_libraryHandle);
            sg_query_desc = GetLibraryFunction<d_sg_query_desc>(_libraryHandle);
            sg_query_backend = GetLibraryFunction<d_sg_query_backend>(_libraryHandle);
            sg_query_features = GetLibraryFunction<d_sg_query_features>(_libraryHandle);
            sg_query_limits = GetLibraryFunction<d_sg_query_limits>(_libraryHandle);
            sg_query_pixelformat = GetLibraryFunction<d_sg_query_pixelformat>(_libraryHandle);
            sg_query_buffer_state = GetLibraryFunction<d_sg_query_buffer_state>(_libraryHandle);
            sg_query_image_state = GetLibraryFunction<d_sg_query_image_state>(_libraryHandle);
            sg_query_shader_state = GetLibraryFunction<d_sg_query_shader_state>(_libraryHandle);
            sg_query_pipeline_state = GetLibraryFunction<d_sg_query_pipeline_state>(_libraryHandle);
            sg_query_pass_state = GetLibraryFunction<d_sg_query_pass_state>(_libraryHandle);
            sg_query_buffer_info = GetLibraryFunction<d_sg_query_buffer_info>(_libraryHandle);
            sg_query_image_info = GetLibraryFunction<d_sg_query_image_info>(_libraryHandle);
            sg_query_shader_info = GetLibraryFunction<d_sg_query_shader_info>(_libraryHandle);
            sg_query_pipeline_info = GetLibraryFunction<d_sg_query_pipeline_info>(_libraryHandle);
            sg_query_pass_info = GetLibraryFunction<d_sg_query_pass_info>(_libraryHandle);
            sg_query_buffer_defaults = GetLibraryFunction<d_sg_query_buffer_defaults>(_libraryHandle);
            sg_query_image_defaults = GetLibraryFunction<d_sg_query_image_defaults>(_libraryHandle);
            sg_query_shader_defaults = GetLibraryFunction<d_sg_query_shader_defaults>(_libraryHandle);
            sg_query_pipeline_defaults = GetLibraryFunction<d_sg_query_pipeline_defaults>(_libraryHandle);
            sg_query_pass_defaults = GetLibraryFunction<d_sg_query_pass_defaults>(_libraryHandle);
            sg_alloc_buffer = GetLibraryFunction<d_sg_alloc_buffer>(_libraryHandle);
            sg_alloc_image = GetLibraryFunction<d_sg_alloc_image>(_libraryHandle);
            sg_alloc_shader = GetLibraryFunction<d_sg_alloc_shader>(_libraryHandle);
            sg_alloc_pipeline = GetLibraryFunction<d_sg_alloc_pipeline>(_libraryHandle);
            sg_alloc_pass = GetLibraryFunction<d_sg_alloc_pass>(_libraryHandle);
            sg_init_buffer = GetLibraryFunction<d_sg_init_buffer>(_libraryHandle);
            sg_init_image = GetLibraryFunction<d_sg_init_image>(_libraryHandle);
            sg_init_shader = GetLibraryFunction<d_sg_init_shader>(_libraryHandle);
            sg_init_pipeline = GetLibraryFunction<d_sg_init_pipeline>(_libraryHandle);
            sg_init_pass = GetLibraryFunction<d_sg_init_pass>(_libraryHandle);
            sg_fail_buffer = GetLibraryFunction<d_sg_fail_buffer>(_libraryHandle);
            sg_fail_image = GetLibraryFunction<d_sg_fail_image>(_libraryHandle);
            sg_fail_shader = GetLibraryFunction<d_sg_fail_shader>(_libraryHandle);
            sg_fail_pipeline = GetLibraryFunction<d_sg_fail_pipeline>(_libraryHandle);
            sg_fail_pass = GetLibraryFunction<d_sg_fail_pass>(_libraryHandle);
            sg_setup_context = GetLibraryFunction<d_sg_setup_context>(_libraryHandle);
            sg_activate_context = GetLibraryFunction<d_sg_activate_context>(_libraryHandle);
            sg_discard_context = GetLibraryFunction<d_sg_discard_context>(_libraryHandle);

            // Perform a garbage collection as we are using a bunch of C# strings
            GC.Collect();
            // To perform a "full" GC, we have to empty the finalizer queue then call again
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        ///     Unloads the native library `sokol_gfx`.
        /// </summary>
        public static void UnloadApi()
        {
            if (_libraryHandle == IntPtr.Zero)
            {
                return;
            }

            FreeLibrary(_libraryHandle);

            sg_setup = default;
            sg_shutdown = default;
            sg_isvalid = default;
            sg_reset_state_cache = default;
            sg_install_trace_hooks = default;
            sg_push_debug_group = default;
            sg_pop_debug_group = default;
            sg_make_buffer = default;
            sg_make_image = default;
            sg_make_shader = default;
            sg_make_pipeline = default;
            sg_make_pass = default;
            sg_destroy_buffer = default;
            sg_destroy_image = default;
            sg_destroy_shader = default;
            sg_destroy_pipeline = default;
            sg_destroy_pass = default;
            sg_update_buffer = default;
            sg_update_image = default;
            sg_append_buffer = default;
            sg_query_buffer_overflow = default;
            sg_begin_default_pass = default;
            sg_begin_pass = default;
            sg_apply_viewport = default;
            sg_apply_scissor_rect = default;
            sg_apply_pipeline = default;
            sg_apply_bindings = default;
            sg_apply_uniforms = default;
            sg_draw = default;
            sg_end_pass = default;
            sg_commit = default;
            sg_query_desc = default;
            sg_query_backend = default;
            sg_query_features = default;
            sg_query_limits = default;
            sg_query_pixelformat = default;
            sg_query_buffer_state = default;
            sg_query_image_state = default;
            sg_query_shader_state = default;
            sg_query_pipeline_state = default;
            sg_query_pass_state = default;
            sg_query_buffer_info = default;
            sg_query_image_info = default;
            sg_query_shader_info = default;
            sg_query_pipeline_info = default;
            sg_query_pass_info = default;
            sg_query_buffer_defaults = default;
            sg_query_image_defaults = default;
            sg_query_shader_defaults = default;
            sg_query_pipeline_defaults = default;
            sg_query_pass_defaults = default;
            sg_alloc_buffer = default;
            sg_alloc_image = default;
            sg_alloc_shader = default;
            sg_alloc_pipeline = default;
            sg_alloc_pass = default;
            sg_init_buffer = default;
            sg_init_image = default;
            sg_init_shader = default;
            sg_init_pipeline = default;
            sg_init_pass = default;
            sg_fail_buffer = default;
            sg_fail_image = default;
            sg_fail_shader = default;
            sg_fail_pipeline = default;
            sg_fail_pass = default;
            sg_setup_context = default;
            sg_activate_context = default;
            sg_discard_context = default;
        }

        /// <summary>
        ///     Loads the native library `sokol_gfx` and sets up the function pointers given a specified
        ///     <see cref="GraphicsBackend" />.
        /// </summary>
        /// <param name="backend">The <see cref="GraphicsBackend" /> to load.</param>
        /// <remarks>
        ///     <para>
        ///         <see cref="LoadApi(GraphicsBackend)" /> will attempt to find the library path for `sokol_gfx` given
        ///         <paramref name="backend" /> by calling <see cref="LoadApi(string)" />.
        ///     </para>
        /// </remarks>
        public static void LoadApi(GraphicsBackend backend)
        {
            var libraryPath = GraphicsHelper.GetLibraryPath(backend, "sokol_gfx");
            LoadApi(libraryPath);
        }
    }
}
