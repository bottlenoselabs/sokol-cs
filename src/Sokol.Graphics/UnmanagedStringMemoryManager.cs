// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

internal static class UnmanagedStringMemoryManager
{
    private static readonly Dictionary<string, IntPtr> _stringsToPointers = new Dictionary<string, IntPtr>();
    private static readonly Dictionary<IntPtr, string> _pointersToStrings = new Dictionary<IntPtr, string>();

    public static string GetString(in IntPtr pointer)
    {
        return _pointersToStrings.TryGetValue(pointer, out var value) ? value : string.Empty;
    }

    public static IntPtr SetString(string @string)
    {
        if (_stringsToPointers.TryGetValue(@string, out var pointer))
        {
            return pointer;
        }

        pointer = Marshal.StringToHGlobalAnsi(@string);
        _pointersToStrings.Add(pointer, @string);
        _stringsToPointers.Add(@string, pointer);

        return pointer;
    }

    public static void Clear()
    {
        foreach (var pointer in _pointersToStrings.Keys)
        {
            Marshal.FreeHGlobal(pointer);
        }

        _pointersToStrings.Clear();
        _stringsToPointers.Clear();
    }
}
