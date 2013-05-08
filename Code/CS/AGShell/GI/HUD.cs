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

        public void InputEvent(int msg, int lParam, int wParam)
        {
            OnInputEvent(msg, lParam, wParam);
        }

        protected abstract void OnRender(AGGDI gdi);
        protected abstract void OnInputEvent(int msg, int lParam, int wParam);
    }
}
