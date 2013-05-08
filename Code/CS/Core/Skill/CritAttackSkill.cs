using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CritAttackSkill : Skill
{
    public override bool Check(IEngine engine, Object2D obj)
    {
        obj.Unit.CritProbability = 30;
        return false;
    }

    public override void OnCast(Object2D obj)
    {
    }
}