// Original code derived from:
// (1) https://github.com/mellinoe/veldrid/blob/master/src/Veldrid.MetalBindings/CAMetalLayer.cs

/*
The MIT License (MIT)

Copyright (c) 2017 Eric Mellino and Veldrid contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;
using Sokol.CoreGraphics;
using Sokol.Metal;
using Sokol.ObjCRuntime;
using static Sokol.ObjCRuntime.Messaging;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace Sokol.CoreAnimation
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CAMetalLayer
    {
        public readonly IntPtr Handle;
        
        public MTLDevice device
        {
            get => t_objc_msgSend<MTLDevice>(Handle, sel_device);
            set => void_objc_msgSend_intptr(Handle, sel_setDevice, value);
        }
        
        public MTLPixelFormat pixelFormat
        {
            get => (MTLPixelFormat)uint_objc_msgSend(Handle, sel_pixelFormat);
            set => void_objc_msgSend_uint(Handle, sel_setPixelFormat, (uint)value);
        }
        
        public BlittableBoolean framebufferOnly
        {
            get => bool_objc_msgSend(Handle, sel_framebufferOnly);
            set => void_objc_msgSend_bool(Handle, sel_setFramebufferOnly, value);
        }
        
        public CGSize drawableSize
        {
            get => cgsize_objc_msgSend(Handle, sel_drawableSize);
            set => void_objc_msgSend_cgsize(Handle, sel_setDrawableSize, value);
        }

        public BlittableBoolean displaySyncEnabled
        {
            get => bool_objc_msgSend(Handle, sel_displaySyncEnabled);
            set => void_objc_msgSend_bool(Handle, sel_setDisplaySyncEnabled, value);
        }
        
        public static CAMetalLayer New()
        {
            var cls = new Class("CAMetalLayer");
            return cls.AllocInit<CAMetalLayer>();
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
        private static readonly Selector sel_setDevice = "setDevice:";
        private static readonly Selector sel_pixelFormat = "pixelFormat";
        private static readonly Selector sel_setPixelFormat = "setPixelFormat:";
        private static readonly Selector sel_framebufferOnly = "framebufferOnly";
        private static readonly Selector sel_setFramebufferOnly = "setFramebufferOnly:";
        private static readonly Selector sel_drawableSize = "drawableSize";
        private static readonly Selector sel_setDrawableSize = "setDrawableSize:";
        private static readonly Selector sel_displaySyncEnabled = "displaySyncEnabled";
        private static readonly Selector sel_setDisplaySyncEnabled = "setDisplaySyncEnabled:";
        private static readonly Selector sel_nextDrawable = "nextDrawable";
    }
}