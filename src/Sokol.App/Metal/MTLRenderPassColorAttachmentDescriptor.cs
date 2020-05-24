// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using static Metal.MTLRenderPassAttachment;
using static ObjCRuntime.Messaging;

namespace Metal
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
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
