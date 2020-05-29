// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBeInternal
#nullable disable

namespace Sokol.App
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style.")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1307", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1401", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "C style.")]
    public static class PInvoke
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_android_get_native_activity();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_cancel_quit();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_color_format();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_consume_event();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_d3d11_get_depth_stencil_view();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_d3d11_get_device();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_d3d11_get_device_context();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_d3d11_get_render_target_view();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_depth_format();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate float d_sapp_dpi_scale();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ulong d_sapp_frame_count();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_get_clipboard_string();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sapp_gles2();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_height();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sapp_high_dpi();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_html5_ask_leave_site(BlittableBoolean ask);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_ios_get_window();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sapp_isvalid();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sapp_keyboard_shown();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_macos_get_window();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_metal_get_device();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_metal_get_drawable();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_metal_get_renderpass_descriptor();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sapp_mouse_shown();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate AppDescriptor d_sapp_query_desc();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_quit();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_request_quit();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_run([In] ref AppDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_sample_count();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_set_clipboard_string([In] IntPtr str);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_show_keyboard(BlittableBoolean visible);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sapp_show_mouse(BlittableBoolean visible);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_userdata();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_wgpu_get_depth_stencil_view();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_wgpu_get_device();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_wgpu_get_render_view();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_wgpu_get_resolve_view();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sapp_width();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate IntPtr d_sapp_win32_get_hwnd();

        public static d_sapp_isvalid sapp_isvalid;

        public static d_sapp_width sapp_width;

        public static d_sapp_height sapp_height;

        public static d_sapp_color_format sapp_color_format;

        public static d_sapp_depth_format sapp_depth_format;

        public static d_sapp_sample_count sapp_sample_count;

        public static d_sapp_high_dpi sapp_high_dpi;

        public static d_sapp_dpi_scale sapp_dpi_scale;

        public static d_sapp_show_keyboard sapp_show_keyboard;

        public static d_sapp_keyboard_shown sapp_keyboard_shown;

        public static d_sapp_show_mouse sapp_show_mouse;

        public static d_sapp_mouse_shown sapp_mouse_shown;

        public static d_sapp_userdata sapp_userdata;

        public static d_sapp_query_desc sapp_query_desc;

        public static d_sapp_request_quit sapp_request_quit;

        public static d_sapp_cancel_quit sapp_cancel_quit;

        public static d_sapp_quit sapp_quit;

        public static d_sapp_consume_event sapp_consume_event;

        public static d_sapp_frame_count sapp_frame_count;

        public static d_sapp_set_clipboard_string sapp_set_clipboard_string;

        public static d_sapp_get_clipboard_string sapp_get_clipboard_string;

        public static d_sapp_run sapp_run;

        public static d_sapp_gles2 sapp_gles2;

        public static d_sapp_html5_ask_leave_site sapp_html5_ask_leave_site;

        public static d_sapp_metal_get_device sapp_metal_get_device;

        public static d_sapp_metal_get_renderpass_descriptor sapp_metal_get_renderpass_descriptor;

        public static d_sapp_metal_get_drawable sapp_metal_get_drawable;

        public static d_sapp_macos_get_window sapp_macos_get_window;

        public static d_sapp_ios_get_window sapp_ios_get_window;

        public static d_sapp_d3d11_get_device sapp_d3d11_get_device;

        public static d_sapp_d3d11_get_device_context sapp_d3d11_get_device_context;

        public static d_sapp_d3d11_get_render_target_view sapp_d3d11_get_render_target_view;

        public static d_sapp_d3d11_get_depth_stencil_view sapp_d3d11_get_depth_stencil_view;

        public static d_sapp_win32_get_hwnd sapp_win32_get_hwnd;

        public static d_sapp_wgpu_get_device sapp_wgpu_get_device;

        public static d_sapp_wgpu_get_render_view sapp_wgpu_get_render_view;

        public static d_sapp_wgpu_get_resolve_view sapp_wgpu_get_resolve_view;

        public static d_sapp_wgpu_get_depth_stencil_view sapp_wgpu_get_depth_stencil_view;

        public static d_sapp_android_get_native_activity sapp_android_get_native_activity;
    }
}
