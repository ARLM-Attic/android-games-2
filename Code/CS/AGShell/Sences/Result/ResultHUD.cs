using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class ResultHUD : HUD
    {
        public ResultHUD(IEngine engine)
            : base(engine)
        {
            AGTextButton button = new AGTextButton(
                "退出",
                new Point2D(MainWindow.Width - 200, MainWindow.Height - 100),
                new Size2D(50, 150));
            button.Click += new EventHandler(button_Click);
            _controls.Add(button);
        }

        void button_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new StagesSence(_engine));
        }
    }
}
