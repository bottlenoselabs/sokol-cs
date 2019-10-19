using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#pragma warning disable 649
// ReSharper disable InconsistentNaming

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        private static readonly List<object[]> _sokolStructs = new List<object[]>();
        private static readonly List<object[]> _sokolStructsWithExpectedSizes = new List<object[]>();

        public static IEnumerable<object[]> SokolStructs => _sokolStructs;
        public static IEnumerable<object[]> SokolStructsWithExpectedSizes => _sokolStructsWithExpectedSizes;

        static StructTests()
        {
            var sokolType = typeof(sokol_gfx);
            foreach (var type in sokolType.GetNestedTypes())
            {
                if (!type.IsValueType || type.IsEnum)
                {
                    continue;
                }

                _sokolStructs.Add(new object[] {type});

                var constSizeName = $"{type.Name.ToUpper()}_SIZE";
                var fieldInfo = sokolType.GetField(constSizeName,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if (fieldInfo == null || !fieldInfo.IsLiteral)
                {
                    throw new Exception($"Did you forget to add a const {constSizeName} for the struct {type.Name}?");
                }

                var expectedSize = fieldInfo.GetRawConstantValue();

                _sokolStructsWithExpectedSizes.Add(new[] {type, expectedSize});
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