using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Camp
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public MapPos StartPos { get; set; }

    public List<Object2D> ObjList { get; set; }

    public List<Camp> OpponentCamps { get; set; }

    public int IncomePreSec { get; set; }
    public int Income { get; set; }

    public int Population { get; set; }
    public int PopulationLimit { get; set; }

    /// <summary>
    /// 可用的单位列表
    /// </summary>
    public List<Unit2D> AvailableUnitList { get; set; }

    public GameResult Result { get; set; }

    /// <summary>
    /// 目标位置,该阵营的单位会自动往该地方移动
    /// </summary>
    public MapPos TargetPos { get; set; }

    public CampType Type { get; set; }

    public Camp()
    {
        ObjList = new List<Object2D>();
        OpponentCamps = new List<Camp>();
        AvailableUnitList = new List<Unit2D>();

        IncomePreSec = 10;
        Income = 30;
        Population = 0;
        PopulationLimit = 8;
    }

    public Camp(int id, string caption)
        : this()
    {
        Id = id;
        Caption = caption;
    }

    public override string ToString()
    {
        return string.Format("{0}({1})", Caption, Id);
    }
}