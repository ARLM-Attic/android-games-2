using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public abstract class HUD
    {
        public void Render(AGGDI gdi)
        {
            OnRender(gdi);
        }

        public bool InputEvent(int msg, int lParam, int wParam)
        {
            return OnInputEvent(msg, lParam, wParam);
        }

        protected abstract void OnRender(AGGDI gdi);
        protected abstract bool OnInputEvent(int msg, int lParam, int wParam);

        public abstract bool MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY);
    }
}
