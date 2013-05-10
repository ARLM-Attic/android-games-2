using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGShell
{
    public class BigMapPanel : AGControl
    {
        public Model2D Model { get; set; }

        private List<AGControl> _controls;

        public event Action<int> SelectMap;

        public BigMapPanel(Model2D model)
        {
            Model = model;

            _controls = new List<AGControl>();

            List<int> maps = DATUtility.GetMaps();
            for (int mapIndex = 0; mapIndex < maps.Count; mapIndex++)
            {
                AGTextButton button = new AGTextButton(
                    maps[mapIndex].ToString(),
                    new Point2D(MainWindow.Width / 2, 150 + mapIndex * 50),
                    new Size2D(50, 150));
                button.Click += new EventHandler(button_Click);
                _controls.Add(button);
                button.Parent = this;
            }

            Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
            Size = new Size2D(frame.Width, frame.Height);
        }

        void button_Click(object sender, EventArgs e)
        {
            if (SelectMap != null)
            {
                SelectMap(100);
            }
        }

        protected override void OnRender(AGGDI gdi)
        {
            Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
            gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
                0,
                Pos.Y,
                frame.Width,
                frame.Height,
                frame.Width,
                frame.Height);

            for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
            {
                _controls[ctlIndex].Render(gdi);
            }
        }

        public override void OnInputEvent(MouseMessage mouse)
        {
            for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
            {
                if (_controls[ctlIndex].InRect(mouse.X, mouse.Y))
                {
                    _controls[ctlIndex].OnInputEvent(mouse);
                    return;
                }
            }

            Pos.Y += mouse.DeltaY;
            if (Pos.Y < -(Model.GetFrame(0x01, 0x01, 0x01).Height - MainWindow.Height))
            {
                Pos.Y = -(Model.GetFrame(0x01, 0x01, 0x01).Height - MainWindow.Height);
            }
            else if (Pos.Y > 0)
            {
                Pos.Y = 0;
            }
        }
    }
}
