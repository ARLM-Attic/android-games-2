﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapCell
{
    //public const int Width = 40;
    //public const int Height = 40;
    public const int Width = 64;
    public const int Height = 48;
    public const int Zoom = 1;

    public List<Object2D> ObjList { get; set; }

    public MapPos MapPos { get; set; }

    #region 属性
    public int Type { get; set; }
    /// <summary>
    /// 地形编号
    /// </summary>
    public int TerrainId { get; set; }
    public int TerrainDir { get; set; }
    /// <summary>
    /// 该地形中具体的切片编号
    /// </summary>
    public int TerrainIndex { get; set; }
    #endregion

    public Texture2D Texture2D { get; set; }

    public bool HasEnuRange(Point2D pt, int size)
    {
        if (Type != 0)
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