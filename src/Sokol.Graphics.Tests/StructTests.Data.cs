using System;
using System.Collections.Generic;
using System.Reflection;

#pragma warning disable 649
// ReSharper disable InconsistentNaming
// ReSharper disable SuspiciousTypeConversion.Global

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        public static List<object[]> SokolStructs { get; } = new List<object[]>();
        public static List<object[]> SokolStructsWithExpectedSizes { get; } = new List<object[]>();

        static StructTests()
        {
            var sokolStructs = SokolStructs;
            var sokolStructsWithExpectedSizes = SokolStructsWithExpectedSizes;
            
            var sokolType = typeof(sokol_gfx);
            foreach (var type in sokolType.GetNestedTypes())
            {
                if (!type.IsValueType || type.IsEnum)
                {
                    continue;
                }

                sokolStructs.Add(new object[] {type});

                var constSizeName = $"{type.Name.ToUpper()}_SIZE";
                var fieldInfo = sokolType.GetField(constSizeName,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if (fieldInfo == null || !fieldInfo.IsLiteral)
                {
                    throw new Exception($"Did you forget to add a const {constSizeName} for the struct {type.Name}?");
                }

                var expectedSize = fieldInfo.GetRawConstantValue();

                sokolStructsWithExpectedSizes.Add(new[] {type, expectedSize});
            }
        }

        private struct MyNonBlittableStruct_Char
        {
            public uint Uint;
            public char Char;
        }

        private struct MyNonBlittableStruct_IntArray
        {
            public uint Uint;
            public int[] Integers;
        }

    }
}