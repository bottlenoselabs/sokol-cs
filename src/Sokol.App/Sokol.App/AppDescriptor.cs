// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.App
{
    [StructLayout(LayoutKind.Explicit, Size = 144, Pack = 8)]
    public struct AppDescriptor
    {
        [FieldOffset(40)] /* size = 8, padding = 0 */
        public IntPtr UserData;

        [FieldOffset(88)] /* size = 4, padding = 0 */
        public int Width;

        [FieldOffset(92)] /* size = 4, padding = 0 */
        public int Height;

        [FieldOffset(96)] /* size = 4, padding = 0 */
        public int SampleCount;

        [FieldOffset(100)] /* size = 4, padding = 0 */
        public int SwapInterval;

        [FieldOffset(104)] /* size = 1, padding = 0 */
        public BlittableBoolean HighDpi;

        [FieldOffset(105)] /* size = 1, padding = 0 */
        public BlittableBoolean FullScreen;

        [FieldOffset(106)] /* size = 1, padding = 5 */
        public BlittableBoolean Alpha;

        [FieldOffset(112)] /* size = 8, padding = 0 */
        public IntPtr WindowTitle;

        [FieldOffset(120)] /* size = 1, padding = 0 */
        public BlittableBoolean UserCursor;

        [FieldOffset(121)] /* size = 1, padding = 2 */
        public BlittableBoolean EnableClipboard;

        [FieldOffset(124)] /* size = 4, padding = 0 */
        public int ClipboardSize;

        [FieldOffset(128)] /* size = 8, padding = 0 */
        public IntPtr Html5CanvasName;

        [FieldOffset(136)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5CanvasResize;

        [FieldOffset(137)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5PreserveDrawingBuffer;

        [FieldOffset(138)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5PreMultipliedAlpha;

        [FieldOffset(139)] /* size = 1, padding = 0 */
        public BlittableBoolean Html5AskLeaveSite;

        [FieldOffset(140)] /* size = 1, padding = 0 */
        [SuppressMessage("ReSharper", "SA1305", Justification = "Brand name.")]
        [SuppressMessage("ReSharper", "SA1307", Justification = "Brand name.")]
        public BlittableBoolean iOSKeyboardResizesCanvas;

        [FieldOffset(141)] /* size = 1, padding = 2 */
        public BlittableBoolean GLForceGles2;

        [FieldOffset(0)] /* size = 8, padding = 0 */
        internal IntPtr InitializeCallback;

        [FieldOffset(8)] /* size = 8, padding = 0 */
        internal IntPtr FrameCallback;

        [FieldOffset(16)] /* size = 8, padding = 0 */
        internal IntPtr CleanUpCallback;

        [FieldOffset(24)] /* size = 8, padding = 0 */
        internal IntPtr EventCallback;

        [FieldOffset(32)] /* size = 8, padding = 0 */
        internal IntPtr FailCallback;

        [FieldOffset(48)] /* size = 8, padding = 0 */
        internal IntPtr InitializeUserDataCallback;

        [FieldOffset(56)] /* size = 8, padding = 0 */
        internal IntPtr FrameUserDataCallback;

        [FieldOffset(64)] /* size = 8, padding = 0 */
        internal IntPtr CleanUpUserDataCallback;

        [FieldOffset(72)] /* size = 8, padding = 0 */
        internal IntPtr EventUserDataCallback;

        [FieldOffset(80)] /* size = 8, padding = 0 */
        internal IntPtr FailUserDataCallback;
    }
}
