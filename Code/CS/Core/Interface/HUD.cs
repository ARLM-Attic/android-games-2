using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class HUD
{
    protected IEngine _engine;
    protected List<AGControl> _controls;

    public HUD(IEngine engine)
    {
        _engine = engine;
        _controls = new List<AGControl>();
    }

    public void Render(IGDI gdi)
    {
        for (int iControl = 0; iControl < _controls.Count; iControl++)
        {
            _controls[iControl].Render(gdi);
        }
        OnRender(gdi);
    }

    public void Loop(IEngine engine)
    {
        for (int ctlIndex = _controls.Count - 1; ctlIndex >= 0; ctlIndex--)
        {
            if (engine.IDI.Mouse.IsHandled)
            {
                return;
            }

            AGControl control = _controls[ctlIndex];
            if (control.InRect(engine.IDI.Mouse.X, engine.IDI.Mouse.Y))
            {
                if (control.OnInputEvent(engine.IDI.Mouse))
                {
                    engine.IDI.Mouse.IsHandled = true;
                    return;
                }
            }
        }
    }

    //public bool InputEvent(int msg, int lParam, int wParam)
    //{
    //    return OnInputEvent(msg, lParam, wParam);
    //}

    protected virtual void OnRender(IGDI gdi)
    {
    }

    //protected abstract bool OnInputEvent(int msg, int lParam, int wParam);

    //public bool MouseInput(MouseMessage mouse)
    //{
    //    for (int ctlIndex = _controls.Count - 1; ctlIndex >= 0; ctlIndex--)
    //    {
    //        AGControl control = _controls[ctlIndex];
    //        if (control.InRect(mouse.X, mouse.Y))
    //        {
    //            if (control.OnInputEvent(mouse))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
}