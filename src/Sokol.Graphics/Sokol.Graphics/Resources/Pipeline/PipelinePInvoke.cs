// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "Internal usage")]
    public static class PipelinePInvoke
    {
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_pipeline_defaults")]
        public static extern PipelineDescription QueryDefaults([In] ref PipelineDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_pipeline")]
        public static extern Pipeline Alloc();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_pipeline")]
        public static extern Pipeline Create([In] ref PipelineDescription description);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_pipeline")]
        public static extern void Init(Pipeline pipeline, [In] ref PipelineDescription desc);

        [DllImport(Sg.LibraryName, EntryPoint = "destroy_pipeline")]
        public static extern void Destroy(Pipeline pipeline);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_pipeline")]
        public static extern void Fail(Pipeline pipeline);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_apply_pipeline")]
        public static extern void Apply(Pipeline pipeline);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_apply_uniforms")]
        public static extern void ApplyUniforms(
            ShaderStageType stage,
            int uniformBlockIndex,
            IntPtr dataPointer,
            int dataSize);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_apply_bindings")]
        public static extern void ApplyBindings([In] ref PipelineResourceBindings bindings);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_pipeline_state")]
        public static extern ResourceState QueryState(Pipeline pipeline);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_pipeline_info")]
        public static extern PipelineInfo QueryInfo(Pipeline pipeline);
    }
}
