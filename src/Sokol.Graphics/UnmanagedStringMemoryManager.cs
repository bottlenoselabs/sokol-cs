// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

internal static class UnmanagedStringMemoryManager
{
    private static readonly Dictionary<string, IntPtr> StringsToPointers = new Dictionary<string, IntPtr>();
    private static readonly Dictionary<IntPtr, string> PointersToStrings = new Dictionary<IntPtr, string>();

    public static string GetString(in IntPtr pointer)
    {
        return PointersToStrings.TryGetValue(pointer, out var value) ? value : string.Empty;
    }

    public static IntPtr SetString(string @string)
    {
        if (StringsToPointers.TryGetValue(@string, out var pointer))
        {
            return pointer;
        }

        pointer = Marshal.StringToHGlobalAnsi(@string);
        PointersToStrings.Add(pointer, @string);
        StringsToPointers.Add(@string, pointer);

        return pointer;
    }

    public static void Clear()
    {
        foreach (var pointer in PointersToStrings.Keys)
        {
            Marshal.FreeHGlobal(pointer);
        }

        PointersToStrings.Clear();
        StringsToPointers.Clear();

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}
