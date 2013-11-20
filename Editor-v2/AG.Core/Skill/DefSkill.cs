using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DefSkill : Skill
{
    public DefSkill()
        : base(null, 0)
    {
    }

    public override bool Check(IEngine engine, Object2D obj)
    {
        obj.DefProbability = 50;
        return false;
    }

    public override void OnCast(Object2D obj)
    {
    }
}