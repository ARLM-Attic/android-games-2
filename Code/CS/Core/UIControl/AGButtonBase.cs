using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AGButtonBase : AGControl
{
    private bool _isPreClick = false;
    public event EventHandler Click;

    protected override void OnRender(IGDI gdi)
    {
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