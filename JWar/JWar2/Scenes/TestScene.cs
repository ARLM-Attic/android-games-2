using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace JWar2.Scenes
{
    public class TestScene : JScene
    {
        JWar2Core.UI.JTextBlock textBlock;
        public TestScene()
            : base()
        {
            textBlock = new JWar2Core.UI.JTextBlock();
            textBlock.Font = JResource.Load<SpriteFont>("SpriteFont1");
            textBlock.Text = "this is a textblock!";
            textBlock.Foreground = new Color(255, 0, 0);
            this._objectList.Add(textBlock);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                textBlock.Text = "Left key down";
                JCore.Show(new HomeScene());
                return true;
            }
            else
            {
                textBlock.Text = "None";
            }

            return base.OnUpdate(gameTime);
        }
    }
}
