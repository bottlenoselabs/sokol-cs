// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.App;
using Sokol.Graphics;

namespace Samples.Clear
{
    public class Application : App
    {
        private Rgba32F _clearColor;

        public Application()
        {
            // initially set the frame buffer clear color to red
            _clearColor = Rgba32F.Red;
        }

        protected override void HandleInput(InputState state)
        {
        }

        protected override void Update(AppTime time)
        {
            // move the color towards yellow, then reset, in repeat
            _clearColor.G = _clearColor.G > 1.0f ? 0.0f : _clearColor.G + 0.01f;
        }

        protected override void Draw(AppTime time)
        {
            // begin a frame buffer render pass
            var pass = BeginDefaultPass(_clearColor);
            // end the frame buffer render pass
            pass.End();
        }
    }
}
