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
        Object2D obj = new Object2D(Action2DDef.Stand.Id, direction);
        obj.ID = ++map.ObjectIdIndex;
        obj.Caption = caption;
        obj.SetUnit(unit);
        obj.SitePos = pos;
        obj.CurrentPoint = obj.SitePos.Center;
        obj.Map = map;
        obj.Camp = camp;

        camp.Income -= unit.CostM;
        camp.Population += unit.CostP;
        camp.ObjList.Add(obj);
        map.Widgets.Add(obj);
        return obj;
    }

    public static void RemoveObject(Object2D obj)
    {
        obj.Camp.ObjList.Remove(obj);
        obj.Camp.Population -= obj.Unit.CostP;
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
        MapCell cell = map.GetCell(pos);
        cell.Value = terrain.Value;
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

        FloatString floatString = new FloatString();
        floatString.Pos = new Point2D(obj2.CurrentPoint.X, obj2.CurrentPoint.Y);
        floatString.Text = demage.ToString();
        obj1.Map.AnimationList.Add(floatString);
    }

    public static void CritDemage(Object2D obj1, Object2D obj2)
    {
        int demage = (obj1.AD * 2 - obj2.ADDEF);
        obj2.HP -= demage;

        FloatString floatString = new FloatString();
        floatString.Pos = new Point2D(obj2.CurrentPoint.X, obj2.CurrentPoint.Y);
        floatString.Text = string.Format("{0}!", demage);
        obj1.Map.AnimationList.Add(floatString);
    }

    public static void Victory(Map2D map, Camp camp)
    {
        map.State = GState.Victory;
    }

    public static void Defeat(Map2D map, Camp camp)
    {
        map.State = GState.Defeat;
    }

    #region 结束条件
    public static bool CheckVictory(Map2D map)
    {
        if (map.Camps[1].ObjList.Count == 0)
        {
            return true;
        }
        return false;
    }

    public static bool CheckDefeat(Map2D map)
    {
        if (map.Camps[0].ObjList.Count == 0)
        {
            return true;
        }
        return false;
    }
    #endregion
}
