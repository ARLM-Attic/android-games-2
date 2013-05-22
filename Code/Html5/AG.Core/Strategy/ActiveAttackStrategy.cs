using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ActiveAttackStrategy : IAttackStrategy
{
    public bool Attack(IEngine engine, Map2D map, Object2D obj)
    {
        foreach (var camp in obj.Map.Camps)
        {
            if (camp.Id != obj.Camp.Id)
            {
                foreach (var opponentObj in camp.ObjList)
                {
                    if (opponentObj.Unit.Category != UnitCategoryDef.Ornamental &&!opponentObj.IsDead() && ObjectUtil.CheckAttackDistance(obj, opponentObj))
                    {
                        if (obj.HasAttackCooldown())
                        {
                            obj.TargetObj = opponentObj;
                            engine.ADI.PlayAttackSound(obj.Unit.AttackSound);
                            int random = new Random().Next(0, 100);
                            if (random < obj.Unit.CritProbability)
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                AGSUtility.CritDemage(obj, obj.TargetObj);
                                obj.Attack();
                            }
                            else if (random < obj.Unit.CritProbability + obj.DefProbability)
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                obj.TargetObj.SetAction(ObjState.Def);
                                // 防御提升5
                                obj.TargetObj.ADDEF += 5;
                                AGSUtility.Demage(obj, obj.TargetObj);
                                obj.TargetObj.ADDEF -= 5;
                                obj.Attack();
                            }
                            else
                            {
                                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, obj.TargetObj.CurrentPoint);
                                obj.SetAction(ObjState.Attack);
                                AGSUtility.Demage(obj, obj.TargetObj);
                                obj.Attack();
                            }
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
