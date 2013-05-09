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
        float deltaX = pt2.X - pt1.X;
        float deltaY = pt2.Y - pt1.Y;

        #region 排除在一条直线上
        if (deltaY == 0)
        {
            if (deltaX > 0)
            {
                return Direction2DDef.East.Id;
            }
            else
            {
                return Direction2DDef.West.Id;
            }
        }

        if (deltaX == 0)
        {
            if (deltaY > 0)
            {
                return Direction2DDef.South.Id;
            }
            else
            {
                return Direction2DDef.North.Id;
            }
        }
        #endregion

        float k = deltaY / deltaX;

        if (k > 2 || k < -2)
        {
            if (deltaY > 0)
            {
                return Direction2DDef.South.Id;
            }
            else
            {
                return Direction2DDef.North.Id;
            }
        }
        else if (k < 2 && k > 0.5f)
        {
            if (deltaY > 0)
            {
                return Direction2DDef.SouthEast.Id;
            }
            else
            {
                return Direction2DDef.NorthWest.Id;
            }
        }
        else if ((k < 0.5f && k > 0) || (k < 0 && k > -0.5f))
        {
            if (deltaX > 0)
            {
                return Direction2DDef.East.Id;
            }
            else
            {
                return Direction2DDef.West.Id;
            }
        }
        else if (k < -0.5f && k > -2)
        {
            if (deltaY > 0)
            {
                return Direction2DDef.SouthWest.Id;
            }
            else
            {
                return Direction2DDef.NorthEast.Id;
            }
        }
        return Direction2DDef.South.Id;
    }

    public static int GetDirection(float deltaX, float deltaY)
    {
        int directionId = 0;
        if (deltaX > 0)
        {
            if (deltaY > 0)
            {
                directionId = Direction2DDef.SouthEast.Id;
            }
            else if (deltaY < 0)
            {
                directionId = Direction2DDef.NorthEast.Id;
            }
            else
            {
                directionId = Direction2DDef.East.Id;
            }
        }
        else if (deltaX < 0)
        {
            if (deltaY > 0)
            {
                directionId = Direction2DDef.SouthWest.Id;
            }
            else if (deltaY < 0)
            {
                directionId = Direction2DDef.NorthWest.Id;
            }
            else
            {
                directionId = Direction2DDef.West.Id;
            }
        }
        else
        {
            if (deltaY > 0)
            {
                directionId = Direction2DDef.South.Id;
            }
            else if (deltaY < 0)
            {
                directionId = Direction2DDef.North.Id;
            }
        }
        return directionId;
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

    public static void GetDeltaXY(int directId, int mSpeed, out float deltaX, out float deltaY)
    {
        if (directId == 1)
        {
            deltaX = 0;
            deltaY = mSpeed;
        }
        else if (directId == 2)
        {
            deltaX = -mSpeed;
            deltaY = mSpeed;
        }
        else if (directId == 3)
        {
            deltaX = -mSpeed;
            deltaY = 0;
        }
        else if (directId == 4)
        {
            deltaX = -mSpeed;
            deltaY = -mSpeed;
        }
        else if (directId == 5)
        {
            deltaX = 0;
            deltaY = -mSpeed;
        }
        else if (directId == 6)
        {
            deltaX = mSpeed;
            deltaY = -mSpeed;
        }
        else if (directId == 7)
        {
            deltaX = mSpeed;
            deltaY = 0;
        }
        else
        {
            deltaX = mSpeed;
            deltaY = mSpeed;
        }
    }
}
