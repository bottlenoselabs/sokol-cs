// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds a <see cref="GraphicsBackend" /> viewable surface, such as a window in an
    ///     application, and all <see cref="GraphicsBuffer" />, <see cref="GraphicsImage" />, <see cref="GraphicsPass" />,
    ///     <see cref="GraphicsPipeline" />, and <see cref="GraphicsShader" /> resources associated with the viewable surface.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <see cref="GraphicsContext" /> keeps track of all graphic resources created while it is active. When a
    ///         <see cref="GraphicsContext" /> is destroyed, all graphic resources belonging to it are destroyed as well. A
    ///         graphic resource must only be used with the same active <see cref="GraphicsContext" /> that was used to alloc
    ///         and initialize the resource.
    ///     </para>
    ///     <para>
    ///         A default <see cref="GraphicsContext" /> will be implicitly allocated, initialized, and activated when calling
    ///         <see cref="Graphics.Setup" />, and implicitly destroyed when calling <see cref="Graphics.Shutdown" />.
    ///         This means for a typical application which does not use multiple contexts, dealing with a
    ///         <see cref="GraphicsContext" /> can be ignored.
    ///     </para>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsContext" />, call <see cref="Graphics.SetupContext" />.
    ///     </para>
    ///     <para>
    ///         Currently, <see cref="GraphicsBackend.Metal" /> and <see cref="GraphicsBackend.Direct3D11" /> do not
    ///         support multi-window rendering. For more information see the GitHub issue:
    ///         https://github.com/floooh/sokol/issues/229.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsContext" /> is blittable to the C `sg_context` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsContext
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsContext" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Activate() => PInvoke.sg_activate_context(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Discard() => PInvoke.sg_discard_context(this);

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
