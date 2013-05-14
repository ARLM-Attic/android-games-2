using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SetTargetPosSkill : Skill
{
    private Model2D _model;

    public SetTargetPosSkill()
        : base(DATUtility.GetModel(101), 0)
    {
        _model = DATUtility.GetModel(15);
    }

    public override bool Check(IEngine engine, Object2D obj)
    {
        return false;
    }


    public override void Render(IEngine engine)
    {
        if (IsRepare)
        {
            MapPos pos = new MapPos(engine.IDI.MousePoint.Y / MapCell.Height, engine.IDI.MousePoint.X / MapCell.Width);
            Frame2D frame = _model.GetFrame(1, 1, 1);
            float curX = pos.Center.X - frame.OffsetX;
            float curY = pos.Center.Y - frame.offsetY;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data));
            engine.GDI.DrawImage(
                image,
                curX,
                curY,
                frame.Width,
                frame.Height,
                frame.Width,
                frame.Height);
        }
    }

    public override void OnCast(Object2D obj)
    {
    }

    protected override void OnUpdate(IEngine engine, MouseMessage mouse)
    {
        if (IsRepare && mouse.IsLBDown())
        {
            engine.CurrentMap.Camps[0].TargetPos = new MapPos(
                mouse.Y / MapCell.Height,
                mouse.X / MapCell.Width);
            IsRepare = false;

            mouse.IsHandled = true;
        }
    }
}