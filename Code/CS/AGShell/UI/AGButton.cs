using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class AGButton : AGControl
    {
        public event EventHandler Click;

        public float ColdDown { get; set; }
        public float ColdDownTick { get; set; }
        public int UnitId { get; set; }
        public Unit2D Unit { get; set; }
        public int CostM { get; set; }

        protected override void OnRender(AGGDI gdi)
        {
            gdi.DrawRectangle(Pos.X, Pos.Y, Size.W, Size.H);
            gdi.DrawRectangle(Pos.X + 2, Pos.Y + 2, Size.W - 4, Size.H - 4);

            //gdi.DrawText(Unit.Caption, Pos.X, Pos.Y + 10);
            Frame2D frame = Unit.Model.GetFrame(0x01,0x01,0x01);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                Pos.X,
                Pos.Y,
                Size.W,
                Size.H,
                frame.Width,
                frame.Height);

            if (ColdDownTick == 0)
            {
                gdi.DrawShadowText("ok", Pos.X, Pos.Y);
            }
            else
            {
                gdi.DrawShadowText("cd", Pos.X, Pos.Y + 20);
                gdi.DrawShadowText(((int)(ColdDownTick / 30)).ToString(), Pos.X, Pos.Y);
                ColdDownTick--;
                if (ColdDownTick < 0)
                {
                    ColdDownTick = 0;
                }
            }

            gdi.DrawShadowText(CostM.ToString(), Pos.X + 20, Pos.Y + 25);
        }

        public override void OnInputEvent(int msg, int lParam, int wParam)
        {
            if (msg == 2)
            {
                if (ColdDownTick == 0 && Click != null)
                {
                    Click(this, null);
                }
            }
        }
    }
}
