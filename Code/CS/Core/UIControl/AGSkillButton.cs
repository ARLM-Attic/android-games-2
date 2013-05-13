using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGSkillButton : AGControl
{
    public Model2D Model { get; private set; }
    public Model2D ModelIcon { get; private set; }

    private bool _isPreClick = false;
    public event EventHandler Click;

    public AGSkillButton()
    {
        Model = DATUtility.GetModel(17);
        ModelIcon = DATUtility.GetModel(101);

        Size = new Size2D(Model.GetFrame(1, 1, 1).Width,Model.GetFrame(1, 1, 1).Height);
    }

    protected override void OnRender(IGDI gdi)
    {
        Frame2D frameIcon = ModelIcon.GetFrame(0x01, 0x01, 0x01);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frameIcon.Data)),
            ClientPos.X + 1,
            ClientPos.Y + 1,
            frameIcon.Width,
            frameIcon.Height,
            frameIcon.Width,
            frameIcon.Height);

        Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            ClientPos.X,
            ClientPos.Y,
            Size.W,
            Size.H,
            frame.Width,
            frame.Height);
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
