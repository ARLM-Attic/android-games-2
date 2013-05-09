using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public static class MapRender
    {
        public static void Render(IEngine engine, AGGDI gdi, Map2D map, Camera camera)
        {
            float curWidth = MapCell.Width * camera.Zoom;
            float curHeight = MapCell.Height * camera.Zoom;

            if (map.Background != null)
            {
                Bitmap bgImage = new Bitmap(new MemoryStream(map.Background));
                gdi.DrawImage(bgImage, 0, 0, camera.Width, camera.Height, camera.ViewRect.X, camera.ViewRect.Y, camera.ViewRect.W, camera.ViewRect.H);
            }
#if DEBUG
            for (int row = 0; row < map.Row; row++)
            {
                for (int col = 0; col < map.Col; col++)
                {
                    MapCell cell = map.GetCell(new MapPos(row, col));
                    if (cell.Value != 0)
                    {
                        gdi.DrawBlock(
                            camera.ZeroPoint.X + col * curWidth,
                            camera.ZeroPoint.Y + row * curHeight,
                            curWidth,
                            curHeight);
                    }
                    else if (!cell.HasEnuRange(null, 0))
                    {
                        gdi.DrawBlock(
                            camera.ZeroPoint.X + col * curWidth,
                            camera.ZeroPoint.Y + row * curHeight,
                            curWidth,
                            curHeight);
                    }
                    else
                    {
                        gdi.DrawRectangle(
                            camera.ZeroPoint.X + col * curWidth,
                            camera.ZeroPoint.Y + row * curHeight,
                            curWidth,
                            curHeight);
                    }
                }
            }
#endif

            for (int iObj = 0; iObj < map.Widgets.Count; iObj++)
            {
                Object2D item = map.Widgets[iObj];
                item.Update(engine);
            }

            for (int iObj = 0; iObj < map.Widgets.Count;)
            {
                Object2D item = map.Widgets[iObj];
                if (item.DeadTime > 10)
                {
                    AGSUtility.RemoveObject(item);
                }
                else
                {
                    iObj++;
                }
            }

            // 排序
            for (int iObj = 0; iObj < map.Widgets.Count - 1; iObj++)
            {
                Object2D nextObj = map.Widgets[iObj + 1];
                Object2D item = map.Widgets[iObj];

                if (item.CurrentPoint.Y > nextObj.CurrentPoint.Y)
                {
                    map.Widgets.Remove(nextObj);
                    map.Widgets.Insert(iObj, nextObj);
                }
            }

            for (int iObj = 0; iObj < map.Widgets.Count; iObj++)
            {
                Object2D item = map.Widgets[iObj];
                Model2D model = item.Unit.Model;
                Frame2D frame = item.Unit.Model.GetFrame(item.ActionId, item.DirectionId, item.FrameIndex);
                float frameWidth = frame.Width * item.Unit.Scale;
                float frameHeight = frame.Height * item.Unit.Scale;
                float frameOffsetX = frame.OffsetX * item.Unit.Scale;
                float frameOffsetY = frame.offsetY * item.Unit.Scale;
                Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                float curfw = frameWidth * camera.Zoom;
                float curfh = frameHeight * camera.Zoom;
                float curfx = camera.ZeroPoint.X + (item.CurrentPoint.X - frameOffsetX) * camera.Zoom;
                float curfy = camera.ZeroPoint.Y + (item.CurrentPoint.Y - frameOffsetY) * camera.Zoom;

                gdi.DrawImage(
                    image,
                    curfx,
                    curfy,
                    curfw,
                    curfh,
                    frame.Width,
                    frame.Height);
                gdi.DrawShadowText(
                    item.HP.ToString(),
                    (int)camera.ZeroPoint.X + (int)(item.CurrentPoint.X * camera.Zoom) - 20,
                    (int)camera.ZeroPoint.Y + (int)(item.CurrentPoint.Y * camera.Zoom));

                //gdi.DrawEllipse(
                //    (int)camera.ZeroPoint.X + item.CurrentPoint.X - item.Unit.Size,
                //    (int)camera.ZeroPoint.Y + item.CurrentPoint.Y - item.Unit.Size, 
                //    item.Unit.Size * 2, item.Unit.Size * 2);
            }

            for (int iAnimation = 0; iAnimation < map.AnimationList.Count; iAnimation++)
            {
                map.AnimationList[iAnimation].Update();
                if ((map.AnimationList[iAnimation] as FloatString).IsAlive)
                {
                    gdi.DrawShadowText((map.AnimationList[iAnimation] as FloatString).Text,
                        camera.ZeroPoint.X + (map.AnimationList[iAnimation] as FloatString).Pos.X,
                        camera.ZeroPoint.Y + (map.AnimationList[iAnimation] as FloatString).Pos.Y);
                }
            }
        }
    }
}
