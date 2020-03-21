// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable once CheckNamespace
namespace Sokol
{
    /// <summary>
    ///     Represents a bundle of render target GPU resources and the actions on them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="Pass" /> must only be used or destroyed with the same active <see cref="Context" /> that was
    ///         also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Pass" /> is blittable to the C `sg_pass` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct Pass
    {
        /// <summary>
        ///     A number which uniquely identifies this <see cref="Pass" />.
        /// </summary>
        [FieldOffset(0)]
        public uint Identifier;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
