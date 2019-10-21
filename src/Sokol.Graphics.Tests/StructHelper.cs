using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics.Tests
{
    public static class StructHelper
    {
        private static readonly AssemblyName StructGeneratorAssemblyName = new AssemblyName("GeneratedStructsAssembly");
        private static readonly ModuleBuilder ModuleBuilder;
        private static readonly Dictionary<Type, Type> StructGeneratedTypesByStructTypes = new Dictionary<Type, Type>();

        static StructHelper()
        {
            var assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(StructGeneratorAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder = assemblyBuilder.DefineDynamicModule(StructGeneratorAssemblyName.Name);
        }

        public static Type CStructType(this Type structType)
        {
            if (structType == null) return null;
            if (StructGeneratedTypesByStructTypes.TryGetValue(structType, out var generatedStructType))
            {
                return generatedStructType;
            }

            if (structType.IsPointer)
            {
                var pointerType = typeof(IntPtr);
                StructGeneratedTypesByStructTypes[structType] = pointerType;
                return pointerType;
            }

            if (structType.IsEnum)
            {
                var baseType = Enum.GetUnderlyingType(structType);
                StructGeneratedTypesByStructTypes[structType] = baseType;
                return baseType;
            }

            if (structType.FullName?.StartsWith("System.") ?? false)
            {
                StructGeneratedTypesByStructTypes[structType] = structType;
                return structType;
            }

            if (structType.Name.StartsWith("<"))
            {
                StructGeneratedTypesByStructTypes[structType] = structType;
                return structType;
            }

            var generatedStructName = $"{structType.Name}<C>";
            var generatedStructTypeBuilder = ModuleBuilder.DefineType(generatedStructName,
                TypeAttributes.Public |
                TypeAttributes.Sealed |
                TypeAttributes.SequentialLayout |
                TypeAttributes.Serializable |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit,
                typeof(ValueType));

            var fields = structType.GetTypeInfo().DeclaredFields;
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                var structTypeC = CStructType(fieldType);

                generatedStructTypeBuilder.DefineField(field.Name, structTypeC, FieldAttributes.Public);
            }

            generatedStructType = generatedStructTypeBuilder.CreateType();
            StructGeneratedTypesByStructTypes[structType] = generatedStructType;

            return generatedStructType;
        }
    }
}