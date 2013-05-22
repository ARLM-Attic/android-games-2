using System;
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
            string cmd = context.Request["cmd"];

            if (cmd == "1001")
            {
                int curRow = Convert.ToInt32(context.Request["cr"]);
                int curCol = Convert.ToInt32(context.Request["cc"]);

                int radius = 10;

                MapRange range = ServerData.Instance.Map.GetRange(curRow, curCol, radius);

                StringBuilder cellBuilder = new StringBuilder();
                for (int row = 0; row < range.Row; row++)
                {
                    for (int col = 0; col < range.Col; col++)
                    {
                        cellBuilder.AppendFormat("{0},", range.Cells[row * range.Row + col]);
                    }
                }

                string dataString = string.Format("sr:{0},sc:{1},r:{2},c:{3},cells:'{4}'",
                    range.StartRow,
                    range.StartCol,
                    range.Row,
                    range.Col,
                    cellBuilder);
                context.Response.ContentType = "text/plain";
                context.Response.Write("{state:'ok', data:{" + dataString + "}}");
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("{state:'ok'}");
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