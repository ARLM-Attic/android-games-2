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
    public class Scene : JObject2D
    {
        public List<JObject2D> _objectList;

        public Scene()
        {
            _objectList = new List<JObject2D>();
        }

        public void AddObject(JObject2D obj)
        {
            _objectList.Add(obj);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            for (int index = 0; index < _objectList.Count; index++)
            {
                _objectList[index].OnUpdate(gameTime);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int index = 0; index < _objectList.Count; index++)
            {
                _objectList[index].OnDraw(spriteBatch, gameTime);
            }
        }
    }
}
