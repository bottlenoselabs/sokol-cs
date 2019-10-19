using System;
using System.Runtime.InteropServices;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        [Fact]
        public void Struct_WithChar_IsNotBlittable()
        {
            Assert.False(BlittableHelper.IsBlittable<MyNonBlittableStruct_Char>());
        }

        [Fact]
        public void Struct_WithArray_IsNotBlittable()
        {
            Assert.False(BlittableHelper.IsBlittable<MyNonBlittableStruct_IntArray>());
        }

        [Theory]
        [MemberData(nameof(SokolStructs))]
        public void SokolStruct_IsBlittable(Type structType)
        {
            Assert.True(structType.IsBlittable());
        }

        [Theory]
        [MemberData(nameof(SokolStructsWithExpectedSizes))]
        public void SokolStruct_IsExpectedSize(Type structType, int expectedSize)
        {
            var actualSize = Marshal.SizeOf(structType);
            Assert.Equal(expectedSize, actualSize);
        }
    }
}