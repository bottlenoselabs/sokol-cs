// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using Metal;
using ObjCRuntime;
using static ObjCRuntime.Messaging;

namespace CoreAnimation
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct CAMetalLayer
    {
        public readonly IntPtr Handle;

        public CAMetalLayer(IntPtr handle)
        {
            Handle = handle;
        }

        public MTLDevice device => t_objc_msgSend<MTLDevice>(Handle, sel_device);

        public BlittableBoolean framebufferOnly
        {
            get => bool_objc_msgSend(Handle, sel_framebufferOnly);
            set => void_objc_msgSend_bool(Handle, sel_setFramebufferOnly, value);
        }

        public CGSize drawableSize => cgsize_objc_msgSend(Handle, sel_drawableSize);

        public BlittableBoolean displaySyncEnabled
        {
            get => bool_objc_msgSend(Handle, sel_displaySyncEnabled);
            set => void_objc_msgSend_bool(Handle, sel_setDisplaySyncEnabled, value);
        }

        public CAMetalDrawable nextDrawable()
        {
            return t_objc_msgSend<CAMetalDrawable>(Handle, sel_nextDrawable);
        }

        public static implicit operator IntPtr(CAMetalLayer value)
        {
            return value.Handle;
        }

        private static readonly Selector sel_device = "device";
        private static readonly Selector sel_framebufferOnly = "framebufferOnly";
        private static readonly Selector sel_setFramebufferOnly = "setFramebufferOnly:";
        private static readonly Selector sel_drawableSize = "drawableSize";
        private static readonly Selector sel_displaySyncEnabled = "displaySyncEnabled";
        private static readonly Selector sel_setDisplaySyncEnabled = "setDisplaySyncEnabled:";
        private static readonly Selector sel_nextDrawable = "nextDrawable";
    }
}
