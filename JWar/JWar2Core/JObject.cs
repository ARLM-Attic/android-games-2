using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JWar2Core
{
    public class JObject
    {
        public bool IsVisible { get; set; }
        public bool IsEnable { get; set; }
        /// <summary>
        /// 排序索引
        /// </summary>
        public int DrawZIndex { get; set; }

        public JObject()
        {
            IsVisible = true;
            IsEnable = true;
        }

        public bool Update(GameTime gameTime)
        {
            if (IsVisible && IsEnable)
            {
                return OnUpdate(gameTime);
            }
            return false;
        }

        protected virtual bool OnUpdate(GameTime gameTime)
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsVisible)
            {
                OnDraw(spriteBatch, gameTime);
            }
        }

        protected virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}
