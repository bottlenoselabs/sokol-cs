// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Sokol.Graphics;
using static Sokol.App.PInvoke;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeInternal
// ReSharper disable EventNeverSubscribedTo.Global
#pragma warning disable 67

namespace Sokol.App
{
    /// <summary>
    ///     The base class for Sokol.NET applications that use `sokol_app`.
    /// </summary>
    public abstract class App
    {
        [SuppressMessage("ReSharper", "SA1401", Justification = "Internal")]
        internal static App Instance = null!;

        private static int _isInitialized;

        private static GCHandle _getMetalRenderPassDescriptorCallbackHandle;
        private static GCHandle _getMetalDrawableCallbackHandle;

        private static GCHandle _initializeCallbackHandle;
        private static GCHandle _frameCallbackHandle;
        private static GCHandle _cleanUpCallbackHandle;
        private static GCHandle _eventCallbackHandle;

        /// <summary>
        ///     Gets the current <see cref="GraphicsBackend" />.
        /// </summary>
        /// <value>The current <see cref="GraphicsBackend" />.</value>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public GraphicsBackend Backend => GraphicsDevice.Backend;

        public int Width => sapp_width();

        public int Height => sapp_height();

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class using an optional <see cref="GraphicsBackend " />.
        /// </summary>
        /// <param name="backend">
        ///     The requested back-end for the app. May be overriden if <paramref name="backend" />doesn't make
        ///     sense for the platform.
        /// </param>
        protected App(GraphicsBackend? backend = null)
        {
            if (Interlocked.CompareExchange(ref _isInitialized, 1, 0) == 1)
            {
                throw new InvalidOperationException("Another application has already been initialized.");
            }

            Instance = this;

            var backend1 = backend ?? GraphicsHelper.DefaultBackend();
            Sokol.Graphics.Native.LoadApi(backend1);
            Native.LoadApi(backend1);
        }

        /// <summary>
        ///     Starts the <see cref="App" />.
        /// </summary>
        public void Run()
        {
            var appDesc = default(AppDescriptor);
            CreateCallbacks(ref appDesc);
            sapp_run(ref appDesc);
        }

        public void RequestQuit()
        {
            sapp_request_quit();
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

        protected virtual void Initialize()
        {
        }

        protected abstract void Frame();

        protected virtual void Event(ref Event @event)
        {
        }

        protected virtual void CleanUp()
        {
        }

        private static void CreateCallbacks(ref AppDescriptor desc)
        {
            var initializeCallbackDelegate = new AppCallbackDelegateVoid(InitializeCallback);
            _initializeCallbackHandle = GCHandle.Alloc(initializeCallbackDelegate);
            desc.InitializeCallback = Marshal.GetFunctionPointerForDelegate(initializeCallbackDelegate);

            var frameCallbackDelegate = new AppCallbackDelegateVoid(FrameCallback);
            _frameCallbackHandle = GCHandle.Alloc(frameCallbackDelegate);
            desc.FrameCallback = Marshal.GetFunctionPointerForDelegate(frameCallbackDelegate);

            var cleanUpCallbackDelegate = new AppCallbackDelegateVoid(CleanUpCallback);
            _cleanUpCallbackHandle = GCHandle.Alloc(cleanUpCallbackDelegate);
            desc.CleanUpCallback = Marshal.GetFunctionPointerForDelegate(cleanUpCallbackDelegate);

            var eventCallbackDelegate = new AppCallbackDelegateVoidEvent(EventCallback);
            _eventCallbackHandle = GCHandle.Alloc(eventCallbackDelegate);
            desc.EventCallback = Marshal.GetFunctionPointerForDelegate(eventCallbackDelegate);
        }

        private static IntPtr GetMetalRenderPassDescriptorCallback()
        {
            return sapp_metal_get_renderpass_descriptor();
        }

        private static IntPtr GetMetalDrawableCallback()
        {
            return sapp_metal_get_drawable();
        }

        private static void InitializeCallback()
        {
            InitializeGraphics();
            Instance.Initialize();
        }

        private static void FrameCallback()
        {
            Instance.Frame();
        }

        private static void CleanUpCallback()
        {
            Instance.CleanUp();
            GraphicsDevice.Shutdown();
        }

        private static void EventCallback(ref Event @event)
        {
            Instance.Event(ref @event);
        }

        private static void InitializeGraphics()
        {
            var getMetalRenderPassDescriptorCallbackDelegate =
                new AppCallbackDelegateIntPtr(GetMetalRenderPassDescriptorCallback);
            _getMetalRenderPassDescriptorCallbackHandle = GCHandle.Alloc(getMetalRenderPassDescriptorCallbackDelegate);
            var getMetalRenderPassDescriptor =
                Marshal.GetFunctionPointerForDelegate(getMetalRenderPassDescriptorCallbackDelegate);

            var getMetalDrawableCallbackDelegate = new AppCallbackDelegateIntPtr(GetMetalDrawableCallback);
            _getMetalDrawableCallbackHandle = GCHandle.Alloc(getMetalDrawableCallbackDelegate);
            var getMetalDrawableCallback = Marshal.GetFunctionPointerForDelegate(getMetalDrawableCallbackDelegate);

            var graphicsDescriptor = default(GraphicsDescriptor);
            ref var context = ref graphicsDescriptor.Context;
            context.ColorFormat = (PixelFormat)sapp_color_format();
            context.DepthFormat = (PixelFormat)sapp_depth_format();
            context.SampleCount = sapp_sample_count();
            context.GL.ForceGLES2 = sapp_gles2();
            context.Metal.Device = sapp_metal_get_device();
            context.Metal.RenderPassDescriptorCallback = getMetalRenderPassDescriptor;
            context.Metal.DrawableCallback = getMetalDrawableCallback;
            // desc.Direct3D11.device = sapp_d3d11_get_device();
            // desc.Direct3D11.device_context = sapp_d3d11_get_device_context();
            // desc.Direct3D11.render_target_view_cb = sapp_d3d11_get_render_target_view;
            // desc.Direct3D11.depth_stencil_view_cb = sapp_d3d11_get_depth_stencil_view;
            // desc.WebGPU.Device = sapp_wgpu_get_device();
            // desc.WebGPU.RenderViewCallback = sapp_wgpu_get_render_view();
            // desc.WebGPU.ResolveViewCallback = sapp_wgpu_get_resolve_view();
            // desc.WebGPU.DepthStencilViewCallback = sapp_wgpu_get_depth_stencil_view();

            GraphicsDevice.Setup(ref graphicsDescriptor);
        }
    }
}
