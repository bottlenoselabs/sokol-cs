// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about an <see cref="Image" /> that will be as input to the
    ///     <see cref="Shader" /> in the "per-fragment processing" stage. Apart of <see cref="ShaderDescriptor" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="ShaderImageDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ShaderImageDescriptor" /> is blittable to the C `sg_shader_image_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8, CharSet = CharSet.Ansi)]
    public struct ShaderImageDescriptor
    {
        /// <summary>
        ///     The <see cref="ImageType" /> of the <see cref="Image" /> that will be as input.
        /// </summary>
        [FieldOffset(8)]
        public ImageType Type;

        [FieldOffset(0)]
        private IntPtr _name;

        /// <summary>
        ///     Gets or sets the string with the the name of the sampler in the "per-fragment processing"
        ///     stage source code. Required for <see cref="GraphicsBackend.OpenGLES2" />. Optional for every other
        ///     <see cref="GraphicsBackend" /> implementation.
        /// </summary>
        /// <value>The string with the name of the sampler.
        /// </value>
        public string Name
        {
            get => UnmanagedStringMemoryManager.GetString(_name);
            set => _name = UnmanagedStringMemoryManager.SetString(value);
        }
    }
}
