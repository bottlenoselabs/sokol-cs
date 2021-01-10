// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

namespace Sokol
{
    /// <summary>
    ///     Defines the different keys of a keyboard.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="AppKeyCode" /> is blittable to the C `sapp_keycode` enum found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum AppKeyCode
    {
        /// <summary>
        ///     This value is reserved for the default initialization of structures.
        /// </summary>
        Invalid = 0,

        /// <summary>
        ///     The white-space key.
        /// </summary>
        Space = 32,

        /// <summary>
        ///     The <c>'</c> key.
        /// </summary>
        Apostrophe = 39,

        /// <summary>
        ///     The <c>,</c> key.
        /// </summary>
        Comma = 44,

        /// <summary>
        ///     The <c>-</c> key.
        /// </summary>
        Minus = 45,

        /// <summary>
        ///     The <c>.</c> key.
        /// </summary>
        Period = 46,

        /// <summary>
        ///     The <c>/</c> key.
        /// </summary>
        Slash = 47,

        /// <summary>
        ///     The numeric value of <c>0</c> key.
        /// </summary>
        Number0 = 48,

        /// <summary>
        ///     The numeric value of <c>1</c> key.
        /// </summary>
        Number1 = 49,

        /// <summary>
        ///     The numeric value of <c>2</c> key.
        /// </summary>
        Number2 = 50,

        /// <summary>
        ///     The numeric value of <c>3</c> key.
        /// </summary>
        Number3 = 51,

        /// <summary>
        ///     The numeric value of <c>4</c> key.
        /// </summary>
        Number4 = 52,

        /// <summary>
        ///     The numeric value of <c>5</c> key.
        /// </summary>
        Number5 = 53,

        /// <summary>
        ///     The numeric value of <c>6</c> key.
        /// </summary>
        Number6 = 54,

        /// <summary>
        ///     The numeric value of <c>7</c> key.
        /// </summary>
        Number7 = 55,

        /// <summary>
        ///     The numeric value of <c>8</c> key.
        /// </summary>
        Number8 = 56,

        /// <summary>
        ///     The numeric value of <c>9</c> key.
        /// </summary>
        Number9 = 57,

        /// <summary>
        ///     The <c>;</c> key.
        /// </summary>
        SemiColon = 59,

        /// <summary>
        ///     The <c>=</c> key.
        /// </summary>
        Equals = 61,

        /// <summary>
        ///     The <c>a</c> key.
        /// </summary>
        A = 65,

        /// <summary>
        ///     The <c>b</c> key.
        /// </summary>
        B = 66,

        /// <summary>
        ///     The <c>c</c> key.
        /// </summary>
        C = 67,

        /// <summary>
        ///     The <c>d</c> key.
        /// </summary>
        D = 68,

        /// <summary>
        ///     The <c>e</c> key.
        /// </summary>
        E = 69,

        /// <summary>
        ///     The <c>f</c> key.
        /// </summary>
        F = 70,

        /// <summary>
        ///     The <c>g</c> key.
        /// </summary>
        G = 71,

        /// <summary>
        ///     The <c>h</c> key.
        /// </summary>
        H = 72,

        /// <summary>
        ///     The <c>i</c> key.
        /// </summary>
        I = 73,

        /// <summary>
        ///     The <c>j</c> key.
        /// </summary>
        J = 74,

        /// <summary>
        ///     The <c>k</c> key.
        /// </summary>
        K = 75,

        /// <summary>
        ///     The <c>l</c> key.
        /// </summary>
        L = 76,

        /// <summary>
        ///     The <c>m</c> key.
        /// </summary>
        M = 77,

        /// <summary>
        ///     The <c>n</c> key.
        /// </summary>
        N = 78,

        /// <summary>
        ///     The <c>o</c> key.
        /// </summary>
        O = 79,

        /// <summary>
        ///     The <c>p</c> key.
        /// </summary>
        P = 80,

        /// <summary>
        ///     The <c>q</c> key.
        /// </summary>
        Q = 81,

        /// <summary>
        ///     The <c>r</c> key.
        /// </summary>
        R = 82,

        /// <summary>
        ///     The <c>s</c> key.
        /// </summary>
        S = 83,

        /// <summary>
        ///     The <c>t</c> key.
        /// </summary>
        T = 84,

        /// <summary>
        ///     The <c>u</c> key.
        /// </summary>
        U = 85,

        /// <summary>
        ///     The <c>v</c> key.
        /// </summary>
        V = 86,

        /// <summary>
        ///     The <c>w</c> key.
        /// </summary>
        W = 87,

        /// <summary>
        ///     The <c>x</c> key.
        /// </summary>
        X = 88,

        /// <summary>
        ///     The <c>y</c> key.
        /// </summary>
        Y = 89,

        /// <summary>
        ///     The <c>z</c> key.
        /// </summary>
        Z = 90,

        /// <summary>
        ///     The <c>[</c> key.
        /// </summary>
        LeftBracket = 91,

        /// <summary>
        ///     The <c>\</c> key.
        /// </summary>
        Backslash = 92,

        /// <summary>
        ///     The <c>]</c> key.
        /// </summary>
        RightBracket = 93,

        /// <summary>
        ///     The <c>`</c> key.
        /// </summary>
        GraveAccent = 96,

        /// <summary>
        ///     The <c>esc</c> key.
        /// </summary>
        Escape = 256,

        /// <summary>
        ///     THe <c>return</c> key.
        /// </summary>
        Enter = 257,

        /// <summary>
        ///     The <c>tab</c> key.
        /// </summary>
        Tab = 258,

        /// <summary>
        ///     The back-space key.
        /// </summary>
        Backspace = 259,

        /// <summary>
        ///     The insert key.
        /// </summary>
        Insert = 260,

        /// <summary>
        ///     The delete key.
        /// </summary>
        Delete = 261,

        /// <summary>
        ///     The right-arrow navigation key.
        /// </summary>
        Right = 262,

        /// <summary>
        ///     The left-arrow navigation key.
        /// </summary>
        Left = 263,

        /// <summary>
        ///     The down-arrow navigation key.
        /// </summary>
        Down = 264,

        /// <summary>
        ///     The up-arrow navigation key.
        /// </summary>
        Up = 265,

        /// <summary>
        ///     The page-up key.
        /// </summary>
        PageUp = 266,

        /// <summary>
        ///     The page-down key.
        /// </summary>
        PageDown = 267,

        /// <summary>
        ///     The home key.
        /// </summary>
        Home = 268,

        /// <summary>
        ///     The end key.
        /// </summary>
        End = 269,

        /// <summary>
        ///     The caps-lock key.
        /// </summary>
        CapsLock = 280,

        /// <summary>
        ///     The scroll-lock key.
        /// </summary>
        ScrollLock = 281,

        /// <summary>
        ///     The number-lock key.
        /// </summary>
        NumLock = 282,

        /// <summary>
        ///     The print-screen key.
        /// </summary>
        PrintScreen = 283,

        /// <summary>
        ///     The pause key.
        /// </summary>
        Pause = 284,

        /// <summary>
        ///     The F1 key.
        /// </summary>
        F1 = 290,

        /// <summary>
        ///     The F2 key.
        /// </summary>
        F2 = 291,

        /// <summary>
        ///     The F3 key.
        /// </summary>
        F3 = 292,

        /// <summary>
        ///     The F4 key.
        /// </summary>
        F4 = 293,

        /// <summary>
        ///     The F5 key.
        /// </summary>
        F5 = 294,

        /// <summary>
        ///     The F6 key.
        /// </summary>
        F6 = 295,

        /// <summary>
        ///     The F7 key.
        /// </summary>
        F7 = 296,

        /// <summary>
        ///     The F8 key.
        /// </summary>
        F8 = 297,

        /// <summary>
        ///     The F9 key.
        /// </summary>
        F9 = 298,

        /// <summary>
        ///     The F10 key.
        /// </summary>
        F10 = 299,

        /// <summary>
        ///     The F11 key.
        /// </summary>
        F11 = 300,

        /// <summary>
        ///     The F12 key.
        /// </summary>
        F12 = 301,

        /// <summary>
        ///     The F13 key.
        /// </summary>
        F13 = 302,

        /// <summary>
        ///     The F14 key.
        /// </summary>
        F14 = 303,

        /// <summary>
        ///     The F15 key.
        /// </summary>
        F15 = 304,

        /// <summary>
        ///     The F16 key.
        /// </summary>
        F16 = 305,

        /// <summary>
        ///     The F17 key.
        /// </summary>
        F17 = 306,

        /// <summary>
        ///     The F18 key.
        /// </summary>
        F18 = 307,

        /// <summary>
        ///     The F19 key.
        /// </summary>
        F19 = 308,

        /// <summary>
        ///     The F20 key.
        /// </summary>
        F20 = 309,

        /// <summary>
        ///     The F21 key.
        /// </summary>
        F21 = 310,

        /// <summary>
        ///     The F22 key.
        /// </summary>
        F22 = 311,

        /// <summary>
        ///     The F23 key.
        /// </summary>
        F23 = 312,

        /// <summary>
        ///     The F24 key.
        /// </summary>
        F24 = 313,

        /// <summary>
        ///     The F25 key.
        /// </summary>
        F25 = 314,

        /// <summary>
        ///     The key-pad numeric value of <c>0</c> key.
        /// </summary>
        KeyPad0 = 320,

        /// <summary>
        ///     The key-pad numeric value of <c>1</c> key.
        /// </summary>
        KeyPad1 = 321,

        /// <summary>
        ///     The key-pad numeric value of <c>2</c> key.
        /// </summary>
        KeyPad2 = 322,

        /// <summary>
        ///     The key-pad numeric value of <c>3</c> key.
        /// </summary>
        KeyPad3 = 323,

        /// <summary>
        ///     The key-pad numeric value of <c>4</c> key.
        /// </summary>
        KeyPad4 = 324,

        /// <summary>
        ///     The key-pad numeric value of <c>5</c> key.
        /// </summary>
        KeyPad5 = 325,

        /// <summary>
        ///     The key-pad numeric value of <c>6</c> key.
        /// </summary>
        KeyPad6 = 326,

        /// <summary>
        ///     The key-pad numeric value of <c>7</c> key.
        /// </summary>
        KeyPad7 = 327,

        /// <summary>
        ///     The key-pad numeric value of <c>8</c> key.
        /// </summary>
        KeyPad8 = 328,

        /// <summary>
        ///     The key-pad numeric value of <c>9</c> key.
        /// </summary>
        KeyPad9 = 329,

        /// <summary>
        ///     The key-pad <c>.</c> key.
        /// </summary>
        KeyPadDecimal = 330,

        /// <summary>
        ///     The key-pad <c>/</c> key.
        /// </summary>
        KeyPadDivide = 331,

        /// <summary>
        ///     The key-pad <c>*</c> key.
        /// </summary>
        KeyPadMultiply = 332,

        /// <summary>
        ///     The key-pad <c>-</c> key.
        /// </summary>
        KeyPadSubtract = 333,

        /// <summary>
        ///     The key-pad <c>+</c> key.
        /// </summary>
        KeyPadAdd = 334,

        /// <summary>
        ///     The key-pad enter key.
        /// </summary>
        KeyPadEnter = 335,

        /// <summary>
        ///     The key-pad <c>=</c> key.
        /// </summary>
        KeyPadEqual = 336,

        /// <summary>
        ///     The left <c>shift</c> key.
        /// </summary>
        LeftShift = 340,

        /// <summary>
        ///     The left <c>control</c> key.
        /// </summary>
        LeftControl = 341,

        /// <summary>
        ///     The left <c>alt</c> key.
        /// </summary>
        LeftAlt = 342,

        /// <summary>
        ///     The right <c>shift</c> key.
        /// </summary>
        RightShift = 344,

        /// <summary>
        ///     The right <c>control</c> key.
        /// </summary>
        RightControl = 345,

        /// <summary>
        ///     The right <c>alt</c> key.
        /// </summary>
        RightAlt = 346,

        /// <summary>
        ///     The right <c>menu</c> key.
        /// </summary>
        Menu = 348
    }
}
