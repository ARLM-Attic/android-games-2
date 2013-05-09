using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class StagesHUD : HUD
    {
        public StagesHUD(AGEngine engine)
            : base(engine)
        {
            _controls = new List<AGControl>();

            List<int> maps = DATUtility.GetMaps();

            for (int mapIndex = 0; mapIndex < maps.Count; mapIndex++)
            {
                AGTextButton button = new AGTextButton(
                    maps[mapIndex].ToString(),
                    new Point2D(MainWindow.Width / 2, mapIndex * 50),
                    new Size2D(50, 150));
                button.Click += new EventHandler(button_Click);
                _controls.Add(button);
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new LoadMapSence(_engine, 100));
        }

        protected override bool OnInputEvent(int msg, int lParam, int wParam)
        {
            return false;
        }

        public override bool MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY)
        {
            for (int iControl = 0; iControl < _controls.Count; iControl++)
            {
                AGControl control = _controls[iControl];
                if (control.InRect(ptX, ptY))
                {
                    _controls[iControl].OnInputEvent(button, ptX, ptY);
                    return true;
                }
            }
            return false;
        }
    }
}
