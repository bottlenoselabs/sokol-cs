// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Sokol
{
    /// <summary>
    ///     Represents a vertex or index buffer GPU resource.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="Buffer" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Buffer" /> is blittable to the C `sg_buffer` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Buffer
    {
        /// <summary>
        ///     A number which uniquely identifies this <see cref="Buffer" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
