using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UnitCategory
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public UnitCategory(int id, string caption)
    {
        Id = id;
        Caption = caption;
    }

    public override string ToString()
    {
        return Caption;
    }
}

public static class UnitCategoryDef
{
    public static List<UnitCategory> Categories = new List<UnitCategory>();
    public static UnitCategory Other = new UnitCategory(0, "其他");
    public static UnitCategory Unit = new UnitCategory(1, "单位");
    public static UnitCategory Building = new UnitCategory(2, "建筑物");
    public static UnitCategory Ornamental = new UnitCategory(3, "装饰物");

    private static List<UnitCategory> _defs = new List<UnitCategory>();

    public static List<UnitCategory> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(Unit);
            _defs.Add(Building);
            _defs.Add(Other);
            _defs.Add(Ornamental);
        }

        return _defs;
    }

    public static UnitCategory Get(int categoryId)
    {
        List<UnitCategory> list = GetDefs();
        foreach (var item in list)
        {
            if (item.Id == categoryId)
            {
                return item;
            }
        }
        return list[0]; ;
    }
}
