using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AttackSkill : Skill
{
    public AttackSkill()
        : base(null, 2 * 30)
    {
    }

    public override bool Check(IEngine engine, Object2D obj)
    {
        if (obj.State == ObjState.Def)
        {
            return true;
        }

        foreach (var camp in obj.Map.Camps)
        {
            if (camp.Id != obj.Camp.Id)
            {
                foreach (var opponentObj in camp.ObjList)
                {
                    if (!opponentObj.IsDead() && ObjectUtil.CheckAttackDistance(obj, opponentObj))
                    {
                        if (IsCoolDown)
                        {
                            obj.TargetObj = opponentObj;
                            engine.ADI.Play();
                            int random = new Random().Next(0, 100);
                            if (random < obj.Unit.CritProbability)
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                Cast(obj);
                                AGSUtility.CritDemage(obj, obj.TargetObj);
                            }
                            else if (random < obj.Unit.CritProbability + obj.DefProbability)
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                obj.TargetObj.SetAction(ObjState.Def);
                                Cast(obj);
                                // 防御提升5
                                obj.TargetObj.ADDEF += 5;
                                AGSUtility.Demage(obj, obj.TargetObj);
                                obj.TargetObj.ADDEF -= 5;
                            }
                            else
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                Cast(obj);
                                AGSUtility.Demage(obj, obj.TargetObj);
                            }
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public override void OnCast(Object2D obj)
    {
    }
}
