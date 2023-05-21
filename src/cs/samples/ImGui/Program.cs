// Copyright (c) Bottlenose Labs Inc. (https://github.com/bottlenoselabs). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System.Runtime.InteropServices;
using bottlenoselabs.Sokol;
using static bottlenoselabs.Sokol.PInvoke;

namespace ImGui;

internal static unsafe class Program
{
    private static void Main()
    {
        var desc = default(App.Desc);
        desc.InitCb.Pointer = &Initialize;
        desc.FrameCb.Pointer = &Frame;
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

        var imGuiDesc = default(PInvoke.ImGui.DescT);
        PInvoke.ImGui.Setup(&imGuiDesc);
    }

    [UnmanagedCallersOnly]
    private static void Frame()
    {
        Draw();
        Graphics.Commit();
    }

    private static void Draw()
    {
        var width = App.Width();
        var height = App.Height();

        var imGuiFrameDesc = default(PInvoke.ImGui.FrameDescT);
        imGuiFrameDesc.Width = width;
        imGuiFrameDesc.Height = height;
        imGuiFrameDesc.DeltaTime = App.FrameDuration();
        imGuiFrameDesc.DpiScale = App.DpiScale();
        PInvoke.ImGui.NewFrame(&imGuiFrameDesc);

        var action = default(Graphics.PassAction);
        ref var colorAttachment = ref action.Colors[0];
        colorAttachment.Action = Graphics.Action.Clear;
        colorAttachment.Value = Rgba32F.Black;
        Graphics.BeginDefaultPass(&action, width, height);

        // 1. Show a simple window
        // Tip: if we don't call ImGui::Begin()/ImGui::End() the widgets appears in a window automatically called "Debug"
        static float f = 0.0f;
        igText("Hello, world!");
        igSliderFloat("float", &f, 0.0f, 1.0f, "%.3f", ImGuiSliderFlags_None);
        igColorEdit3("clear color", &state.pass_action.colors[0].clear_value.r, 0);
        if (igButton("Test Window", (ImVec2) { 0.0f, 0.0f})) state.show_test_window ^= 1;
        if (igButton("Another Window", (ImVec2) { 0.0f, 0.0f })) state.show_another_window ^= 1;
        igText("Application average %.3f ms/frame (%.1f FPS)", 1000.0f / igGetIO()->Framerate, igGetIO()->Framerate);

        // 2. Show another simple window, this time using an explicit Begin/End pair
        igSetNextWindowSize((ImVec2){200,100}, ImGuiCond_FirstUseEver);
        igBegin("Another Window", &state.show_another_window, 0);
        igText("Hello");
        igEnd();

        // 3. Show the ImGui test window. Most of the sample code is in ImGui::ShowDemoWindow()
        igSetNextWindowPos((ImVec2){460,20}, ImGuiCond_FirstUseEver, (ImVec2){0,0});
        igShowDemoWindow(0);

        PInvoke.ImGui.Render();
        Graphics.EndPass();
    }
}
