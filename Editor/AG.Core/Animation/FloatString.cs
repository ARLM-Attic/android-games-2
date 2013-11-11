using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FloatString : Animation
{
    public int Alive = 1 * 30;
    public int AliveCount = 0;
    public bool IsAlive { get; set; }

    public Point2D Pos { get; set; }

    public string Text { get; set; }

    public FloatString()
    {
        IsAlive = true;
    }

    public override void Update()
    {
        if (IsAlive)
        {
            //gdi.DrawText("float string", Pos.X, Pos.Y);

            Pos.Y -= 1.0f;

            AliveCount++;
            if (AliveCount >= Alive)
            {
                IsAlive = false;
            }
        }
    }
}