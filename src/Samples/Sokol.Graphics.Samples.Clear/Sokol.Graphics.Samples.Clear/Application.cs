// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.SDL2;

namespace Sokol.Graphics.Samples.Clear
{
    public class Application : App
    {
        private PassAction _passAction;

        public Application()
        {
            // initially set the frame buffer clear color to red
            _passAction = PassAction.Clear(Rgba32F.Red);
        }

        protected override void Draw(int width, int height)
        {
            // get the color used to clear the framebuffer
            ref var clearColor = ref _passAction.Color().Value;
            // move the color towards yellow, then reset, in repeat
            clearColor.G = clearColor.G > 1.0f ? 0.0f : clearColor.G + 0.01f;

            // begin a framebuffer render pass
            var pass = GraphicsDevice.BeginDefaultPass(ref _passAction, width, height);

            // end the framebuffer render pass
            pass.End();
        }
    }
}
