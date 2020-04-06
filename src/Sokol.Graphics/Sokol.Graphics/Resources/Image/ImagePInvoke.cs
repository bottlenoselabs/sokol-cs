// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "Internal usage")]
    public static class ImagePInvoke
    {
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_defaults")]
        public static extern ImageDescription QueryDefaults([In] ref ImageDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_image")]
        public static extern Image Alloc();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_image")]
        public static extern Image Create([In] ref ImageDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_image")]
        public static extern void Init(Image image, [In] ref ImageDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_image")]
        public static extern void Destroy(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_image")]
        public static extern void Fail(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_update_image")]
        public static extern void Update(Image image, ref ImageContent data);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_state")]
        public static extern ResourceState QueryState(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_info")]
        public static extern ImageInfo QueryInfo(Image image);
    }
}
