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

    public bool IsLBDown()
    {
        if (Buttons[0] == 0)
        {
            return false;
        }
        return true;
    }
}
