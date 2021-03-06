﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class UpgradeSence : Sence
    {
        public UpgradeSence(IEngine engine)
            : base(engine)
        {
            
        }

        protected override HUD CreateHUD()
        {
            return new UpgradeHUD(_engine);
        }

        protected override void OnRender(IGDI gdi)
        {
            gdi.DrawText(AGRES.LargeUIFontHandle, 0x22ff22, "Upgrade", 210, 100);
        }

        protected override void OnLoop(IEngine engine)
        {
        }
    }
}
