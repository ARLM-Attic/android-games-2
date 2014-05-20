using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace JWar2Core
{
    public static class ResourceManager
    {
        private static ContentManager _contentManager;

        public static void Init(ContentManager content)
        {
            _contentManager = content;
        }

        public static Texture2D GetTexture(string name)
        {
            return null;
        }

        public static T Load<T>(string name)
        {
            return _contentManager.Load<T>(name);
        }
    }
}
