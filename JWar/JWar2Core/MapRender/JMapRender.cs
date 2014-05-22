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

namespace JWar2Core
{
    public class JMapRender : JObject
    {
        private JMap _map;
        Texture2D _tex;
        public JMapRender(JMap map)
        {
            _map = map;
            _tex = JResource.Global.Load<Texture2D>("Grounds\\ground1");
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int row = 0; row < _map.Row; row++)
            {
                for (int col = 0; col < _map.Col; col++)
                {
                    Rectangle rect = new Rectangle(col * 40, row * 40, 40, 40);
                    spriteBatch.Draw(_tex, rect, Color.White);
                }
            }

            for (int index = 0; index < _map.Widgets.Count; index++)
            {
                JSprite sprite = _map.Widgets[index];
                sprite.Position = sprite.PositionInMap;
                sprite.Draw(spriteBatch, gameTime);
            }

            base.OnDraw(spriteBatch, gameTime);
        }
    }
}
