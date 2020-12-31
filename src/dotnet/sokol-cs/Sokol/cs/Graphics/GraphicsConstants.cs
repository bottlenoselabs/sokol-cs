// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Constants of `sokol_gfx`. Changes to these constants are only observed from C#; changes to these constants
    ///     here are not observed in the C library.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public constants.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public static class GraphicsConstants
    {
        /// <summary>
        ///     The invalid resource identifier.
        /// </summary>
        public const int InvalidIdentifier = 0;

        /// <summary>
        ///     The number of shader stages.
        /// </summary>
        public const int ShaderStagesCount = 2;

        /// <summary>
        ///     The number of simultaneous frames to increase throughput and avoid bottlenecks.
        /// </summary>
        public const int InflightFramesCount = 2;

        /// <summary>
        ///     The maximum number of color attachments for a framebuffer.
        /// </summary>
        public const int MaximumColorAttachments = 4;

        /// <summary>
        ///     The maximum number of vertex buffers and index buffers that can be used in a shader stage.
        /// </summary>
        public const int MaximumShaderStageBuffers = 8;

        /// <summary>
        ///     The maximum number of images that can be used in a shader stage.
        /// </summary>
        public const int MaximumShaderStageImages = 12;

        /// <summary>
        ///     The maximum number of uniform blocks that can be used in a shader stage.
        /// </summary>
        public const int MaximumShaderStageUniformBlockSlots = 4;

        /// <summary>
        ///     The maximum number of uniforms that can be in one shader stage.
        /// </summary>
        public const int MaximumUniformBufferMembers = 16;

        /// <summary>
        ///     The maximum number of vertex attributes for a pipeline.
        /// </summary>
        public const int MaximumVertexAttributes = 16;

        /// <summary>
        ///     The maximum number of mip-map levels for an image.
        /// </summary>
        public const int MaximumMipMaps = 16;

        /// <summary>
        ///     The maximum number of texture array layers for an image.
        /// </summary>
        public const int MaximumTextureArrayLayers = 128;
    }
}
