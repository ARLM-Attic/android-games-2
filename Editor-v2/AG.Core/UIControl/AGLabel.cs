using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGLabel : AGControl
{
    public string Text { get; set; }

    public AGLabel(string text)
    {
        Text = text;
    }

    protected override void OnRender(IGDI gdi)
    {
        gdi.DrawText(AGRES.SmallUIHfont, 0x224488, Text, (int)Pos.X, (int)Pos.Y);
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        return false;
    }
}
