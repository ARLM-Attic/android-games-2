using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public abstract class AGControl
    {
        public Point2D Pos { get; set; }
        public Size2D Size { get; set; }

        public void Render(AGGDI gdi)
        {
            OnRender(gdi);
        }

        protected abstract void OnRender(AGGDI gdi);

        public bool InRect(int x, int y)
        {
            if (x >= Pos.X && x <= Pos.X + Size.W
                && y >= Pos.Y && y <= Pos.Y + Size.H)
            {
                return true;
            }
            return false;
        }

        public abstract void OnInputEvent(int msg, int lParam, int wParam);
    }
}
