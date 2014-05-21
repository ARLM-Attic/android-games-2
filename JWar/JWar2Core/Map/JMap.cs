using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWar2Core
{
    public class JMap
    {
        public int ID { get; set; }
        public string Caption { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        /// <summary>
        /// 摄像机所在的地图位置
        /// </summary>
        public JMapPos CameraTargetPos { get; set; }

        public JMapCell[] Cells { get; set; }

        public List<JSprite> Widgets { get; set; }

        //public List<Camp> Camps { get; set; }

        public long GameTime { get; set; }

        /// <summary>
        /// 单位的编号索引，用于为创建的单位分配新的编号
        /// </summary>
        public int ObjectIdIndex { get; set; }

        //public GState State { get; set; }

        //public List<Animation> AnimationList { get; private set; }

        //public PlayerSkill PlayerSkill { get; set; }

        //public List<Skill> SkillList { get; set; }
        //public List<Skill> SkillList2 { get; set; }

        public JMap(int row, int col)
        {
            //Camps = new List<Camp>();

            //AnimationList = new List<Animation>();

            //PlayerSkill = new global::PlayerSkill();

            //SkillList = new List<Skill>();
            //SkillList.Add(new BuildUnitSkill(DATUtility.GetUnit(301)));
            //SkillList.Add(new BuildUnitSkill(DATUtility.GetUnit(303)));
            //SkillList.Add(new BuildUnitSkill(DATUtility.GetUnit(304)));
            //SkillList.Add(new BuildUnitSkill(DATUtility.GetUnit(306)));
            //SkillList.Add(new BuildUnitSkill(DATUtility.GetUnit(305)));

            //SkillList2 = new List<Skill>();
            //SkillList2.Add(new SetTargetPosSkill());

            Widgets = new List<JSprite>();
            CameraTargetPos = new JMapPos(0, 0);

            ObjectIdIndex = 1;

            Row = row;
            Col = col;
            Cells = new JMapCell[row * col];
            //State = GState.Running;
        }


        //public Camera Camera { get { return _camera; } }


        /// <summary>
        /// 通过窗口坐标获取对应的Map cell
        /// </summary>
        /// <param name="ptInWindow"></param>
        /// <returns></returns>
        //public MapCell GetCell(Point2D ptInWindow)
        //{
        //    MapPos mapPos = Transform(ptInWindow);
        //    if (mapPos != null)
        //    {
        //        return this.Cells[mapPos.Row * Col + mapPos.Col];
        //    }
        //    return null;
        //}

        /// <summary>
        /// 窗口坐标转换为世界坐标
        /// </summary>
        /// <param name="ptInWindow"></param>
        /// <returns></returns>
        //public MapPos Transform(Point2D ptInWindow)
        //{
        //    Point2D ptInMap = new Point2D(ptInWindow.X - _zeroPoint.X,
        //        ptInWindow.Y - _zeroPoint.Y);
        //    int col = (int)(ptInMap.X / MapCell.Width);
        //    int row = (int)(ptInMap.Y / MapCell.Height);

        //    if (col < 0 || row < 0 || col >= Col || row >= Row)
        //    {
        //        return null;
        //    }
        //    return new MapPos(row, col);
        //}

        public void SortWidget()
        {
            //this.Widgets.Sort(ComparisonWidget);
        }

        //private int ComparisonWidget(Object2D x, Object2D y)
        //{
        //    if (x.SitePos.Row > y.SitePos.Row)
        //    {
        //        return -1;
        //    }
        //    else if (x.SitePos.Row == y.SitePos.Row)
        //    {
        //        if (x.SitePos.Col < y.SitePos.Col)
        //        {
        //            return -1;
        //        }
        //        else if (x.SitePos.Col > y.SitePos.Col)
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    return 1;
        //}

        //public void AddObject(JUnit unit, MapPos pos)
        //{
        //    Object2D obj = new Object2D();
        //    obj.ID = Widgets.Count;
        //    obj.Caption = string.Format("obj{0}", obj.ID);
        //    obj.SetUnit(unit);
        //    obj.SitePos = pos;

        //    Widgets.Add(obj);
        //}

        //public Camp GetCamp(int campId)
        //{
        //    for (int index = 0; index < Camps.Count; index++)
        //    {
        //        if (Camps[index].Id == campId)
        //        {
        //            return Camps[index];
        //        }
        //    }
        //    return null;
        //}

        public override string ToString()
        {
            return string.Format("{0}({1}", Caption, ID);
        }
    }
}