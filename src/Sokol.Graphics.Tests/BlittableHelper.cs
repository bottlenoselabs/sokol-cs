// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Sokol.Graphics.Tests
{
    internal static class BlittableHelper
    {
        public static bool IsBlittable<T>()
        {
            return IsBlittableCache<T>.Value;
        }

        public static bool IsBlittable(this Type type)
        {
            if (type == typeof(decimal))
            {
                return false;
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                return elementType != null && elementType.IsValueType && IsBlittable(elementType);
            }

            try
            {
                var instance = FormatterServices.GetUninitializedObject(type);
                GCHandle.Alloc(instance, GCHandleType.Pinned).Free();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static class IsBlittableCache<T>
        {
            public static readonly bool Value = IsBlittable(typeof(T));
        }
    }
}
