// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ObjCRuntime;

namespace Metal
{
    internal static class MTLRenderPassAttachment
    {
        internal static readonly Selector sel_texture = "texture";
        internal static readonly Selector sel_setTexture = "setTexture:";
    }
}
