// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds the programmable "per-vertex processing" and "per-fragment processing" stages
    ///     of a <see cref="Pipeline" /> and the variables used in those stages known as uniform blocks.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="Shader" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Shader" /> is blittable to the C `sg_shader` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly partial struct Shader
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="Shader" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document `ImageInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ShaderInfo Info => QueryInfo(this);

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State => QueryState(this);

        /// <summary>
        ///     Destroys the <see cref="Shader" />.
        /// </summary>
        public void Destroy()
        {
            Destroy(this);
        }

        // TODO: Document manual initialization of a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public void Init([In] ref ShaderDescription description)
        {
            Init(this, ref description);
        }

        // TODO: Document failing a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public void Fail()
        {
            Fail(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
