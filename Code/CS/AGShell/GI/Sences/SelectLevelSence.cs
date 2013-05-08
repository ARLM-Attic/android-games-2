using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell.GI
{
    public class SelectLevelSence : Sence
    {
        public SelectLevelSence(AGEngine engine)
            : base(engine)
        {
        }

        protected override void OnRender(AGGDI gdi)
        {
            gdi.DrawText("选择地图100", MainWindow.Width / 2, MainWindow.Height / 2);
            gdi.DrawText("按任意键继续", MainWindow.Width / 2, MainWindow.Height / 2 + 20);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (msg == 1)
            {
                _engine.SwitchSence(new LoadMapSence(_engine));
            }
        }

        public override void MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY)
        {
        }
    }
}
