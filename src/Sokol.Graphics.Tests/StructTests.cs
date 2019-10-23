using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        [Fact]
        public void Struct_Char_IsNotBlittable()
        {
            // Arrange
            
            // Act
            var isBlittable = BlittableHelper.IsBlittable<Struct_Char>();

            isBlittable.Should().BeFalse("struct is not blittable");
        }

        [Fact]
        public void Struct_IntArray_IsNotBlittable()
        {
            // Arrange
            
            // Act
            var isBlittable = BlittableHelper.IsBlittable<Struct_IntArray>();

            isBlittable.Should().BeFalse("struct is not blittable");
        }
        
        [Fact]
        public void Struct_TightPack_IsBlittable()
        {
            // Arrange
            
            // Act
            var isBlittable = BlittableHelper.IsBlittable<Struct_TightPack>();

            isBlittable.Should().BeTrue("struct is blittable");
        }
        
        [Fact]
        public void Struct_SloppyPack_IsBlittable()
        {
            // Arrange
            
            // Act
            var isBlittable = BlittableHelper.IsBlittable<Struct_SloppyPack>();

            isBlittable.Should().BeTrue("struct is blittable");
        }
        
        [Theory]
        [MemberData(nameof(SokolStructs))]
        public void Struct_IsBlittable(Type structType)
        {
            Assert.True(structType.IsBlittable());
        }

        [Fact]
        public void Struct_TightPack_IsNotCEquivalent()
        {
            // Arrange
            var structType = typeof(Struct_TightPack);
            var generatedStructCopyType = structType.CStructType();
            
            // Act
            var expectedSize = Marshal.SizeOf(structType);
            var actualSize = Marshal.SizeOf(generatedStructCopyType);
            
            // Assert
            expectedSize.Should().NotBe(actualSize, "struct is not C equivalent");
        }
        
        [Fact]
        public void Struct_SloppyPack_IsCEquivalent()
        {
            // Arrange
            var structType = typeof(Struct_SloppyPack);
            var generatedStructCopyType = structType.CStructType();
            
            // Act
            var expectedSize = Marshal.SizeOf(structType);
            var actualSize = Marshal.SizeOf(generatedStructCopyType);
            
            // Assert
            expectedSize.Should().Be(actualSize, "struct is C equivalent");
        }

        [Fact]
        public void Struct_Nested_IsNotCEquivalent()
        {
            var structType = typeof(MyStruct_Nested);
            var generatedStructCopyType = structType.CStructType();
            var expectedSize = Marshal.SizeOf(structType);
            var actualSize = Marshal.SizeOf(generatedStructCopyType);
            expectedSize.Should().NotBe(actualSize, "struct is not C equivalent");
            Assert.NotEqual(expectedSize, actualSize);
        }

        [Theory]
        [MemberData(nameof(SokolStructsAndCCopies))]
        public void Struct_IsCEquivalent(Type structType, Type structTypeC)
        {
            var structFields = structType.GetTypeInfo().DeclaredFields.ToArray();
            var structCFields = structTypeC.GetTypeInfo().DeclaredFields.ToArray();
            
            var structFieldIndex = 0;
            var structCFieldIndex = 0;
            var currentFieldPosition = -1;
            while (true)
            {
                if (structFieldIndex >= structFields.Length || structCFieldIndex >= structCFields.Length)
                {
                    break;
                }
                
                var expectedField = structFields[structFieldIndex];
                var actualField = structCFields[structCFieldIndex];
                
                var fieldOffsetAttribute = expectedField.GetCustomAttribute<FieldOffsetAttribute>();
                if (fieldOffsetAttribute?.Value == currentFieldPosition)
                {
                    structFieldIndex++;
                    continue;
                }
                
                if (fieldOffsetAttribute != null)
                {
                    currentFieldPosition = fieldOffsetAttribute.Value;
                }
                
                var expectedOffset = Marshal.OffsetOf(structType, expectedField.Name);
                var actualOffset = Marshal.OffsetOf(structTypeC, actualField.Name);
                
                expectedOffset.Should().BeEquivalentTo(actualOffset, "field offsets should be equal");
                structFieldIndex++;
                structCFieldIndex++;
            }

            var expectedSize = Marshal.SizeOf(structType);
            var actualSize = Marshal.SizeOf(structTypeC);
            expectedSize.Should().Be(actualSize, "marshaled struct sizes should be equal");
        }
    }
}