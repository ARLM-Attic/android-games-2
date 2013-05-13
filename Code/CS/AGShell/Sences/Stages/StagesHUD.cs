using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class StagesHUD : HUD
    {
        private int _y;
        private Model2D _model;
        private BigMapPanel _panel;

        public StagesHUD(AGEngine engine, Model2D model)
            : base(engine)
        {
            _model = model;

            _panel = new BigMapPanel(model);
            _panel.Pos = new Point2D(0, 0);
            _panel.SelectMap += new Action<int>(_panel_SelectMap);
            _controls.Add(_panel);

            AGTextButton btnUpgrade = new AGTextButton("Upgrade", new Point2D(10, MainWindow.Height - 60), new Size2D(100, 100));
            btnUpgrade.Click += new EventHandler(btnUpgrade_Click);
            _controls.Add(btnUpgrade);
        }

        void _panel_SelectMap(int obj)
        {
            _engine.SwitchSence(new LoadMapSence(_engine, obj));
        }

        void btnUpgrade_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new UpgradeSence(_engine));
        }

        protected override void OnRender(AGGDI gdi)
        {
            base.OnRender(gdi);
        }

        protected override bool OnInputEvent(int msg, int lParam, int wParam)
        {
            return false;
        }
    }
}
