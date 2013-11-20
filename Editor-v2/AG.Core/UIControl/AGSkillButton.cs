using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

public class AGSkillButton : AGControl
{
    private Skill _skill;
    private Model2D _btnModel;

    private bool _isPreClick = false;
    public event EventHandler Click;

    public Skill Skill { get { return _skill; } }

    public AGSkillButton(Skill skill)
    {
        _btnModel = DATUtility.GetModel(17);
        //ModelIcon = DATUtility.GetModel(101);
        _skill = skill;

        Size = new Size2D(_btnModel.GetFrame(1, 1, 1).Width, _btnModel.GetFrame(1, 1, 1).Height);
    }

    protected override void OnRender(IGDI gdi)
    {
        Frame2D frameIcon = _skill.IconModel.GetFrame(0x01, 0x01, 0x01);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frameIcon.Data)),
            ClientPos.X + 1,
            ClientPos.Y + 1,
            frameIcon.Width,
            frameIcon.Height,
            frameIcon.Width,
            frameIcon.Height);

        Frame2D frame = _btnModel.GetFrame(0x01, 0x01, 0x01);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            ClientPos.X,
            ClientPos.Y,
            Size.W,
            Size.H,
            frame.Width,
            frame.Height);

        if (Skill.IsCoolDown)
        {
            gdi.DrawShadowText("ok", Pos.X + 1, Pos.Y + 1);
        }
        else
        {
            gdi.DrawShadowText(((int)(Skill.CoolDownConter / 30)).ToString(), Pos.X, Pos.Y + 1);
        }

        Skill.RenderOnIcon(gdi, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.W, (int)Size.H));
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        if (mouse.IsLBDown())
        {
            _isPreClick = true;
            return true;
        }
        else
        {
            if (_isPreClick)
            {
                _isPreClick = false;
                if (Click != null)
                {
                    Click(this, null);
                }
                
            }
        }
        return false;
    }
}
