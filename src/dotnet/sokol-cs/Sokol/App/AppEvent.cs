// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     An application event which represents some kind of change in the state of the application such as
    ///     mouse state changes, keyboard state changes, etc.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppEventType" /> is blittable to the C `sapp_event` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 272, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public readonly unsafe struct AppEvent
    {
        /// <summary>
        ///     The current frame number of the application.
        /// </summary>
        [FieldOffset(0)]
        public readonly ulong FrameCount;

        /// <summary>
        ///     The <see cref="AppEventType" />.
        /// </summary>
        [FieldOffset(8)]
        public readonly AppEventType Type;

        /// <summary>
        ///     The <see cref="KeyCode" /> that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="KeyCode" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.KeyDown" /> or <see cref="AppEventType.KeyUp" />. In all other cases,
        ///         <see cref="KeyCode" /> has a default value of <see cref="AppKeyCode.Invalid" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(12)]
        public readonly AppKeyCode KeyCode;

        /// <summary>
        ///     The Unicode code point of the keyboard key that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="CharCode" /> only has meaningful data if <see cref="AppEvent.Type" /> is
        ///         <see cref="AppEventType.Char" />. In all other cases, <see cref="CharCode" /> has a default value of
        ///         <c>0</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(16)]
        public readonly uint CharCode;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the keyboard key that generated the
        ///     <see cref="AppEvent" /> is repeated.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="KeyRepeat" /> only has meaningful data if <see cref="AppEvent.Type" /> is
        ///         <see cref="AppEventType.KeyDown" />, <see cref="AppEventType.KeyUp" />, or <see cref="AppEventType.Char" />.
        ///         In all other cases, <see cref="KeyRepeat" /> has a default value of <c>false</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(20)]
        public readonly CBool KeyRepeat;

        /// <summary>
        ///     The keyboard key states that generated the <see cref="AppEvent" /> such as if the caps-lock, shift,
        ///     control, etc, have been pressed.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="KeyModifiers" /> only has meaningful data if <see cref="AppEvent.Type" /> is
        ///         <see cref="AppEventType.KeyDown" />, <see cref="AppEventType.KeyUp" />, or <see cref="AppEventType.Char" />.
        ///         In all other cases, <see cref="KeyModifiers" /> has a default value of <c>0</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(24)]
        public readonly uint KeyModifiers;

        /// <summary>
        ///     The <see cref="MouseButton" /> that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="MouseButton" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.MouseDown" />, <see cref="AppEventType.MouseUp" />,
        ///         <see cref="AppEventType.MouseScroll" />, <see cref="AppEventType.MouseMove" />,
        ///         <see cref="AppEventType.MouseEnter" />, or <see cref="AppEventType.MouseLeave" />.
        ///         In all other cases, <see cref="MouseButton" /> has a default value of
        ///         <see cref="AppMouseButton.Invalid" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(28)]
        public readonly AppMouseButton MouseButton;

        /// <summary>
        ///     The mouse <see cref="Vector2" /> position that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="MousePosition" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.MouseDown" />, <see cref="AppEventType.MouseUp" />,
        ///         <see cref="AppEventType.MouseScroll" />, <see cref="AppEventType.MouseMove" />,
        ///         <see cref="AppEventType.MouseEnter" />, or <see cref="AppEventType.MouseLeave" />.
        ///         In all other cases, <see cref="MousePosition" /> has a default value of <see cref="Vector2.Zero" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(32)]
        public readonly Vector2 MousePosition;

        /// <summary>
        ///     The mouse <see cref="Vector2" /> position delta that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="MousePosition" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.MouseDown" />, <see cref="AppEventType.MouseUp" />,
        ///         <see cref="AppEventType.MouseScroll" />, <see cref="AppEventType.MouseMove" />,
        ///         <see cref="AppEventType.MouseEnter" />, or <see cref="AppEventType.MouseLeave" />.
        ///         In all other cases, <see cref="MousePosition" /> has a default value of <see cref="Vector2.Zero" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(40)]
        public readonly Vector2 MousePositionDelta;

        /// <summary>
        ///     The mouse <see cref="Vector2" /> scroll position that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="ScrollPosition" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.MouseScroll" />. In all other cases, <see cref="ScrollPosition" /> has a
        ///         default value of <see cref="Vector2.Zero" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(48)]
        public readonly Vector2 ScrollPosition;

        /// <summary>
        ///     The number of touch points that generated the <see cref="AppEvent" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="TouchesCount" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="AppEventType.TouchesBegan" />, <see cref="AppEventType.TouchesMoved" />,
        ///         <see cref="AppEventType.TouchesEnded" /> or <see cref="AppEventType.TouchesCancelled" />. In all other
        ///         cases, <see cref="TouchesCount" /> has a default value of <c>0</c>.
        ///     </para>
        ///     <para>
        ///         If <see cref="TouchesCount" /> is <c>0</c>, it means that there are no touches.
        ///     </para>
        ///     <para>
        ///         <see cref="TouchesCount" /> can not exceed <see cref="AppConstants.MaximumTouchPointsCount" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(56)]
        public readonly int TouchesCount;

        [FieldOffset(64)]
        private readonly ulong _touches; /* size = 192, padding = 0 */ /* original type is `sapp_touchpoint [8]` */

        /// <summary>
        ///     Gets the <see cref="AppTouchPoint" />, by reference, given the specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="AppTouchPoint" /> by reference.</returns>
        /// <remarks>
        ///     <para>
        ///         <see cref="Touch" /> will only return meaningful data if <see cref="AppEvent.Type" /> is
        ///         <see cref="AppEventType.TouchesBegan" />, <see cref="AppEventType.TouchesMoved" />,
        ///         <see cref="AppEventType.TouchesEnded" /> or <see cref="AppEventType.TouchesCancelled" />. In all other
        ///         cases, <see cref="Touch" /> will return a default value of <c>default(TouchPoint)</c>.
        ///     </para>
        /// </remarks>
        public ref AppTouchPoint Touch(int index = 0)
        {
            fixed (AppEvent* @this = &this)
            {
                var pointer = (AppTouchPoint*)&@this->_touches;
                return ref *(pointer + index);
            }
        }

        /// <summary>
        ///     The width of the window.
        /// </summary>
        [FieldOffset(256)]
        internal readonly int WindowWidth;

        /// <summary>
        ///     The height of the window.
        /// </summary>
        [FieldOffset(260)]
        internal readonly int WindowHeight;

        /// <summary>
        ///     The width of the default <see cref="GraphicsPass" /> attachments.
        /// </summary>
        [FieldOffset(264)]
        internal readonly int FramebufferWidth;

        /// <summary>
        ///     The height of default <see cref="GraphicsPass" /> attachments.
        /// </summary>
        [FieldOffset(268)]
        internal readonly int FramebufferHeight;
    }
}
