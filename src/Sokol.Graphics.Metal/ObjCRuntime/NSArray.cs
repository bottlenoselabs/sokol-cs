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
using static Sokol.ObjCRuntime.Messaging;

// ReSharper disable InconsistentNaming

namespace Sokol.ObjCRuntime
{
    internal static class NSArray
    {
        public static unsafe T objectAtIndexedSubscript<T>(IntPtr receiver, UIntPtr index) where T : unmanaged
        {
            var value = intptr_objc_msgSend_nsuinteger(receiver, sel_objectAtIndexedSubscript, index);
            return Unsafe.AsRef<T>(&value);
        }

        public static void setObjectAtIndexedSubscript<T>(IntPtr receiver, IntPtr value, UIntPtr index) where T : unmanaged
        {
            void_objc_msgSend_intptr_nsuinteger(receiver, sel_setObjectAtIndexedSubscript, value, index);
        }
        
        private static readonly Selector sel_objectAtIndexedSubscript = "objectAtIndexedSubscript:";
        private static readonly Selector sel_setObjectAtIndexedSubscript = "setObject:atIndexedSubscript:";
    }
}