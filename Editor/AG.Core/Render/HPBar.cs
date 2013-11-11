using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HPBar
{
    public static void Render(IEngine engine, Camera camera, Object2D obj, Frame2D currentFrame)
    {
        if (obj.Unit.Category != UnitCategoryDef.Ornamental)
        {
            //#region 显示HP条
            //float sizeScale = (float)item.Unit.Size / MapCell.Width;
            //float curfx1 = camera.ZeroPoint.X + (item.CurrentPoint.X - frameHP.OffsetX * sizeScale) * camera.Zoom;
            //float curfy1 = camera.ZeroPoint.Y + (item.CurrentPoint.Y - frameHP.offsetY * sizeScale) * camera.Zoom;
            //gdi.Draw(frameHP.Texture,
            //    curfx1,
            //    curfy1,
            //    frameHP.Width * sizeScale * item.Unit.Scale,
            //    frameHP.Height * sizeScale * item.Unit.Scale);
            //#endregion

            float actualWidth = obj.Unit.Size * ((float)obj.HP / (float)obj.Unit.MaxHP);
            float deltaW = obj.Unit.Size - actualWidth;
            float curfx1 = camera.ZeroPoint.X + (obj.CurrentPoint.X) * camera.Zoom - obj.Unit.Size / 2;
            float curfy1 = camera.ZeroPoint.Y + (obj.CurrentPoint.Y) * camera.Zoom - currentFrame.offsetY;
            engine.GDI.DrawRectangle(0x000000, curfx1 - 1, curfy1 - 1, obj.Unit.Size + 2, 5);
            engine.GDI.DrawRectangle(0xFF0000, curfx1 + deltaW, curfy1, actualWidth, 3);
        }
    }
}