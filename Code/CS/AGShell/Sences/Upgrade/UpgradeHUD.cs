using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class UpgradeHUD : HUD
    {
        public UpgradeHUD(AGEngine engine)
            : base(engine)
        {
            UnitPanel uPanel = new UnitPanel(DATUtility.GetUnit(301));

            uPanel.Pos = new Point2D(MainWindow.Width / 2, MainWindow.Height / 2);
            _controls.Add(uPanel);


            AGTextButton btnBack = new AGTextButton("Back", new Point2D(10, MainWindow.Height - 60), new Size2D(100, 100));
            btnBack.Click += new EventHandler(btnBack_Click);
            _controls.Add(btnBack);
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new StagesSence(_engine));
        }

        protected override bool OnInputEvent(int msg, int lParam, int wParam)
        {
            return false;
        }
    }
}
