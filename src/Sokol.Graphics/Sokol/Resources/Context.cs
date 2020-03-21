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
    ///     Represents a <see cref="GraphicsBackend" /> viewable surface, such as a window in an application, and all
    ///     <see cref="Buffer" />, <see cref="Image" />, <see cref="Pass" />, <see cref="Pipeline" />, and
    ///     <see cref="Shader" /> resources associated with the viewable surface.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="Context" /> keeps track of all resources created while it is active. When a
    ///         <see cref="Context" /> is destroyed, all resources belonging to it are destroyed as well.
    ///     </para>
    ///     <para>
    ///         Any <see cref="Buffer" />, <see cref="Image" />, <see cref="Pass" />, <see cref="Pipeline" />, or
    ///         <see cref="Shader" /> must only be used or destroyed with the same active <see cref="Context" /> that was
    ///         also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         A default <see cref="Context" /> will be created and activated implicitly when calling
    ///         <see cref="Sg.Setup" />, and destroyed when calling <see cref="Sg.Shutdown" />. This means for a typical
    ///         application which does not use multiple contexts, nothing changes, and calling the context functions
    ///         isn't necessary.
    ///     </para>
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
        ///     A number which uniquely identifies this <see cref="Context" />.
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
