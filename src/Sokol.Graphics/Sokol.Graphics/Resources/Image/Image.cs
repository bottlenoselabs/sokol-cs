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
    ///         An <see cref="Image" /> can be mapped to <see cref="ResourceBindings.VertexStageImage" /> or
    ///         <see cref="ResourceBindings.FragmentStageImage" />. When this is done, the
    ///         <see cref="Image" /> is used as input to a <see cref="Shader" /> in either of two programmable stages,
    ///         <see cref="ShaderStageType.VertexStage" /> or <see cref="ShaderStageType.FragmentStage" />, respectively.
    ///         An <see cref="Image" /> used like this is often called a texture.
    ///     </para>
    ///     <para>
    ///         An <see cref="Image" /> can be mapped to attachments of a rendering <see cref="Pass" /> as a color,
    ///         depth, or stencil target. When this is done, the <see cref="Image" /> is used as output from a
    ///         <see cref="Shader" /> from the <see cref="ShaderStageType.FragmentStage" />. This is often called a
    ///         render target. However, a render target can still be used as a texture (input) to a <see cref="Shader" />
    ///         in another rendering <see cref="Pass" />. This leads to a composition technique where one or more
    ///         off-screen rendering <see cref="Pass"/>es are used to generate an intermediate <see cref="Image" />
    ///         before being rendered with a final <see cref="Pass" /> to the framebuffer (screen).
    ///     </para>
    ///     <para>
    ///         To create a <see cref="Image" /> synchronously, call <see cref="GraphicsDevice.CreateImage" /> with a
    ///         specified <see cref="ImageDescriptor" />. To create a <see cref="Image" /> asynchronously, call
    ///         <see cref="GraphicsDevice.AllocImage" /> to get an un-initialized <see cref="Image" /> and then call
    ///         <see cref="Initialize" /> with a specified <see cref="ImageDescriptor" />.
    ///     </para>
    ///     <para>
    ///         An <see cref="Image" /> may have multiple layers called "sub images" which are most often used as
    ///         mipmaps.
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
        ///     Fill any zero-initialized members of an <see cref="ImageDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating an image.</param>
        /// <returns>An <see cref="ImageDescriptor" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescriptor QueryDefaults([In] ref ImageDescriptor descriptor)
        {
            return PInvoke.sg_query_image_defaults(ref descriptor);
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
            get => PInvoke.sg_query_image_info(this);
        }

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => PInvoke.sg_query_image_state(this);
        }

        // TODO: Document manual initialization of an image.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize([In] ref ImageDescriptor descriptor)
        {
            PInvoke.sg_init_image(this, ref descriptor);
        }

        /// <summary>
        ///     Destroys the <see cref="Image" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            PInvoke.sg_destroy_image(this);
        }

        // TODO: Document failing an image.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            PInvoke.sg_fail_image(this);
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
            PInvoke.sg_update_image(this, ref data);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
