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

        private bool _isFirst = true;

        public SplashSence(IEngine engine)
            : base(engine)
        {
            _model = DATUtility.GetModel(12);
        }

        protected override void OnRender(IGDI gdi)
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
            int f = _counter / 30;
            if (f > 0)
            {
                if (_isFirst)
                {
                    _engine.ADI.PlayBGM(1);
                    _isFirst = false;
                }
                gdi.DrawText(AGRES.LargeUIFontHandle, 0xffff00, "Pet & Monster", 120, 100);
                if (f > 1)
                {
                    gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "version 1.0", 280, 180);
                    if (f > 2)
                    {
                        gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "loading...", MainWindow.Width / 2 - 60, MainWindow.Height / 2 + 50);
                        if (f > 3)
                        {
                            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "Email:ly.jaeho@gmail.com", MainWindow.Width / 2 - 100, MainWindow.Height - 70);
                            gdi.DrawText(AGRES.SmallUIHfont, 0xffffff, "QQ:345241086", MainWindow.Width / 2 - 60, MainWindow.Height - 50);
                        }
                    }
                }
            }

            if (_counter > 2 * 30)
            {
                _engine.SwitchSence(new StagesSence(_engine));
            }
            //_hud.Render(gdi);
        }
    }
}
