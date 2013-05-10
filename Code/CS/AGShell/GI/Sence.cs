using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public abstract class Sence
    {
        protected AGEngine _engine;

        public Sence(AGEngine engine)
        {
            _engine = engine;
        }

        public void Render(AGGDI gdi)
        {
            OnRender(gdi);
        }

        protected abstract void OnRender(AGGDI gdi);

        public abstract void InputEvent(int msg, int lParam, int wParam);
        public abstract void MouseInput(MouseMessage mouse);
    }
}
