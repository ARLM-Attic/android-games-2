using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class StagesSence : Sence
    {
        public StagesSence(IEngine engine)
            : base(engine)
        {
            _engine.ADI.PlayBGM(2);
        }

        protected override HUD CreateHUD()
        {
            Model2D model = DATUtility.GetModel(13);
            return new StagesHUD(_engine, model);
        }

        protected override void OnRender(IGDI gdi)
        {
        }

        protected override void OnLoop(IEngine engine)
        {
        }
    }
}
