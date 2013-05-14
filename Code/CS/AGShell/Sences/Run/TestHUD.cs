using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class TestHUD : HUD
    {
        private Map2D _map;
        private Model2D _skillBarModel;

        public TestHUD(IEngine engine, Map2D map)
            : base(engine)
        {
            _skillBarModel = DATUtility.GetModel(18);

            _controls = new List<AGControl>();
            _map = map;

            //for (int iUnit = 0; iUnit < map.Camps[0].AvailableUnitList.Count; iUnit++)
            //{
            //    AGButton button = new AGButton();
            //    button.ColdDown = 5 * 30; //5s
            //    button.Unit = map.Camps[0].AvailableUnitList[iUnit];
            //    button.UnitId = map.Camps[0].AvailableUnitList[iUnit].Id;
            //    button.CostM = map.Camps[0].AvailableUnitList[iUnit].CostM;
            //    button.Pos = new Point2D(MainWindow.Width / 2 + iUnit * 50, MainWindow.Height - 50);
            //    button.Size = new Size2D(50, 50);
            //    button.Click += button_Click;
            //    _controls.Add(button);
            //}

            Frame2D frame = _skillBarModel.GetFrame(1, 1, 1);
            int startX = (MainWindow.Width - frame.Width) / 2 + 18;
            int startY = (MainWindow.Height - frame.Height) + 10;

            for (int skillIndex = 0; skillIndex < map.SkillList.Count; skillIndex++)
            {
                AGSkillButton skillButton = new AGSkillButton(map.SkillList[skillIndex]);
                skillButton.Pos = new Point2D(startX + skillIndex * 38, startY);
                skillButton.Click += new EventHandler(skillButton_Click);
                _controls.Add(skillButton);
            }

            int startX2 = (MainWindow.Width - frame.Width) / 2 + 226;
            for (int skillIndex = 0; skillIndex < map.SkillList2.Count; skillIndex++)
            {
                AGSkillButton skillButton = new AGSkillButton(map.SkillList2[skillIndex]);
                skillButton.Pos = new Point2D(startX2 + skillIndex * 38, startY);
                skillButton.Click += new EventHandler(skillButton_Click);
                _controls.Add(skillButton);
            }
        }

        void skillButton_Click(object sender, EventArgs e)
        {
            //_map.PlayerSkill.IsPrepare = !_map.PlayerSkill.IsPrepare;
            AGSkillButton btn = sender as AGSkillButton;
            btn.Skill.Begin();
        }

        protected override void OnRender(IGDI gdi)
        {
            //gdi.DrawRectangle(0, MainWindow.Height - 50, MainWindow.Width, 50);

            // 头像区域 100*120
            //gdi.DrawRectangle(0, MainWindow.Height - 120, 100, 120);

            // 地图区域 120*120
            //gdi.DrawRectangle(MainWindow.Width - 120, MainWindow.Height - 120, 120, 120);

            // 资源栏 width*10
            //gdi.DrawRectangle(0, 0, MainWindow.Width, 20);
            gdi.DrawText(string.Format("M:{0}", _map.Camps[0].Income), MainWindow.Width - 200, 5);
            gdi.DrawText(string.Format("U:{0}/{1}", _map.Camps[0].Population, _map.Camps[0].PopulationLimit), MainWindow.Width - 100, 5);

            Frame2D frame = _skillBarModel.GetFrame(1, 1, 1);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                (MainWindow.Width - frame.Width) / 2,
                (MainWindow.Height - frame.Height),
                frame.Width,
                frame.Height,
                frame.Width,
                frame.Height);
        }

        void button_Click(object sender, EventArgs e)
        {
            AGButton button = sender as AGButton;
            Unit2D unit = DATUtility.GetUnit(button.UnitId);
            if (_map.Camps[0].Income >= unit.CostM)
            {
                Object2D obj = AGSUtility.CreateObject(_map, _map.Camps[0], DATUtility.GetUnit(button.UnitId), "unknown", _map.Camps[0].StartPos, Direction2DDef.South.Id);
                AGSUtility.MoveTo(obj, _map.Camps[0].TargetPos);
                button.ColdDownTick = button.ColdDown;
            }
        }
    }
}
