// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Sokol.App
{
#pragma warning disable 1591
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public class AppTime
    {
        public TimeSpan TotalTime { get; internal set; }

        public TimeSpan ElapsedTime { get; set; }

        public float Alpha { get; internal set; }
    }
}
