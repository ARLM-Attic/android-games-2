using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class LoadMapSence : Sence
    {
        private bool _first;
        private int _mapId;
        public LoadMapSence(IEngine engine, int mapId)
            : base(engine)
        {
            _mapId = mapId;
            _first = true;
        }

        protected override void OnRender(IGDI gdi)
        {
            if (_first)
            {
                _engine.LoadMap(_mapId);
                _first = false;
            }
            else if (_engine.CurrentMap != null)
            {
                _engine.ADI.PlayBGM(_engine.CurrentMap.ID);
                _engine.SwitchSence(new RunSence(_engine, _engine.CurrentMap));
            }
            else
            {
                gdi.DrawText("地图加载中", 400, 300);
            }
        }
    }
}
