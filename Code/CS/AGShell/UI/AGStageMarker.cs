using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class AGStageMarker : AGControl
    {
        public event EventHandler Click;

        public string Text { get; private set; }
        public Model2D Model { get; private set; }
        private int _frameIndex = 1;

        public AGStageMarker(string text, Point2D pt, Size2D size)
        {
            Text = text;
            Pos = pt;
            Size = size;

            Model = DATUtility.GetModel(14);
            Size.W = Model.GetFrame(1, 1, 1).Width;
            Size.H = Model.GetFrame(1, 1, 1).Height;
        }

        protected override void OnRender(AGGDI gdi)
        {
            Frame2D frame = Model.GetFrame(0x01, 0x01, _frameIndex);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                ClientPos.X,
                ClientPos.Y,
                Size.W,
                Size.H,
                frame.Width,
                frame.Height);
            gdi.DrawShadowText(Text, ClientPos.X + 20, ClientPos.Y + 5);
        }

        public override bool OnInputEvent(MouseMessage mouse)
        {
            if (mouse.IsLBDown())
            {
                _frameIndex = (_frameIndex + 1) % 3;
                _frameIndex++;

                if (Click != null)
                {
                    Click(this, null);
                }

                return true;
            }
            return false;
        }
    }
}
