// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sokol.App
{
#pragma warning disable 1591
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public class AppTime
    {
        public TimeSpan TotalTime { get; internal set; }

        public float TotalSeconds
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (float)TotalTime.TotalSeconds;
        }

        public TimeSpan ElapsedTime { get; set; }

        public float ElapsedSeconds
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (float)ElapsedTime.TotalSeconds;
        }

        public float Alpha { get; internal set; }
    }
}
