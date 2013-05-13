using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// NonCollisionDetection move strategy
/// </summary>
public class NCDMoveStrategy : IMoveStrategy
{
    public bool Move(Map2D map, Object2D obj)
    {
        if (obj.TargetPos != null)
        {
            List<MapPos> path = new AStart2(this).FindWay(new PointAStart(obj.SitePos), new PointAStart(obj.TargetPos), map);

            if (path.Count > 0)
            {
                MapPos nextPos = path[0];
                float deltaX = (nextPos.Center.X - obj.CurrentPoint.X);
                float deltaY = (nextPos.Center.Y - obj.CurrentPoint.Y);

                if (path.Count > 1)
                {
                    nextPos = path[1];
                    deltaX = (nextPos.Center.X - obj.CurrentPoint.X);
                    deltaY = (nextPos.Center.Y - obj.CurrentPoint.Y);
                }

                if (deltaX == 0 && deltaY == 0)
                {
                    obj.TargetPos = null;
                }
                else
                {
                    obj.DirectionId = ObjectUtil.GetDirection(deltaX, deltaY);

                    if (deltaX > 0)
                    {
                        if (deltaX > obj.Unit.MSpeed)
                        {
                            deltaX = obj.Unit.MSpeed;
                        }
                        else
                        {
                            deltaX = deltaX;
                        }
                    }
                    else if (deltaX < 0)
                    {
                        if (Math.Abs(deltaX) > obj.Unit.MSpeed)
                        {
                            deltaX = -obj.Unit.MSpeed;
                        }
                        else
                        {
                            deltaX = -Math.Abs(deltaX);
                        }
                    }

                    if (deltaY > 0)
                    {
                        if (deltaY > obj.Unit.MSpeed)
                        {
                            deltaY = obj.Unit.MSpeed;
                        }
                        else
                        {
                            deltaY = deltaY;
                        }
                    }
                    else if (deltaY < 0)
                    {
                        if (Math.Abs(deltaY) > obj.Unit.MSpeed)
                        {
                            deltaY = -obj.Unit.MSpeed;
                        }
                        else
                        {
                            deltaY = -Math.Abs(deltaY);
                        }
                    }

                    //ObjectUtil.GetDeltaXY(deltaX, deltaY, obj.Unit.MSpeed, out deltaX, out deltaY);
                    float nextPointX = obj.CurrentPoint.X + deltaX;
                    float nextPointY = obj.CurrentPoint.Y + deltaY;


                    obj.CurrentPoint.X += deltaX;
                    obj.CurrentPoint.Y += deltaY;

                    int row = (int)obj.CurrentPoint.Y / MapCell.Height;
                    int col = (int)obj.CurrentPoint.X / MapCell.Width;

                    if (obj.SitePos.Row != row || obj.SitePos.Col != col)
                    {
                        MapCell curCell = map.GetCell(obj.SitePos);
                        curCell.ObjList.Remove(obj);
                        obj.SitePos = new MapPos(row, col);
                        MapCell cell = map.GetCell(obj.SitePos);
                        cell.ObjList.Add(obj);
                    }

                    obj.SetAction(ObjState.Move);
                }
            }
        }

        if (obj.TargetPos == null)
        {
            obj.SetAction(ObjState.Stand);
            return false;
        }
        return true;
    }

    public bool IsBar(Map2D map, MapPos pos)
    {
        MapCell cell = map.GetCell(pos);

        if (cell.Value != 0)
        {
            return true;
        }
        return false;
    }
}
