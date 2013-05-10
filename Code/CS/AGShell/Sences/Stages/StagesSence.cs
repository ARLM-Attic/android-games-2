using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class StagesSence : Sence
    {
        private StagesHUD _hud;

        public StagesSence(AGEngine engine)
            : base(engine)
        {
            Model2D model = DATUtility.GetModel(13);
            _hud = new StagesHUD(engine, model);
        }

        protected override void OnRender(AGGDI gdi)
        {
            _hud.Render(gdi);

            gdi.DrawText(AGRES.LargeUIFontHandle, 0xffffff, "Select Stages", 120, 10);
            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "version 1.0", MainWindow.Width - 100, MainWindow.Height - 20);
            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "Email:ly.jaeho@gmail.com", 10, MainWindow.Height - 20);
            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "QQ:345241086", 230, MainWindow.Height - 20);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            //_engine.SwitchSence(new LoadMapSence(_engine));
        }

        public override void MouseInput(MouseMessage mouse)
        {
            if (mouse.IsLBDown())
            {
                if (_hud.MouseInput(mouse))
                {
                    return;
                }
            }
        }
    }
}
