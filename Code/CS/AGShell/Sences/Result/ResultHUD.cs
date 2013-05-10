using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class ResultHUD : HUD
    {
        public ResultHUD(AGEngine engine)
            : base(engine)
        {
            AGTextButton button = new AGTextButton(
                "exist",
                new Point2D(MainWindow.Width - 200, MainWindow.Height - 100),
                new Size2D(50, 150));
            button.Click += new EventHandler(button_Click);
            _controls.Add(button);
        }

        void button_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new StagesSence(_engine));
        }

        protected override bool OnInputEvent(int msg, int lParam, int wParam)
        {
            return false;
        }

        public override bool MouseInput(MouseMessage mouse)
        {
            for (int iControl = 0; iControl < _controls.Count; iControl++)
            {
                AGControl control = _controls[iControl];
                if (control.InRect(mouse.X, mouse.Y))
                {
                    _controls[iControl].OnInputEvent(mouse);
                    return true;
                }
            }
            return false;
        }
    }
}
