using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class AGTextButton : AGControl
{
    public event EventHandler Click;

    private bool _isPreClick = false;

    public string Text { get; private set; }
    public Model2D Model { get; private set; }
    private int _frameIndex = 1;

    private float _offsetX;
    private float _offsetY;

    public AGTextButton(string text, Point2D pt, Size2D size)
    {
        Text = text;
        Pos = pt;
        Size = size;

        Model = DATUtility.GetModel(11);
        Size.W = Model.GetFrame(1, 1, 1).Width;
        Size.H = Model.GetFrame(1, 1, 1).Height;

    }

    protected override void OnRender(IGDI gdi)
    {
        Frame2D frame = Model.GetFrame(0x01, 0x01, _frameIndex);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            ClientPos.X,
            ClientPos.Y,
            Size.W,
            Size.H,
            frame.Width,
            frame.Height);

        Rectangle rect = new Rectangle((int)ClientPos.X, (int)ClientPos.Y, (int)Size.W, (int)Size.H);
        gdi.DrawText(
            AGRES.GetNormalUIFont(),
            0x222222,
            Text,
            rect);
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        if (mouse.IsLBDown())
        {
            _isPreClick = true;
            return true;
        }
        else
        {
            if (_isPreClick)
            {
                _isPreClick = false;
                if (Click != null)
                {
                    Click(this, null);
                }
            }

            _frameIndex = (_frameIndex + 1) % 3;
            _frameIndex++;
        }
        return false;
    }
}