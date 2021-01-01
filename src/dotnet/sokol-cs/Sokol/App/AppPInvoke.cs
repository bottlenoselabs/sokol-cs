// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style.")]
    internal static unsafe class AppPInvoke
    {
        private const string LibraryName = "sokol";

        [DllImport(LibraryName)]
        public static extern sokol_app.sapp_desc sokol_main(int argc, byte* argv);

        [DllImport(LibraryName)]
        public static extern CBool sapp_isvalid();

        [DllImport(LibraryName)]
        public static extern int sapp_width();

        [DllImport(LibraryName)]
        public static extern int sapp_height();

        [DllImport(LibraryName)]
        public static extern int sapp_color_format();

        [DllImport(LibraryName)]
        public static extern int sapp_depth_format();

        [DllImport(LibraryName)]
        public static extern int sapp_sample_count();

        [DllImport(LibraryName)]
        public static extern CBool sapp_high_dpi();

        [DllImport(LibraryName)]
        public static extern float sapp_dpi_scale();

        [DllImport(LibraryName)]
        public static extern void sapp_show_keyboard(CBool show);

        [DllImport(LibraryName)]
        public static extern CBool sapp_keyboard_shown();

        [DllImport(LibraryName)]
        public static extern CBool sapp_is_fullscreen();

        [DllImport(LibraryName)]
        public static extern void sapp_toggle_fullscreen();

        [DllImport(LibraryName)]
        public static extern void sapp_show_mouse(CBool show);

        [DllImport(LibraryName)]
        public static extern CBool sapp_mouse_shown();

        [DllImport(LibraryName)]
        public static extern void sapp_lock_mouse(CBool @lock);

        [DllImport(LibraryName)]
        public static extern CBool sapp_mouse_locked();

        [DllImport(LibraryName)]
        public static extern void* sapp_userdata();

        [DllImport(LibraryName)]
        public static extern sokol_app.sapp_desc sapp_query_desc();

        [DllImport(LibraryName)]
        public static extern void sapp_request_quit();

        [DllImport(LibraryName)]
        public static extern void sapp_cancel_quit();

        [DllImport(LibraryName)]
        public static extern void sapp_quit();

        [DllImport(LibraryName)]
        public static extern void sapp_consume_event();

        [DllImport(LibraryName)]
        public static extern ulong sapp_frame_count();

        [DllImport(LibraryName)]
        public static extern void sapp_set_clipboard_string([In] byte* str);

        [DllImport(LibraryName)]
        public static extern byte* sapp_get_clipboard_string();

        [DllImport(LibraryName)]
        public static extern void sapp_set_window_title([In] byte* str);

        [DllImport(LibraryName)]
        public static extern int sapp_get_num_dropped_files();

        [DllImport(LibraryName)]
        public static extern byte* sapp_get_dropped_file_path(int index);

        [DllImport(LibraryName)]
        public static extern void sapp_run([In] ref AppDescriptor desc);

        [DllImport(LibraryName)]
        public static extern CBool sapp_gles2();

        [DllImport(LibraryName)]
        public static extern void sapp_html5_ask_leave_site(CBool ask);

        [DllImport(LibraryName)]
        public static extern uint sapp_html5_get_dropped_file_size(int index);

        [DllImport(LibraryName)]
        public static extern void sapp_html5_fetch_dropped_file([In] sokol_app.sapp_html5_fetch_request* request);

        [DllImport(LibraryName)]
        public static extern void* sapp_metal_get_device();

        [DllImport(LibraryName)]
        public static extern void* sapp_metal_get_renderpass_descriptor();

        [DllImport(LibraryName)]
        public static extern void* sapp_metal_get_drawable();

        [DllImport(LibraryName)]
        public static extern void* sapp_macos_get_window();

        [DllImport(LibraryName)]
        public static extern void* sapp_ios_get_window();

        [DllImport(LibraryName)]
        public static extern void* sapp_d3d11_get_device();

        [DllImport(LibraryName)]
        public static extern void* sapp_d3d11_get_device_context();

        [DllImport(LibraryName)]
        public static extern void* sapp_d3d11_get_render_target_view();

        [DllImport(LibraryName)]
        public static extern void* sapp_d3d11_get_depth_stencil_view();

        [DllImport(LibraryName)]
        public static extern void* sapp_win32_get_hwnd();

        [DllImport(LibraryName)]
        public static extern void* sapp_wgpu_get_device();

        [DllImport(LibraryName)]
        public static extern void* sapp_wgpu_get_render_view();

        [DllImport(LibraryName)]
        public static extern void* sapp_wgpu_get_resolve_view();

        [DllImport(LibraryName)]
        public static extern void* sapp_wgpu_get_depth_stencil_view();

        [DllImport(LibraryName)]
        public static extern void* sapp_android_get_native_activity();

        [DllImport(LibraryName)]
        public static extern GraphicsContextDescriptor sapp_sgcontext();
    }
}
