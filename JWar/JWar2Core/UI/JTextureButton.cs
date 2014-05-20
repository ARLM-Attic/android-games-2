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
    public class JTextureButton : JButtonBase
    {
        private string _text;

        /// <summary>
        /// 字符串的大小
        /// </summary>
        private Vector2 _textSize;

        public Texture2D NormalTexture { get; set; }
        public Texture2D PressTexture { get; set; }

        /// <summary>
        /// 文字
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                _textSize = Font.MeasureString(_text);
            }
        }

        public JTextureButton(Texture2D normalTexture, Texture2D pressTexture)
        {
            NormalTexture = normalTexture;
            PressTexture = pressTexture;
            this.Size = new Vector2(NormalTexture.Width, NormalTexture.Height);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_isPress)
            {
                if (PressTexture != null)
                {
                    spriteBatch.Draw(PressTexture, Rect, Color.White);
                }
            }
            else
            {
                if (NormalTexture != null)
                {
                    spriteBatch.Draw(NormalTexture, Rect, Color.White);
                }
            }

            if (!String.IsNullOrEmpty(Text) && Font != null)
            {
                spriteBatch.DrawString(this.Font,
                    this.Text,
                    this.Position,
                    this.Foreground,
                    0,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0);

            }
            base.OnDraw(spriteBatch, gameTime);
        }
    }
}
