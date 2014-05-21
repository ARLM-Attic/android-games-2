using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JWar2Core.UI
{
    public class JModelViewer : JControl
    {
        /// <summary>
        /// 显示的模型
        /// </summary>
        public JModel Model { get; set; }

        public int CurrentActionId { get; set; }
        public int CurrentDirectionId { get; set; }
        public int CurrentFrameId { get; set; }

        public JModelViewer(JModel model)
        {
            Model = model;
            CurrentActionId = 1;
            CurrentDirectionId = 1;
            CurrentFrameId = 1;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //if (Texture2D != null)
            //{
            //    spriteBatch.Draw(Texture2D, Rect, Color.White);
            //}
            JFrame frame = Model.GetFrame(CurrentActionId, CurrentDirectionId, CurrentFrameId);

            Rectangle rect = new Rectangle((int)this.Position.X - frame.OffsetX,
                (int)this.Position.Y - frame.OffsetY,
                frame.Width,
                frame.Height);
            spriteBatch.Draw(frame.GetTexture(), rect, Color.White);

            base.OnDraw(spriteBatch, gameTime);
        }
    }
}
