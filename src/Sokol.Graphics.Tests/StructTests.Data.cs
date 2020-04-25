// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Xunit;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable 649

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        public static List<object[]> SokolStructs { get; }

        public static List<object[]> SokolStructsAndCCopies { get; }

        static StructTests()
        {
            SokolStructs = InitializeSokolStructs();
            SokolStructsAndCCopies = InitializeSokolStructsAndGeneratedLayoutSequentialCopies();
        }

        private static List<object[]> InitializeSokolStructs()
        {
            var structs = new List<object[]>();
            var sokolType = typeof(sokol_gfx);
            var types = sokolType.GetNestedTypes();
            var typesSet = new HashSet<Type>();

            foreach (var type in types)
            {
                if (typesSet.Contains(type))
                {
                    continue;
                }

                if (!type.IsValueType || type.IsEnum)
                {
                    typesSet.Add(type);
                    continue;
                }

                structs.Add(new object[] { type });
                typesSet.Add(type);
            }

            return structs;
        }

        private static List<object[]> InitializeSokolStructsAndGeneratedLayoutSequentialCopies()
        {
            var structs = new List<object[]>();
            var sokolType = typeof(sokol_gfx);
            var types = sokolType.GetNestedTypes();
            var typesSet = new HashSet<Type>();

            foreach (var type in types)
            {
                if (typesSet.Contains(type))
                {
                    continue;
                }

                if (!type.IsValueType || type.IsEnum)
                {
                    typesSet.Add(type);
                    continue;
                }

                var generatedStruct = type.CStructType();
                structs.Add(new object[] { type, generatedStruct });

                typesSet.Add(type);
            }

            return structs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Struct_Char
        {
            public uint Uint;
            public char Char;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Struct_IntArray
        {
            public uint Uint;
            public int[] Integers;
        }

        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct Struct_SloppyPack
        {
            [FieldOffset(0)]
            public byte B1;

            [FieldOffset(4)]
            public int I1;

            [FieldOffset(8)]
            public byte B2;

            [FieldOffset(12)]
            public int I2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Struct_TightPack
        {
            public byte B1;
            public int I1;
            public byte B2;
            public int I2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MyStruct_Nested
        {
            public Struct_SloppyPack SloppyPack;

            // 2 BYTES OF PADDING FOR ALIGNMENT
            public Struct_TightPack TightPack;
        }
    }
}
