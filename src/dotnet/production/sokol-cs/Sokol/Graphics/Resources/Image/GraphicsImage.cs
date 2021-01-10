// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     A GPU resource that holds one or many slices of structured texture elements, called texels, and the
    ///     related information such as how many elements there are and how they are encoded/organized.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsImage" /> can either be used as input to or as output from a <see cref="GraphicsPass" />.
    ///     </para>
    ///     <para>
    ///         When used as input, the <see cref="GraphicsImage" /> is commonly referred to as a texture. At render time, a
    ///         <see cref="GraphicsShader" /> retrieves data from a texture in a process called texture sampling. Exactly how a
    ///         texel, or group of texels, within the texture are fetched (and possibly combined) to map onto some mathematical
    ///         geometry is defined with <see cref="GraphicsImageFilter" /> and <see cref="GraphicsImageWrap" />.
    ///         Once sampled, the <see cref="GraphicsShader" /> may perform developer logic with the data before writing it to
    ///         some texture attachment of a <see cref="GraphicsPass" />.
    ///     </para>
    ///     <para>
    ///         When used as output, the <see cref="GraphicsImage" /> is commonly referred to as a render target. Render
    ///         targets allow for off-screen rendering where they are the texture attachments of a <see cref="GraphicsPass" />.
    ///         The content of a render target (output) can be later used as a texture (input) allowing for chaining together
    ///         multiple <see cref="GraphicsPass" /> resources. The screen is a default <see cref="GraphicsPass" /> for which
    ///         the texture attachments are pre-defined. However it can be pragmatic to think about the screen as a default
    ///         render target even if it is not a <see cref="GraphicsImage" /> resource exactly.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsImage" /> may have multiple sub-textures which are intended to be used as mipmaps. Mipmaps
    ///         are smaller, pre-filtered versions of a texture, representing different levels of detail (LOD). They are often
    ///         stored in sequences of progressively smaller textures that are as half as small in size as the previous.
    ///         Depending on the difference in size of texels to pixels, a lower resolution texture in the mipmap chain may be
    ///         sampled instead of the full sized texture. Mipmaps can significantly increase the detail, reduce aliasing, and
    ///         in some cases even improve performance for sampling a <see cref="GraphicsImage" /> when there are more texels
    ///         than pixels (down-scaling or minification). Mipmaps do not apply when there are more pixels than texels
    ///         (up-scaling or magnification).
    ///     </para>
    ///     <para>
    ///         The type, format, size, number of mipmaps, encoding, and organization of a <see cref="GraphicsImage" /> never
    ///         change once initialized. However, you can change the contents either by rendering to it or copying data into
    ///         it.
    ///     </para>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsImage" />, call <see cref="Graphics.MakeImage" />. To allocate
    ///         a <see cref="GraphicsImage" /> and initialize it later, call <see cref="Graphics.AllocImage" /> to get an
    ///         un-initialized <see cref="GraphicsImage" /> and then call <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsImage" /> must only be used or destroyed with the same active
    ///         <see cref="GraphicsContext" /> that was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsImage" /> is blittable to the C `sg_image` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsImage
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsImage" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsImageInfo Info => GraphicsPInvoke.sg_query_image_info(this);

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsResourceState State => GraphicsPInvoke.sg_query_image_state(this);

        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="GraphicsImageDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating an image.</param>
        /// <returns>A <see cref="GraphicsImageDescriptor" /> with any zero-initialized members set to default values.</returns>
        public static GraphicsImageDescriptor QueryDefaults([In] ref GraphicsImageDescriptor descriptor) =>
            GraphicsPInvoke.sg_query_image_defaults(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <param name="descriptor">.</param>
        public void Initialize(ref GraphicsImageDescriptor descriptor) => GraphicsPInvoke.sg_init_image(this, ref descriptor);

        /// <summary>
        ///     Destroys the <see cref="GraphicsImage" />.
        /// </summary>
        public void Destroy() => GraphicsPInvoke.sg_destroy_image(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Fail() => GraphicsPInvoke.sg_fail_image(this);

        /// <summary>
        ///     Overwrites the contents of the <see cref="GraphicsImage" /> by copying the pointed memory of the specified
        ///     <see cref="GraphicsImageContent" />. The <see cref="GraphicsImage" /> must have been created
        ///     using <see cref="GraphicsResourceUsage.Dynamic" /> or <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The new contents of the image.</param>
        public void Update(ref GraphicsImageContent data) => GraphicsPInvoke.sg_update_image(this, ref data);

        /// <summary>
        ///     Overwrites the contents of the <see cref="GraphicsImage" /> for the first <see cref="GraphicsImageSubContent" /> by
        ///     copying the pointed memory of the specified <see cref="Span{T}" /> that is unmanaged or externally pinned. The
        ///     <see cref="GraphicsImage" /> must have been created using <see cref="GraphicsResourceUsage.Dynamic" /> or <see cref="GraphicsResourceUsage.Stream" />.
        /// </summary>
        /// <param name="data">The block of memory to copy.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void Update<T>(Span<T> data)
        {
            var imageContent = default(GraphicsImageContent);
            imageContent.SubImage().SetData(data);
            Update(ref imageContent);
        }

        /// <summary>
        ///     Overwrites the contents of the <see cref="GraphicsImage" /> for the first <see cref="GraphicsImageSubContent" /> by
        ///     copying the pointed memory of the specified <see cref="Memory{T}" />. The <see cref="GraphicsImage" /> must have
        ///     been created using <see cref="GraphicsResourceUsage.Dynamic" /> or <see cref="GraphicsResourceUsage.Stream" />. It
        ///     is
        ///     assumed that the <paramref name="data" /> is already unmanaged or externally pinned.
        /// </summary>
        /// <param name="data">The block of memory to copy.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void Update<T>(Memory<T> data) => Update(data.Span);

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
