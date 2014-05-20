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
    public class TestScene : Scene
    {
        JWar2Core.UI.TextBlock textBlock;
        public TestScene()
            : base()
        {
            textBlock = new JWar2Core.UI.TextBlock();
            textBlock.Font = ResourceManager.Load<SpriteFont>("SpriteFont1");
            textBlock.Text = "this is a textblock!";
            textBlock.Color = new Color(255, 0, 0);
            this._objectList.Add(textBlock);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                textBlock.Text = "Left key down";
            }
            else
            {
                textBlock.Text = "None";
            }

            base.OnUpdate(gameTime);
        }
    }
}
