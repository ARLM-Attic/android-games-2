using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class AGControl
{
    public Point2D Pos { get; set; }
    public Size2D Size { get; set; }

    public AGControl Parent { get; set; }

    public bool IsVisible { get; set; }

    public Point2D ClientPos
    {
        get
        {
            if (Parent != null)
            {
                return new Point2D(Parent.ClientPos.X + Pos.X, Parent.ClientPos.Y + Pos.Y);
            }
            return new Point2D(Pos.X, Pos.Y);
        }
    }

    public AGControl()
    {
        IsVisible = true;
    }

    public void Render(IGDI gdi)
    {
        if (IsVisible)
        {
            OnRender(gdi);
        }
    }

    protected abstract void OnRender(IGDI gdi);

    public bool InRect(int x, int y)
    {
        if (x >= ClientPos.X && x <= ClientPos.X + Size.W
            && y >= ClientPos.Y && y <= ClientPos.Y + Size.H)
        {
            return true;
        }
        return false;
    }

    public abstract bool OnInputEvent(MouseMessage mouse);
}