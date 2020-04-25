// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace Metal
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MTLRenderPassColorAttachmentDescriptorArray
    {
        private readonly IntPtr _handle;

        public MTLRenderPassColorAttachmentDescriptorArray(IntPtr handle)
        {
            _handle = handle;
        }

        public unsafe MTLRenderPassColorAttachmentDescriptor this[uint index]
        {
            get => NSArray.objectAtIndexedSubscript<MTLRenderPassColorAttachmentDescriptor>(_handle, (UIntPtr)index);
            set => NSArray.setObjectAtIndexedSubscript(_handle, (IntPtr)(&value), (UIntPtr)index);
        }

        public static implicit operator IntPtr(MTLRenderPassColorAttachmentDescriptorArray value)
        {
            return value._handle;
        }
    }
}
