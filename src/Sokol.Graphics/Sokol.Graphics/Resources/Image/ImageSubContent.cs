// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeInternal
// ReSharper disable MemberCanBePrivate.Global

namespace Sokol.Graphics
{
    /// <summary>
    ///     Pointer to and the size of the data for an <see cref="Image" /> layer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public struct ImageSubContent
    {
        /// <summary>
        ///     The pointer to the starting address of the block of bytes as data.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Data;

        /// <summary>
        ///     The size of the block of bytes as data.
        /// </summary>
        [FieldOffset(8)]
        public int Size;

        /// <summary>
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields of the <see cref="ImageSubContent" /> given
        ///     the specified <see cref="Span{T}" /> struct. It is assumed that the <paramref name="data" /> is
        ///     already unmanaged or externally pinned.
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
        ///     Sets the <see cref="Data" /> and <see cref="Size" /> fields of the <see cref="ImageSubContent" /> given
        ///     the specified <see cref="Memory{T}" /> struct.
        /// </summary>
        /// <param name="data">The memory block.</param>
        /// <typeparam name="T">The type of data in the memory block.</typeparam>
        public void SetData<T>(Memory<T> data)
        {
            SetData(data.Span);
        }
    }
}
