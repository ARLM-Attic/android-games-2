using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CampType
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public static CampType Player = new CampType(1, "玩家");
    public static CampType Computer = new CampType(2, "电脑");

    public CampType(int id, string caption)
    {
        Id = id;
        Caption = caption;
    }

    public override string ToString()
    {
        return Caption;
    }

    private static List<CampType> _defs = new List<CampType>();

    public static List<CampType> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(Player);
            _defs.Add(Computer);
        }

        return _defs;
    }

    public static CampType Get(int id)
    {
        List<CampType> list = GetDefs();
        foreach (var item in list)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return list[0];
    }
}