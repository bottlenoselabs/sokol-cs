// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sokol.Graphics;

namespace Sokol.App
{
    /// <inheritdoc />
    /// <summary>
    ///     The base class for Sokol.NET applications using `sokol_app`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The <see cref="Application" /> class serves as a convenience for developers who are used to the
    ///         traditional object-oriented style of programming. Developers who do not wish to use the
    ///         <see cref="Application" /> class have the choice to use the static <see cref="App" /> class instead.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global", Justification = "OOP.")]
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "Public API.")]
    public abstract class Application : IDisposable
    {
        private readonly AppDescriptor _descriptor;

        /// <summary>
        ///     Gets the <see cref="GraphicsBackend" /> of the `sokol_app` application.
        /// </summary>
        /// <value>The current <see cref="GraphicsBackend" />.</value>
        public GraphicsBackend Backend => GraphicsDevice.Backend;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Application" /> class.
        /// </summary>
        /// <param name="descriptor">The <see cref="AppDescriptor" />.</param>
        /// <param name="backend">
        ///     The <see cref="GraphicsBackend" /> to use. If <c>null</c>, the default
        ///     <see cref="GraphicsBackend" /> will be used for current platform.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         If the <param name="descriptor" /> is provided, the
        ///         <see cref="AppDescriptor.InitializeCallback" />, <see cref="AppDescriptor.FrameCallback" />,
        ///         <see cref="AppDescriptor.CleanUpCallback" />, <see cref="AppDescriptor.EventCallback" />, and
        ///         <see cref="AppDescriptor.FailCallback" /> will be overriden.
        ///     </para>
        /// </remarks>
        protected Application(AppDescriptor? descriptor = null, GraphicsBackend? backend = null)
        {
            _descriptor = descriptor ?? default;
            LoadApi(backend);
            AddHooks();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Starts the application.
        /// </summary>
        public void Run()
        {
            App.Run(_descriptor);
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.DontCare" /> as the action.
        /// </summary>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public Pass BeginDefaultPass()
        {
            return App.BeginDefaultPass();
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction.Clear" /> as the action.
        /// </summary>
        /// <param name="clearColor">The color to clear the color attachments.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public Pass BeginDefaultPass(Rgba32F clearColor)
        {
            return App.BeginDefaultPass(clearColor);
        }

        /// <summary>
        ///     Begins and returns the frame buffer <see cref="Pass" /> with the specified width, height, and
        ///     <see cref="PassAction" />.
        /// </summary>
        /// <param name="passAction">The frame buffer pass action.</param>
        /// <returns>The frame buffer <see cref="Pass" />.</returns>
        public Pass BeginDefaultPass([In] ref PassAction passAction)
        {
            return App.BeginDefaultPass(ref passAction);
        }

        /// <summary>
        ///     Called when the application window, 3D rendering context, and swapchain have been created and the
        ///     application should create any resources.
        /// </summary>
        protected virtual void CreateResources()
        {
        }

        /// <summary>
        ///     Called when the application is about to quit and should destroy any resources.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you don't explicitly destroy graphics resources, such as <see cref="Graphics.Buffer" />,
        ///         <see cref="Image" />, <see cref="Pass" />, <see cref="Pipeline" />, and <see cref="Shader" />, they
        ///         are implicitly destroyed when the application quits gracefully.
        ///     </para>
        /// </remarks>
        protected virtual void DestroyResources()
        {
        }

        /// <summary>
        ///     Called when the application should update per-frame state and perform all rendering.
        /// </summary>
        protected abstract void Frame();

        /// <summary>
        ///     Called when the application's state changed such as when the mouse state changes, keyboard state
        ///     changes, etc.
        /// </summary>
        /// <param name="event">The <see cref="HandleEvent" />.</param>
        protected virtual void HandleEvent(in Event @event)
        {
        }

        /// <summary>
        ///     Called when the dimensions of the <see cref="Framebuffer" /> have changed.
        /// </summary>
        /// <param name="width">The new width.</param>
        /// <param name="height">The new height.</param>
        protected virtual void Resized(int width, int height)
        {
        }

        /// <summary>
        ///     Called when the application encounters an error.
        /// </summary>
        /// <param name="message">The error message.</param>
        protected virtual void HandleError(string message)
        {
        }

        /// <inheritdoc cref="IDisposable" />
        [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "IDisposable.")]
        protected virtual void Dispose(bool disposing)
        {
            Native.UnloadApi();
            Graphics.Native.UnloadApi();
            RemoveHooks();
        }

        private static void LoadApi(GraphicsBackend? backend)
        {
            var backend1 = backend ?? GraphicsHelper.DefaultBackend();
            Native.LoadApi(backend1);
        }

        private void AddHooks()
        {
            App.CreateResources += CreateResources;
            App.DestroyResources += DestroyResources;
            App.Frame += Frame;
            App.Event += HandleEvent;
            App.Resized += Resized;
            App.Error += HandleError;
        }

        private void RemoveHooks()
        {
            App.CreateResources -= CreateResources;
            App.DestroyResources -= DestroyResources;
            App.Frame -= Frame;
            App.Event -= HandleEvent;
            App.Resized -= Resized;
            App.Error -= HandleError;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Application" /> class.
        /// </summary>
        ~Application()
        {
            Dispose(false);
        }
    }
}
