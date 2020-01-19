/* 
MIT License

Copyright (c) 2020 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace Sokol
{
    public static class Sg
    {
        private const string SokolGfxLibraryName = "sokol_gfx";
        
        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_setup")]
        public static extern void Setup([In] ref SgDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_shutdown")]
        public static extern void Shutdown();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_isvalid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValid();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_reset_state_cache")]
        public static extern void ResetStateCache();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_install_trace_hooks")]
        public static extern SgTraceHooks InstallTraceHooks(ref SgTraceHooks traceHooks);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_push_debug_group")]
        public static extern void PushDebugGroup(IntPtr name);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_pop_debug_group")]
        public static extern void PopDebugGroup();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_make_buffer")]
        public static extern SgBuffer MakeBuffer([In] ref SgBufferDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_make_image")]
        public static extern SgImage MakeImage([In] ref SgImageDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_make_shader")]
        public static extern SgShader MakeShader([In] ref SgShaderDescription description);
        
        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_make_pipeline")]
        public static extern SgPipeline MakePipeline([In] ref SgPipelineDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_make_pass")]
        public static extern SgPass MakePass([In] ref SgPassDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_destroy_buffer")]
        public static extern void DestroyBuffer(SgBuffer buffer);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_destroy_image")]
        public static extern void DestroyImage(SgImage image);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_destroy_shader")]
        public static extern void DestroyShader(SgShader shader);

        [DllImport(SokolGfxLibraryName, EntryPoint = "destroy_pipeline")]
        public static extern void DestroyPipeline(SgPipeline pipeline);

        [DllImport(SokolGfxLibraryName, EntryPoint = "destroy_pass")]
        public static extern void DestroyPass(SgPass pass);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_update_buffer")]
        public static extern void UpdateBuffer(SgBuffer buffer, IntPtr dataPointer, int dataSize);

        public static unsafe void UpdateBuffer<T>(SgBuffer buffer, Memory<T> data, int? count = null) where T : unmanaged
        {
            var dataHandle = data.Pin();
            var dataLength = count ?? data.Length;
            var dataSize =  Marshal.SizeOf<T>() * dataLength;
            
            UpdateBuffer(buffer, (IntPtr) dataHandle.Pointer, dataSize);
            
            dataHandle.Dispose();
        }

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_update_image")]
        public static extern void UpdateImage(SgImage image, ref SgImageContent data);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_append_buffer")]
        public static extern int AppendBuffer(SgBuffer buffer, IntPtr dataPointer, int dataSize);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_buffer_overflow")]
        public static extern bool QueryBufferOverflow(SgBuffer buffer);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_begin_default_pass")]
        public static extern void BeginDefaultPass([In] ref SgPassAction passAction, int width, int height);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_begin_pass")]
        public static extern void BeginPass(SgPass pass, [In] ref SgPassAction passAction);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_apply_viewport")]
        public static extern void ApplyViewport(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_apply_scissor_rect")]
        public static extern void ApplyScissorRectangle(int x, int y, int width, int height, bool originTopLeft = false);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_apply_pipeline")]
        public static extern void ApplyPipeline(SgPipeline pipeline);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_apply_bindings")]
        public static extern void ApplyBindings([In] ref SgBindings bindings);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_apply_uniforms")]
        public static extern void ApplyUniforms(SgShaderStageType stage, int uniformBlockIndex, IntPtr dataPointer, int dataSize);

        public static unsafe void ApplyUniforms<T>(SgShaderStageType stage, int uniformBlockIndex, ref T value) where T : unmanaged
        {
            var dataPointer = (IntPtr) Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            ApplyUniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_draw")]
        public static extern void Draw(int baseElement, int elementCount, int instanceCount);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_end_pass")]
        public static extern void EndPass();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_commit")]
        public static extern void Commit();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_desc")]
        public static extern SgDescription QueryDescription();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_backend")]
        public static extern GraphicsBackend QueryBackend();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_features")]
        public static extern SgFeatures QueryFeatures();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_limits")]
        public static extern SgLimits QueryLimits();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pixelformat")]
        public static extern SgPixelFormatInfo QueryPixelFormat(SgPixelFormat format);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_buffer_state")]
        public static extern SgResourceState QueryBufferState(SgBuffer buffer);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_image_state")]
        public static extern SgResourceState QueryImageState(SgImage image);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_shader_state")]
        public static extern SgResourceState QueryShaderState(SgShader shader);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pipeline_state")]
        public static extern SgResourceState QueryPipelineState(SgPipeline pipeline);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pass_state")]
        public static extern SgResourceState QueryPassState(SgPass pass);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_buffer_info")]
        public static extern SgBufferInfo QueryBufferInfo(SgBuffer buffer);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_image_info")]
        public static extern SgImageInfo QueryImageInfo(SgImage image);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_shader_info")]
        public static extern SgShaderInfo QueryShaderInfo(SgShader shader);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pipeline_info")]
        public static extern SgPipelineInfo QueryPipelineInfo(SgPipeline pipeline);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pass_info")]
        public static extern SgPassInfo QueryPassInfo(SgPass pass);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_buffer_defaults")]
        public static extern SgBufferDescription QueryBufferDefaults([In] ref SgBufferDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_image_defaults")]
        public static extern SgImageDescription QueryImageDefaults([In] ref SgImageDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_shader_defaults")]
        public static extern SgShaderDescription QueryShaderDefaults([In] ref SgShaderDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pipeline_defaults")]
        public static extern SgPipelineDescription QueryPipelineDefaults([In] ref SgPipelineDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_query_pass_defaults")]
        public static extern SgPassDescription QueryPassDefaults([In] ref SgPassDescription description);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_alloc_buffer")]
        public static extern SgBuffer AllocBuffer();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_alloc_image")]
        public static extern SgImage AllocImage();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_alloc_shader")]
        public static extern SgShader AllocShader();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_alloc_pipeline")]
        public static extern SgPipeline AllocPipeline();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_alloc_pass")]
        public static extern SgPass AllocPass();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_init_buffer")]
        public static extern void InitBuffer(SgBuffer buffer, [In] ref SgBufferDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_init_image")]
        public static extern void InitImage(SgImage image, [In] ref SgImageDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_init_shader")]
        public static extern void InitShader(SgShader shader, [In] ref SgShaderDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_init_pipeline")]
        public static extern void InitPipeline(SgPipeline pipeline, [In] ref SgPipelineDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_init_pass")]
        public static extern void InitPass(SgPass pass, [In] ref SgPassDescription desc);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_fail_buffer")]
        public static extern void FailBuffer(SgBuffer buffer);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_fail_image")]
        public static extern void FailImage(SgImage image);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_fail_shader")]
        public static extern void FailShader(SgShader shader);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_fail_pipeline")]
        public static extern void FailPipeline(SgPipeline pipeline);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_fail_pass")]
        public static extern void FailPass(SgPass pass);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_setup_context")]
        public static extern SgContext SetupContext();

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_activate_context")]
        public static extern void ActivateContext(SgContext context);

        [DllImport(SokolGfxLibraryName, EntryPoint = "sg_discard_context")]
        public static extern void DiscardContext(SgContext context);
    }
}