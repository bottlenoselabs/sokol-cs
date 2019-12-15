// Original code derived from:
// (1) https://github.com/mellinoe/veldrid/blob/master/src/Veldrid.MetalBindings/NSView.cs

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
using Sokol.CoreGraphics;
using Sokol.ObjCRuntime;
using static Sokol.ObjCRuntime.Messaging;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

namespace Sokol.AppKit
{
    public struct NSView
    {
        public readonly IntPtr Handle;
        
        public BlittableBoolean wantsLayer
        {
            get => bool_objc_msgSend(Handle, sel_wantsLayer);
            set => void_objc_msgSend_bool(Handle, sel_setWantsLayer, value);
        }

        public IntPtr layer
        {
            get => intptr_objc_msgSend(Handle, sel_layer);
            set => void_objc_msgSend_intptr(Handle, sel_setLayer, value);
        }

        public CGRect frame => t_objc_msgSend_stret<CGRect>(Handle, sel_frame);

        public readonly CGPoint convertToBacking(CGPoint point)
        {
            return cgpoint_objc_msgSend_cgpoint(Handle, sel_convertToBacking, point);
        }

        public NSView(IntPtr pointer)
        {
            Handle = pointer;
        }
        
        public static implicit operator IntPtr(NSView value)
        {
            return value.Handle;
        }
        
        private static readonly Selector sel_wantsLayer = "wantsLayer";
        private static readonly Selector sel_setWantsLayer = "setWantsLayer:";
        private static readonly Selector sel_layer = "layer";
        private static readonly Selector sel_setLayer = "setLayer:";
        private static readonly Selector sel_frame = "frame";
        private static readonly Selector sel_convertToBacking = "convertPointToBacking:";
    }
}