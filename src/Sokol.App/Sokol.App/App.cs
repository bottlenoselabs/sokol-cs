// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sokol.Graphics;
using static Sokol.App.PInvoke;

namespace Sokol.App
{
    /// <summary>
    ///     The static methods of `sokol_app`.
    /// </summary>
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public static class App
    {
        /// <summary>
        ///     The default delegate for `sokol_app`.
        /// </summary>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppDelegate();

        /// <summary>
        ///     The event delegate for `sokol_app`.
        /// </summary>
        /// <param name="event">The <see cref="Event" />.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppEventDelegate(in Event @event);

        /// <summary>
        ///     The resized delegate for `sokol_app`.
        /// </summary>
        /// <param name="width">The new width of the <see cref="Framebuffer" />.</param>
        /// <param name="height">The new height of the <see cref="Framebuffer" />.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppResizedDelegate(int width, int height);

        /// <summary>
        ///     The message delegate for `sokol_app`.
        /// </summary>
        /// <param name="str">The string.</param>
        [SuppressMessage("ReSharper", "SA1202", Justification = "Delegates are details.")]
        public delegate void AppStringDelegate(string str);

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
        public static GraphicsBackend Backend => GraphicsDevice.Backend;

        /// <summary>
        ///     Occurs when the application window, 3D rendering context, and
        ///     swapchain have been created and the application should create any resources.
        /// </summary>
        public static event AppDelegate? CreateResources;

        /// <summary>
        ///     Occurs before the application quits and the application should destroy any resources.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you don't explicitly destroy graphics resources, such as <see cref="Graphics.Buffer" />,
        ///         <see cref="Image" />, <see cref="Pass" />, <see cref="Pipeline" />, and <see cref="Shader" />, they
        ///         are implicitly destroyed when the application quits gracefully.
        ///     </para>
        /// </remarks>
        public static event AppDelegate? DestroyResources;

        /// <summary>
        ///     Occurs when the application should update per-frame state and perform all rendering.
        /// </summary>
        public static event AppDelegate? Frame;

        /// <summary>
        ///     Occurs when the application's state changed such as when the mouse state changes, keyboard state
        ///     changes, etc.
        /// </summary>
        public static event AppEventDelegate? Event;

        /// <summary>
        ///     Occurs when the application encounters an error.
        /// </summary>
        public static event AppStringDelegate? Error;

        /// <summary>
        ///     Occurs when the <see cref="Framebuffer.Width" /> or the <see cref="Framebuffer.Height" /> change.
        /// </summary>
        public static event AppResizedDelegate? Resized;

        /// <summary>
        ///     Starts the application.
        /// </summary>
        /// <param name="descriptor">The <see cref="AppDescriptor" />.</param>
        /// <remarks>
        ///     <para>
        ///         If the
        ///         <param name="descriptor" />
        ///         is provided, the
        ///         <see cref="AppDescriptor.InitializeCallback" />, <see cref="AppDescriptor.FrameCallback" />,
        ///         <see cref="AppDescriptor.CleanUpCallback" />, <see cref="AppDescriptor.EventCallback" />, and
        ///         <see cref="AppDescriptor.FailCallback" /> will be overriden.
        ///     </para>
        /// </remarks>
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global", Justification = "C# wrapper.")]
        public static void Run(AppDescriptor? descriptor = null)
        {
            var appDesc = descriptor ?? default;
            FillDescriptor(ref appDesc);
            sapp_run(ref appDesc);
        }

        /// <summary>
        ///     Requests a quit of the application.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Calling <see cref="RequestQuit" /> sends the <see cref="EventType.QuitRequested" /> to the
        ///         <see cref="EventCallback" />. This allows the quit process to be cancelled by user code.
        ///     </para>
        /// </remarks>
        public static void RequestQuit()
        {
            sapp_request_quit();
        }

        /// <summary>
        ///     Quits the application immediately.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Calling <see cref="Quit" /> does not send the <see cref="EventType.QuitRequested" /> to the
        ///         <see cref="EventCallback" />.
        ///     </para>
        /// </remarks>
        public static void Quit()
        {
            sapp_quit();
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.DontCare" /> as the action.
        /// </summary>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass()
        {
            return GraphicsDevice.BeginDefaultPass(sapp_width(), sapp_height());
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass(Rgba32F clearColor)
        {
            return GraphicsDevice.BeginDefaultPass(sapp_width(), sapp_height(), clearColor);
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction" />.
        /// </summary>
        /// <param name="passAction">The frame buffer pass action.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public static Pass BeginDefaultPass([In] ref PassAction passAction)
        {
            return GraphicsDevice.BeginDefaultPass(sapp_width(), sapp_height(), ref passAction);
        }

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
            UnmanagedStringMemoryManager.Clear();
            Resized?.Invoke(Framebuffer.Width, Framebuffer.Height);
        }

        private static void FrameCallback()
        {
            Frame?.Invoke();
        }

        private static void CleanUpCallback()
        {
            ReleaseUnmanagedResources();
            DestroyResources?.Invoke();
            GraphicsDevice.Shutdown();
        }

        private static void EventCallback(in Event @event)
        {
            if (@event.Type == EventType.Resized)
            {
                Resized?.Invoke(@event.FramebufferWidth, @event.FramebufferHeight);
            }

            Event?.Invoke(in @event);
        }

        private static void FailCallback(IntPtr ansiStringPointer)
        {
            var message = UnmanagedStringMemoryManager.GetString(ansiStringPointer);
            Error?.Invoke(message);
            UnmanagedStringMemoryManager.Clear(ansiStringPointer);
        }

        private static void InitializeGraphics()
        {
            var graphicsDescriptor = default(GraphicsDescriptor);
            graphicsDescriptor.Context = sapp_sgcontext();

            GraphicsDevice.Setup(ref graphicsDescriptor);
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
        private delegate void AppCallbackDelegateEvent(in Event @event);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AppCallbackDelegateAnsiStringMessage(IntPtr message);
    }
}
