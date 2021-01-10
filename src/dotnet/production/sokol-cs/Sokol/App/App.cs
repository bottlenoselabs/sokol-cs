// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;
using static Sokol.AppPInvoke;

namespace Sokol
{
    /// <summary>
    ///     The functions of `sokol_app`.
    /// </summary>
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "?")]
    public static class App
    {
        /// <summary>
        ///     The default delegate for `sokol_app`.
        /// </summary>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppCallback();

        /// <summary>
        ///     The event delegate for `sokol_app`.
        /// </summary>
        /// <param name="event">The <see cref="AppEvent" />.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppEventCallback(in AppEvent @event);

        /// <summary>
        ///     The resized delegate for `sokol_app`.
        /// </summary>
        /// <param name="width">The new width of the default <see cref="GraphicsPass" /> attachments.</param>
        /// <param name="height">The new height of default <see cref="GraphicsPass" /> attachments.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppResizedCallback(int width, int height);

        /// <summary>
        ///     The message delegate for `sokol_app`.
        /// </summary>
        /// <param name="str">The string.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppStringCallback(string str);

        private static GCHandle _initializeCallbackHandle;
        private static GCHandle _cleanUpCallbackHandle;
        private static GCHandle _eventCallbackHandle;
        private static GCHandle _frameCallbackHandle;
        private static GCHandle _failCallbackHandle;

        /// <summary>
        ///     Gets the <see cref="GraphicsBackend" /> of the `sokol_app` application.
        /// </summary>
        /// <value>The current <see cref="GraphicsBackend" />.</value>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public static GraphicsBackend Backend => Graphics.Backend;

        /// <summary>
        ///     Gets the current width of the default <see cref="GraphicsPass" /> attachments. May change from frame-to-frame.
        /// </summary>
        /// <value>The current width of the default <see cref="GraphicsPass" /> attachments.</value>
        public static int Width => sapp_width();

        /// <summary>
        ///     Gets the current height of the default <see cref="GraphicsPass" /> attachments. May change from frame-to-frame.
        /// </summary>
        /// <value>The current height of the default <see cref="GraphicsPass" /> attachments.</value>
        public static int Height => sapp_height();

        /// <summary>
        ///     Gets a value indicating whether the default <see cref="GraphicsPass" /> attachments are currently using
        ///     full-resolution for displays with increased pixel density (dots-per-inch).
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When <see cref="IsHighDpi" /> is <c>false</c>, <see cref="Width" /> and <see cref="Height" />
        ///         will not up-scaled, but the rendered content will be up-scaled by the rendering system composer.
        ///     </para>
        ///     <para>
        ///         When <see cref="IsHighDpi" /> is <c>true></c>, <see cref="Width" /> and <see cref="Height" />
        ///         will be up-scaled according to <see cref="DpiScale" />.
        ///     </para>
        /// </remarks>
        /// <value>
        ///     <c>true</c> if the default <see cref="GraphicsPass" /> attachments are currently using full-resolution for displays
        ///     with increased pixel density (dots-per-inch); otherwise, <c>false</c>.
        /// </value>
        public static bool IsHighDpi => sapp_high_dpi();

        /// <summary>
        ///     Gets the scaling factor for increased pixel density (dots-per-inch).
        /// </summary>
        /// <value>The scaling factor for the increased pixel density (dots-per-inch).</value>
        /// <remarks>
        ///     <para>
        ///         Use <see cref="DpiScale" /> to convert window dimensions to default <see cref="GraphicsPass" /> attachments
        ///         dimensions, or vice-versa.
        ///     </para>
        /// </remarks>
        public static float DpiScale => sapp_dpi_scale();

        /// <summary>
        ///     Gets the <see cref="GraphicsPixelFormat" /> of default <see cref="GraphicsPass" /> color attachments.
        /// </summary>
        /// <value>A color <see cref="GraphicsPixelFormat" />.</value>
        public static GraphicsPixelFormat ColorFormat => (GraphicsPixelFormat)sapp_color_format();

        /// <summary>
        ///     Gets the <see cref="GraphicsPixelFormat" /> of default <see cref="GraphicsPass" /> depth attachment.
        /// </summary>
        /// <value>A depth <see cref="GraphicsPixelFormat" />.</value>
        public static GraphicsPixelFormat DepthFormat => (GraphicsPixelFormat)sapp_depth_format();

        /// <summary>
        ///     Gets the multi-sample anti-aliasing (MSAA) sample count of the default <see cref="GraphicsPass" /> attachments.
        /// </summary>
        /// <value>The multi-sample anti-aliasing (MSAA) sample count.</value>
        public static int SampleCount => sapp_sample_count();

        /// <summary>
        ///     Occurs when the application window, 3D rendering context, and
        ///     swapchain have been created and the application should create any resources.
        /// </summary>
        public static event AppCallback? CreateResources;

        /// <summary>
        ///     Occurs before the application quits and the application should destroy any resources.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you don't explicitly destroy graphics resources, such as <see cref="GraphicsBuffer" />,
        ///         <see cref="GraphicsImage" />, <see cref="GraphicsPass" />, <see cref="GraphicsPipeline" />, and
        ///         <see cref="GraphicsShader" />, they are implicitly destroyed when the application quits gracefully.
        ///     </para>
        /// </remarks>
        public static event AppCallback? DestroyResources;

        /// <summary>
        ///     Occurs when the application should update per-frame state and perform all rendering.
        /// </summary>
        public static event AppCallback? Frame;

        /// <summary>
        ///     Occurs when the application's state changed such as when the mouse state changes, keyboard state
        ///     changes, etc.
        /// </summary>
        public static event AppEventCallback? Event;

        /// <summary>
        ///     Occurs when the application encounters an error.
        /// </summary>
        public static event AppStringCallback? Error;

        /// <summary>
        ///     Occurs when the <see cref="Width" /> or the <see cref="Height" /> change.
        /// </summary>
        public static event AppResizedCallback? Resize;

        /// <summary>
        ///     Starts the application.
        /// </summary>
        /// <param name="descriptor">The <see cref="AppDescriptor" />.</param>
        /// <param name="backend">The <see cref="GraphicsBackend" /> to use.</param>
        /// <remarks>
        ///     <para>
        ///         If the <paramref name="descriptor" /> is provided, the <see cref="AppDescriptor.InitializeCallback" />,
        ///         <see cref="AppDescriptor.FrameCallback" />, <see cref="AppDescriptor.CleanUpCallback" />,
        ///         <see cref="AppDescriptor.EventCallback" />, and <see cref="AppDescriptor.FailCallback" /> will be overriden in
        ///         <paramref name="descriptor" />.
        ///     </para>
        /// </remarks>
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global", Justification = "C# wrapper.")]
        public static void Run(AppDescriptor? descriptor = null, GraphicsBackend? backend = null)
        {
            Native.SetDllImportResolverCallback(Assembly.GetExecutingAssembly());

            var appDesc = descriptor ?? default;
            FillDescriptor(ref appDesc);
            sapp_run(ref appDesc);
        }

        /// <summary>
        ///     Requests application to quit gracefully.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Calling <see cref="RequestQuit" /> sends the <see cref="AppEventType.QuitRequested" /> to the
        ///         <see cref="EventCallback" />. This allows the quit process to be cancelled by user code.
        ///     </para>
        /// </remarks>
        public static void RequestQuit() => sapp_request_quit();

        /// <summary>
        ///     Quits the application immediately.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Calling <see cref="Quit" /> does not send the <see cref="AppEventType.QuitRequested" /> to the
        ///         <see cref="EventCallback" />.
        ///     </para>
        /// </remarks>
        public static void Quit() => sapp_quit();

        /// <summary>
        ///     Begins and returns the default <see cref="GraphicsPass" /> with the specified width, height, and
        ///     <see cref="GraphicsPassAction.DontCare" /> as the action.
        /// </summary>
        /// <returns>The default <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass BeginDefaultPass() => Graphics.BeginDefaultPass(Width, Height);

        /// <summary>
        ///     Begins and returns the default <see cref="GraphicsPass" /> with the specified width, height, and
        ///     <see cref="GraphicsPassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        /// <returns>The default <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass BeginDefaultPass(Rgba32F clearColor) => Graphics.BeginDefaultPass(Width, Height, clearColor);

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="GraphicsPass" /> with the specified width, height, and
        ///     <see cref="GraphicsPassAction" />.
        /// </summary>
        /// <param name="passAction">The frame buffer pass action.</param>
        /// <returns>The default <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass BeginDefaultPass([In] ref GraphicsPassAction passAction) => Graphics.BeginDefaultPass(Width, Height, ref passAction);

        private static void FillDescriptor(ref AppDescriptor desc)
        {
            var initializeCallbackDelegate = new AppCallbackDelegate(InitializeCallback);
            _initializeCallbackHandle = GCHandle.Alloc(initializeCallbackDelegate);
            desc.InitializeCallback = Marshal.GetFunctionPointerForDelegate(initializeCallbackDelegate);

            var frameCallbackDelegate = new AppCallbackDelegate(FrameCallback);
            _frameCallbackHandle = GCHandle.Alloc(frameCallbackDelegate);
            desc.FrameCallback = Marshal.GetFunctionPointerForDelegate(frameCallbackDelegate);

            var cleanUpCallbackDelegate = new AppCallbackDelegate(CleanUpCallback);
            _cleanUpCallbackHandle = GCHandle.Alloc(cleanUpCallbackDelegate);
            desc.CleanUpCallback = Marshal.GetFunctionPointerForDelegate(cleanUpCallbackDelegate);

            var eventCallbackDelegate = new AppCallbackDelegateEvent(EventCallback);
            _eventCallbackHandle = GCHandle.Alloc(eventCallbackDelegate);
            desc.EventCallback = Marshal.GetFunctionPointerForDelegate(eventCallbackDelegate);

            var failCallbackDelegate = new AppCallbackDelegateAnsiStringMessage(FailCallback);
            _failCallbackHandle = GCHandle.Alloc(eventCallbackDelegate);
            desc.FailCallback = Marshal.GetFunctionPointerForDelegate(eventCallbackDelegate);
        }

        private static void InitializeCallback()
        {
            InitializeGraphics();
            CreateResources?.Invoke();
            Native.ClearStrings();
            Resize?.Invoke(Width, Height);
        }

        private static void FrameCallback() => Frame?.Invoke();

        private static void CleanUpCallback()
        {
            ReleaseUnmanagedResources();
            DestroyResources?.Invoke();
            Graphics.Shutdown();
        }

        private static void EventCallback(in AppEvent @event)
        {
            if (@event.Type == AppEventType.Resized)
            {
                Resize?.Invoke(@event.FramebufferWidth, @event.FramebufferHeight);
            }

            Event?.Invoke(in @event);
        }

        private static void FailCallback(IntPtr stringC)
        {
            var message = Native.GetStringFromIntPtr(stringC);
            Error?.Invoke(message);
            Native.ClearString(stringC);
        }

        private static void InitializeGraphics()
        {
            var graphicsDescriptor = default(GraphicsDescriptor);
            graphicsDescriptor.Context = sapp_sgcontext();
            Graphics.Setup(ref graphicsDescriptor);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private static void ReleaseUnmanagedResources()
        {
            if (_initializeCallbackHandle.IsAllocated)
            {
                _initializeCallbackHandle.Free();
            }

            if (_frameCallbackHandle.IsAllocated)
            {
                _frameCallbackHandle.Free();
            }

            if (_cleanUpCallbackHandle.IsAllocated)
            {
                _cleanUpCallbackHandle.Free();
            }

            if (_eventCallbackHandle.IsAllocated)
            {
                _eventCallbackHandle.Free();
            }

            if (_failCallbackHandle.IsAllocated)
            {
                _failCallbackHandle.Free();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AppCallbackDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AppCallbackDelegateEvent(in AppEvent @event);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AppCallbackDelegateAnsiStringMessage(IntPtr message);
    }
}
