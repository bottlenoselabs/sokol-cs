// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static SDL2.SDL;

// ReSharper disable MemberCanBeInternal

namespace Sokol.App
{
#pragma warning disable 1591
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public class AppLoop
    {
        private static readonly InputState InputState = new InputState();
        private readonly AppTime _time;

        protected bool IsRunning
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }

        protected AppLoop()
        {
            _time = new AppTime();
        }

        public virtual void Run()
        {
            IsRunning = true;

            var previousTicks = SDL_GetPerformanceCounter();
            var accumulatedTime = TimeSpan.Zero;
            var totalTime = TimeSpan.Zero;

            while (true)
            {
                PumpEvents();
                if (!IsRunning)
                {
                    break;
                }

                var currentTicks = SDL_GetPerformanceCounter();
                var elapsedSeconds = (currentTicks - previousTicks) / (double)SDL_GetPerformanceFrequency();
                var elapsedTime = new TimeSpan((long)(elapsedSeconds * TimeSpan.TicksPerSecond));
                accumulatedTime += elapsedTime;
                previousTicks = currentTicks;

                HandleInput(elapsedTime);
                Update(totalTime, elapsedTime);
                Draw(totalTime, elapsedTime, 1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Stop()
        {
            IsRunning = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void PumpEvents()
        {
            while (SDL_PollEvent(out var e) != 0)
            {
                HandleEvent(e);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void HandleInput(TimeSpan elapsedTime)
        {
            InputState.Update(elapsedTime);
            App.Instance.DoInput(InputState);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Update(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            _time.TotalTime = totalTime;
            _time.ElapsedTime = elapsedTime;
            _time.Alpha = 0;
            App.Instance.DoUpdate(_time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Draw(TimeSpan totalTime, TimeSpan elapsedTime, float alpha = 0.0f)
        {
            _time.TotalTime = totalTime;
            _time.ElapsedTime = elapsedTime;
            _time.Alpha = alpha;
            App.Instance.DoDraw(_time);
        }

        private static void HandleEvent(SDL_Event e)
        {
            // https://wiki.libsdl.org/SDL_EventType
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                case SDL_EventType.SDL_APP_TERMINATING:
                    App.Instance!.Exit();
                    break;
                case SDL_EventType.SDL_WINDOWEVENT:
                    HandleWindowEvent(e.window);
                    break;
                case SDL_EventType.SDL_KEYDOWN:
                case SDL_EventType.SDL_KEYUP:
                    HandleKeyboardEvent(e.key);
                    break;
                case SDL_EventType.SDL_TEXTINPUT:
                    HandleTextInputEvent(e.text);
                    break;
                case SDL_EventType.SDL_MOUSEMOTION:
                    HandleMouseMotionEvent(e.motion);
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    HandleMouseButtonEvent(e.button);
                    break;
                case SDL_EventType.SDL_MOUSEWHEEL:
                    HandleMouseWheelEvent(e.wheel);
                    break;
                case SDL_EventType.SDL_DROPFILE:
                case SDL_EventType.SDL_DROPBEGIN:
                case SDL_EventType.SDL_DROPTEXT:
                    HandleDropEvent(e.drop);
                    break;
            }
        }

        private static void HandleWindowEvent(SDL_WindowEvent e)
        {
            var app = App.Instance;
            // https://wiki.libsdl.org/SDL_WindowEventID
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.windowEvent)
            {
                case SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                    app.Window.OnShown();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                    app.Window.OnHidden();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                    app.Window.OnExposed();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                    app.Window.OnMoved(e.data1, e.data2);
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                case SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                case SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                    app.Window.OnResize(e.data1, e.data2);
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                    app.Window.OnFocused();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                    app.Window.OnUnfocused();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                    app.Window.Close();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                    app.Window.OnMouseEntered();
                    break;
                case SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                    app.Window.OnMouseLeft();
                    break;
            }
        }

        private static void HandleKeyboardEvent(SDL_KeyboardEvent e)
        {
            var key = MapKey(e.keysym.scancode);
            var modifiers = MapModifierKeys(e.keysym.mod);
            var isDown = e.state == 1;

            InputState.HandleKeyboardEvent(key, isDown, modifiers);

            if (isDown)
            {
                App.Instance.Window.OnKeyDown(key, modifiers);
            }
            else
            {
                App.Instance.Window.OnKeyUp(key, modifiers);
            }
        }

        private static KeyboardKey MapKey(SDL_Scancode scanCode)
        {
            return scanCode switch
            {
                SDL_Scancode.SDL_SCANCODE_A => KeyboardKey.A,
                SDL_Scancode.SDL_SCANCODE_B => KeyboardKey.B,
                SDL_Scancode.SDL_SCANCODE_C => KeyboardKey.C,
                SDL_Scancode.SDL_SCANCODE_D => KeyboardKey.D,
                SDL_Scancode.SDL_SCANCODE_E => KeyboardKey.E,
                SDL_Scancode.SDL_SCANCODE_F => KeyboardKey.F,
                SDL_Scancode.SDL_SCANCODE_G => KeyboardKey.G,
                SDL_Scancode.SDL_SCANCODE_H => KeyboardKey.H,
                SDL_Scancode.SDL_SCANCODE_I => KeyboardKey.I,
                SDL_Scancode.SDL_SCANCODE_J => KeyboardKey.J,
                SDL_Scancode.SDL_SCANCODE_K => KeyboardKey.K,
                SDL_Scancode.SDL_SCANCODE_L => KeyboardKey.L,
                SDL_Scancode.SDL_SCANCODE_M => KeyboardKey.M,
                SDL_Scancode.SDL_SCANCODE_N => KeyboardKey.N,
                SDL_Scancode.SDL_SCANCODE_O => KeyboardKey.O,
                SDL_Scancode.SDL_SCANCODE_P => KeyboardKey.P,
                SDL_Scancode.SDL_SCANCODE_Q => KeyboardKey.Q,
                SDL_Scancode.SDL_SCANCODE_R => KeyboardKey.R,
                SDL_Scancode.SDL_SCANCODE_S => KeyboardKey.S,
                SDL_Scancode.SDL_SCANCODE_T => KeyboardKey.T,
                SDL_Scancode.SDL_SCANCODE_U => KeyboardKey.U,
                SDL_Scancode.SDL_SCANCODE_V => KeyboardKey.V,
                SDL_Scancode.SDL_SCANCODE_W => KeyboardKey.W,
                SDL_Scancode.SDL_SCANCODE_X => KeyboardKey.X,
                SDL_Scancode.SDL_SCANCODE_Y => KeyboardKey.Y,
                SDL_Scancode.SDL_SCANCODE_Z => KeyboardKey.Z,
                SDL_Scancode.SDL_SCANCODE_1 => KeyboardKey.Number1,
                SDL_Scancode.SDL_SCANCODE_2 => KeyboardKey.Number2,
                SDL_Scancode.SDL_SCANCODE_3 => KeyboardKey.Number3,
                SDL_Scancode.SDL_SCANCODE_4 => KeyboardKey.Number4,
                SDL_Scancode.SDL_SCANCODE_5 => KeyboardKey.Number5,
                SDL_Scancode.SDL_SCANCODE_6 => KeyboardKey.Number6,
                SDL_Scancode.SDL_SCANCODE_7 => KeyboardKey.Number7,
                SDL_Scancode.SDL_SCANCODE_8 => KeyboardKey.Number8,
                SDL_Scancode.SDL_SCANCODE_9 => KeyboardKey.Number9,
                SDL_Scancode.SDL_SCANCODE_0 => KeyboardKey.Number0,
                SDL_Scancode.SDL_SCANCODE_RETURN => KeyboardKey.Enter,
                SDL_Scancode.SDL_SCANCODE_ESCAPE => KeyboardKey.Escape,
                SDL_Scancode.SDL_SCANCODE_BACKSPACE => KeyboardKey.BackSpace,
                SDL_Scancode.SDL_SCANCODE_TAB => KeyboardKey.Tab,
                SDL_Scancode.SDL_SCANCODE_SPACE => KeyboardKey.Space,
                SDL_Scancode.SDL_SCANCODE_MINUS => KeyboardKey.Minus,
                SDL_Scancode.SDL_SCANCODE_EQUALS => KeyboardKey.Plus,
                SDL_Scancode.SDL_SCANCODE_LEFTBRACKET => KeyboardKey.BracketLeft,
                SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET => KeyboardKey.BracketRight,
                SDL_Scancode.SDL_SCANCODE_BACKSLASH => KeyboardKey.BackSlash,
                SDL_Scancode.SDL_SCANCODE_SEMICOLON => KeyboardKey.Semicolon,
                SDL_Scancode.SDL_SCANCODE_APOSTROPHE => KeyboardKey.Quote,
                SDL_Scancode.SDL_SCANCODE_GRAVE => KeyboardKey.Grave,
                SDL_Scancode.SDL_SCANCODE_COMMA => KeyboardKey.Comma,
                SDL_Scancode.SDL_SCANCODE_PERIOD => KeyboardKey.Period,
                SDL_Scancode.SDL_SCANCODE_SLASH => KeyboardKey.Slash,
                SDL_Scancode.SDL_SCANCODE_CAPSLOCK => KeyboardKey.CapsLock,
                SDL_Scancode.SDL_SCANCODE_F1 => KeyboardKey.F1,
                SDL_Scancode.SDL_SCANCODE_F2 => KeyboardKey.F2,
                SDL_Scancode.SDL_SCANCODE_F3 => KeyboardKey.F3,
                SDL_Scancode.SDL_SCANCODE_F4 => KeyboardKey.F4,
                SDL_Scancode.SDL_SCANCODE_F5 => KeyboardKey.F5,
                SDL_Scancode.SDL_SCANCODE_F6 => KeyboardKey.F6,
                SDL_Scancode.SDL_SCANCODE_F7 => KeyboardKey.F7,
                SDL_Scancode.SDL_SCANCODE_F8 => KeyboardKey.F8,
                SDL_Scancode.SDL_SCANCODE_F9 => KeyboardKey.F9,
                SDL_Scancode.SDL_SCANCODE_F10 => KeyboardKey.F10,
                SDL_Scancode.SDL_SCANCODE_F11 => KeyboardKey.F11,
                SDL_Scancode.SDL_SCANCODE_F12 => KeyboardKey.F12,
                SDL_Scancode.SDL_SCANCODE_PRINTSCREEN => KeyboardKey.PrintScreen,
                SDL_Scancode.SDL_SCANCODE_SCROLLLOCK => KeyboardKey.ScrollLock,
                SDL_Scancode.SDL_SCANCODE_PAUSE => KeyboardKey.Pause,
                SDL_Scancode.SDL_SCANCODE_INSERT => KeyboardKey.Insert,
                SDL_Scancode.SDL_SCANCODE_HOME => KeyboardKey.Home,
                SDL_Scancode.SDL_SCANCODE_PAGEUP => KeyboardKey.PageUp,
                SDL_Scancode.SDL_SCANCODE_DELETE => KeyboardKey.Delete,
                SDL_Scancode.SDL_SCANCODE_END => KeyboardKey.End,
                SDL_Scancode.SDL_SCANCODE_PAGEDOWN => KeyboardKey.PageDown,
                SDL_Scancode.SDL_SCANCODE_RIGHT => KeyboardKey.Right,
                SDL_Scancode.SDL_SCANCODE_LEFT => KeyboardKey.Left,
                SDL_Scancode.SDL_SCANCODE_DOWN => KeyboardKey.Down,
                SDL_Scancode.SDL_SCANCODE_UP => KeyboardKey.Up,
                SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR => KeyboardKey.NumLock,
                SDL_Scancode.SDL_SCANCODE_KP_DIVIDE => KeyboardKey.KeypadDivide,
                SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY => KeyboardKey.KeypadMultiply,
                SDL_Scancode.SDL_SCANCODE_KP_MINUS => KeyboardKey.KeypadMinus,
                SDL_Scancode.SDL_SCANCODE_KP_PLUS => KeyboardKey.KeypadPlus,
                SDL_Scancode.SDL_SCANCODE_KP_ENTER => KeyboardKey.KeypadEnter,
                SDL_Scancode.SDL_SCANCODE_KP_1 => KeyboardKey.Keypad1,
                SDL_Scancode.SDL_SCANCODE_KP_2 => KeyboardKey.Keypad2,
                SDL_Scancode.SDL_SCANCODE_KP_3 => KeyboardKey.Keypad3,
                SDL_Scancode.SDL_SCANCODE_KP_4 => KeyboardKey.Keypad4,
                SDL_Scancode.SDL_SCANCODE_KP_5 => KeyboardKey.Keypad5,
                SDL_Scancode.SDL_SCANCODE_KP_6 => KeyboardKey.Keypad6,
                SDL_Scancode.SDL_SCANCODE_KP_7 => KeyboardKey.Keypad7,
                SDL_Scancode.SDL_SCANCODE_KP_8 => KeyboardKey.Keypad8,
                SDL_Scancode.SDL_SCANCODE_KP_9 => KeyboardKey.Keypad9,
                SDL_Scancode.SDL_SCANCODE_KP_0 => KeyboardKey.Keypad0,
                SDL_Scancode.SDL_SCANCODE_KP_PERIOD => KeyboardKey.KeypadPeriod,
                SDL_Scancode.SDL_SCANCODE_NONUSBACKSLASH => KeyboardKey.NonUsBackSlash,
                SDL_Scancode.SDL_SCANCODE_KP_EQUALS => KeyboardKey.KeypadPlus,
                SDL_Scancode.SDL_SCANCODE_F13 => KeyboardKey.F13,
                SDL_Scancode.SDL_SCANCODE_F14 => KeyboardKey.F14,
                SDL_Scancode.SDL_SCANCODE_F15 => KeyboardKey.F15,
                SDL_Scancode.SDL_SCANCODE_F16 => KeyboardKey.F16,
                SDL_Scancode.SDL_SCANCODE_F17 => KeyboardKey.F17,
                SDL_Scancode.SDL_SCANCODE_F18 => KeyboardKey.F18,
                SDL_Scancode.SDL_SCANCODE_F19 => KeyboardKey.F19,
                SDL_Scancode.SDL_SCANCODE_F20 => KeyboardKey.F20,
                SDL_Scancode.SDL_SCANCODE_F21 => KeyboardKey.F21,
                SDL_Scancode.SDL_SCANCODE_F22 => KeyboardKey.F22,
                SDL_Scancode.SDL_SCANCODE_F23 => KeyboardKey.F23,
                SDL_Scancode.SDL_SCANCODE_F24 => KeyboardKey.F24,
                SDL_Scancode.SDL_SCANCODE_MENU => KeyboardKey.Menu,
                SDL_Scancode.SDL_SCANCODE_LCTRL => KeyboardKey.ControlLeft,
                SDL_Scancode.SDL_SCANCODE_LSHIFT => KeyboardKey.ShiftLeft,
                SDL_Scancode.SDL_SCANCODE_LALT => KeyboardKey.AltLeft,
                SDL_Scancode.SDL_SCANCODE_RCTRL => KeyboardKey.ControlRight,
                SDL_Scancode.SDL_SCANCODE_RSHIFT => KeyboardKey.ShiftRight,
                SDL_Scancode.SDL_SCANCODE_RALT => KeyboardKey.AltRight,
                _ => KeyboardKey.Unknown
            };
        }

        private static KeyboardModifierKeys MapModifierKeys(SDL_Keymod mod)
        {
            var mods = KeyboardModifierKeys.None;

            if ((mod & (SDL_Keymod.KMOD_LSHIFT | SDL_Keymod.KMOD_RSHIFT)) != 0)
            {
                mods |= KeyboardModifierKeys.Shift;
            }

            if ((mod & (SDL_Keymod.KMOD_LALT | SDL_Keymod.KMOD_RALT)) != 0)
            {
                mods |= KeyboardModifierKeys.Alt;
            }

            if ((mod & (SDL_Keymod.KMOD_LCTRL | SDL_Keymod.KMOD_RCTRL)) != 0)
            {
                mods |= KeyboardModifierKeys.Control;
            }

            return mods;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void HandleTextInputEvent(SDL_TextInputEvent e)
        {
            // TODO
        }

        // ReSharper disable once UnusedParameter.Local
        private static void HandleMouseMotionEvent(SDL_MouseMotionEvent e)
        {
            InputState.HandleMouseMotion(e.x, e.y);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void HandleMouseButtonEvent(SDL_MouseButtonEvent e)
        {
            // TODO
        }

        // ReSharper disable once UnusedParameter.Local
        private static void HandleMouseWheelEvent(SDL_MouseWheelEvent e)
        {
            // TODO
        }

        // ReSharper disable once UnusedParameter.Local
        private static void HandleDropEvent(SDL_DropEvent e)
        {
            // TODO
        }
    }
}
