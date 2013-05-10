using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace AGShell
{
    public class UnitPanel : AGControl
    {
        private Unit2D _unit;
        public float Zoom { get; set; }

        public UnitPanel(Unit2D unit)
        {
            _unit = unit;
            Zoom = 1.0f;
        }

        protected override void OnRender(AGGDI gdi)
        {
            Frame2D frame = _unit.Model.GetFrame(0x01, 0x01, 0x01);
            float frameWidth = frame.Width * _unit.Scale;
            float frameHeight = frame.Height * _unit.Scale;
            float frameOffsetX = frame.OffsetX * _unit.Scale;
            float frameOffsetY = frame.offsetY * _unit.Scale;
            Bitmap image = new Bitmap(new MemoryStream(frame.Data));
            float curfw = frameWidth * Zoom;
            float curfh = frameHeight * Zoom;
            float curfx = Pos.X - frameOffsetX * Zoom;
            float curfy = Pos.Y - frameOffsetY * Zoom;

            gdi.DrawImage(
                image,
                curfx,
                curfy,
                curfw,
                curfh,
                frame.Width,
                frame.Height);
        }

        public override void OnInputEvent(MouseMessage mouse)
        {
        }
    }
}
