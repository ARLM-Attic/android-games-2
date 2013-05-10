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
            //Frame2D frame = _model.GetFrame(0x01, 0x01, 0x01);
            //gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            //    12,
            //    50,
            //    frame.Width,
            //    frame.Height,
            //    frame.Width,
            //    frame.Height);
            gdi.DrawText(AGRES.LargeUIFontHandle, 0xffff00, "Pet & Monster", 120, 100);
            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "version 1.0", 280, 180);
            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "loading...", MainWindow.Width / 2 - 60, MainWindow.Height / 2 + 50);

            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "Email:ly.jaeho@gmail.com", MainWindow.Width / 2 - 100, MainWindow.Height - 70);
            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "QQ:345241086", MainWindow.Width / 2 - 60, MainWindow.Height - 50);

            if (_counter > 2 * 30)
            {
                _engine.SwitchSence(new StagesSence(_engine));
            }
            //_hud.Render(gdi);
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            //_engine.SwitchSence(new StagesSence(_engine));
        }

        public override void MouseInput(MouseMessage mouse)
        {
            //_hud.MouseInput(button, state, deltaX, deltaY, deltaZ, ptX, ptY);
        }
    }
}
