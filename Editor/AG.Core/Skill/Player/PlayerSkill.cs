using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerSkill
{
    public Model2D Model { get; set; }

    public bool IsPrepare { get; set; }
    public MapPos Pos { get; set; }

    public PlayerSkill()
    {
        Model = DATUtility.GetModel(15);
    }

    public void Select()
    {
    }

    public void Cast()
    {
    }

    public void OnCasting()
    {
    }
}