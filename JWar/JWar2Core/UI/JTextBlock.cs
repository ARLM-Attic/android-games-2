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
    public class JTextBlock : JControl
    {
        /// <summary>
        /// ÎÄ±¾ÄÚÈÝ
        /// </summary>
        public string Text { get; set; }

        public JTextBlock()
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!String.IsNullOrEmpty(Text) && Font != null)
            {
                spriteBatch.DrawString(this.Font,
                    this.Text,
                    this.Position,
                    this.Foreground,
                    0,
                    Position,
                    1,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
