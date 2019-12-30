using System;
using System.Runtime.InteropServices;

namespace Sokol.Samples
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetPointerDelegate();
}