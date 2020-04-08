// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable UnusedType.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds one or many layers of structured texture elements, called texels, and the
    ///     related information such as how many elements there are and how they are encoded and organized.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         An <see cref="Image" /> is either used as the input to a <see cref="Shader" /> or as the output of a
    ///         <see cref="Pass" />. When an <see cref="Image" /> is used as input, it is often referred to as a
    ///         "texture". When it is used as an output it is often referred to as a "render target". However, a render
    ///         target is still a texture and can be used as input to another <see cref="Shader" />.
    ///     </para>
    ///     <para>
    ///         An <see cref="Image" /> may have multiple layers called "sub images" which are most often used as
    ///         mipmaps. See <a cref="http://en.wikipedia.org/wiki/Mipmap">https://en.wikipedia.org/wiki/Mipmap</a>
    ///         for more information.
    ///     </para>
    ///     <para>
    ///         The type, format, encoding, and organization of an <see cref="Image" /> never change. However, you can
    ///         change the contents either by rendering to it or copying data into it.
    ///     </para>
    ///     <para>
    ///         An <see cref="Image" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Image" /> is blittable to the C `sg_image` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Image
    {
        /// <summary>
        ///     Fill any zero-initialized members of an <see cref="ImageDescription" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating an image.</param>
        /// <returns>An <see cref="ImageDescription" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription QueryDefaults([In] ref ImageDescription description)
        {
            return ImagePInvoke.QueryDefaults(ref description);
        }

        // TODO: Document allocating an image
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image Alloc()
        {
            return ImagePInvoke.Alloc();
        }

        /// <summary>
        ///     Creates an <see cref="Image" /> from the specified <see cref="ImageDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating an image.</param>
        /// <returns>An <see cref="Image" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image Create([In] ref ImageDescription description)
        {
            return ImagePInvoke.Create(ref description);
        }

        /// <summary>
        ///     A number which uniquely identifies the <see cref="Image" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document `BufferInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ImageInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ImagePInvoke.QueryInfo(this);
        }

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ImagePInvoke.QueryState(this);
        }

        // TODO: Document manual initialization of an image.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init([In] ref ImageDescription description)
        {
            ImagePInvoke.Init(this, ref description);
        }

        /// <summary>
        ///     Destroys the <see cref="Image" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            ImagePInvoke.Destroy(this);
        }

        // TODO: Document failing an image.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            ImagePInvoke.Fail(this);
        }

        /// <summary>
        ///     Overwrites the contents of the <see cref="Image" /> by copying any pointed memory in the specified
        ///     <see cref="ImageContent" />. The <see cref="Image" /> must have been created
        ///     using <see cref="ResourceUsage.Dynamic" /> or <see cref="ResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The new contents of the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref ImageContent data)
        {
            ImagePInvoke.Update(this, ref data);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
