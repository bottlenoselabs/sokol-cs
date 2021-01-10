// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;

namespace Sokol
{
    /// <summary>
    ///     Information about an <see cref="GraphicsImage" /> that will be as input to the <see cref="GraphicsShader" /> for
    ///     sampling.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsShaderImageDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderImageDescriptor" /> is blittable to the C `sg_shader_image_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsShaderImageDescriptor
    {
        [FieldOffset(0)]
        private byte* _name;

        /// <summary>
        ///     The <see cref="GraphicsImageType" /> of the <see cref="GraphicsImage" /> that will be used as input.
        /// </summary>
        [FieldOffset(8)]
        public GraphicsImageType ImageType;

        /// <summary>
        ///     The <see cref="GraphicsShaderImageSamplerType" /> describing how the <see cref="GraphicsImage" /> will be sampled.
        /// </summary>
        [FieldOffset(12)]
        public GraphicsShaderImageSamplerType SamplerType;

        /// <summary>
        ///     Sets the name of the sampler used in the <see cref="GraphicsShader" /> stage source code. Required for
        ///     <see cref="GraphicsBackend.OpenGLES2" />. Optional for every other <see cref="GraphicsBackend" />
        ///     implementation.
        /// </summary>
        /// <value>The string with the name of the sampler.</value>
        public string Name
        {
            set => _name = Native.GetBytePointerFromString(value);
        }
    }
}
