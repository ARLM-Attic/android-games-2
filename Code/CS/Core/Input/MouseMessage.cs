using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MouseMessage
{
    public byte[] Buttons { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int DeltaX { get; set; }
    public int DeltaY { get; set; }
    public int DeltaZ { get; set; }
    public bool IsHandled { get; set; }

    public bool IsLBDown()
    {
        if (Buttons[0] == 0)
        {
            return false;
        }
        return true;
    }

    public MouseMessage()
    {
        IsHandled = false;
    }

    public void Copy(MouseMessage source)
    {
        this.Buttons = source.Buttons;
        this.X = source.X;
        this.Y = source.Y;
        this.DeltaX = source.DeltaX;
        this.DeltaY = source.DeltaY;
        this.DeltaZ = source.DeltaZ;
        this.IsHandled = source.IsHandled;
    }
}
