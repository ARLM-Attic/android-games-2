using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell.GI
{
    public class DefeatSence : Sence
    {
        private Map2D _map;
        public DefeatSence(AGEngine engine, Map2D map)
            : base(engine)
        {
            _map = map;
        }

        protected override void OnRender(AGGDI gdi)
        {
            gdi.DrawText("失败", MainWindow.Width / 2, MainWindow.Height / 2);
            gdi.DrawText("按任意键继续", MainWindow.Width / 2, MainWindow.Height / 2 + 20);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (msg == 1)
            {
                _engine.SwitchSence(new SelectLevelSence(_engine));
            }
        }
    }
}
