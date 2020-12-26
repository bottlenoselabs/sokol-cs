// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NativeTools;
using Sokol;
using static sokol_app;
using static sokol_gfx;

namespace Samples.C.Clear
{
    internal static unsafe class Program
    {
        private static Rgba32F _clearColor;

        private static void Main()
        {
            LibraryLoader.SetDllImportResolver(Assembly.GetAssembly(typeof(sokol_gfx))!);
            Run();
        }

        private static void Run()
        {
            var descriptor = default(sapp_desc);
            descriptor.window_title = UnmanagedStrings.GetBytes("Clear");
            FillCallbacks(ref descriptor);
            sapp_run(&descriptor);
        }

        private static void FillCallbacks(ref sapp_desc descriptor)
        {
            var initializeCallbackFunctionPointer = (delegate* unmanaged[Cdecl] <void>)&Initialize;
            descriptor.init_cb = (IntPtr)initializeCallbackFunctionPointer;

            var frameCallbackFunctionPointer = (delegate* unmanaged[Cdecl] <void>)&Frame;
            descriptor.frame_cb = (IntPtr)frameCallbackFunctionPointer;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void Initialize()
        {
            InitializeGraphics();
            CreateResources();
            UnmanagedStrings.Clear();
        }

        private static void InitializeGraphics()
        {
            var graphicsDescriptor = default(sokol_gfx.sg_desc);
            graphicsDescriptor.context = sokol_glue.sapp_sgcontext();
            sg_setup(&graphicsDescriptor);
        }

        private static void CreateResources()
        {
            _clearColor = Rgba32F.Red;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void Frame()
        {
            // move the color towards yellow, then reset, in repeat
            _clearColor.G = _clearColor.G > 1.0f ? 0.0f : _clearColor.G + 0.01f;

            var passAction = default(sg_pass_action);

            ref var color0 = ref passAction.colors();
            color0.action = sg_action.SG_ACTION_CLEAR;
            color0.val = _clearColor;

            var width = sapp_width();
            var height = sapp_height();
            sg_begin_default_pass(&passAction, width, height);
            sg_end_pass();

            sg_commit();
        }
    }
}
