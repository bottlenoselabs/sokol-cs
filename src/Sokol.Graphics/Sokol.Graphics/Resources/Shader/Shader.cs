// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds the programmable "per-vertex processing" and "per-fragment processing" stages
    ///     of a <see cref="Pipeline" /> and the global variables used in those stages known as uniforms.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between drawing commands.
    ///     </para>
    ///     <para>
    ///         A <see cref="Shader" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Shader" /> is blittable to the C `sg_shader` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Shader
    {
        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="ShaderDescription" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating a shader.</param>
        /// <returns>A <see cref="ShaderDescription" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShaderDescription QueryDefaults([In] ref ShaderDescription description)
        {
            return ShaderPInvoke.QueryDefaults(ref description);
        }

        // TODO: Document allocating a shader
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shader AllocShader()
        {
            return ShaderPInvoke.AllocShader();
        }

        /// <summary>
        ///     Creates a <see cref="Shader" /> from the specified <see cref="ShaderDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating a shader.</param>
        /// <returns>A <see cref="Shader" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shader CreateShader([In] ref ShaderDescription description)
        {
            return ShaderPInvoke.CreateShader(ref description);
        }

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

        /// <summary>
        ///     A number which uniquely identifies the <see cref="Shader" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document `ImageInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ShaderInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ShaderPInvoke.QueryInfo(this);
        }

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ShaderPInvoke.QueryState(this);
        }

        // TODO: Document manual initialization of a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init([In] ref ShaderDescription description)
        {
            ShaderPInvoke.Init(this, ref description);
        }

        /// <summary>
        ///     Destroys the <see cref="Shader" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            ShaderPInvoke.Destroy(this);
        }

        // TODO: Document failing a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            ShaderPInvoke.Fail(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
