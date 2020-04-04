// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    public readonly partial struct Shader
    {
        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="ShaderDescription" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating a shader.</param>
        /// <returns>A <see cref="ShaderDescription" /> with any zero-initialized members set to default values.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_defaults")]
        public static extern ShaderDescription QueryDefaults([In] ref ShaderDescription description);

        // TODO: Document allocating a shader
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_shader")]
        public static extern Shader AllocShader();

        /// <summary>
        ///     Creates a <see cref="Shader" /> from the specified <see cref="ShaderDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating a shader.</param>
        /// <returns>A <see cref="Shader" />.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_shader")]
        public static extern Shader CreateShader([In] ref ShaderDescription description);

        /// <summary>
        ///     Creates a <see cref="Shader" /> from the specified <see cref="ShaderDescription" />. For convenience,
        ///     fills in the <see cref="ShaderDescription" /> with specified vertex and fragment stage source code
        ///     before creating the <see cref="Shader" />.
        /// </summary>
        /// <param name="description">The parameters for creating a shader.</param>
        /// <param name="vertexStageSourceCode">The "per-vertex processing" stage source code.</param>
        /// <param name="fragmentStageSourceCode">The "per-fragment processing" stage source code.</param>
        /// <returns>A <see cref="Shader" />.</returns>
        public static Shader CreateShader(
            [In] ref ShaderDescription description,
            string vertexStageSourceCode,
            string fragmentStageSourceCode)
        {
            var vertexStageCodePointer = Marshal.StringToHGlobalAnsi(vertexStageSourceCode);
            var fragmentStageCodePointer = Marshal.StringToHGlobalAnsi(fragmentStageSourceCode);

            description.VertexStage.SourceCode = vertexStageCodePointer;
            description.FragmentStage.SourceCode = fragmentStageCodePointer;

            var shader = CreateShader(ref description);

            Marshal.FreeHGlobal(vertexStageCodePointer);
            Marshal.FreeHGlobal(fragmentStageCodePointer);

            return shader;
        }

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_shader")]
        private static extern void Init(Shader shader, [In] ref ShaderDescription desc);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_shader")]
        private static extern void Destroy(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_shader")]
        private static extern void Fail(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_state")]
        private static extern ResourceState QueryState(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_info")]
        private static extern ShaderInfo QueryInfo(Shader shader);
    }
}
