// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using static NativeLibrary;

// ReSharper disable MemberCanBeInternal
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ShiftExpressionRealShiftCountIsZero

/// <summary>
///     The structs, enums, and static methods of `sokol_app`. Everything in this module exactly matches what is in
///     C, and the naming conventions used in C are maintained. For documentation see the comments in the
///     <a href="https://github.com/floooh/sokol/blob/master/sokol_app.h">`sokol_app.h` source code</a>.
/// </summary>
/// <remarks>
///     <para>
///         For practicality, it's recommended to import the module like so:
///         <c>using static sokol_app;</c>.
///     </para>
/// </remarks>
[SuppressMessage("ReSharper", "SA1204", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1300", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1307", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1310", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1401", Justification = "Public API.")]
[SuppressMessage("ReSharper", "SA1600", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1602", Justification = "C style code.")]
[SuppressMessage("ReSharper", "CommentTypo", Justification = "C style code.")]
[SuppressMessage("ReSharper", "NotAccessedField.Global", Justification = "Public API.")]
public static unsafe class sokol_app
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeCallbackDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeCallbackDelegateEvent(in sapp_event @event);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_android_get_native_activity();

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
    public delegate void* d_sapp_d3d11_get_device();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_d3d11_get_device_context();

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
    public delegate byte* d_sapp_get_clipboard_string();

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
    public delegate void* d_sapp_ios_get_window();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate BlittableBoolean d_sapp_isvalid();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate BlittableBoolean d_sapp_keyboard_shown();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_macos_get_window();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_metal_get_device();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate BlittableBoolean d_sapp_mouse_shown();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate sapp_desc d_sapp_query_desc();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void d_sapp_quit();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void d_sapp_request_quit();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int d_sapp_run([In] sapp_desc* desc);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int d_sapp_sample_count();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void d_sapp_set_clipboard_string([In] byte* str);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void d_sapp_show_keyboard(BlittableBoolean visible);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void d_sapp_show_mouse(BlittableBoolean visible);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_userdata();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_wgpu_get_depth_stencil_view();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_wgpu_get_device();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_wgpu_get_render_view();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_wgpu_get_resolve_view();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int d_sapp_width();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void* d_sapp_win32_get_hwnd();

    public enum sapp_event_type : uint
    {
        SAPP_EVENTTYPE_INVALID = 0U,
        SAPP_EVENTTYPE_KEY_DOWN = 1U,
        SAPP_EVENTTYPE_KEY_UP = 2U,
        SAPP_EVENTTYPE_CHAR = 3U,
        SAPP_EVENTTYPE_MOUSE_DOWN = 4U,
        SAPP_EVENTTYPE_MOUSE_UP = 5U,
        SAPP_EVENTTYPE_MOUSE_SCROLL = 6U,
        SAPP_EVENTTYPE_MOUSE_MOVE = 7U,
        SAPP_EVENTTYPE_MOUSE_ENTER = 8U,
        SAPP_EVENTTYPE_MOUSE_LEAVE = 9U,
        SAPP_EVENTTYPE_TOUCHES_BEGAN = 10U,
        SAPP_EVENTTYPE_TOUCHES_MOVED = 11U,
        SAPP_EVENTTYPE_TOUCHES_ENDED = 12U,
        SAPP_EVENTTYPE_TOUCHES_CANCELLED = 13U,
        SAPP_EVENTTYPE_RESIZED = 14U,
        SAPP_EVENTTYPE_ICONIFIED = 15U,
        SAPP_EVENTTYPE_RESTORED = 16U,
        SAPP_EVENTTYPE_SUSPENDED = 17U,
        SAPP_EVENTTYPE_RESUMED = 18U,
        SAPP_EVENTTYPE_UPDATE_CURSOR = 19U,
        SAPP_EVENTTYPE_QUIT_REQUESTED = 20U,
        SAPP_EVENTTYPE_CLIPBOARD_PASTED = 21U,
        _SAPP_EVENTTYPE_NUM = 22U,
        _SAPP_EVENTTYPE_FORCE_U32 = 2147483647U
    }

    public enum sapp_keycode : uint
    {
        SAPP_KEYCODE_INVALID = 0U,
        SAPP_KEYCODE_SPACE = 32U,
        SAPP_KEYCODE_APOSTROPHE = 39U,
        SAPP_KEYCODE_COMMA = 44U,
        SAPP_KEYCODE_MINUS = 45U,
        SAPP_KEYCODE_PERIOD = 46U,
        SAPP_KEYCODE_SLASH = 47U,
        SAPP_KEYCODE_0 = 48U,
        SAPP_KEYCODE_1 = 49U,
        SAPP_KEYCODE_2 = 50U,
        SAPP_KEYCODE_3 = 51U,
        SAPP_KEYCODE_4 = 52U,
        SAPP_KEYCODE_5 = 53U,
        SAPP_KEYCODE_6 = 54U,
        SAPP_KEYCODE_7 = 55U,
        SAPP_KEYCODE_8 = 56U,
        SAPP_KEYCODE_9 = 57U,
        SAPP_KEYCODE_SEMICOLON = 59U,
        SAPP_KEYCODE_EQUAL = 61U,
        SAPP_KEYCODE_A = 65U,
        SAPP_KEYCODE_B = 66U,
        SAPP_KEYCODE_C = 67U,
        SAPP_KEYCODE_D = 68U,
        SAPP_KEYCODE_E = 69U,
        SAPP_KEYCODE_F = 70U,
        SAPP_KEYCODE_G = 71U,
        SAPP_KEYCODE_H = 72U,
        SAPP_KEYCODE_I = 73U,
        SAPP_KEYCODE_J = 74U,
        SAPP_KEYCODE_K = 75U,
        SAPP_KEYCODE_L = 76U,
        SAPP_KEYCODE_M = 77U,
        SAPP_KEYCODE_N = 78U,
        SAPP_KEYCODE_O = 79U,
        SAPP_KEYCODE_P = 80U,
        SAPP_KEYCODE_Q = 81U,
        SAPP_KEYCODE_R = 82U,
        SAPP_KEYCODE_S = 83U,
        SAPP_KEYCODE_T = 84U,
        SAPP_KEYCODE_U = 85U,
        SAPP_KEYCODE_V = 86U,
        SAPP_KEYCODE_W = 87U,
        SAPP_KEYCODE_X = 88U,
        SAPP_KEYCODE_Y = 89U,
        SAPP_KEYCODE_Z = 90U,
        SAPP_KEYCODE_LEFT_BRACKET = 91U,
        SAPP_KEYCODE_BACKSLASH = 92U,
        SAPP_KEYCODE_RIGHT_BRACKET = 93U,
        SAPP_KEYCODE_GRAVE_ACCENT = 96U,
        SAPP_KEYCODE_WORLD_1 = 161U,
        SAPP_KEYCODE_WORLD_2 = 162U,
        SAPP_KEYCODE_ESCAPE = 256U,
        SAPP_KEYCODE_ENTER = 257U,
        SAPP_KEYCODE_TAB = 258U,
        SAPP_KEYCODE_BACKSPACE = 259U,
        SAPP_KEYCODE_INSERT = 260U,
        SAPP_KEYCODE_DELETE = 261U,
        SAPP_KEYCODE_RIGHT = 262U,
        SAPP_KEYCODE_LEFT = 263U,
        SAPP_KEYCODE_DOWN = 264U,
        SAPP_KEYCODE_UP = 265U,
        SAPP_KEYCODE_PAGE_UP = 266U,
        SAPP_KEYCODE_PAGE_DOWN = 267U,
        SAPP_KEYCODE_HOME = 268U,
        SAPP_KEYCODE_END = 269U,
        SAPP_KEYCODE_CAPS_LOCK = 280U,
        SAPP_KEYCODE_SCROLL_LOCK = 281U,
        SAPP_KEYCODE_NUM_LOCK = 282U,
        SAPP_KEYCODE_PRINT_SCREEN = 283U,
        SAPP_KEYCODE_PAUSE = 284U,
        SAPP_KEYCODE_F1 = 290U,
        SAPP_KEYCODE_F2 = 291U,
        SAPP_KEYCODE_F3 = 292U,
        SAPP_KEYCODE_F4 = 293U,
        SAPP_KEYCODE_F5 = 294U,
        SAPP_KEYCODE_F6 = 295U,
        SAPP_KEYCODE_F7 = 296U,
        SAPP_KEYCODE_F8 = 297U,
        SAPP_KEYCODE_F9 = 298U,
        SAPP_KEYCODE_F10 = 299U,
        SAPP_KEYCODE_F11 = 300U,
        SAPP_KEYCODE_F12 = 301U,
        SAPP_KEYCODE_F13 = 302U,
        SAPP_KEYCODE_F14 = 303U,
        SAPP_KEYCODE_F15 = 304U,
        SAPP_KEYCODE_F16 = 305U,
        SAPP_KEYCODE_F17 = 306U,
        SAPP_KEYCODE_F18 = 307U,
        SAPP_KEYCODE_F19 = 308U,
        SAPP_KEYCODE_F20 = 309U,
        SAPP_KEYCODE_F21 = 310U,
        SAPP_KEYCODE_F22 = 311U,
        SAPP_KEYCODE_F23 = 312U,
        SAPP_KEYCODE_F24 = 313U,
        SAPP_KEYCODE_F25 = 314U,
        SAPP_KEYCODE_KP_0 = 320U,
        SAPP_KEYCODE_KP_1 = 321U,
        SAPP_KEYCODE_KP_2 = 322U,
        SAPP_KEYCODE_KP_3 = 323U,
        SAPP_KEYCODE_KP_4 = 324U,
        SAPP_KEYCODE_KP_5 = 325U,
        SAPP_KEYCODE_KP_6 = 326U,
        SAPP_KEYCODE_KP_7 = 327U,
        SAPP_KEYCODE_KP_8 = 328U,
        SAPP_KEYCODE_KP_9 = 329U,
        SAPP_KEYCODE_KP_DECIMAL = 330U,
        SAPP_KEYCODE_KP_DIVIDE = 331U,
        SAPP_KEYCODE_KP_MULTIPLY = 332U,
        SAPP_KEYCODE_KP_SUBTRACT = 333U,
        SAPP_KEYCODE_KP_ADD = 334U,
        SAPP_KEYCODE_KP_ENTER = 335U,
        SAPP_KEYCODE_KP_EQUAL = 336U,
        SAPP_KEYCODE_LEFT_SHIFT = 340U,
        SAPP_KEYCODE_LEFT_CONTROL = 341U,
        SAPP_KEYCODE_LEFT_ALT = 342U,
        SAPP_KEYCODE_LEFT_SUPER = 343U,
        SAPP_KEYCODE_RIGHT_SHIFT = 344U,
        SAPP_KEYCODE_RIGHT_CONTROL = 345U,
        SAPP_KEYCODE_RIGHT_ALT = 346U,
        SAPP_KEYCODE_RIGHT_SUPER = 347U,
        SAPP_KEYCODE_MENU = 348U
    }

    public enum sapp_mousebutton
    {
        SAPP_MOUSEBUTTON_INVALID = -1,
        SAPP_MOUSEBUTTON_LEFT = 0,
        SAPP_MOUSEBUTTON_RIGHT = 1,
        SAPP_MOUSEBUTTON_MIDDLE = 2
    }

    public const int SAPP_MAX_TOUCHPOINTS = 8;
    public const int SAPP_MAX_MOUSEBUTTONS = 3;
    public const int SAPP_MAX_KEYCODES = 512;
    public const int SAPP_MODIFIER_SHIFT = 1;
    public const int SAPP_MODIFIER_CTRL = 2;
    public const int SAPP_MODIFIER_ALT = 4;
    public const int SAPP_MODIFIER_SUPER = 8;

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

    public static IntPtr sapp_metal_get_renderpass_descriptor;

    public static IntPtr sapp_metal_get_drawable;

    public static d_sapp_macos_get_window sapp_macos_get_window;

    public static d_sapp_ios_get_window sapp_ios_get_window;

    public static d_sapp_d3d11_get_device sapp_d3d11_get_device;

    public static d_sapp_d3d11_get_device_context sapp_d3d11_get_device_context;

    public static IntPtr sapp_d3d11_get_render_target_view;

    public static IntPtr sapp_d3d11_get_depth_stencil_view;

    public static d_sapp_win32_get_hwnd sapp_win32_get_hwnd;

    public static d_sapp_wgpu_get_device sapp_wgpu_get_device;

    public static IntPtr sapp_wgpu_get_render_view;

    public static IntPtr sapp_wgpu_get_resolve_view;

    public static IntPtr sapp_wgpu_get_depth_stencil_view;

    public static d_sapp_android_get_native_activity sapp_android_get_native_activity;

    private static IntPtr _libraryHandle;

    public static void LoadApi(string libraryPath)
    {
        GraphicsHelper.EnsureIs64BitArchitecture();

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
        sapp_metal_get_renderpass_descriptor = GetLibraryFunctionPointer(
            _libraryHandle, "sapp_metal_get_renderpass_descriptor");
        sapp_metal_get_drawable = GetLibraryFunctionPointer(_libraryHandle, "sapp_metal_get_drawable");
        sapp_macos_get_window = GetLibraryFunction<d_sapp_macos_get_window>(_libraryHandle);
        sapp_ios_get_window = GetLibraryFunction<d_sapp_ios_get_window>(_libraryHandle);
        sapp_d3d11_get_device = GetLibraryFunction<d_sapp_d3d11_get_device>(_libraryHandle);
        sapp_d3d11_get_device_context = GetLibraryFunction<d_sapp_d3d11_get_device_context>(_libraryHandle);
        sapp_d3d11_get_render_target_view =
            GetLibraryFunctionPointer(_libraryHandle, "sapp_d3d11_get_render_target_view");
        sapp_d3d11_get_depth_stencil_view =
            GetLibraryFunctionPointer(_libraryHandle, "sapp_d3d11_get_depth_stencil_view");
        sapp_win32_get_hwnd = GetLibraryFunction<d_sapp_win32_get_hwnd>(_libraryHandle);
        sapp_wgpu_get_device = GetLibraryFunction<d_sapp_wgpu_get_device>(_libraryHandle);
        sapp_wgpu_get_render_view =
            GetLibraryFunctionPointer(_libraryHandle, "sapp_wgpu_get_render_view");
        sapp_wgpu_get_resolve_view =
            GetLibraryFunctionPointer(_libraryHandle, "sapp_wgpu_get_resolve_view");
        sapp_wgpu_get_depth_stencil_view =
            GetLibraryFunctionPointer(_libraryHandle, "sapp_wgpu_get_depth_stencil_view");
        sapp_android_get_native_activity = GetLibraryFunction<d_sapp_android_get_native_activity>(_libraryHandle);

        // Perform a garbage collection as we are using a bunch of C# strings
        GC.Collect();
        // To perform a "full" GC, we have to empty the finalizer queue then call again
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    public static void UnloadApi()
    {
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
        sapp_metal_get_renderpass_descriptor = default;
        sapp_metal_get_drawable = default;
        sapp_macos_get_window = default;
        sapp_ios_get_window = default;
        sapp_d3d11_get_device = default;
        sapp_d3d11_get_device_context = default;
        sapp_d3d11_get_render_target_view = default;
        sapp_d3d11_get_depth_stencil_view = default;
        sapp_win32_get_hwnd = default;
        sapp_wgpu_get_device = default;
        sapp_wgpu_get_render_view = default;
        sapp_wgpu_get_resolve_view = default;
        sapp_wgpu_get_depth_stencil_view = default;
        sapp_android_get_native_activity = default;
    }

    public static void LoadApi(GraphicsBackend graphicsBackend)
    {
        var libraryPath = GraphicsHelper.GetLibraryPath(graphicsBackend, "sokol_app");
        LoadApi(libraryPath);
    }

    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public readonly struct sapp_touchpoint
    {
        [FieldOffset(0)] /* size = 8, padding = 0 */
        public readonly void* identifier;

        [FieldOffset(8)] /* size = 4, padding = 0 */
        public readonly float pos_x;

        [FieldOffset(12)] /* size = 4, padding = 0 */
        public readonly float pos_y;

        [FieldOffset(16)] /* size = 1, padding = 7 */
        public readonly BlittableBoolean changed;
    }

    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    public readonly struct sapp_event
    {
        [FieldOffset(0)] /* size = 8, padding = 0 */
        public readonly ulong frame_count;

        [FieldOffset(8)] /* size = 4, padding = 0 */
        public readonly sapp_event_type type;

        [FieldOffset(12)] /* size = 4, padding = 0 */
        public readonly sapp_keycode key_code;

        [FieldOffset(16)] /* size = 4, padding = 0 */
        public readonly uint char_code;

        [FieldOffset(20)] /* size = 1, padding = 3 */
        public readonly BlittableBoolean key_repeat;

        [FieldOffset(24)] /* size = 4, padding = 0 */
        public readonly uint modifiers;

        [FieldOffset(28)] /* size = 4, padding = 0 */
        public readonly sapp_mousebutton mouse_button;

        [FieldOffset(32)] /* size = 4, padding = 0 */
        public readonly float mouse_x;

        [FieldOffset(36)] /* size = 4, padding = 0 */
        public readonly float mouse_y;

        [FieldOffset(40)] /* size = 4, padding = 0 */
        public readonly float scroll_x;

        [FieldOffset(44)] /* size = 4, padding = 0 */
        public readonly float scroll_y;

        [FieldOffset(48)] /* size = 4, padding = 4 */
        public readonly int num_touches;

        [FieldOffset(56)] /* size = 192, padding = 0 */
        public readonly ulong _touches; /* original type is `sapp_touchpoint [8]` */

        [FieldOffset(248)] /* size = 4, padding = 0 */
        public readonly int window_width;

        [FieldOffset(252)] /* size = 4, padding = 0 */
        public readonly int window_height;

        [FieldOffset(256)] /* size = 4, padding = 0 */
        public readonly int framebuffer_width;

        [FieldOffset(260)] /* size = 4, padding = 0 */
        public readonly int framebuffer_height;

        public ref sapp_touchpoint touch(int index = 0)
        {
            fixed (sapp_event* @this = &this)
            {
                var pointer = (sapp_touchpoint*)&@this->_touches;
                return ref *(pointer + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 144, Pack = 8)]
    public struct sapp_desc
    {
        [FieldOffset(0)] /* size = 8, padding = 0 */
        public void* init_cb;

        [FieldOffset(8)] /* size = 8, padding = 0 */
        public void* frame_cb;

        [FieldOffset(16)] /* size = 8, padding = 0 */
        public void* cleanup_cb;

        [FieldOffset(24)] /* size = 8, padding = 0 */
        public void* event_cb;

        [FieldOffset(32)] /* size = 8, padding = 0 */
        public void* fail_cb;

        [FieldOffset(40)] /* size = 8, padding = 0 */
        public void* user_data;

        [FieldOffset(48)] /* size = 8, padding = 0 */
        public void* init_userdata_cb;

        [FieldOffset(56)] /* size = 8, padding = 0 */
        public void* frame_userdata_cb;

        [FieldOffset(64)] /* size = 8, padding = 0 */
        public void* cleanup_userdata_cb;

        [FieldOffset(72)] /* size = 8, padding = 0 */
        public void* event_userdata_cb;

        [FieldOffset(80)] /* size = 8, padding = 0 */
        public void* fail_userdata_cb;

        [FieldOffset(88)] /* size = 4, padding = 0 */
        public int width;

        [FieldOffset(92)] /* size = 4, padding = 0 */
        public int height;

        [FieldOffset(96)] /* size = 4, padding = 0 */
        public int sample_count;

        [FieldOffset(100)] /* size = 4, padding = 0 */
        public int swap_interval;

        [FieldOffset(104)] /* size = 1, padding = 0 */
        public BlittableBoolean high_dpi;

        [FieldOffset(105)] /* size = 1, padding = 0 */
        public BlittableBoolean fullscreen;

        [FieldOffset(106)] /* size = 1, padding = 5 */
        public BlittableBoolean alpha;

        [FieldOffset(112)] /* size = 8, padding = 0 */
        public byte* window_title;

        [FieldOffset(120)] /* size = 1, padding = 0 */
        public BlittableBoolean user_cursor;

        [FieldOffset(121)] /* size = 1, padding = 2 */
        public BlittableBoolean enable_clipboard;

        [FieldOffset(124)] /* size = 4, padding = 0 */
        public int clipboard_size;

        [FieldOffset(128)] /* size = 8, padding = 0 */
        public byte* html5_canvas_name;

        [FieldOffset(136)] /* size = 1, padding = 0 */
        public BlittableBoolean html5_canvas_resize;

        [FieldOffset(137)] /* size = 1, padding = 0 */
        public BlittableBoolean html5_preserve_drawing_buffer;

        [FieldOffset(138)] /* size = 1, padding = 0 */
        public BlittableBoolean html5_premultiplied_alpha;

        [FieldOffset(139)] /* size = 1, padding = 0 */
        public BlittableBoolean html5_ask_leave_site;

        [FieldOffset(140)] /* size = 1, padding = 0 */
        public BlittableBoolean ios_keyboard_resizes_canvas;

        [FieldOffset(141)] /* size = 1, padding = 2 */
        public BlittableBoolean gl_force_gles2;
    }
}
