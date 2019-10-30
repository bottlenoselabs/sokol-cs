using static SDL2.SDL;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.Clear
{
    public class ClearApplication : App
    {
        private sg_pass_action _passAction;

        public unsafe ClearApplication()
            : base(true)
        {
            _passAction = new sg_pass_action();
            var colors = _passAction.GetColors();
            colors[0] = new sg_color_attachment_action
            {
                action = sg_action.SG_ACTION_CLEAR, 
                val = RgbaFloat.Red
            };
        }
        
        protected override unsafe void Draw()
        {
            var g = _passAction.GetColors()[0].val.G + 0.01f;
            _passAction.GetColors()[0].val.G = g > 1.0f ? 0.0f : g;
            SDL_GL_GetDrawableSize(WindowHandle, out var width, out var height);
            sg_begin_default_pass(ref _passAction, width, height);
            sg_end_pass();
        }
    }
}