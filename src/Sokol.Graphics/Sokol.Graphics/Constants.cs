// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Constants of `sokol_gfx`.
    /// </summary>
    [SuppressMessage("ReSharper", "SA1600", Justification = "Self explanatory.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public constants.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public static class Constants
    {
        public const int InvalidIdentifier = 0;
        public const int ShaderStagesCount = 2;
        public const int InflightFramesCount = 2;
        public const int MaximumColorAttachments = 4;
        public const int MaximumShaderStageBuffers = 8;
        public const int MaximumShaderStageImages = 12;
        public const int MaximumShaderStageUniformBuffers = 4;
        public const int MaximumUniformBufferMembers = 16;
        public const int MaximumVertexAttributes = 16;
        public const int MaximumMipMaps = 16;
        public const int MaximumTextureArrayLayers = 128;
    }
}
