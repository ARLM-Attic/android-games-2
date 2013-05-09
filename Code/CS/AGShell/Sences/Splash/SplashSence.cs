using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class SplashSence : Sence
    {
        private int _counter;
        private Model2D _model;

        public SplashSence(AGEngine engine)
            : base(engine)
        {
            _model = DATUtility.GetModel(12);
        }

        protected override void OnRender(AGGDI gdi)
        {
            _counter++;
            Frame2D frame = _model.GetFrame(0x01, 0x01, 0x01);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                12,
                50,
                frame.Width,
                frame.Height,
                frame.Width,
                frame.Height);
            gdi.DrawText("加载中，请等待", MainWindow.Width / 2, MainWindow.Height / 2 + 50);

            if (_counter > 5 * 30)
            {
                _engine.SwitchSence(new SelectLevelSence(_engine));
            }
            //_hud.Render(gdi);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            //_engine.SwitchSence(new SelectLevelSence(_engine));
        }

        public override void MouseInput(int button, int state, int deltaX, int deltaY, int deltaZ, int ptX, int ptY)
        {
            //_hud.MouseInput(button, state, deltaX, deltaY, deltaZ, ptX, ptY);
        }
    }
}
