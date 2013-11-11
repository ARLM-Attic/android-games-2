using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGContainer : AGControl
{
    protected List<AGControl> _controls;

    public List<AGControl> Controls { get { return _controls; } }

    public AGContainer()
    {
        _controls = new List<AGControl>();
    }

    protected override void OnRender(IGDI gdi)
    {
        for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
        {
            _controls[ctlIndex].Render(gdi);
        }
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
        {
            if (_controls[ctlIndex].InRect(mouse.X, mouse.Y))
            {
                if (_controls[ctlIndex].OnInputEvent(mouse))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AddChildren(AGControl control)
    {
        _controls.Add(control);
        control.Parent = this;
    }
}