﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGWeb;
using System.Text;

namespace AGWebHost.AGI
{
    /// <summary>
    /// Summary description for Action
    /// </summary>
    public class Action : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string cmd = context.Request.Form["cmd"];

            if (cmd == "1002")
            {
                int mapId = Convert.ToInt32(context.Request.Form["map"]);
                int curRow = Convert.ToInt32(context.Request.Form["cr"]);
                int curCol = Convert.ToInt32(context.Request.Form["cc"]);

                Map2D map = DATUtility.GetMap(mapId);
                if (map == null)
                {
                    context.Response.Write("{result:1,cmd:" + cmd + "}");
                    return;
                }

                int radius = 20;

                MapRange range = Map.GetRange(map, curRow, curCol, radius);

                StringBuilder cellBuilder = new StringBuilder();
                for (int row = 0; row < range.Row; row++)
                {
                    for (int col = 0; col < range.Col; col++)
                    {
                        cellBuilder.AppendFormat("{0},", range.Cells[row * range.Row + col]);
                    }
                }

                StringBuilder objBuilder = new StringBuilder();
                for (int index = 0; index < range.Objs.Count; index++)
                {
                    Object2D obj = range.Objs[index];
                    if (index > 0)
                    {
                        objBuilder.Append(",{");
                    }
                    else
                    {
                        objBuilder.Append("{");
                    }
                    objBuilder.AppendFormat("id:{0},model:{1},caption:'{2}',pr:{3},pc:{4},px:{5},py:{6}",
                        obj.ID,
                        obj.Unit.Model.Id,
                        obj.Caption,
                        obj.SitePos.Row,
                        obj.SitePos.Col,
                        obj.CurrentPoint.X,
                        obj.CurrentPoint.Y);
                    objBuilder.Append("}");
                }

                string dataString = string.Format("sr:{0},sc:{1},r:{2},c:{3},cells:'{4}',objs:[{5}]",
                    range.StartRow,
                    range.StartCol,
                    range.Row,
                    range.Col,
                    cellBuilder,
                    objBuilder);
                context.Response.Write("{result:0,cmd:" + cmd + ", data:{" + dataString + "}}");
            }
            else if (cmd == "1000")
            {
                // 登陆之后获取信息
                int mapId = Convert.ToInt32(context.Request.Form["map"]);
                Map2D map = DATUtility.GetMap(mapId);
                int row = new Random().Next(0, map.Row);
                int col = new Random().Next(0, map.Col);

                Object2D obj = AGSUtility.CreateObject(map, map.Camps[0], DATUtility.GetUnit(101), "角色", new MapPos(row, col), 1);
                string dataString = string.Format("id:{0},pr:{1},pc:{2},px:{3},py:{4}",
                    obj.ID,
                    obj.SitePos.Row,
                    obj.SitePos.Col,
                    obj.CurrentPoint.X,
                    obj.CurrentPoint.Y);
                context.Response.Write("{result:0,cmd:" + cmd + ",data:{" + dataString + "}}");

            }
            else if (cmd == "1003")
            {
                int modelId = Convert.ToInt32(context.Request["model"]);
                // 获取模型信息
                Model2D model = DATUtility.GetModel(modelId);
            }
            else if (cmd == "")
            {
                // cr,cc,tr,tc
                int id = Convert.ToInt32(context.Request.Form["id"]);
                int act = Convert.ToInt32(context.Request.Form["act"]);
                int cr = Convert.ToInt32(context.Request.Form["cr"]);
                int cc = Convert.ToInt32(context.Request.Form["cc"]);
                int tr = Convert.ToInt32(context.Request.Form["tr"]);
                int tc = Convert.ToInt32(context.Request.Form["tc"]);

            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("{result:0,cmd:" + cmd + "}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}