using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

public class MapCoordinate
{
    public static MapPos MapPtToPos(Point2D pt)
    {
        int col = (int)(pt.X / MapCell.Width - pt.Y / MapCell.Height);
        var row = (int)(pt.X / MapCell.Width + pt.Y / MapCell.Height);
        return new MapPos(row, col);
    }

    public static Point2D PosToMapPt(MapPos pos)
    {
        Point2D pt = new Point2D(0, 0);
        pt.X = (float)(pos.Row + pos.Col) * ((float)MapCell.Width / 2);
        pt.Y = (float)(pos.Row - pos.Col) * ((float)MapCell.Height / 2);
        return pt;
    }

    public static SizeF CalculateSize(int row, int col)
    {
        SizeF size = new SizeF();
        size.Width = (((float)(row + col)) / 2 * MapCell.Width);
        size.Height = (((float)(row + col)) / 2 * MapCell.Height);
        return size;
    }
}