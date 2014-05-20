using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace JWar2Core
{
    public static class JResource
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

        public static JModel GetModel(int id)
        {
            JModel model = _contentManager.Load<JModel>(string.Format("Models\\{0}", id));
            for (int actIndex = 0; actIndex < model.Actions.Count; actIndex++)
            {
                JAction action = model.Actions[actIndex];
                for (int dirIndex = 0; dirIndex < action.Directions.Count; dirIndex++)
                {
                    JDirection direction = action.Directions[dirIndex];
                    for (int fIndex = 0; fIndex < direction.Frames.Count; fIndex++)
                    {
                        JFrame frame = direction.Frames[fIndex];

                        Texture2D texture = _contentManager.Load<Texture2D>(string.Format("Models\\{0}\\{1}", id, frame.Id));
                        frame.SetTexture(texture);
                    }
                }
            }
            return null;
        }
    }
}
