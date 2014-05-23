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
    public class JListBox : JControl
    {
        public List<JListBoxItem> Items { get; set; }

        public event EventHandler SelectChanged;

        public JListBoxItem SelectedItem { get; set; }

        public JListBox()
        {
            Items = new List<JListBoxItem>();
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int index = 0; index < Items.Count; index++)
                {
                    Vector2 itemPos = new Vector2(this.Position.X, this.Position.Y + index * 20);
                    if (itemPos.X <= mouseState.X && itemPos.Y <= mouseState.Y
                        && mouseState.X <= itemPos.X + Size.X
                        && mouseState.Y <= itemPos.Y + Size.Y)
                    {
                        SelectedItem = Items[index];
                        if (SelectChanged != null)
                        {
                            SelectChanged(this, null);
                        }
                        return true;
                    }
                }
            }

            return base.OnUpdate(gameTime);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for(int index = 0; index < Items.Count; index++)
            {
                spriteBatch.DrawString(this.Font,
                    Items[index].Text,
                    new Vector2(this.Position.X, this.Position.Y + index * 20),
                    this.Foreground,
                    0,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
