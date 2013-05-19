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

        private float _x;

        private int _frameIndex;
        private int _frameCounter;

        private bool _isFirst = true;

        public SplashSence(IEngine engine)
            : base(engine)
        {
            _model = DATUtility.GetModel(12);

            _frameIndex = 1;
            _x = (MainWindow.Width - _model.GetFrame(1, 1, 1).Width) / 2;
        }

        protected override void OnRender(IGDI gdi)
        {
            if (_isFirst)
            {
                _engine.ADI.PlayBGM(1);
                _isFirst = false;
            }

            _counter++;
            _frameCounter++;
            if (_frameCounter > 15)
            {
                _frameIndex++;
                _frameCounter = 0;
            }

            if (_model.GetFrames(0x01, 0x01).Count < _frameIndex)
            {
                _frameIndex = _model.GetFrames(0x01, 0x01).Count;
            }

            Frame2D frame = _model.GetFrame(0x01, 0x01, _frameIndex);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                _x,
                100,
                frame.Width,
                frame.Height,
                frame.Width,
                frame.Height);

            gdi.DrawText(AGRES.NormalUIHfont, 0xffffff, "ly.jaeho@mail.com", MainWindow.Width / 2 - 90, MainWindow.Height / 2 + 250);

            if (_frameIndex == _model.GetFrames(0x01, 0x01).Count)
            {
                if (_counter >= 30)
                {
                    _counter = 1;
                }

                //System.Diagnostics.Debug.WriteLine(string.Format("{0}, {1}", _counter, _counter % 30));
                if (_counter % 30 <= 15)
                {
                    gdi.DrawText(AGRES.NormalUIHfont, 0xff0000, "点击鼠标开始游戏", MainWindow.Width / 2 - 80, MainWindow.Height / 2 + 50);
                }
                else
                {
                    gdi.DrawText(AGRES.NormalUIHfont, 0xffff00, "点击鼠标开始游戏", MainWindow.Width / 2 - 80, MainWindow.Height / 2 + 50);
                }
            }
        }

        protected override void OnLoop(IEngine engine)
        {
            if (_frameIndex == _model.GetFrames(0x01, 0x01).Count)
            {
                if (engine.IDI.Mouse.IsLBDown())
                {
                    _engine.SwitchSence(new StagesSence(_engine));
                    return;
                }
            }
            base.OnLoop(engine);
        }
    }
}
