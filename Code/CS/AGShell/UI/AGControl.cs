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

        public AGControl Parent { get; set; }

        public Point2D ClientPos
        {
            get
            {
                if (Parent != null)
                {
                    return new Point2D(Parent.ClientPos.X + Pos.X, Parent.ClientPos.Y + Pos.Y);
                }
                return Pos;
            }
        }

        public void Render(AGGDI gdi)
        {
            OnRender(gdi);
        }

        protected abstract void OnRender(AGGDI gdi);

        public bool InRect(int x, int y)
        {
            if (x >= ClientPos.X && x <= ClientPos.X + Size.W
                && y >= ClientPos.Y && y <= ClientPos.Y + Size.H)
            {
                return true;
            }
            return false;
        }

        public abstract void OnInputEvent(MouseMessage mouse);
    }
}
