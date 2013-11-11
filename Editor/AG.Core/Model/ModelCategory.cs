using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ModelCategory
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public ModelCategory(int id, string caption)
    {
        Id = id;
        Caption = caption;
    }

    public static ModelCategory Unit = new ModelCategory(1, "unit");
    public static ModelCategory Head = new ModelCategory(2, "head");
    public static ModelCategory UI = new ModelCategory(3, "ui");
    public static ModelCategory Design = new ModelCategory(4, "design");
    public static ModelCategory Icon = new ModelCategory(5, "icon");
    public static ModelCategory Terrain = new ModelCategory(6, "Terrain(地形)");
    public static ModelCategory Ornamental = new ModelCategory(7, "Ornamental(装饰物)");
    public static ModelCategory Building = new ModelCategory(8, "Building(建筑物)");

    public override string ToString()
    {
        return Caption;
    }

    private static List<ModelCategory> _defs = new List<ModelCategory>();

    public static List<ModelCategory> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(Unit);
            _defs.Add(Head);
            _defs.Add(UI);
            _defs.Add(Design);
            _defs.Add(Icon);
            _defs.Add(Terrain);
            _defs.Add(Ornamental);
            _defs.Add(Building);
        }

        return _defs;
    }

    public static ModelCategory Get(int stirpsId)
    {
        List<ModelCategory> list = GetDefs();
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