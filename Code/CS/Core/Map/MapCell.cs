using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapCell
{
    public const int Width = 32;
    public const int Height = 32;
    public const int Zoom = 1;

    public List<Object2D> ObjList { get; set; }

    public MapPos MapPos { get; set; }

    public int Value { get; set; }

    public bool HasEnuRange(Point2D pt, int size)
    {
        if (Value != 0)
        {
            return false;
        }

        if (ObjList.Count > 0)
        {
            return false;
        }
        return true;
    }

    public Point2D Center
    {
        get
        {
            return new Point2D(MapPos.Col * MapCell.Width + MapCell.Width / 2, MapPos.Row * MapCell.Height + MapCell.Height / 2);
        }
    }

    public MapCell()
    {
        ObjList = new List<Object2D>();
    }
}