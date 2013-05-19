using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AGShell
{
    public class StagesHUD : HUD
    {
        private int _y;
        private Model2D _model;
        private AGWorldMapPanel _panel;

        private MapInfo _selectedMap;

        #region child controls
        private AGTextButton _ctlBtnStart;
        #endregion

        public StagesHUD(IEngine engine, Model2D model)
            : base(engine)
        {
            _model = model;

            _panel = new AGWorldMapPanel(1, new Point2D(0, 50), new Size2D(MainWindow.Width, MainWindow.Height - 100));
            //_panel.Pos = new Point2D(0, 50);
            _panel.SelectMap += new Action<MapInfo>(_panel_SelectMap);
            _controls.Add(_panel);

            AGTextButton btnUpgrade = new AGTextButton("升级", new Point2D(500, MainWindow.Height - 40), new Size2D(100, 100));
            btnUpgrade.Click += new EventHandler(btnUpgrade_Click);
            //_controls.Add(btnUpgrade);

            AGTextButton btnTalent = new AGTextButton("天赋", new Point2D(360, MainWindow.Height - 40), new Size2D(100, 100));
            btnTalent.Click += new EventHandler(btnTalent_Click);
            //_controls.Add(btnTalent);

            AGLabel labelPlayer = new AGLabel("player:" + PlayerData.Current.Name);
            labelPlayer.Pos = new Point2D(10, 10);
            labelPlayer.Size = new Size2D(10, 10);
            //_controls.Add(labelPlayer);

            AGLabel labelMonney = new AGLabel("money:" + PlayerData.Current.Money.ToString());
            labelMonney.Pos = new Point2D(200, 10);
            labelMonney.Size = new Size2D(10, 10);
            //_controls.Add(labelMonney);


            AGTextButton btnExit = new AGTextButton("退出", new Point2D(10, MainWindow.Height - 40), new Size2D(100, 100));
            btnExit.Click += new EventHandler(btnExit_Click);
            _controls.Add(btnExit);

            _ctlBtnStart = new AGTextButton("开始", new Point2D(MainWindow.Width - 140, MainWindow.Height - 40), new Size2D(100, 100));
            _ctlBtnStart.Click += new EventHandler(btnStart_Click);
            _controls.Add(_ctlBtnStart);
            _ctlBtnStart.IsVisible = false;
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new LoadMapSence(_engine, _selectedMap.Id));
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            _engine.Exit();
        }

        void btnTalent_Click(object sender, EventArgs e)
        {
        }

        void _panel_SelectMap(MapInfo obj)
        {
            _selectedMap = obj;
            _ctlBtnStart.IsVisible = true;
        }

        void btnUpgrade_Click(object sender, EventArgs e)
        {
            _engine.SwitchSence(new UpgradeSence(_engine));
        }

        protected override void OnRender(IGDI gdi)
        {
            base.OnRender(gdi);

            if (_selectedMap != null)
            {
                Rectangle rect = new Rectangle((int)0, (int)MainWindow.Height-50, (int)MainWindow.Width, (int)50);
                gdi.DrawText(
                    AGRES.GetNormalUIFont(),
                    0x222222,
                    _selectedMap.Caption,
                    rect);
            }
        }
    }
}
