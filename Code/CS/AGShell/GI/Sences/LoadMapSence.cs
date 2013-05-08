using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell.GI
{
    public class LoadMapSence : Sence
    {
        private bool _first;
        public LoadMapSence(AGEngine engine)
            : base(engine)
        {
            _first = true;
        }

        protected override void OnRender(AGGDI gdi)
        {
            if (_first)
            {
                _engine.LoadMap(100);
                _first = false;
            }
            else if (_engine.CurrentMap != null)
            {
                _engine.SwitchSence(new MapTestSence(_engine, _engine.CurrentMap));
            }
            else
            {
                gdi.DrawText("地图加载中", 400, 300);
            }
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
        }
    }
}
