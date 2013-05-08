using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ObjectUtil
{
    public static bool CheckAttackDistance(Object2D obj1, Object2D obj2)
    {
        float distance = CalcDistance(obj1.CurrentPoint,obj2.CurrentPoint);
        if (distance <= obj1.Unit.Size + obj1.Unit.ADDistance + obj2.Unit.Size)
        {
            return true;
        }
        return false;
    }

    public static float CalcDistance(Point2D pt1, Point2D pt2)
    {
        double x = pt1.X - pt2.X;
        double y = pt1.Y - pt2.Y;
        return (float)Math.Sqrt(x * x + y * y);
    }

    public static int GetDirection(Point2D pt1, Point2D pt2)
    {
        if (pt1.X < pt2.X)
        {
            if (pt1.Y < pt2.Y)
            {
                return Direction2DDef.SouthEast.Id;
            }
            else if (pt1.Y > pt2.Y)
            {
                return Direction2DDef.NorthEast.Id;
            }
            else
            {
                return Direction2DDef.East.Id;
            }
        }
        else if (pt1.X > pt2.X)
        {
            if (pt1.Y < pt2.Y)
            {
                return Direction2DDef.SouthWest.Id;
            }
            else if (pt1.Y > pt2.Y)
            {
                return Direction2DDef.NorthWest.Id;
            }
            else
            {
                return Direction2DDef.West.Id;
            }
        }
        else
        {
            if (pt1.Y < pt2.Y)
            {
                return Direction2DDef.North.Id;
            }
            else
            {
                return Direction2DDef.South.Id;
            }
        }
    }

    public static void GetDeltaXY(float deltaX, float deltaY, int r, out float x, out float y)
    {
        double c = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        if (c > r)
        {
            x = (float)(deltaX * r / c);
            y = (float)(deltaY * r / c);
        }
        else
        {
            x = deltaX;
            y = deltaY;
        }
    }
}
