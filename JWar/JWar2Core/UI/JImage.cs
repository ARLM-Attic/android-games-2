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
    public class JImage : JControl
    {
        public Texture2D Texture2D { get; set; }

        public JImage()
        {
        }

        public JImage(Texture2D texture2D)
        {
            Texture2D = texture2D;
            this.Size = new Vector2(texture2D.Width, texture2D.Height);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Texture2D != null)
            {
                spriteBatch.Draw(Texture2D, Rect, Color.White);
            }
            base.OnDraw(spriteBatch, gameTime);
        }
    }
}
