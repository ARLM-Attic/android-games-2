using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public abstract class HUD
    {
        protected AGEngine _engine;
        protected List<AGControl> _controls;

        public HUD(AGEngine engine)
        {
            _engine = engine;
            _controls = new List<AGControl>();
        }

        public void Render(AGGDI gdi)
        {
            for (int iControl = 0; iControl < _controls.Count; iControl++)
            {
                _controls[iControl].Render(gdi);
            }
            OnRender(gdi);
        }

        public bool InputEvent(int msg, int lParam, int wParam)
        {
            return OnInputEvent(msg, lParam, wParam);
        }

        protected virtual void OnRender(AGGDI gdi)
        {
        }

        protected abstract bool OnInputEvent(int msg, int lParam, int wParam);

        public abstract bool MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY);
    }
}
