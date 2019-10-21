using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#pragma warning disable 649
// ReSharper disable InconsistentNaming
// ReSharper disable SuspiciousTypeConversion.Global
// ReSharper disable CollectionNeverQueried.Global

namespace Sokol.Graphics.Tests
{
    public partial class StructTests
    {
        private static Random _random;
        private static List<object[]> _sokolStructs;
        private static List<object[]> _sokolStructsAndGeneratedLayoutSequentialCopies;

        public static Random Random => _random ??= new Random(Guid.NewGuid().GetHashCode());

        public static List<object[]> SokolStructs
        {
            get
            {
                if (_sokolStructs != null)
                {
                    return _sokolStructs;
                }
                
                _sokolStructs = new List<object[]>();
                InitializeSokolStructs();
                return _sokolStructs;
            }
        }

        public static List<object[]> SokolStructsAndCCopies
        {
            get
            {
                if (_sokolStructsAndGeneratedLayoutSequentialCopies != null)
                {
                    return _sokolStructsAndGeneratedLayoutSequentialCopies;
                }
                
                _sokolStructsAndGeneratedLayoutSequentialCopies = new List<object[]>();
                InitializeSokolStructsAndGeneratedLayoutSequentialCopies();
                return _sokolStructsAndGeneratedLayoutSequentialCopies;
            }
        }

        private static void InitializeSokolStructs()
        {
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

                _sokolStructs.Add(new object[] {type});
                typesSet.Add(type);
            }
        }
        
        private static void InitializeSokolStructsAndGeneratedLayoutSequentialCopies()
        {
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
                _sokolStructsAndGeneratedLayoutSequentialCopies.Add(new object[] {type, generatedStruct});
                
                typesSet.Add(type);
            }
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