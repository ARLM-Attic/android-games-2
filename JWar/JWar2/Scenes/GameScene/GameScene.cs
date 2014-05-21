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
        }

        void _btnOK_Click(JButtonBase sender)
        {
            JResource.Global.GetModel(1);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            return base.OnUpdate(gameTime);
        }
    }
}
