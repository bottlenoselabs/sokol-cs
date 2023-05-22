// Copyright (c) Bottlenose Labs Inc. (https://github.com/bottlenoselabs). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using bottlenoselabs.Sokol;
using static bottlenoselabs.ImGui.PInvoke;
using static bottlenoselabs.Sokol.PInvoke;
using ImGui = bottlenoselabs.ImGui.PInvoke.ImGui;

namespace Samples;

internal static unsafe class Program
{
    private static ProgramState _state;

    private struct ProgramState
    {
        public Rgba32F ClearColor;

        public bool ShowTestWindow;
        public bool ShowAnotherWindow;
    }

    private static void Main()
    {
        var desc = default(App.Desc);
        desc.InitCb.Pointer = &Initialize;
        desc.FrameCb.Pointer = &Frame;
        desc.EventCb.Pointer = &Event;
        desc.Width = 800;
        desc.Height = 600;
        desc.SampleCount = 4;
        desc.WindowTitle = "ImGui";
        desc.Icon.SokolDefault = true;

        App.Run(&desc);
    }

    [UnmanagedCallersOnly]
    private static void Initialize()
    {
        var desc = default(Graphics.Desc);
        desc.Context = App.Sgcontext();
        Graphics.Setup(&desc);

        var imGuiDesc = default(SImGui.DescT);
        SImGui.Setup(&imGuiDesc);
    }

    [UnmanagedCallersOnly]
    private static void Frame()
    {
        Draw();
        Graphics.Commit();
    }

    [UnmanagedCallersOnly]
    private static void Event(App.Event* e)
    {
        SImGui.HandleEvent(e);
    }

    private static void Draw()
    {
        var width = App.Width();
        var height = App.Height();

        var imGuiFrameDesc = default(SImGui.FrameDescT);
        imGuiFrameDesc.Width = width;
        imGuiFrameDesc.Height = height;
        imGuiFrameDesc.DeltaTime = App.FrameDuration();
        imGuiFrameDesc.DpiScale = App.DpiScale();
        SImGui.NewFrame(&imGuiFrameDesc);

        var action = default(Graphics.PassAction);
        ref var colorAttachment = ref action.Colors[0];
        colorAttachment.Action = Graphics.Action.Clear;
        colorAttachment.Value = Rgba32F.Black;
        Graphics.BeginDefaultPass(&action, width, height);

        // 1. Show a simple window
        // Tip: if we don't call ImGui::Begin()/ImGui::End() the widgets appears in a window automatically called "Debug"
        var f = 0.0f;
        ImGui.Text((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Hello, world!");
        ImGui.SliderFloat((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"float", &f, 0.0f, 1.0f, (bottlenoselabs.ImGui.PInvoke.Runtime.CString)"%.3f", ImGuiSliderFlags.None);

        ImGui.ColorEdit3((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"clear color", (float*)Unsafe.AsPointer(ref _state.ClearColor.R), 0);
        if (ImGui.Button((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Test Window", Vector2.Zero))
        {
            _state.ShowTestWindow = !_state.ShowTestWindow;
        }

        var format = string.Format(
            CultureInfo.InvariantCulture,
            "Application average {0} ms/frame {1} FPS)",
            1000.0f / ImGui.GetIO()->Framerate,
            ImGui.GetIO()->Framerate);
        ImGui.Text((bottlenoselabs.ImGui.PInvoke.Runtime.CString)format);

        var format2 = string.Format(
            CultureInfo.InvariantCulture,
            "w: {0}, h: {1}, dpi_scale: {2}",
            App.Width(),
            App.Height(),
            App.DpiScale());
        ImGui.Text((bottlenoselabs.ImGui.PInvoke.Runtime.CString)format2);

        var format3 = App.IsFullscreen() ? (bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Switch to windowed" : (bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Switch to fullscreen";
        if (ImGui.Button(format3, Vector2.Zero))
        {
            App.ToggleFullscreen();
        }

        if (_state.ShowTestWindow)
        {
            ImGui.SetNextWindowSize(new Vector2(200, 100), ImGuiCond.FirstUseEver);
            ImGui.Begin((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Another Window", (bottlenoselabs.ImGui.PInvoke.Runtime.CBool*)Unsafe.AsPointer(ref _state.ShowAnotherWindow), 0);
            ImGui.Text((bottlenoselabs.ImGui.PInvoke.Runtime.CString)"Hello");
            ImGui.End();
        }

        SImGui.Render();
        Graphics.EndPass();
    }
}
