// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using static NativeLibrary;
using static Sokol.App.PInvoke;

// ReSharper disable MemberCanBeInternal
namespace Sokol.App
{
    /// <summary>
    ///     Native utility methods for working with `sokol_app`.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public static class Native
    {
        private static IntPtr _libraryHandle;

        /// <summary>
        ///     Loads the native library `sokol_app` and sets up the function pointers given a specified file path to
        ///     the `sokol_app` native library.
        /// </summary>
        /// <param name="libraryPath">The library path to `sokol_gfx`.</param>
        public static void LoadApi(string libraryPath)
        {
            Graphics.Native.LoadApi(libraryPath);

            _libraryHandle = LoadLibrary(libraryPath);

            sapp_isvalid = GetLibraryFunction<d_sapp_isvalid>(_libraryHandle);
            sapp_width = GetLibraryFunction<d_sapp_width>(_libraryHandle);
            sapp_height = GetLibraryFunction<d_sapp_height>(_libraryHandle);
            sapp_color_format = GetLibraryFunction<d_sapp_color_format>(_libraryHandle);
            sapp_depth_format = GetLibraryFunction<d_sapp_depth_format>(_libraryHandle);
            sapp_sample_count = GetLibraryFunction<d_sapp_sample_count>(_libraryHandle);
            sapp_high_dpi = GetLibraryFunction<d_sapp_high_dpi>(_libraryHandle);
            sapp_dpi_scale = GetLibraryFunction<d_sapp_dpi_scale>(_libraryHandle);
            sapp_show_keyboard = GetLibraryFunction<d_sapp_show_keyboard>(_libraryHandle);
            sapp_keyboard_shown = GetLibraryFunction<d_sapp_keyboard_shown>(_libraryHandle);
            sapp_show_mouse = GetLibraryFunction<d_sapp_show_mouse>(_libraryHandle);
            sapp_mouse_shown = GetLibraryFunction<d_sapp_mouse_shown>(_libraryHandle);
            sapp_userdata = GetLibraryFunction<d_sapp_userdata>(_libraryHandle);
            sapp_query_desc = GetLibraryFunction<d_sapp_query_desc>(_libraryHandle);
            sapp_request_quit = GetLibraryFunction<d_sapp_request_quit>(_libraryHandle);
            sapp_cancel_quit = GetLibraryFunction<d_sapp_cancel_quit>(_libraryHandle);
            sapp_quit = GetLibraryFunction<d_sapp_quit>(_libraryHandle);
            sapp_consume_event = GetLibraryFunction<d_sapp_consume_event>(_libraryHandle);
            sapp_frame_count = GetLibraryFunction<d_sapp_frame_count>(_libraryHandle);
            sapp_set_clipboard_string = GetLibraryFunction<d_sapp_set_clipboard_string>(_libraryHandle);
            sapp_get_clipboard_string = GetLibraryFunction<d_sapp_get_clipboard_string>(_libraryHandle);
            sapp_run = GetLibraryFunction<d_sapp_run>(_libraryHandle);
            sapp_gles2 = GetLibraryFunction<d_sapp_gles2>(_libraryHandle);
            sapp_html5_ask_leave_site = GetLibraryFunction<d_sapp_html5_ask_leave_site>(_libraryHandle);
            sapp_metal_get_device = GetLibraryFunction<d_sapp_metal_get_device>(_libraryHandle);
            sapp_macos_get_window = GetLibraryFunction<d_sapp_macos_get_window>(_libraryHandle);
            sapp_ios_get_window = GetLibraryFunction<d_sapp_ios_get_window>(_libraryHandle);
            sapp_d3d11_get_device = GetLibraryFunction<d_sapp_d3d11_get_device>(_libraryHandle);
            sapp_d3d11_get_device_context = GetLibraryFunction<d_sapp_d3d11_get_device_context>(_libraryHandle);
            sapp_win32_get_hwnd = GetLibraryFunction<d_sapp_win32_get_hwnd>(_libraryHandle);
            sapp_wgpu_get_device = GetLibraryFunction<d_sapp_wgpu_get_device>(_libraryHandle);
            sapp_android_get_native_activity = GetLibraryFunction<d_sapp_android_get_native_activity>(_libraryHandle);
            sapp_sgcontext = GetLibraryFunction<d_sapp_sgcontext>(_libraryHandle);

            // Perform a garbage collection as we are using a bunch of C# strings
            GC.Collect();
            // To perform a "full" GC, we have to empty the finalizer queue then call again
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        ///     Unloads the native library `sokol_app`.
        /// </summary>
        public static void UnloadApi()
        {
            Graphics.Native.UnloadApi();

            if (_libraryHandle == IntPtr.Zero)
            {
                return;
            }

            FreeLibrary(_libraryHandle);

            sapp_isvalid = default;
            sapp_width = default;
            sapp_height = default;
            sapp_color_format = default;
            sapp_depth_format = default;
            sapp_sample_count = default;
            sapp_high_dpi = default;
            sapp_dpi_scale = default;
            sapp_show_keyboard = default;
            sapp_keyboard_shown = default;
            sapp_show_mouse = default;
            sapp_mouse_shown = default;
            sapp_userdata = default;
            sapp_query_desc = default;
            sapp_request_quit = default;
            sapp_cancel_quit = default;
            sapp_quit = default;
            sapp_consume_event = default;
            sapp_frame_count = default;
            sapp_set_clipboard_string = default;
            sapp_get_clipboard_string = default;
            sapp_run = default;
            sapp_gles2 = default;
            sapp_html5_ask_leave_site = default;
            sapp_metal_get_device = default;
            sapp_macos_get_window = default;
            sapp_ios_get_window = default;
            sapp_d3d11_get_device = default;
            sapp_d3d11_get_device_context = default;
            sapp_win32_get_hwnd = default;
            sapp_wgpu_get_device = default;
            sapp_android_get_native_activity = default;
        }

        /// <summary>
        ///     Loads the native library `sokol_app` and sets up the function pointers given a specified
        ///     <see cref="GraphicsBackend" />.
        /// </summary>
        /// <param name="backend">The <see cref="GraphicsBackend" /> to load.</param>
        /// <remarks>
        ///     <para>
        ///         <see cref="LoadApi(GraphicsBackend)" /> will attempt to find the library path for `sokol_app` given
        ///         <paramref name="backend" /> by calling <see cref="LoadApi(string)" />.
        ///     </para>
        /// </remarks>
        public static void LoadApi(GraphicsBackend backend)
        {
            var libraryPath = GraphicsHelper.GetLibraryPath(backend, "sokol_app");
            LoadApi(libraryPath);
        }
    }
}
