using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace AGWebHost.Actions
{
    /// <summary>
    /// Summary description for GetImage
    /// </summary>
    public class GetImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string imageFile = context.Request["file"];
            string bmpFile = context.Server.MapPath(string.Format("~/{0}.bmp", imageFile));
            string pngFile = context.Server.MapPath(string.Format("~/{0}.png", imageFile));

            if (!System.IO.File.Exists(pngFile))
            {
                Bitmap image = new Bitmap(bmpFile);
                Bitmap destImage = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                for (int wIndex = 0; wIndex < image.Width; wIndex++)
                {
                    for (int hIndex = 0; hIndex < image.Height; hIndex++)
                    {
                        Color color = image.GetPixel(wIndex, hIndex);

                        if (color.R == 0x00 && color.G == 0x00 && color.B == 0x00)
                        {
                            destImage.SetPixel(wIndex, hIndex, Color.FromArgb(0, 255, 255, 255));
                        }
                        else
                        {
                            destImage.SetPixel(wIndex, hIndex, color);
                        }
                    }
                }

                destImage.Save(pngFile, System.Drawing.Imaging.ImageFormat.Png);

                context.Response.ContentType = "image/png";
                context.Response.WriteFile(pngFile);
            }
            else
            {
                context.Response.ContentType = "image/png";
                context.Response.WriteFile(pngFile);
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