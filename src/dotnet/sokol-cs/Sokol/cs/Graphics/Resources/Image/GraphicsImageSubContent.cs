// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The size and pointer to the data for a <see cref="GraphicsImage" /> slice.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsImageContent" /> is blittable to the C `sg_subimage_content` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public struct GraphicsImageSubContent
    {
        /// <summary>
        ///     The pointer to the starting address of the data as a block of bytes.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Data;

        /// <summary>
        ///     The size of the data as a block of bytes.
        /// </summary>
        [FieldOffset(8)]
        public int Size;

        /// <summary>
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields of the <see cref="GraphicsImageSubContent" /> given
        ///     the pointed memory of the specified <see cref="Span{T}" /> that is unmanaged or externally pinned.
        /// </summary>
        /// <param name="data">The memory block.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public unsafe void SetData<T>(Span<T> data)
        {
            ref var reference = ref MemoryMarshal.GetReference(data);
            Data = (IntPtr)Unsafe.AsPointer(ref reference);
            Size = Marshal.SizeOf<T>() * data.Length;
        }

        /// <summary>
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields of the <see cref="GraphicsImageSubContent" /> given
        ///     the pointed memory of the specified <see cref="Memory{T}" />.
        /// </summary>
        /// <param name="data">The memory block.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void SetData<T>(Memory<T> data) => SetData(data.Span);
    }
}
