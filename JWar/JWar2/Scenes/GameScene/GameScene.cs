using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using JWar2Core.UI;

namespace JWar2.Scenes
{
    public class GameScene : JScene
    {
        JImage _image;
        JTextureButton _btnOK;
        GameHudLayer _hudLayer;

        JMap _map;

        JMapRender _mapRender;

        public GameScene()
        {
            _image = new JImage(JResource.Global.Load<Texture2D>("Images\\SelectMode"));
            this.AddObject(_image);

            _btnOK = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnOK.Position = new Vector2(100, 100);
            _btnOK.Text = "Start";
            _btnOK.Click += new OnClickEventHandler(_btnOK_Click);
            this.AddObject(_btnOK);

            _hudLayer = new GameHudLayer();
            this.AddObject(_hudLayer);

            _map = LoadMap0();
            _mapRender = new JMapRender(_map);
            this.AddObject(_mapRender);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            JResource.Global.GetModel(1);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            return base.OnUpdate(gameTime);
        }

        private JMap LoadMap0()
        {
            int m_rowCount = 15;
            int m_colCount = 15;
            JMap map = new JMap(m_rowCount, m_colCount);

            for (int i = 0; i < m_rowCount; i++)
            {
                for (int j = 0; j < m_colCount; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        map.Cells[i * m_colCount + j] = new JMapCell(new JMapPos(i,j), 0);
                    }
                    else
                    {
                        map.Cells[i * m_colCount + j] = new JMapCell(new JMapPos(i, j), 1);
                    }
                }
            }
            return map;
        }
    }
}
