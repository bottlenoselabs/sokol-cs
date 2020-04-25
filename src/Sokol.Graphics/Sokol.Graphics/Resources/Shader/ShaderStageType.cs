// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the programmable stages of a <see cref="Shader" />.
    /// </summary>
    public enum ShaderStageType
    {
        /// <summary>
        ///     The "per-vertex processing" shader stage.
        /// </summary>
        VertexStage,

        /// <summary>
        ///     The "per-fragment processing" shader stage.
        /// </summary>
        FragmentStage
    }
}
