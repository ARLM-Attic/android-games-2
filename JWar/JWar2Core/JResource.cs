using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace JWar2Core
{
    public class JResource
    {
        private static ContentManager s_contentManager;

        public static void Init(ContentManager content)
        {
            s_contentManager = content;
        }

        private static JResource s_shareResource;

        /// <summary>
        /// 全局起源加载器
        /// </summary>
        public static JResource Global
        {
            get
            {
                if (s_shareResource == null)
                {
                    s_shareResource = new JResource();
                }
                return s_shareResource;
            }
        }

        private JResource()
        {
        }

        public Texture2D GetTexture(string name)
        {
            return null;
        }

        public T Load<T>(string name)
        {
            return s_contentManager.Load<T>(name);
        }

        public JModel GetModel(int id)
        {
            JModel model = s_contentManager.Load<JModel>(string.Format("Models\\{0:d4}", id));
            for (int actIndex = 0; actIndex < model.Actions.Count; actIndex++)
            {
                JAction action = model.Actions[actIndex];
                for (int dirIndex = 0; dirIndex < action.Directions.Count; dirIndex++)
                {
                    JDirection direction = action.Directions[dirIndex];
                    for (int fIndex = 0; fIndex < direction.Frames.Count; fIndex++)
                    {
                        JFrame frame = direction.Frames[fIndex];

                        Texture2D texture = s_contentManager.Load<Texture2D>(string.Format("Models\\{0:d4}\\{0:d4}-{1:d4}-{2:d4}-{3:d4}", id,
                            action.Id,
                            direction.Id,
                            frame.Id));
                        frame.SetTexture(texture);
                    }
                }
            }
            return model;
        }
    }
}
