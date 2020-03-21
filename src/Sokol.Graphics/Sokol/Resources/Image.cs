// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
namespace Sokol
{
    /// <summary>
    ///     Represents a texture or render target GPU resource.
    /// </summary>
    /// <remarks>
    ///    <para>
    ///         <see cref="Image" /> is blittable to the C `sg_image` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Image
    {
        /// <summary>
        ///     A number which uniquely identifies this <see cref="Image"/>.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
