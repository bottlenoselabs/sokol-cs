// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using static Metal.MTLRenderPassAttachment;
using static ObjCRuntime.Messaging;

namespace Metal
{
    public readonly struct MTLRenderPassColorAttachmentDescriptor
    {
        private readonly IntPtr _handle;

        public MTLRenderPassColorAttachmentDescriptor(IntPtr handle)
        {
            _handle = handle;
        }

        public MTLTexture texture
        {
            get => t_objc_msgSend<MTLTexture>(_handle, sel_texture);
            set => void_objc_msgSend_intptr(_handle, sel_setTexture, value);
        }

        public static implicit operator IntPtr(MTLRenderPassColorAttachmentDescriptor value)
        {
            return value._handle;
        }
    }
}
