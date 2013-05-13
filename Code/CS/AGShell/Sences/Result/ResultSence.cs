using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class ResultSence : Sence
    {
        private Map2D _map;
        private GameResult _result;

        public ResultSence(IEngine engine, Map2D map, GameResult result)
            : base(engine)
        {
            _map = map;
            _result = result;
        }

        protected override HUD CreateHUD()
        {
            return new ResultHUD(_engine);
        }

        protected override void OnRender(IGDI gdi)
        {
            if (_result.IsVictory)
            {
                gdi.DrawText(AGRES.LargeUIFontHandle, 0x22ff22, "Victory", 210, 100);
            }
            else
            {
                gdi.DrawText(AGRES.LargeUIFontHandle, 0xff2222, "Defeat", 210, 100);
            }

            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "Game Time:", 200, 170);
            gdi.DrawText(AGRES.NormalUIHfont, 0xeeee22, string.Format("{0}", _result.GameTime), 380, 170);
            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "Build Unit:", 200, 200);
            gdi.DrawText(AGRES.NormalUIHfont, 0xeeee22, string.Format("{0}", _result.BuildCount), 380, 200);
            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "Killed Unit:", 200, 230);
            gdi.DrawText(AGRES.NormalUIHfont, 0xeeee22, string.Format("{0}", _result.KilledCount), 380, 230);
            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "Deaded Unit:", 200, 260);
            gdi.DrawText(AGRES.NormalUIHfont, 0xeeee22, string.Format("{0}", _result.DeadCount), 380, 260);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (msg == 1)
            {
                _engine.SwitchSence(new StagesSence(_engine));
            }
        }

    }
}
