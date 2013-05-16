using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UnitStirps
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public UnitStirps(int id, string caption)
    {
        Id = id;
        Caption = caption;
    }

    public static UnitStirps Unit = new UnitStirps(1, "人类");
    public static UnitStirps Building = new UnitStirps(2, "怪兽");
    public static UnitStirps Ornamental = new UnitStirps(3, "装饰物");

    public override string ToString()
    {
        return Caption;
    }

    private static List<UnitStirps> _defs = new List<UnitStirps>();

    public static List<UnitStirps> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(Unit);
            _defs.Add(Building);
            _defs.Add(Ornamental);
        }

        return _defs;
    }

    public static UnitStirps Get(int stirpsId)
    {
        List<UnitStirps> list = GetDefs();
        foreach (var item in list)
        {
            if (item.Id == stirpsId)
            {
                return item;
            }
        }
        return list[0];
    }
}