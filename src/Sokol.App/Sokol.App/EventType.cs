// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.App
{
    /// <summary>
    ///     Defines the different types of an <see cref="Event" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public enum EventType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures.
        /// </summary>
        Invalid,

        /// <summary>
        ///     The <see cref="Event" /> represents a key down state of the active keyboard device.
        /// </summary>
        KeyDown,

        /// <summary>
        ///     The <see cref="Event" /> represents a key up state of the active keyboard device.
        /// </summary>
        KeyUp,

        /// <summary>
        ///     The <see cref="Event" /> represents a key press state of the active keyboard device.
        /// </summary>
        Char,

        /// <summary>
        ///     The <see cref="Event" /> represents a down button state of the active pointing device.
        /// </summary>
        MouseDown,

        /// <summary>
        ///     The <see cref="Event" /> represents a up button state of the active pointing device.
        /// </summary>
        MouseUp,

        /// <summary>
        ///     The <see cref="Event" /> represents a change in scroll position of the active pointing device.
        /// </summary>
        MouseScroll,

        /// <summary>
        ///     The <see cref="Event" /> represents a change in position of the active pointing device.
        /// </summary>
        MouseMove,

        /// <summary>
        ///     The <see cref="Event" /> represents a change in position of the active pointing device where the
        ///     position was previously pointing outside the application but now has entered the application.
        /// </summary>
        MouseEnter,

        /// <summary>
        ///     The <see cref="Event" /> represents a change in position of the active pointing device where the
        ///     position was previously pointing inside the application but now has left the application.
        /// </summary>
        MouseLeave,

        /// <summary>
        ///     The <see cref="Event" /> represents the start of one or more touch-points with active touch-screen
        ///     device.
        /// </summary>
        TouchesBegan,

        /// <summary>
        ///     The <see cref="Event" /> represents the change in position of one or more touch-points with active
        ///     touch-screen device.
        /// </summary>
        TouchesMoved,

        /// <summary>
        ///     The <see cref="Event" /> represents the end of one or more touch-points with active touch-screen device.
        /// </summary>
        TouchesEnded,

        /// <summary>
        ///     The <see cref="Event" /> represents the end of one or more touch-points with active touch-screen device.
        /// </summary>
        TouchesCancelled,

        /// <summary>
        ///     The <see cref="Event" /> represents a change in the dimensions of the <see cref="Framebuffer" />.
        /// </summary>
        Resized,

        /// <summary>
        ///     The <see cref="Event" /> represents the application being minimized.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Iconified" /> is only applicable for applications which have an active window such as
        ///         <see cref="GraphicsPlatform.Windows" />, <see cref="GraphicsPlatform.macOS" />, and
        ///         <see cref="GraphicsPlatform.Linux" />.
        ///     </para>
        /// </remarks>
        Iconified,

        /// <summary>
        ///     The <see cref="Event" /> represents the application the window has been restored from a minimized state.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Restored" /> is only applicable for applications which have an active window such as
        ///         <see cref="GraphicsPlatform.Windows" />, <see cref="GraphicsPlatform.macOS" />, and
        ///         <see cref="GraphicsPlatform.Linux" />.
        ///     </para>
        /// </remarks>
        Restored,

        /// <summary>
        ///     The <see cref="Event" /> represents when the application has been moved to an in-active state.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Suspended" /> is traditionally only applicable for mobile applications such as
        ///         <see cref="GraphicsPlatform.Android" /> or <see cref="GraphicsPlatform.iOS" /> which can
        ///         switch to another active application such as an incoming call or SMS message.
        ///     </para>
        ///     <para>
        ///         A <see cref="Suspended" /> application continues to run but does not dispatch any
        ///         <see cref="Event" />.
        ///     </para>
        /// </remarks>
        Suspended,

        /// <summary>
        ///     The <see cref="Event" /> represents when the application has been moved back to an active state.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Resumed" /> is traditionally only applicable for mobile applications such as
        ///         <see cref="GraphicsPlatform.Android" /> or <see cref="GraphicsPlatform.iOS" /> which can
        ///         switch to another active application such as an incoming call or SMS message.
        ///     </para>
        /// </remarks>
        Resumed,

        /// <summary>
        ///     The <see cref="Event" /> represents when the application is expected to change the cursor image.
        /// </summary>
        UpdateCursor,

        /// <summary>
        ///     The <see cref="Event" /> represents a request to quit the application.
        /// </summary>
        QuitRequested,

        /// <summary>
        ///     The <see cref="Event" /> represents a paste operation of the current clipboard.
        /// </summary>
        ClipboardPasted
    }
}
