using System.Numerics;
using static Sokol.sokol_gfx;

namespace Sokol.Samples.Clear
{
    public class ClearApplication : App
    {
        private sg_pass_action _passAction;

        public unsafe ClearApplication()
        {
            _passAction = new sg_pass_action();
            var colors = _passAction.GetColors();
            colors[0] = new sg_color_attachment_action
            {
                action = sg_action.SG_ACTION_CLEAR, val = new Vector4(1.0f, 0.0f, 0.0f, 1.0f)
            };
        }
        
        protected override unsafe void Draw()
        {
            var g = _passAction.GetColors()[0].val.Y + 0.01f;
            _passAction.GetColors()[0].val.Y = g > 1.0f ? 0.0f : g;
            sg_begin_default_pass(ref _passAction, 800, 600);
            sg_end_pass();
        }
    }
}