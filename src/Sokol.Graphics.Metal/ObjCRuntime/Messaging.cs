// Original code derived from:
// (1) https://github.com/mellinoe/veldrid/blob/master/src/Veldrid.MetalBindings/ObjectiveCRuntime.cs
// (2) https://github.com/xamarin/xamarin-macios/blob/master/src/ObjCRuntime/Messaging.mac.cs

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
Xamarin SDK

The MIT License (MIT)

Copyright (c) .NET Foundation Contributors

All rights reserved.

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
SOFTWARE./
 */

/* 
MIT License

Copyright (c) 2020 Lucas Girouard-Stranks

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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sokol.CoreGraphics;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Sokol.ObjCRuntime
{
    internal static unsafe class Messaging
    {
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend(IntPtr receiver, Selector selector);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_bool(IntPtr receiver, Selector selector, BlittableBoolean arg1);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_intptr(IntPtr receiver, Selector selector, IntPtr arg1);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_uintptr(IntPtr receiver, Selector selector, UIntPtr arg1);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_intptr_nsuinteger(IntPtr receiver, Selector selector, IntPtr arg1, UIntPtr arg2);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern void void_objc_msgSend_cgsize(IntPtr receiver, Selector selector, CGSize arg1);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend_stret")]
        public static extern void ptr_objc_msgSend_stret(void* retPtr, IntPtr receiver, Selector selector);

        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern BlittableBoolean bool_objc_msgSend(IntPtr receiver, Selector selector);
        
        [DllImport (Constants.ObjCLibrary, EntryPoint="objc_msgSend")]
        public static extern uint uint_objc_msgSend(IntPtr receiver, IntPtr selector);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern IntPtr intptr_objc_msgSend(IntPtr receiver, Selector selector);
        
        [DllImport(Constants.ObjCLibrary, EntryPoint = "objc_msgSend")]
        public static extern IntPtr intptr_objc_msgSend_nsuinteger(IntPtr receiver, Selector selector, UIntPtr arg1);

        public static T t_objc_msgSend<T>(IntPtr receiver, Selector selector) where T : unmanaged
        {
            var value = intptr_objc_msgSend(receiver, selector);
            return Unsafe.AsRef<T>(&value);
        }
        
        [DllImport (Constants.ObjCLibrary, EntryPoint="objc_msgSend")]
        public static extern CGSize cgsize_objc_msgSend(IntPtr receiver, Selector selector);
    }
}