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
        public static void Render(IEngine engine, IGDI gdi, Map2D map, Camera camera)
        {
            float curWidth = MapCell.Width * camera.Zoom;
            float curHeight = MapCell.Height * camera.Zoom;

            #region 渲染地图背景或者地图地形
            if (map.Background != null)
            {
                Bitmap bgImage = new Bitmap(new MemoryStream(map.Background));
                gdi.DrawImage(bgImage, 0, 0, camera.Width, camera.Height, camera.RectInMap.X, camera.RectInMap.Y, camera.RectInMap.W, camera.RectInMap.H);
            }
            else
            {
                MapPos viewTLPos = camera.GetMapPos(camera.RectInMap.X, camera.RectInMap.Y);
                MapPos viewRBPos = camera.GetMapPos(camera.RectInMap.Right, camera.RectInMap.Bottom);
                int vsr = viewTLPos.Row;
                int vsc = viewTLPos.Col;
                int vr = viewRBPos.Row; // view row
                int vc = viewRBPos.Col; // view col
                for (int row = vsr; row <= vr; row++)
                {
                    for (int col = vsc; col <= vc; col++)
                    {
                        MapCell cell = map.GetCell(new MapPos(row, col));
                        gdi.Draw(cell.Texture2D,
                            camera.ZeroPoint.X + col * curWidth,
                            camera.ZeroPoint.Y + row * curHeight,
                            curWidth,
                            curHeight);
                        #region
                        //if (cell.Value != 0)
                        //{
                        //    gdi.DrawBlock(
                        //        camera.ZeroPoint.X + col * curWidth,
                        //        camera.ZeroPoint.Y + row * curHeight,
                        //        curWidth,
                        //        curHeight);
                        //}
                        //else if (!cell.HasEnuRange(null, 0))
                        //{
                        //    gdi.DrawBlock(
                        //        camera.ZeroPoint.X + col * curWidth,
                        //        camera.ZeroPoint.Y + row * curHeight,
                        //        curWidth,
                        //        curHeight);
                        //}
                        //else
                        //{
                        //    //gdi.DrawRectangle(
                        //    //    camera.ZeroPoint.X + col * curWidth,
                        //    //    camera.ZeroPoint.Y + row * curHeight,
                        //    //    curWidth,
                        //    //    curHeight);
                        //}
                        #endregion
                    }
                }
            }
            #endregion

            #region 渲染各个阵营的目标位置
            for (int campIndex = 0; campIndex < map.Camps.Count; campIndex++)
            {
                Camp camp = map.Camps[campIndex];

                Frame2D frame = DATUtility.GetModel(15).GetFrame(1, 1, 1);
                float curX = camp.TargetPos.Center.X - frame.OffsetX;
                float curY = camp.TargetPos.Center.Y - frame.offsetY;
                Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                gdi.DrawImage(
                    image,
                    curX,
                    curY,
                    frame.Width,
                    frame.Height,
                    frame.Width,
                    frame.Height);
            }
            #endregion

            #region 渲染技能效果
            for (int skillIndex = 0; skillIndex < map.SkillList.Count; skillIndex++)
            {
                Skill skill = map.SkillList[skillIndex];
                if (skill.IsRepare)
                {
                    skill.Render(engine);
                }
            }
            for (int skillIndex = 0; skillIndex < map.SkillList2.Count; skillIndex++)
            {
                Skill skill = map.SkillList2[skillIndex];
                if (skill.IsRepare)
                {
                    skill.Render(engine);
                }
            }
            #endregion

            for (int iObj = 0; iObj < map.Widgets.Count; iObj++)
            {
                Model2D modelHPFrame = DATUtility.GetModel(19);
                Frame2D frameHP = modelHPFrame.GetFrame(1, 1, 1);
                if (frameHP.Texture == null)
                {
                    frameHP.Texture = engine.GDI.CreateTexture(frameHP.Data);
                }

                Object2D item = map.Widgets[iObj];
                Model2D model = item.Unit.Model;
                Frame2D frame = item.Unit.Model.GetFrame(item.ActionId, item.DirectionId, item.FrameIndex);
                if (frame.Texture == null)
                {
                    frame.Texture = engine.GDI.CreateTexture(frame.Data);
                }
                float frameWidth = frame.Width * item.Unit.Scale;
                float frameHeight = frame.Height * item.Unit.Scale;
                float frameOffsetX = frame.OffsetX * item.Unit.Scale;
                float frameOffsetY = frame.offsetY * item.Unit.Scale;
                float curfw = frameWidth * camera.Zoom;
                float curfh = frameHeight * camera.Zoom;
                float curfx = camera.ZeroPoint.X + (item.CurrentPoint.X - frameOffsetX) * camera.Zoom;
                float curfy = camera.ZeroPoint.Y + (item.CurrentPoint.Y - frameOffsetY) * camera.Zoom;

                if (item.Unit.Stirps != UnitStirps.Ornamental)
                {
                    #region 显示HP条
                    float curfx1 = camera.ZeroPoint.X + (item.CurrentPoint.X - frameHP.OffsetX) * camera.Zoom;
                    float curfy1 = camera.ZeroPoint.Y + (item.CurrentPoint.Y - frameHP.offsetY) * camera.Zoom;
                    gdi.Draw(frameHP.Texture,
                        curfx1,
                        curfy1,
                        frameHP.Width * item.Unit.Scale,
                        frameHP.Height * item.Unit.Scale);
                    #endregion
                }

                gdi.Draw(frame.Texture,
                    curfx,
                    curfy,
                    curfw,
                    curfh);

                gdi.DrawShadowText(
                    item.HP.ToString(),
                    (int)camera.ZeroPoint.X + (int)(item.CurrentPoint.X * camera.Zoom) - 20,
                    (int)camera.ZeroPoint.Y + (int)(item.CurrentPoint.Y * camera.Zoom));
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
