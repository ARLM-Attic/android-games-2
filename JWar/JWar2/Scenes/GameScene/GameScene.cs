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
        JSprite _sprite;

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

            _sprite = new JSprite(JResource.Global.GetModel(1));
            _sprite.Pos = new JMapPos(1, 1);
            _map.Widgets.Add(_sprite);

            for (int index = 0; index < 5; index++)
            {
                JSprite _box1 = new JSprite(JResource.Global.GetModel(8001));
                _box1.Pos = new JMapPos(1, 1);
                _box1.PositionInMap = new Vector2(40 + 40 * index, 160);
                _map.Widgets.Add(_box1);
            }

            this.AddObject(_mapRender);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            JResource.Global.GetModel(1);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _sprite.Move(-5, 0);
            }
            else if(keyboardState.IsKeyDown(Keys.Right))
            {
                _sprite.Move(5,0);
            }
            else if(keyboardState.IsKeyDown(Keys.Up))
            {
                _sprite.Move(0,-5);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _sprite.Move(0, 5);
            }

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
