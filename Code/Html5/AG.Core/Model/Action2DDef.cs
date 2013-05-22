using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action2DDef
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Caption { get; set; }

    private Action2DDef(int id, string code, string caption)
    {
        Id = id;
        Code = code;
        Caption = caption;
    }

    public override string ToString()
    {
        return Caption;
    }

    public static readonly Action2DDef Stand = new Action2DDef(0x01, "STD", "站立");
    public static readonly Action2DDef Move = new Action2DDef(0x02, "MOV", "移动");
    public static readonly Action2DDef Attack = new Action2DDef(0x03, "ATK", "攻击");
    public static readonly Action2DDef BHit = new Action2DDef(0x04, "BHIT", "挨打");
    public static readonly Action2DDef Defense = new Action2DDef(0x05, "DEF", "防御");
    public static readonly Action2DDef Die = new Action2DDef(0x06, "DIA", "死亡");

    private static List<Action2DDef> _defs = new List<Action2DDef>();

    public static List<Action2DDef> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(Stand);
            _defs.Add(Move);
            _defs.Add(Attack);
            _defs.Add(BHit);
            _defs.Add(Defense);
            _defs.Add(Die);
        }

        return _defs;
    }
}