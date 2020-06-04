// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    /// <summary>
    ///     An application event which represents some kind of change in the state of the application such as
    ///     mouse state changes, keyboard state changes, etc.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public readonly unsafe struct Event
    {
        /// <summary>
        ///     The current frame number of the application.
        /// </summary>
        [FieldOffset(0)]
        public readonly ulong FrameCount;

        /// <summary>
        ///     The <see cref="EventType" />.
        /// </summary>
        [FieldOffset(8)]
        public readonly EventType Type;

        /// <summary>
        ///     The <see cref="Key" /> that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Key" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.KeyDown" /> or <see cref="EventType.KeyUp" />. In all other cases,
        ///         <see cref="Key" /> has a default value of <see cref="KeyboardKey.Invalid" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(12)]
        public readonly KeyboardKey Key;

        /// <summary>
        ///     The Unicode character code of the keyboard key that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="Char" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.Char" />. In all other cases, <see cref="Char" /> has a default value of
        ///         <c>0</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(16)]
        public readonly uint Char;

        /// <summary>
        ///     A <see cref="bool" /> value that indicates whether the keyboard key that generated the
        ///     <see cref="Event" /> is repeated.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="KeyRepeat" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.KeyDown" />, <see cref="EventType.KeyUp" />, or <see cref="EventType.Char" />.
        ///         In all other cases, <see cref="KeyRepeat" /> has a default value of <c>false</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(20)]
        public readonly BlittableBoolean KeyRepeat;

        /// <summary>
        ///     The keyboard key states that generated the <see cref="Event" /> such as if the caps-lock, shift,
        ///     control, etc, have been pressed.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="KeyModifiers" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.KeyDown" />, <see cref="EventType.KeyUp" />, or <see cref="EventType.Char" />.
        ///         In all other cases, <see cref="KeyModifiers" /> has a default value of <c>0</c>.
        ///     </para>
        /// </remarks>
        [FieldOffset(24)]
        public readonly uint KeyModifiers;

        /// <summary>
        ///     The <see cref="MouseButton" /> that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="MouseButton" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.MouseDown" />, <see cref="EventType.MouseUp" />,
        ///         <see cref="EventType.MouseScroll" />, <see cref="EventType.MouseMove" />,
        ///         <see cref="EventType.MouseEnter" />, or <see cref="EventType.MouseLeave" />.
        ///         In all other cases, <see cref="MouseButton" /> has a default value of
        ///         <see cref="Sokol.App.MouseButton.Invalid" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(28)]
        public readonly MouseButton MouseButton;

        /// <summary>
        ///     The mouse <see cref="Vector2" /> position that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="MousePosition" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.MouseDown" />, <see cref="EventType.MouseUp" />,
        ///         <see cref="EventType.MouseScroll" />, <see cref="EventType.MouseMove" />,
        ///         <see cref="EventType.MouseEnter" />, or <see cref="EventType.MouseLeave" />.
        ///         In all other cases, <see cref="MousePosition" /> has a default value of <see cref="Vector2.Zero" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(32)]
        public readonly Vector2 MousePosition;

        /// <summary>
        ///     The mouse <see cref="Vector2" /> scroll position that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="ScrollPosition" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.MouseScroll" />. In all other cases, <see cref="ScrollPosition" /> has a
        ///         default value of <see cref="Vector2.Zero" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(40)]
        public readonly Vector2 ScrollPosition;

        /// <summary>
        ///     The number of touch points that generated the <see cref="Event" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <see cref="TouchesCount" /> only has meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.TouchesBegan" />, <see cref="EventType.TouchesMoved" />,
        ///         <see cref="EventType.TouchesEnded" /> or <see cref="EventType.TouchesCancelled" />. In all other
        ///         cases, <see cref="TouchesCount" /> has a default value of <c>0</c>.
        ///     </para>
        ///     <para>
        ///         If <see cref="TouchesCount" /> is <c>0</c>, it means that there are no touches.
        ///     </para>
        ///     <para>
        ///         <see cref="TouchesCount" /> can not exceed <see cref="Constants.MaximumTouchPointsCount" />.
        ///     </para>
        /// </remarks>
        [FieldOffset(48)]
        public readonly int TouchesCount;

        [FieldOffset(56)]
        [SuppressMessage("ReSharper", "SA1600", Justification = "Internal fixed array.")]
#pragma warning disable 1591
        public readonly ulong _touches;
#pragma warning restore 1591

        /// <summary>
        ///     The width of the window.
        /// </summary>
        [FieldOffset(248)]
        internal readonly int WindowWidth;

        /// <summary>
        ///     The height of the window.
        /// </summary>
        [FieldOffset(252)]
        internal readonly int WindowHeight;

        /// <summary>
        ///     The width of the <see cref="Framebuffer" />.
        /// </summary>
        [FieldOffset(256)]
        internal readonly int FramebufferWidth;

        /// <summary>
        ///     The height of the <see cref="Framebuffer" />.
        /// </summary>
        [FieldOffset(260)]
        internal readonly int FramebufferHeight;

        /// <summary>
        ///     Gets the <see cref="TouchPoint" />, by reference, given the specified slot or index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="TouchPoint"/> by reference.</returns>
        /// <remarks>
        ///     <para>
        ///         <see cref="Touch" /> will only return meaningful data if <see cref="Type" /> is
        ///         <see cref="EventType.TouchesBegan" />, <see cref="EventType.TouchesMoved" />,
        ///         <see cref="EventType.TouchesEnded" /> or <see cref="EventType.TouchesCancelled" />. In all other
        ///         cases, <see cref="Touch" /> will return a default value of <c>default(TouchPoint)</c>.
        ///     </para>
        /// </remarks>
        public ref TouchPoint Touch(int index = 0)
        {
            fixed (Event* @this = &this)
            {
                var pointer = (TouchPoint*)&@this->_touches;
                return ref *(pointer + index);
            }
        }
    }
}
