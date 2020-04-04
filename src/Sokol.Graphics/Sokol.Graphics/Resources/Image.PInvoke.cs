// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    public readonly partial struct Image
    {
        /// <summary>
        ///     Fill any zero-initialized members of an <see cref="ImageDescription" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating an image.</param>
        /// <returns>An <see cref="ImageDescription" /> with any zero-initialized members set to default values.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_defaults")]
        public static extern ImageDescription QueryImageDefaults([In] ref ImageDescription description);

        // TODO: Document allocating an image
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_image")]
        public static extern Image Alloc();

        /// <summary>
        ///     Creates an <see cref="Image" /> from the specified <see cref="ImageDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating an image.</param>
        /// <returns>An <see cref="Image" />.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_image")]
        public static extern Image Create([In] ref ImageDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_image")]
        private static extern void Destroy(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_image")]
        private static extern void Init(Image image, [In] ref ImageDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_image")]
        private static extern void Fail(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_update_image")]
        private static extern void Update(Image image, ref ImageContent data);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_state")]
        private static extern ResourceState QueryState(Image image);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_image_info")]
        private static extern ImageInfo QueryInfo(Image image);
    }
}
