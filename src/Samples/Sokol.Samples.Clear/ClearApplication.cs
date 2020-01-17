namespace Sokol.Samples.Clear
{
    public class ClearApplication : App
    {
        private SgPassAction _frameBufferPassAction;

        public ClearApplication()
        {
            // set the framebuffer render pass action to clear as red
            _frameBufferPassAction = SgPassAction.Clear(RgbaFloat.Red);
        }

        protected override void Draw(int width, int height)
        {
            // get the color used to clear the framebuffer
            ref var colorAttachment0Action = ref _frameBufferPassAction.Color(0);
            var clearColor = colorAttachment0Action.Value;
            // move the color towards yellow from red, then back to yellow
            clearColor.G = clearColor.G > 1.0f ? 0.0f : clearColor.G + 0.01f;
            // set the color
            colorAttachment0Action.Value = clearColor;

            // begin a framebuffer render pass
            SgDefaultPass.Begin(ref _frameBufferPassAction, width, height);

            // end the framebuffer render pass
            SgDefaultPass.End();
        }
    }
}