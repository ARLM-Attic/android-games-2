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
    public class JLayer : JObject
    {
        public List<JObject> _objectList;

        public JLayer()
        {
            _objectList = new List<JObject>();
        }

        public void AddObject(JObject obj)
        {
            _objectList.Add(obj);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            for (int index = 0; index < _objectList.Count; index++)
            {
                if (_objectList[index].Update(gameTime))
                {
                    return true;
                }
            }
            return base.OnUpdate(gameTime);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int index = 0; index < _objectList.Count; index++)
            {
                _objectList[index].Draw(spriteBatch, gameTime);
            }
        }
    }
}
