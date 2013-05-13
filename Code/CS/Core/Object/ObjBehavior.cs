//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//class ObjBehavior
//{
//    public static bool Move(Object2D obj, Map2D map)
//    {
//        if (obj.TargetPos != null)
//        {
//            List<MapPos> path = new AStart().FindWay(new PointAStart(obj.SitePos), new PointAStart(obj.TargetPos), map);

//            if (path.Count > 0)
//            {
//                MapPos nextPos = path[0];
//                float deltaX = (nextPos.Center.X - obj.CurrentPoint.X);
//                float deltaY = (nextPos.Center.Y - obj.CurrentPoint.Y);

//                if (path.Count > 1)
//                {
//                    nextPos = path[1];
//                    deltaX = (nextPos.Center.X - obj.CurrentPoint.X);
//                    deltaY = (nextPos.Center.Y - obj.CurrentPoint.Y);
//                }

//                if (deltaX == 0 && deltaY == 0)
//                {
//                    obj.TargetPos = null;
//                }
//                else
//                {
//                    obj.DirectionId = ObjectUtil.GetDirection(deltaX, deltaY);

//                    if (deltaX > 0)
//                    {
//                        if (deltaX > obj.Unit.MSpeed)
//                        {
//                            deltaX = obj.Unit.MSpeed;
//                        }
//                        else
//                        {
//                            deltaX = deltaX;
//                        }
//                    }
//                    else if (deltaX < 0)
//                    {
//                        if (Math.Abs(deltaX) > obj.Unit.MSpeed)
//                        {
//                            deltaX = -obj.Unit.MSpeed;
//                        }
//                        else
//                        {
//                            deltaX = -Math.Abs(deltaX);
//                        }
//                    }

//                    if (deltaY > 0)
//                    {
//                        if (deltaY > obj.Unit.MSpeed)
//                        {
//                            deltaY = obj.Unit.MSpeed;
//                        }
//                        else
//                        {
//                            deltaY = deltaY;
//                        }
//                    }
//                    else if (deltaY < 0)
//                    {
//                        if (Math.Abs(deltaY) > obj.Unit.MSpeed)
//                        {
//                            deltaY = -obj.Unit.MSpeed;
//                        }
//                        else
//                        {
//                            deltaY = -Math.Abs(deltaY);
//                        }
//                    }

//                    //ObjectUtil.GetDeltaXY(deltaX, deltaY, obj.Unit.MSpeed, out deltaX, out deltaY);
//                    float nextPointX = obj.CurrentPoint.X + deltaX;
//                    float nextPointY = obj.CurrentPoint.Y + deltaY;
                    
//                    //
//                    bool isCanMove = true;
//                    for (int iUnit = 0; iUnit < map.Widgets.Count; iUnit++)
//                    {
//                        Object2D objInstance = map.Widgets[iUnit];
//                        if (objInstance.ID != obj.ID && !objInstance.IsDead())// objInstance.Unit.Model.Id != 1056)
//                        {
//                            if (ObjectUtil.CalcDistance(new Point2D(nextPointX, nextPointY), objInstance.CurrentPoint) <= obj.Unit.Size + objInstance.Unit.Size)
//                            {
//                                isCanMove = CheckNear(map, obj, out deltaX, out deltaY);
//                                break;
//                            }
//                        }
//                    }


//                    if (isCanMove)
//                    {
//                        obj.CurrentPoint.X += deltaX;
//                        obj.CurrentPoint.Y += deltaY;

//                        int row = (int)obj.CurrentPoint.Y / MapCell.Height;
//                        int col = (int)obj.CurrentPoint.X / MapCell.Width;

//                        if (obj.SitePos.Row != row || obj.SitePos.Col != col)
//                        {
//                            MapCell curCell = map.GetCell(obj.SitePos);
//                            curCell.ObjList.Remove(obj);
//                            obj.SitePos = new MapPos(row, col);
//                            MapCell cell = map.GetCell(obj.SitePos);
//                            cell.ObjList.Add(obj);
//                        }

//                        obj.SetAction(ObjState.Move);
//                    }
//                    else
//                    {
//                        obj.SetAction(ObjState.Stand);
//                    }
//                }
//            }
//        }

//        if (obj.TargetPos == null)
//        {
//            obj.SetAction(ObjState.Stand);
//            return false;
//        }
//        return true;
//    }

//    public static bool Attack(Object2D obj)
//    {
//        foreach (var camp in obj.Map.Camps)
//        {
//            if (camp.Id != obj.Camp.Id)
//            {
//                foreach (var opponentObj in camp.ObjList)
//                {
//                    if (ObjectUtil.CheckAttackDistance(obj, opponentObj))
//                    {
//                        obj.ActionId = Action2DDef.Attack.Id;
//                        return true;
//                    }
//                }
//            }
//        }
//        return false;
//    }

//    private static bool CheckNear(Map2D map, Object2D obj, out float  deltaX, out float deltaY)
//    {
//        bool bcm = true;
//        float nextPointX = 0.0f;
//        float nextPointY = 0.0f;
//        deltaX = 0.0f;
//        deltaY = 0.0f;
//        for (int dirIndex = -2; dirIndex <= 2; dirIndex++)
//        {
//            bcm = true;
//            //System.Diagnostics.Debug.WriteLine(string.Format("check {0}", dirIndex));
//            int dirId = Direction2D.Right(obj.DirectionId, dirIndex);
//            ObjectUtil.GetDeltaXY(dirId, obj.Unit.MSpeed, out deltaX, out deltaY);
//            nextPointX = obj.CurrentPoint.X + deltaX;
//            nextPointY = obj.CurrentPoint.Y + deltaY;

//            for (int iUnit = 0; iUnit < map.Widgets.Count; iUnit++)
//            {
//                Object2D objInstance = map.Widgets[iUnit];
//                if (objInstance.ID != obj.ID)
//                {
//                    MapCell cell = map.GetCell(new MapPos((int)(nextPointY / MapCell.Height), (int)(nextPointX / MapCell.Width)));

//                    if (cell != null && cell.Value == 0
//                        && ObjectUtil.CalcDistance(new Point2D(nextPointX, nextPointY), objInstance.CurrentPoint) <= obj.Unit.Size + objInstance.Unit.Size)
//                    {
//                        bcm = false;
//                        //System.Diagnostics.Debug.WriteLine(string.Format("{0} can not pass", dirIndex));
//                        break;
//                    }
//                }
//            }

//            if (bcm)
//            {
//                obj.DirectionId = ObjectUtil.GetDirection(obj.CurrentPoint, new Point2D(nextPointX, nextPointY));

//                //System.Diagnostics.Debug.WriteLine(string.Format("{0} can pass", dirIndex));
//                return bcm;
//            }
//        }
//        return bcm;
//    }
//}