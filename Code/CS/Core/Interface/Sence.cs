using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Sence
{
    protected IEngine _engine;
    private HUD _hud;
    private bool _isInitate = false;

    public Sence(IEngine engine)
    {
        _engine = engine;
    }

    public void Init()
    {
        if (!_isInitate)
        {
            _hud = CreateHUD();
            _isInitate = true;
        }
    }

    public void Render(IGDI gdi)
    {
        OnRender(gdi);

        if (_hud != null)
        {
            _hud.Render(gdi);
        }
    }

    public void Loop(IEngine engine)
    {
        if (_hud != null)
        {
            _hud.Loop(engine);
        }

        OnLoop(engine);
    }

    protected virtual HUD CreateHUD()
    {
        return null;
    }

    protected abstract void OnRender(IGDI gdi);

    protected virtual void OnLoop(IEngine engine)
    {
    }

    //[Obsolete("废弃的函数")]
    //public abstract void InputEvent(int msg, int lParam, int wParam);

    //[Obsolete("废弃的函数")]
    //public void MouseInput(MouseMessage mouse)
    //{
    //    if (_hud != null)
    //    {
    //        _hud.MouseInput(mouse);
    //    }

    //    OnMouseInput(mouse);
    //}

    protected virtual void OnMouseInput(MouseMessage mouse)
    {
    }
}