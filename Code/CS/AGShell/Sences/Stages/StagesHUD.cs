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

        public StagesHUD(IEngine engine, Model2D model)
            : base(engine)
        {
            _model = model;

            _panel = new BigMapPanel(model, new Point2D(0, 50), new Size2D(MainWindow.Width, MainWindow.Height - 100));
            //_panel.Pos = new Point2D(0, 50);
            _panel.SelectMap += new Action<int>(_panel_SelectMap);
            _controls.Add(_panel);

            AGTextButton btnUpgrade = new AGTextButton("Upgrade", new Point2D(500, MainWindow.Height - 40), new Size2D(100, 100));
            btnUpgrade.Click += new EventHandler(btnUpgrade_Click);
            _controls.Add(btnUpgrade);

            AGTextButton btnTalent = new AGTextButton("Talent", new Point2D(360, MainWindow.Height - 40), new Size2D(100, 100));
            btnTalent.Click += new EventHandler(btnTalent_Click);
            _controls.Add(btnTalent);

            AGLabel labelPlayer = new AGLabel("player:" + PlayerData.Current.Name);
            labelPlayer.Pos = new Point2D(10, 10);
            labelPlayer.Size = new Size2D(10, 10);
            _controls.Add(labelPlayer);

            AGLabel labelMonney = new AGLabel("money:" + PlayerData.Current.Money.ToString());
            labelMonney.Pos = new Point2D(200, 10);
            labelMonney.Size = new Size2D(10, 10);
            _controls.Add(labelMonney);
        }

        void btnTalent_Click(object sender, EventArgs e)
        {
        }

        void _panel_SelectMap(int obj)
        {
            _engine.SwitchSence(new LoadMapSence(_engine, obj));
        }

        void btnUpgrade_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new UpgradeSence(_engine));
        }

        protected override void OnRender(IGDI gdi)
        {
            base.OnRender(gdi);
        }

        protected override bool OnInputEvent(int msg, int lParam, int wParam)
        {
            return false;
        }
    }
}
