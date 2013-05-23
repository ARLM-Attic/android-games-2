using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGSUtility
{
    /// <summary>
    /// 创建单位对象
    /// </summary>
    /// <param name="map"></param>
    /// <param name="camp"></param>
    /// <param name="unit"></param>
    /// <param name="caption"></param>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Object2D CreateObject(Map2D map, Camp camp, Unit2D unit, string caption, MapPos pos, int direction)
    {
        Random rd = new Random();

        Object2D obj = new Object2D(Action2DDef.Stand.Id, direction);
        obj.ID = ++map.ObjectIdIndex;
        obj.Caption = caption;
        obj.SetUnit(unit);
        obj.SitePos = pos;
        obj.CurrentPoint = new Point2D(
            pos.Center.X - MapCell.Width / 2 + rd.Next(0, MapCell.Width / 2),
            pos.Center.Y - MapCell.Height / 2 + rd.Next(0, MapCell.Height / 2));
        obj.Map = map;
        obj.Camp = camp;

        if (obj.Camp.Result != null)
        {
            obj.Camp.Result.BuildCount++;
        }

        camp.Income -= unit.CostM;
        camp.Population += unit.CostP;
        camp.ObjList.Add(obj);
        map.Widgets.Add(obj);
        System.Diagnostics.Debug.WriteLine(string.Format(">{0} createobj:{1} p:{2}", camp.Caption, obj.ID, camp.Population));
        return obj;
    }
    public static Object2D CreateObject(Map2D map, Camp camp, Unit2D unit, string caption, Point2D pt, MapPos pos, int direction)
    {
        Random rd = new Random();

        Object2D obj = new Object2D(Action2DDef.Stand.Id, direction);
        obj.ID = ++map.ObjectIdIndex;
        obj.Caption = caption;
        obj.SetUnit(unit);
        obj.SitePos = pos;
        obj.CurrentPoint = new Point2D(pt.X, pt.Y);
        obj.Map = map;
        obj.Camp = camp;

        if (obj.Camp.Result != null)
        {
            obj.Camp.Result.BuildCount++;
        }

        camp.Income -= unit.CostM;
        camp.Population += unit.CostP;
        camp.ObjList.Add(obj);
        map.Widgets.Add(obj);
        System.Diagnostics.Debug.WriteLine(string.Format(">{0} createobj:{1} p:{2}", camp.Caption, obj.ID, camp.Population));
        return obj;
    }

    /// <summary>
    /// 对象死亡，清除占用的人口
    /// </summary>
    /// <param name="deadObj"></param>
    /// <param name="murderer"></param>
    public static void ObjectDie(Object2D deadObj, Object2D murderer)
    {
        deadObj.Camp.Population -= deadObj.Unit.CostP;
        System.Diagnostics.Debug.WriteLine(string.Format(">{0} dieobj:{1} p:{2}", deadObj.Camp.Caption, deadObj.ID, deadObj.Camp.Population));
    }

    public static void RemoveObject(Object2D obj)
    {
        MapCell cell = obj.Map.GetCell(obj.SitePos);
        for (int objIndex = 0; objIndex < cell.ObjList.Count; objIndex++)
        {
            if (cell.ObjList[objIndex].ID == obj.ID)
            {
                cell.ObjList.RemoveAt(objIndex);
                break;
            }
        }

        obj.Camp.ObjList.Remove(obj);
        obj.Map.Widgets.Remove(obj);
    }

    public static void MoveTo(Object2D obj, MapPos targetPos)
    {
        obj.TargetPos = targetPos;
    }

    public static void SetStartPos(Camp camp, MapPos pos)
    {
        camp.StartPos = pos;
    }

    /// <summary>
    /// 设置地形
    /// </summary>
    /// <param name="map"></param>
    /// <param name="pos"></param>
    /// <param name="terrain"></param>
    public static void SetTerrain(Map2D map, MapPos pos, Terrain terrain)
    {
        for (int deltaR = -1; deltaR <= 1; deltaR++)
        {
            for (int deltaC = -2; deltaC <= -2; deltaC++)
            {
                int row = pos.Row + deltaR;
                int col = pos.Col + deltaC;

                if (row < 0 || row >= map.Row
                    || col < 0 || col >= map.Col)
                {
                    continue;
                }

            }
        }

        MapCell cell = map.GetCell(pos);
        cell.Type = terrain.Value;
    }

    /// <summary>
    /// obj1 对 obj2造成伤害
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    public static void Demage(Object2D obj1, Object2D obj2)
    {
        int demage = (obj1.AD - obj2.ADDEF);
        obj2.HP -= demage;
        if (obj2.IsDead())
        {
            ObjectDie(obj2, obj1);
            if (obj2.Camp.Result != null)
            {
                obj2.Camp.Result.DeadCount++;
            }

            if (obj1.Camp.Result != null)
            {
                obj1.Camp.Result.KilledCount++;
            }
        }

        FloatString floatString = new FloatString();
        floatString.Pos = new Point2D(obj2.CurrentPoint.X, obj2.CurrentPoint.Y);
        floatString.Text = demage.ToString();
        obj1.Map.AnimationList.Add(floatString);
    }

    public static void CritDemage(Object2D obj1, Object2D obj2)
    {
        int demage = (obj1.AD * 2 - obj2.ADDEF);
        obj2.HP -= demage;
        if (obj2.IsDead())
        {
            ObjectDie(obj2, obj1);

            if (obj2.Camp.Result != null)
            {
                obj2.Camp.Result.DeadCount++;
            }

            if (obj1.Camp.Result != null)
            {
                obj1.Camp.Result.KilledCount++;
            }
        }

        FloatString floatString = new FloatString();
        floatString.Pos = new Point2D(obj2.CurrentPoint.X, obj2.CurrentPoint.Y);
        floatString.Text = string.Format("{0}!", demage);
        obj1.Map.AnimationList.Add(floatString);
    }

    public static void Victory(Map2D map, Camp camp)
    {
        map.State = GState.Victory;
        if (camp.Result != null)
        {
            camp.Result.IsVictory = true;
        }
    }

    public static void Defeat(Map2D map, Camp camp)
    {
        map.State = GState.Defeat;
        if (camp.Result != null)
        {
            camp.Result.IsVictory = false;
        }
    }

    #region 结束条件
    public static bool CheckVictory(Map2D map)
    {
        for (int iCamps = 0; iCamps < map.Camps.Count; iCamps++)
        {
            if (map.Camps[iCamps].Type == CampType.Computer)
            {
                Camp camp = map.Camps[iCamps];
                for (int iObj = 0; iObj < camp.ObjList.Count; iObj++)
                {
                    if (camp.ObjList[iObj].Unit.Category == UnitCategoryDef.Building && !camp.ObjList[iObj].IsDead())
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public static bool CheckDefeat(Map2D map)
    {
        for (int iCamps = 0; iCamps < map.Camps.Count; iCamps++)
        {
            if (map.Camps[iCamps].Type == CampType.Player)
            {
                Camp camp = map.Camps[iCamps];
                for (int iObj = 0; iObj < camp.ObjList.Count; iObj++)
                {
                    if (camp.ObjList[iObj].Unit.Category == UnitCategoryDef.Building && !camp.ObjList[iObj].IsDead())
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    #endregion
}
