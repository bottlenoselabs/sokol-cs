// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
namespace Sokol
{
    /// <summary>
    ///     Represents a vertex or index buffer GPU resource.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct Buffer
    {
        /// <summary>
        ///     A 32-bit number which uniquely identifies the GPU resource.
        /// </summary>
        [FieldOffset(0)]
        public uint Identifier;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
