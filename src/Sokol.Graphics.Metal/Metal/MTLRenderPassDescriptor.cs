// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using static ObjCRuntime.Messaging;

namespace Metal
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MTLRenderPassDescriptor
    {
        public readonly IntPtr Handle;

        private static readonly Class _class = new Class(nameof(MTLRenderPassDescriptor));

        public static MTLRenderPassDescriptor New()
        {
            return _class.New<MTLRenderPassDescriptor>();
        }

        public MTLRenderPassColorAttachmentDescriptorArray colorAttachments
            => t_objc_msgSend<MTLRenderPassColorAttachmentDescriptorArray>(Handle, sel_colorAttachments);

        private static readonly Selector sel_colorAttachments = "colorAttachments";

        public static implicit operator IntPtr(MTLRenderPassDescriptor value)
        {
            return value.Handle;
        }
    }
}
