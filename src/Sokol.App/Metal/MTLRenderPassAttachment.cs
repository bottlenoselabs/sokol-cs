// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using ObjCRuntime;

namespace Metal
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1304", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1307", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1311", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "PInvoke.")]
    internal static class MTLRenderPassAttachment
    {
        internal static readonly Selector sel_texture = "texture";
        internal static readonly Selector sel_setTexture = "setTexture:";
    }
}
