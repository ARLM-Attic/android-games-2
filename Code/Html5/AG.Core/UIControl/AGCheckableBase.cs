using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGCheckableBase : AGControl
{
    public bool IsChecked { get; set; }

    public event Action<AGControl, bool> Checked;

    protected override void OnRender(IGDI gdi)
    {
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        if (mouse.IsLBDown())
        {
            IsChecked = !IsChecked;

            if (Checked != null)
            {
                Checked(this, IsChecked);
            }
            return true;
        }
        return false;
    }
}