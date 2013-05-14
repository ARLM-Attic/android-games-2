using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BuildUnitSkill : Skill
{
    private bool _isRepare = false;

    private Unit2D _unit;

    public BuildUnitSkill(Unit2D unit)
        : base(unit.IconModel, unit.BuildCoolDown)
    {
        _unit = unit;

        CoolDownTime = unit.BuildCoolDown * 30;
    }

    public override bool Check(IEngine engine, Object2D obj)
    {
        if (!_isRepare)
        {
            _isRepare = true;
            return true;
        }
        else
        {
            _isRepare = false;
            return false;
        }
    }

    public override void OnCast(Object2D obj)
    {
        
    }

    public override void Render(IEngine engine)
    {
        //MapPos pos = new MapPos(engine.IDI.MousePoint.Y / MapCell.Height, engine.IDI.MousePoint.X / MapCell.Width);
        //Frame2D frame = _unit.Model.GetFrame(1, 1, 1);
        //float curX = pos.Center.X - frame.OffsetX;
        //float curY = pos.Center.Y - frame.offsetY;
        //System.Drawing.Bitmap image = new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data));
        //engine.GDI.DrawImage(
        //    image,
        //    curX,
        //    curY,
        //    frame.Width,
        //    frame.Height,
        //    frame.Width,
        //    frame.Height);
    }

    protected override void OnUpdate(IEngine engine)
    {
        if (IsRepare && IsCoolDown)
        {
            if (engine.CurrentMap.Camps[0].Income >= _unit.CostM
                && engine.CurrentMap.Camps[0].PopulationLimit - engine.CurrentMap.Camps[0].Population >= _unit.CostP)
            {
                // 为玩家创建一个单位
                Object2D obj = AGSUtility.CreateObject(engine.CurrentMap, engine.CurrentMap.Camps[0], _unit, "unknown", engine.CurrentMap.Camps[0].StartPos, Direction2DDef.South.Id);
                AGSUtility.MoveTo(obj, engine.CurrentMap.Camps[0].TargetPos);
                IsRepare = false;
                CoolDowning();
            }
        }
    }
}