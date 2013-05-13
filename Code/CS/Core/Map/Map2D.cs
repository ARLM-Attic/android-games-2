using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Map2D
{
    public int ID { get; set; }
    public string Caption { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
    public byte[] Background { get; set; }
    public byte[] BGM { get; set; }

    private Size2D _size;

    /// <summary>
    /// 摄像机所在的地图位置
    /// </summary>
    public MapPos CameraTargetPos { get; set; }

    public Size2D Size { get; set; }

    public MapCell[] Cells { get; set; }

    public List<Object2D> Widgets { get; set; }

    public List<Camp> Camps { get; set; }

    public long GameTime { get; set; }

    /// <summary>
    /// 单位的编号索引，用于为创建的单位分配新的编号
    /// </summary>
    public int ObjectIdIndex { get; set; }

    public GState State { get; set; }

    public List<Animation> AnimationList { get; private set; }

    public PlayerSkill PlayerSkill { get; set; }

    public Map2D()
    {
        Camps = new List<Camp>();

        AnimationList = new List<Animation>();

        PlayerSkill = new global::PlayerSkill();

        Widgets = new List<Object2D>();
        CameraTargetPos = new MapPos(0, 0);

        ObjectIdIndex = 1;

        State = GState.Running;
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

    public MapCell GetCell(MapPos mapPos)
    {
        if (mapPos == null)
        {
            return null;
        }
        if (mapPos.Row < 0)
        {
            mapPos.Row = 0;
        }
        else if (mapPos.Row >= Row)
        {
            mapPos.Row = Row - 1;
        }

        if (mapPos.Col < 0)
        {
            mapPos.Col = 0;
        }
        else if (mapPos.Col >= Col)
        {
            mapPos.Col = Col - 1;
        }
        return Cells[mapPos.Row * Col + mapPos.Col];
    }

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
        this.Widgets.Sort(ComparisonWidget);
    }

    private int ComparisonWidget(Object2D x, Object2D y)
    {
        if (x.SitePos.Row > y.SitePos.Row)
        {
            return -1;
        }
        else if (x.SitePos.Row == y.SitePos.Row)
        {
            if (x.SitePos.Col < y.SitePos.Col)
            {
                return -1;
            }
            else if (x.SitePos.Col > y.SitePos.Col)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        return 1;
    }

    public void AddObject(Unit2D unit, MapPos pos)
    {
        Object2D obj = new Object2D();
        obj.ID = Widgets.Count;
        obj.Caption = string.Format("obj{0}", obj.ID);
        obj.SetUnit(unit);
        obj.SitePos = pos;

        Widgets.Add(obj);
    }

    public Camp GetCamp(int campId)
    {
        for (int index = 0; index < Camps.Count; index++)
        {
            if (Camps[index].Id == campId)
            {
                return Camps[index];
            }
        }
        return null;
    }
}

public class MapPos
{
    public int Row { get; set; }
    public int Col { get; set; }

    public MapPos(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public MapPos()
    {
    }

    public Point2D Center
    {
        get
        {
            return new Point2D(Col * MapCell.Width + MapCell.Width / 2, Row * MapCell.Height + MapCell.Height / 2);
        }
    }

    public bool Compare(MapPos pos)
    {
        if (this.Row == pos.Row && this.Col == pos.Col)
        {
            return true;
        }
        return false;
    }
}

public class Point2D
{
    public float X { get; set; }
    public float Y { get; set; }
    public Point2D(float x, float y)
    {
        X = x;
        Y = y;
    }
}

public class Size2D
{
    public float W { get; set; }
    public float H { get; set; }
    public Size2D(float w, float h)
    {
        W = w;
        H = h;
    }
}

public class Rect2D
{
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
    public float Right
    {
        get
        {
            return X + W;
        }
    }
    public float Bottom
    {
        get
        { return Y + H; }
    }
    public Rect2D(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        W = width;
        H = height;
    }
}