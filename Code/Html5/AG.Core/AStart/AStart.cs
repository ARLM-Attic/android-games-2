//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class AStart
//{
//    Map2D R;

//    //开启列表
//    List<PointAStart> Open_List = new List<PointAStart>();

//    //关闭列表
//    List<PointAStart> Close_List = new List<PointAStart>();

//    //从开启列表查找F值最小的节点
//    private PointAStart GetMinFFromOpenList()
//    {
//        PointAStart Pmin = null;
//        foreach (PointAStart p in Open_List) if (Pmin == null || Pmin.G + Pmin.H > p.G + p.H) Pmin = p;
//        return Pmin;
//    }

//    //判断一个点是不是障碍
//    private bool IsBar(Map2D map, MapPos pos)
//    {
//        MapCell cell = map.GetCell(pos);

//        if (cell.Type != 0)
//        {
//            return true;
//        }

//        if (cell.ObjList.Count > 0)
//        {
//            return true;
//        }
//        return false;
//    }

//    //判断关闭列表是否包含一个坐标的点
//    private bool IsInCloseList(int x, int y)
//    {
//        foreach (PointAStart p in Close_List) if (p.x == x && p.y == y) return true;
//        return false;
//    }
//    //从关闭列表返回对应坐标的点
//    private PointAStart GetPointFromCloseList(int x, int y)
//    {
//        foreach (PointAStart p in Close_List) if (p.x == x && p.y == y) return p;
//        return null;
//    }

//    //判断开启列表是否包含一个坐标的点
//    private bool IsInOpenList(int x, int y)
//    {
//        foreach (PointAStart p in Open_List) if (p.x == x && p.y == y) return true;
//        return false;
//    }
//    //从开启列表返回对应坐标的点
//    private PointAStart GetPointFromOpenList(int x, int y)
//    {
//        foreach (PointAStart p in Open_List) if (p.x == x && p.y == y) return p;
//        return null;
//    }


//    //计算某个点的G值
//    private int GetG(PointAStart p)
//    {
//        if (p.father == null)
//        {
//            return 0;
//        }
//        if (p.x == p.father.x || p.y == p.father.y)
//        {
//            return p.father.G + 10;
//        }
//        else
//        {
//            return p.father.G + 14;
//        }
//    }

//    //计算某个点的H值
//    private int GetH(PointAStart p, PointAStart pb)
//    {
//        return Math.Abs(p.x - pb.x) + Math.Abs(p.y - pb.y);
//    }

//    //检查当前节点附近的节点
//    private void CheckP8(PointAStart p0, Map2D map, PointAStart pa, ref PointAStart pb)
//    {
//        for (int xt = p0.x - 1; xt <= p0.x + 1; xt++)
//        {
//            for (int yt = p0.y - 1; yt <= p0.y + 1; yt++)
//            {
//                //排除超过边界和等于自身的点
//                if ((xt >= 0 && xt < R.Col && yt >= 0 && yt < R.Row)
//                    && !(xt == p0.x && yt == p0.y))
//                {
//                    // 排除跳过2个不能通过的格子，斜穿
//                    //排除障碍点和关闭列表中的点
//                    if ((!IsBar(map,new MapPos(yt, xt)) && CanCross(yt, xt, p0, map))
//                        && !IsInCloseList(xt, yt))
//                    {
//                        if (IsInOpenList(xt, yt))
//                        {
//                            PointAStart pt = GetPointFromOpenList(xt, yt);
//                            int G_new = 0;
//                            if (p0.x == pt.x || p0.y == pt.y) G_new = p0.G + 10;
//                            else G_new = p0.G + 14;
//                            if (G_new < pt.G)
//                            {
//                                Open_List.Remove(pt);
//                                pt.father = p0;
//                                pt.G = G_new;
//                                Open_List.Add(pt);
//                            }
//                        }
//                        else
//                        {
//                            //不在开启列表中
//                            PointAStart pt = new PointAStart();
//                            pt.x = xt;
//                            pt.y = yt;
//                            pt.father = p0;
//                            pt.G = GetG(pt);
//                            pt.H = GetH(pt, pb);
//                            Open_List.Add(pt);
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private bool CanCross(int yt, int xt, PointAStart p0, Map2D map)
//    {
//        if (Math.Abs(yt - p0.y) + Math.Abs(xt - p0.x) == 2)
//        {
//            // 交叉的2个格子都可以通行才能斜着通过
//            if (!IsBar(map,new MapPos(p0.y, xt))
//                && !IsBar(map,new MapPos(yt, p0.x)))
//            {
//                return true;
//            }
//            return false;
//        }
//        else
//        {
//            return true;
//        }
//    }

//    public List<MapPos> FindWay(PointAStart pa, PointAStart pb, Map2D map)
//    {
//        List<MapPos> path = new List<MapPos>();

//        R = map;
//        Open_List.Add(pa);
//        while (!(IsInOpenList(pb.x, pb.y) || Open_List.Count == 0))
//        {
//            PointAStart p0 = GetMinFFromOpenList();
//            if (p0 == null)
//            {
//                return null;
//            }
//            Open_List.Remove(p0);
//            Close_List.Add(p0);
//            CheckP8(p0, R, pa, ref pb);
//        }

//        PointAStart p = GetPointFromOpenList(pb.x, pb.y);
//        if (p == null)
//        {
//            return path;
//        }
//        path.Add(new MapPos(p.y, p.x));
//        while (p.father != null)
//        {
//            path.Insert(0, new MapPos(p.father.y, p.father.x));
//            p = p.father;
//        }
//        return path;
//    }
//}