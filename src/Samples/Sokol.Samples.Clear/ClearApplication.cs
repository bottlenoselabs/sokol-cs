using static Sokol.sokol_gfx;

namespace Sokol.Samples.Clear
{
    public class ClearApplication : App
    {
        private sg_pass_action _frameBufferAction;

        public ClearApplication()
        {
            // set the framebuffer render pass action to clear as red
            _frameBufferAction.color(0).action = sg_action.SG_ACTION_CLEAR;
            _frameBufferAction.color(0).val = RgbaFloat.Red;
        }

        protected override void Draw(int width, int height)
        {
            // get the color used to clear the framebuffer
            ref var clearColor = ref _frameBufferAction.color(0).val;
            // move the color towards yellow from red, then back to yellow
            clearColor.G = clearColor.G > 1.0f ? 0.0f : clearColor.G + 0.01f;

            // begin a framebuffer render pass
            sg_begin_default_pass(ref _frameBufferAction, width, height);
            
            // end the framebuffer render pass
            sg_end_pass();
        }
    }
}