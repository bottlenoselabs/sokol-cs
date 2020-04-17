// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using static SDL2.SDL;

namespace Sokol.App
{
#pragma warning disable 1591
    [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
    public sealed class FixedTimeStepLoop : AppLoop
    {
        private readonly TimeSpan _fixedElapsedTime;
        private readonly TimeSpan _maximumAccumulatedTime;
        private readonly TimeSpan _maximumElapsedTime;

        public FixedTimeStepLoop(
            [AllowNull] TimeSpan? fixedElapsedTime = null,
            [AllowNull] TimeSpan? maximumElapsedTime = null,
            [AllowNull] TimeSpan? maximumAccumulatedTime = null)
        {
            _fixedElapsedTime = fixedElapsedTime ?? TimeSpan.FromSeconds(1 / 60.0);
            _maximumElapsedTime = maximumElapsedTime ?? TimeSpan.FromSeconds(8 / 60.0);
            _maximumAccumulatedTime = maximumAccumulatedTime ?? TimeSpan.FromSeconds(0.5);
        }

        public override void Run()
        {
            IsRunning = true;

            var previousTicks = SDL_GetPerformanceCounter();
            var accumulatedTime = TimeSpan.Zero;
            var totalTime = TimeSpan.Zero;
            var fixedElapsedTime = _fixedElapsedTime;
            var maximumElapsedTime = _maximumElapsedTime;
            var maximumAccumulatedTime = _maximumAccumulatedTime;

            while (IsRunning)
            {
                var currentTicks = SDL_GetPerformanceCounter();
                var elapsedSeconds = (currentTicks - previousTicks) / (double)SDL_GetPerformanceFrequency();
                var elapsedTime = TimeSpan.FromSeconds(elapsedSeconds);
                previousTicks = currentTicks;

                if (elapsedTime > maximumElapsedTime)
                {
                    elapsedTime = maximumElapsedTime;
                }

                PumpEvents(elapsedTime);
                HandleInput();

                accumulatedTime += elapsedTime;
                if (accumulatedTime > maximumAccumulatedTime)
                {
                    accumulatedTime = maximumAccumulatedTime;
                }

                var fixedStepCount = 0;
                while (accumulatedTime >= fixedElapsedTime)
                {
                    accumulatedTime -= fixedElapsedTime;
                    totalTime += fixedElapsedTime;
                    fixedStepCount++;
                    Update(totalTime, elapsedTime);
                }

                elapsedTime = accumulatedTime + (_fixedElapsedTime * fixedStepCount);

                var alpha = accumulatedTime.Ticks / (float)_fixedElapsedTime.Ticks;
                Draw(totalTime, elapsedTime, alpha);
            }
        }
    }
}
