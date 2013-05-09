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

        public ResultSence(AGEngine engine, Map2D map, GameResult result)
            : base(engine)
        {
            _map = map;
            _result = result;
        }

        protected override void OnRender(AGGDI gdi)
        {
            if (_result.IsVictory)
            {
                gdi.DrawText("胜利", MainWindow.Width / 2, 50);
            }
            else
            {
                gdi.DrawText("失败", MainWindow.Width / 2, 50);
            }

            gdi.DrawText(string.Format("GameTime:{0}", _result.GameTime), MainWindow.Width / 2, 70);
            gdi.DrawText(string.Format("Build Unit:{0}", _result.BuildCount), MainWindow.Width / 2, 90);
            gdi.DrawText(string.Format("Killed Unit:{0}", _result.KilledCount), MainWindow.Width / 2, 110);
            gdi.DrawText(string.Format("Deaded Unit:{0}", _result.DeadCount), MainWindow.Width / 2, 130);

            gdi.DrawText("按任意键继续", MainWindow.Width / 2, MainWindow.Height / 2 + 20);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (msg == 1)
            {
                _engine.SwitchSence(new SelectLevelSence(_engine));
            }
        }

        public override void MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY)
        {
        }
    }
}
