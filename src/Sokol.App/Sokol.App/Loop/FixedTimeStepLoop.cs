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
        private readonly TimeSpan _targetElapsedTime;

        public FixedTimeStepLoop(
            [AllowNull] TimeSpan? targetElapsedTime = null)
        {
            _targetElapsedTime = targetElapsedTime ?? TimeSpan.FromSeconds(1 / 60.0);
        }

        public override void Run()
        {
            IsRunning = true;

            var previousTicks = SDL_GetPerformanceCounter();
            var accumulatedTime = TimeSpan.Zero;
            var totalTime = TimeSpan.Zero;

            // WARNING: PUTTING THE THREAD TO SLEEP CAN CAUSE JITTER (~10ms) BECAUSE HOW THE OPERATING SYSTEM WORKS.

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

                if (accumulatedTime < _targetElapsedTime)
                {
                    continue;
                }

                HandleInput(elapsedTime);

                var fixedStepCount = 0;
                // NOTE: `IsRunning` needs to be checked for the edge case where the app is lagging so hard it can't quit
                while (accumulatedTime >= _targetElapsedTime && IsRunning)
                {
                    accumulatedTime -= _targetElapsedTime;
                    totalTime += _targetElapsedTime;
                    Update(totalTime, _targetElapsedTime);
                    fixedStepCount++;
                }

                // NOTE: Step count is greater than 1 => app is having trouble keeping up to the requested time
                // TODO: Have a way to query whether we are lagging so the developer can possibly try to have the app drop some work load

                elapsedTime = accumulatedTime + (_targetElapsedTime * fixedStepCount);
                var alpha = accumulatedTime.Ticks / (float)_targetElapsedTime.Ticks;
                Draw(totalTime, elapsedTime, alpha);
            }
        }
    }
}
