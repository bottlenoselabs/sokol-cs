// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;

namespace Sokol
{
    /// <summary>
    ///     Parameters for initializing `sokol_app`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppDescriptor" /> is blittable to the C `sapp_desc` struct found in `sokol_app`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 160, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "SA1202", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public unsafe struct AppDescriptor
    {
        /// <summary>
        ///     Called once after the application window, 3D rendering context, and swapchain have been created. The
        ///     call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr InitializeCallback;

        /// <summary>
        ///     Called once per-frame which is usually called 60 times per second. This is where your application would
        ///     update per-frame state and perform all rendering. The call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr FrameCallback;

        /// <summary>
        ///     Called once right before the application quits. The call-back takes no arguments and has no return value.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr CleanUpCallback;

        /// <summary>
        ///     Called once per-event such as when the mouse state changes, keyboard state changes, etc. If you don't
        ///     need to handle events, leave this as the default value of <see cref="IntPtr.Zero" />. The call-back has
        ///     one out argument of <see cref="AppEvent" /> (by reference using the <c>ref</c> keyword) and has no return
        ///     value.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr EventCallback;

        /// <summary>
        ///     Called once per-error which doesn't allow the application to continue. The call-back has one out argument
        ///     of <see cref="IntPtr" /> for an C string of the message and has no return value.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr FailCallback;

        /// <summary>
        ///     A pointer to user-data that is used for the call-backs which have a user-data out argument.
        ///     Default is <see cref="IntPtr.Zero" />.
        /// </summary>
        [FieldOffset(40)]
        public IntPtr UserData;

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

        /// <summary>
        ///     The preferred width of the 3D rendering window or canvas. Default is <c>640</c>.
        /// </summary>
        [FieldOffset(88)]
        public int Width;

        /// <summary>
        ///     The preferred height of the 3D rendering window or canvas. Default is <c>480</c>.
        /// </summary>
        [FieldOffset(92)]
        public int Height;

        /// <summary>
        ///     The multi-sample anti-aliasing (MSAA) sample count of the default <see cref="GraphicsPass" /> attachments. Default
        ///     is <c>1</c>.
        /// </summary>
        [FieldOffset(96)]
        public int SampleCount;

        /// <summary>
        ///     The preferred swap interval. Default value is <c>1</c>.
        /// </summary>
        [FieldOffset(100)]
        public int SwapInterval;

        /// <summary>
        ///     A <see cref="bool" /> indicating whether the default <see cref="GraphicsPass" /> attachments are currently
        ///     using full-resolution for displays with increased pixel density (dots-per-inch). Default value is <c>false</c>.
        /// </summary>
        [FieldOffset(104)]
        public CBool IsHighDpi;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the rendering window should be created in full-screen mode.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(105)]
        public CBool IsFullScreen;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the default <see cref="GraphicsPass" /> color attachments should
        ///     have an alpha channel. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(106)]
        public CBool AlphaIsEnabled;

        [FieldOffset(112)]
        private byte* _windowTitle;

        /// <summary>
        ///     Gets or sets the window title. Default is "sokol_app".
        /// </summary>
        /// <value>The window title.</value>
        public string WindowTitle
        {
            readonly get => Native.GetStringFromBytePointer(_windowTitle);
            set => _windowTitle = Native.GetBytePointerFromString(value);
        }

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the window cursor image is to be custom handled when
        ///     <see cref="App.Event" /> is invoked with an event of the <see cref="AppEventType.UpdateCursor" />.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(120)]
        public CBool UseCustomCursor;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether copying and pasting data using the clipboard is enabled.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(121)]
        public CBool IsEnabledClipboard;

        /// <summary>
        ///     The maximum size (in bytes) of clipboard data. If <see cref="IsEnabledClipboard" /> is <c>true</c>,
        ///     default is <c>8192</c> (8 KB); otherwise default is <c>0</c>.
        /// </summary>
        [FieldOffset(124)]
        public int ClipboardSize;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(128)]
        public CBool IsEnabledDragNDrop;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(132)]
        public int MaximumDroppedFiles;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(136)]
        public int MaximumDroppedFilePathLength;

        [FieldOffset(144)]
        private byte* _html5CanvasName;

        /// <summary>
        ///     Gets or sets the name (id) of the HTML5 canvas element. Default is "canvas".
        /// </summary>
        /// <value>The window title.</value>
        public string Html5CanvasName
        {
            readonly get => Native.GetStringFromBytePointer(_html5CanvasName);
            set => _html5CanvasName = Native.GetBytePointerFromString(value);
        }

        /// <summary>
        ///     A <see cref="bool" /> value, which if <c>true</c>, the HTML canvas is resized to <see cref="Width" />
        ///     and <see cref="Height" /> and is fixed for the rest of application life-time; otherwise, the HTML canvas
        ///     is tracked and <see cref="App.Width" /> and <see cref="App.Height" /> are updated to the
        ///     size of the canvas when resized. Default is <c>false</c>.
        /// </summary>
        [FieldOffset(152)]
        public CBool Html5CanvasResize;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to preserve the default <see cref="GraphicsPass" /> attachments
        ///     between frames.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(153)]
        public CBool Html5PreserveDrawingBuffer;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether the rendered pixels use pre-multiplied alpha convention.
        ///     Default is <c>false</c>.
        /// </summary>
        [FieldOffset(154)]
        public CBool Html5PreMultipliedAlpha;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to display the standard "Leave Site?" dialog box when
        ///     <see cref="App.RequestQuit" /> is called.
        /// </summary>
        [FieldOffset(155)]
        public CBool Html5AskLeaveSite;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether displaying the iOS on-screen keyboard shrinks the
        ///     <see cref="App.Width" /> or <see cref="App.Height" /> depending on the device
        ///     orientation.
        /// </summary>
        [FieldOffset(156)]
        [SuppressMessage("ReSharper", "SA1305", Justification = "Brand name.")]
        [SuppressMessage("ReSharper", "SA1307", Justification = "Brand name.")]
        public CBool iOSKeyboardResizesCanvas;

        /// <summary>
        ///     A <see cref="bool" /> value indicating whether to setup <see cref="GraphicsBackend.OpenGLES2" /> even if
        ///     <see cref="GraphicsBackend.OpenGLES3" /> is available.
        /// </summary>
        [FieldOffset(157)]
        public CBool ForceOpenGLES2;
    }
}
