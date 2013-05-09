using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class SelectLevelSence : Sence
    {
        private StagesHUD _hud;

        public SelectLevelSence(AGEngine engine)
            : base(engine)
        {
            _hud = new StagesHUD(engine);
        }

        protected override void OnRender(AGGDI gdi)
        {
            gdi.DrawText("选择地图100", MainWindow.Width / 2, MainWindow.Height / 2);
            _hud.Render(gdi);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            //_engine.SwitchSence(new LoadMapSence(_engine));
        }

        public override void MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY)
        {
            if (button == 0)
            {
                if (state == 1)
                {
                    _hud.MouseInput(button, state, deltaX, deltaY, deltaZ, ptX, ptY);
                }
            }
        }
    }
}
