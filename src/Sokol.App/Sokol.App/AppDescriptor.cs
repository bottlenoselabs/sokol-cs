// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    /// <summary>
    ///     Parameters for initializing `sokol_app`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppDescriptor" /> is blittable to the C `sapp_desc` struct found in `sokol_app`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 144, Pack = 8)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public struct AppDescriptor
    {
        /// <summary>
        ///     A pointer to user-data that is used for the call-backs which have a user-data out argument.
        ///     Default is <see cref="IntPtr.Zero" />.
        /// </summary>
        [FieldOffset(40)] /* size = 8, padding = 0 */
        public IntPtr UserData;

        /// <summary>
        ///     The preferred width of the 3D rendering window or canvas. Default is <c>640</c>.
        /// </summary>
        [FieldOffset(88)] /* size = 4, padding = 0 */
        public int Width;

        /// <summary>
        ///     The preferred height of the 3D rendering window or canvas. Default is <c>480</c>.
        /// </summary>
        [FieldOffset(92)] /* size = 4, padding = 0 */
        public int Height;

        /// <summary>
        ///     The multi-sample anti-aliasing (MSAA) sample count of the default framebuffer. Default is <c>1</c>.
        /// </summary>
        [FieldOffset(96)] /* size = 4, padding = 0 */
        public int SampleCount;

        /// <summary>
        ///     The preferred swap interval. Default value is <c>1</c>.
        /// </summary>
        [FieldOffset(100)] /* size = 4, padding = 0 */
        public int SwapInterval;

        /// <summary>
        ///     Sets <see cref="Framebuffer.IsHighDpi" />. Default value is <c>false</c>.
        /// </summary>
        [FieldOffset(104)] /* size = 1, padding = 0 */
        public BlittableBoolean IsHighDpi;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the rendering window should be created in full-screen mode.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(105)] /* size = 1, padding = 0 */
        public BlittableBoolean IsFullScreen;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the <see cref="Framebuffer" /> should have an alpha channel.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(106)] /* size = 1, padding = 5 */
        public BlittableBoolean FramebufferAlphaIsEnabled;

        [FieldOffset(112)] /* size = 8, padding = 0 */
        private IntPtr _windowTitle;

        /// <summary>
        ///     Gets or sets the window title. Default is "sokol_app".
        /// </summary>
        /// <value>The window title.</value>
        public string WindowTitle
        {
            readonly get => UnmanagedStringMemoryManager.GetString(_windowTitle);
            set => _windowTitle = UnmanagedStringMemoryManager.SetString(value);
        }

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the window cursor image is to be custom handled when
        ///     <see cref="App.Event" /> is invoked with an event of the <see cref="EventType.UpdateCursor" />.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(120)] /* size = 1, padding = 0 */
        public BlittableBoolean UseCustomCursor;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether copying and pasting data using the clipboard is enabled.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(121)] /* size = 1, padding = 2 */
        public BlittableBoolean ClipboardIsEnabled;

        /// <summary>
        ///     The maximum size (in bytes) of clipboard data. If <see cref="ClipboardIsEnabled" /> is <c>true</c>,
        ///     default is <c>8192</c> (8 KB); otherwise default is <c>0</c>.
        /// </summary>
        [FieldOffset(124)] /* size = 4, padding = 0 */
        public int ClipboardSize;

        [FieldOffset(128)] /* size = 8, padding = 0 */
        private IntPtr _html5CanvasName;

        /// <summary>
        ///     Gets or sets the name (id) of the HTML5 canvas element. Default is "canvas".
        /// </summary>
        /// <value>The window title.</value>
        public string Html5CanvasName
        {
            readonly get => UnmanagedStringMemoryManager.GetString(_html5CanvasName);
            set => _html5CanvasName = UnmanagedStringMemoryManager.SetString(value);
        }

        /// <summary>
        ///     A <see cref="bool" /> value, which if <c>true</c>, the HTML canvas is resized to <see cref="Width" />
        ///     and <see cref="Height" /> and is fixed for the rest of application life-time; otherwise, the HTML canvas
        ///     is tracked and <see cref="Framebuffer.Width" /> and <see cref="Framebuffer.Height" /> are updated to the
        ///     size of the canvas when resized. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(136)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5CanvasResize;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to preserve the framebuffer content between frames.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(137)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5PreserveDrawingBuffer;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the rendered pixels use pre-multiplied alpha convention.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(138)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5PreMultipliedAlpha;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to display the standard "Leave Site?" dialog box when
        ///     <see cref="App.RequestQuit" /> is called.
        /// </summary>
        [FieldOffset(139)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5AskLeaveSite;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether displaying the iOS on-screen keyboard shrinks the
        ///     <see cref="Framebuffer.Width" /> or <see cref="Framebuffer.Height" /> depending on the device
        ///     orientation.
        /// </summary>
        [FieldOffset(140)] /* size = 1, padding = 0 */
        [SuppressMessage("ReSharper", "SA1305", Justification = "Brand name.")]
        [SuppressMessage("ReSharper", "SA1307", Justification = "Brand name.")]
        public BlittableBoolean iOSKeyboardResizesCanvas;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to setup <see cref="GraphicsBackend.OpenGLES2" /> even if
        ///     <see cref="GraphicsBackend.OpenGLES3" /> is available.
        /// </summary>
        [FieldOffset(141)] /* size = 1, padding = 2 */
        public BlittableBoolean ForceOpenGLES2;

        /// <summary>
        ///     Called once after the application window, 3D rendering context, and swapchain have been created. The
        ///     call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(0)] /* size = 8, padding = 0 */
        internal IntPtr InitializeCallback;

        /// <summary>
        ///     Called once per-frame which is usually called 60 times per second. This is where your application would
        ///     update per-frame state and perform all rendering. The call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(8)] /* size = 8, padding = 0 */
        internal IntPtr FrameCallback;

        /// <summary>
        ///     Called once right before the application quits. The call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(16)] /* size = 8, padding = 0 */
        internal IntPtr CleanUpCallback;

        /// <summary>
        ///     Called once per-event such as when the mouse state changes, keyboard state changes, etc. If you don't
        ///     need to handle events, leave this as the default value of <see cref="IntPtr.Zero" />. The call-back has
        ///     one out argument of <see cref="Event" /> (by reference using the <c>ref</c> keyword) and has no return
        ///     value.
        /// </summary>
        [FieldOffset(24)] /* size = 8, padding = 0 */
        internal IntPtr EventCallback;

        /// <summary>
        ///     Called once per-error which doesn't allow the application to continue. The call-back has one out argument
        ///     of <see cref="IntPtr" /> for an ANSI string of the message and has no return value.
        /// </summary>
        [FieldOffset(32)] /* size = 8, padding = 0 */
        internal IntPtr FailCallback;

        /// <summary>
        ///     Equivalent of <see cref="InitializeCallback" /> except it has one out argument of <see cref="IntPtr" />
        ///     for the <see cref="UserData" />.
        /// </summary>
        [FieldOffset(48)] /* size = 8, padding = 0 */
        internal IntPtr InitializeUserDataCallback;

        /// <summary>
        ///     Equivalent of <see cref="FrameCallback" /> except it has one out argument of <see cref="IntPtr" />
        ///     for the <see cref="UserData" />.
        /// </summary>
        [FieldOffset(56)] /* size = 8, padding = 0 */
        internal IntPtr FrameUserDataCallback;

        /// <summary>
        ///     Equivalent of <see cref="CleanUpCallback" /> except it has one out argument of <see cref="IntPtr" />
        ///     for the <see cref="UserData" />.
        /// </summary>
        [FieldOffset(64)] /* size = 8, padding = 0 */
        internal IntPtr CleanUpUserDataCallback;

        /// <summary>
        ///     Equivalent of <see cref="EventCallback" /> except it has one additional out argument of
        ///     <see cref="IntPtr" /> for the <see cref="UserData" />.
        /// </summary>
        [FieldOffset(72)] /* size = 8, padding = 0 */
        internal IntPtr EventUserDataCallback;

        /// <summary>
        ///     Equivalent of <see cref="FailCallback" /> except it has one additional out argument of
        ///     <see cref="IntPtr" /> for the <see cref="UserData" />.
        /// </summary>
        [FieldOffset(80)] /* size = 8, padding = 0 */
        internal IntPtr FailUserDataCallback;
    }
}
