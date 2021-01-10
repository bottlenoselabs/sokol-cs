// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using lithiumtoast.NativeTools;

namespace Sokol
{
    /// <summary>
    ///     Defines the <see cref="AppEvent" /> types.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppEventType" /> is blittable to the C `sapp_event_type` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Same meaning as name of built in type.")]
    public enum AppEventType
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures.
        /// </summary>
        Invalid,

        /// <summary>
        ///     A keyboard key was pressed down.
        /// </summary>
        KeyDown,

        /// <summary>
        ///     A keyboard key was released.
        /// </summary>
        KeyUp,

        /// <summary>
        ///     A keyboard key for text was pressed down.
        /// </summary>
        Char,

        /// <summary>
        ///     A mouse button was pressed down.
        /// </summary>
        MouseDown,

        /// <summary>
        ///     A mouse button was released.
        /// </summary>
        MouseUp,

        /// <summary>
        ///     The scroll position of the mouse has changed.
        /// </summary>
        MouseScroll,

        /// <summary>
        ///     The position of the mouse has changed.
        /// </summary>
        MouseMove,

        /// <summary>
        ///     The position of the mouse has changed where the position was previously pointing outside the application but now
        ///     has entered the application.
        /// </summary>
        MouseEnter,

        /// <summary>
        ///     The position of the mouse where the position was previously pointing inside the application but now has left the
        ///     application.
        /// </summary>
        MouseLeave,

        /// <summary>
        ///     The start of one or more touch-points.
        /// </summary>
        TouchesBegan,

        /// <summary>
        ///     The position of one or more touch-points has changed.
        /// </summary>
        TouchesMoved,

        /// <summary>
        ///     The end of one or more touch-points.
        /// </summary>
        TouchesEnded,

        /// <summary>
        ///     The end of one or more touch-points.
        /// </summary>
        TouchesCancelled,

        /// <summary>
        ///     The size of the default <see cref="GraphicsPass" /> attachments has changed.
        /// </summary>
        Resized,

        /// <summary>
        ///     The application has been minimized.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Iconified" /> is only applicable for applications which have an active window such as
        ///         <see cref="NativeRuntimePlatform.Windows" />, <see cref="NativeRuntimePlatform.macOS" />, and
        ///         <see cref="NativeRuntimePlatform.Linux" />.
        ///     </para>
        /// </remarks>
        Iconified,

        /// <summary>
        ///     The application has been restored from being minimized.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Iconified" /> is only applicable for applications which have an active window such as
        ///         <see cref="NativeRuntimePlatform.Windows" />, <see cref="NativeRuntimePlatform.macOS" />, and
        ///         <see cref="NativeRuntimePlatform.Linux" />.
        ///     </para>
        /// </remarks>
        Restored,

        /// <summary>
        ///     The application has been moved to an in-active state.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Suspended" /> is traditionally only applicable for mobile applications such as
        ///         <see cref="NativeRuntimePlatform.Android" /> or <see cref="NativeRuntimePlatform.iOS" /> which can
        ///         switch to another active application such as an incoming call or text message.
        ///     </para>
        ///     <para>
        ///         A <see cref="Suspended" /> application continues to run but does not dispatch any
        ///         <see cref="AppEvent" />.
        ///     </para>
        /// </remarks>
        Suspended,

        /// <summary>
        ///     The application has moved from an in-active state back to an active state.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Resumed" /> is traditionally only applicable for mobile applications such as
        ///         <see cref="NativeRuntimePlatform.Android" /> or <see cref="NativeRuntimePlatform.iOS" /> which can
        ///         switch to another active application such as an incoming call or text message.
        ///     </para>
        /// </remarks>
        Resumed,

        /// <summary>
        ///     The application has an opportunity to change the cursor image.
        /// </summary>
        UpdateCursor,

        /// <summary>
        ///     A request to quit has been made.
        /// </summary>
        QuitRequested,

        /// <summary>
        ///     The clipboard has been pasted.
        /// </summary>
        ClipboardPasted
    }
}
