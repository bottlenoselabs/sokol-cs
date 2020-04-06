// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds a <see cref="GraphicsBackend" /> viewable surface, such as a window in an
    ///     application, and all <see cref="Buffer" />, <see cref="Image" />, <see cref="Pass" />,
    ///     <see cref="Pipeline" />, and <see cref="Shader" /> resources associated with the viewable surface.
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
    ///         <see cref="GraphicsDevice.Setup" />, and destroyed when calling <see cref="GraphicsDevice.Shutdown" />. This means for a typical
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
        ///     Creates a <see cref="Context" />. Must be called once after a GL context has been created and made
        ///     active.
        /// </summary>
        /// <returns>A <see cref="Context" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Context Create()
        {
            return ContextPInvoke.Create();
        }

        /// <summary>
        ///     A number which uniquely identifies the <see cref="Context" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Activates the <see cref="Context" />. Must be called after making a different GL context active.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Calling <see cref="Activate" /> will internally call <see cref="GraphicsDevice.ResetStateCache" />.
        ///     </para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Activate()
        {
            ContextPInvoke.Activate(this);
        }

        /// <summary>
        ///     Destroys the <see cref="Context"/> and all the resources belonging to it. Must be called right before a
        ///     GL context is destroyed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            ContextPInvoke.Destroy(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
