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
    public delegate void OnClickEventHandler(JButtonBase sender);

    public class JButtonBase : JControl
    {
        public Texture2D Texture2D { get; set; }

        public event OnClickEventHandler Click;

        protected bool _isPress = false;

        public JButtonBase()
        {
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (IsHitTest(mouseState.X, mouseState.Y))
                {
                    _isPress = true;
                    return true;
                }
            }
            else if (_isPress)
            {
                if (IsHitTest(mouseState.X, mouseState.Y))
                {
                    _isPress = false;
                    if (Click != null)
                    {
                        Click(this);
                    }
                    return true;
                }
                else
                {
                    _isPress = false;
                }
            }
            return base.OnUpdate(gameTime);
        }
    }
}
