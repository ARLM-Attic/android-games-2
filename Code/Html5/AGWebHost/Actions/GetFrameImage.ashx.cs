using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGWebHost.Actions
{
    /// <summary>
    /// Summary description for GetFrameImage
    /// </summary>
    public class GetFrameImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int modelId = Convert.ToInt32(context.Request.QueryString["m"]);
            int actionId = Convert.ToInt32(context.Request.QueryString["a"]);
            int directionId = Convert.ToInt32(context.Request.QueryString["d"]);
            int frameId = Convert.ToInt32(context.Request.QueryString["f"]);

            Model2D model = DATUtility.GetModel(modelId);

            Frame2D frame = model.GetFrame(actionId, directionId, frameId);

            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(frame.Data);
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