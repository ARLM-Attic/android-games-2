using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class UpgradeSence : Sence
    {
        private UpgradeHUD _hud;

        public UpgradeSence(AGEngine engine)
            : base(engine)
        {
            _hud = new UpgradeHUD(engine);
            
        }

        protected override void OnRender(AGGDI gdi)
        {
            gdi.DrawText(AGRES.LargeUIFontHandle, 0x22ff22, "Upgrade", 210, 100);

            _hud.Render(gdi);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
        }

        public override void MouseInput(MouseMessage mouse)
        {
        }
    }
}
