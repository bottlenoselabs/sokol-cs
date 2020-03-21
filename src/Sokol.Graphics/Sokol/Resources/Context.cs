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
    ///     Represents a <see cref="GraphicsBackend" /> viewable surface GPU resource such as a window in an
    ///     application.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Currently, <see cref="GraphicsBackend.Metal" /> and <see cref="GraphicsBackend.Direct3D11" /> do not
    ///         support multi-window rendering. For more information see the GitHub issue:
    ///         https://github.com/floooh/sokol/issues/229.
    ///     </para>
    ///     <para>
    ///         <see cref="Context" /> is blittable to the C `sg_context` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Context
    {
        /// <summary>
        ///     A number which uniquely identifies this <see cref="Context"/>.
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
