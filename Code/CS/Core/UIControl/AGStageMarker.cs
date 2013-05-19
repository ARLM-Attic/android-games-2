using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

public class AGStageMarker : AGButtonBase
{
    public int MapId { get; private set; }
    public string Text { get; private set; }
    public Model2D Model { get; private set; }
    private int _frameIndex = 1;

    private Point _mapPos { get; set; }

    public MapInfo Stage { get; private set; }

    public AGStageMarker(MapInfo mapInfo, Point2D pt)
    {
        Stage = mapInfo;
        MapId = mapInfo.Id;
        Text = mapInfo.Caption;
        Model = DATUtility.GetModel(14);
        Pos = new Point2D(pt.X, pt.Y);
        Size = new Size2D(Model.GetFrame(1, 1, 1).Width,
            Model.GetFrame(1, 1, 1).Height);

        Frame2D frame = Model.GetFrame(0x01, 0x01, _frameIndex);
        Pos.X = Pos.X - frame.OffsetX;
        Pos.Y = Pos.Y - frame.offsetY;

        _mapPos = new Point((int)pt.X, (int)pt.Y);
    }

    public void SetMapPos(Point2D pt)
    {
        Frame2D frame = Model.GetFrame(0x01, 0x01, _frameIndex);
        Pos = new Point2D(pt.X, pt.Y);
        Pos.X = Pos.X - frame.OffsetX;
        Pos.Y = Pos.Y - frame.offsetY;

        _mapPos = new Point((int)pt.X, (int)pt.Y);
    }

    protected override void OnRender(IGDI gdi)
    {
        Frame2D frame = Model.GetFrame(0x01, 0x01, _frameIndex);
        float curX = ClientPos.X;
        float curY = ClientPos.Y;
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            curX,
            curY,
            Size.W,
            Size.H,
            frame.Width,
            frame.Height);

        Rectangle rect = new Rectangle(
            (int)ClientPos.X + frame.OffsetX - 100,
            (int)ClientPos.Y + frame.offsetY,
            200,
            20);

        gdi.DrawShadowText(
            AGRES.GetNormalUIFont(),
            0xffff00,
            Text,
            rect);
    }
}
