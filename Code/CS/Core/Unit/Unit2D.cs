using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Unit2D
{
    public UnitCategory Category { get; set; }
    public UnitStirps Stirps { get; set; }

    public int Id { get; set; }
    public string Caption { get; set; }

    public Model2D Model { get; set; }
    public Model2D IconModel { get; set; }
    /// <summary>
    /// 攻击音效
    /// </summary>
    public AttackSound AttackSound { get; set; }

    public int MaxHP { get; set; }
    public int MaxMP { get; set; }

    public int MSpeed { get; set; }

    public int APSpeed { get; set; }
    public int ADSpeed { get; set; }

    public int AP { get; set; }
    public int AD { get; set; }
    public int ADDEF { get; set; }
    public int APDEF { get; set; }
    public int CritProbability { get; set; }
    public int DefProbability { get; set; }

    public UnitMatrix2D Matrix { get; set; }

    public int ZDistance { get; set; }

    public int Size { get; set; }
    public int ADDistance { get; set; }
    public float Scale { get; set; }

    public int CostM { get; set; }
    public int CostP { get; set; }
    public int BuildCoolDown { get; set; }

    public Unit2D()
    {
        MSpeed = 6;
        Size = 16;
        ADDistance = 16;

        MaxHP = 100;
        AD = 30;
        ADDEF = 5;

        Scale = 0.5f;

        CostM = 30;
        CostP = 1;
    }
}

public class UnitMatrix2D
{
}