using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AGShell
{
    public class RunHUD : HUD
    {
        private Map2D _map;
        private Model2D _skillBarModel;

        private Model2D _populationModel;
        private Model2D _moneyModel;
        private Model2D _mapTitleBGModel;

        public RunHUD(IEngine engine, Map2D map)
            : base(engine)
        {
            _skillBarModel = DATUtility.GetModel(18);
            _mapTitleBGModel = DATUtility.GetModel(20);

            _moneyModel = DATUtility.GetModel(151);
            _populationModel = DATUtility.GetModel(152);

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
            Frame2D mapTitleFrame = _mapTitleBGModel.GetFrame(1, 1, 1);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(mapTitleFrame.Data)),
                5,
                5,
                mapTitleFrame.Width,
                mapTitleFrame.Height,
                mapTitleFrame.Width,
                mapTitleFrame.Height);

            Rectangle rect = new Rectangle(
                (int)5, 
                (int)5, 
                (int)mapTitleFrame.Width, 
                (int)mapTitleFrame.Height);
            gdi.DrawShadowText(
                AGRES.GetNormalUIFont(),
                0xffff00,
                _map.Caption,
                rect);

            //gdi.DrawRectangle(0, MainWindow.Height - 50, MainWindow.Width, 50);

            // 头像区域 100*120
            //gdi.DrawRectangle(0, MainWindow.Height - 120, 100, 120);

            // 地图区域 120*120
            //gdi.DrawRectangle(MainWindow.Width - 120, MainWindow.Height - 120, 120, 120);

            // 资源栏 width*10
            //gdi.DrawRectangle(0, 0, MainWindow.Width, 20);
            Frame2D moneyFrame = _moneyModel.GetFrame(1, 1, 1);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(moneyFrame.Data)),
                MainWindow.Width - 232,
                5,
                24,
                24,
                moneyFrame.Width,
                moneyFrame.Height);
            gdi.DrawShadowText(AGRES.NormalUIHfont,
                0xffff00, 
                string.Format("{0}", _map.Camps[0].Income), 
                MainWindow.Width - 200,
                5);

            Frame2D popFrame = _populationModel.GetFrame(1, 1, 1);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(popFrame.Data)),
                MainWindow.Width - 132,
                5,
                24,
                24,
                popFrame.Width,
                popFrame.Height);
            gdi.DrawShadowText(AGRES.NormalUIHfont,
                0xffff00,
                string.Format("{0}/{1}", _map.Camps[0].Population, _map.Camps[0].PopulationLimit), 
                MainWindow.Width - 100,
                5);

            for (int index = 0; index < _map.Camps.Count; index++)
            {
                gdi.DrawShadowText(
                    string.Format("U:{0}/{1},{2}", _map.Camps[index].Population, _map.Camps[index].PopulationLimit, _map.Camps[index].Caption),
                    MainWindow.Width - 120,
                    100 + index * 50);
            }

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
