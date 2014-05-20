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
    public class HomeScene : JScene
    {
        JImage _image;
        JTextureButton _btnOK;

        public HomeScene()
        {
            _image = new JImage(JResource.Load<Texture2D>("Images\\SelectMode"));
            this.AddObject(_image);

            _btnOK = new JTextureButton(JResource.Load<Texture2D>("UI\\TextButton"),
                JResource.Load<Texture2D>("UI\\TextButton_U"));
            _btnOK.Position = new Vector2(100, 100);
            _btnOK.Text = "Start";
            _btnOK.Click += new OnClickEventHandler(_btnOK_Click);
            this.AddObject(_btnOK);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            JResource.GetModel(1);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            return base.OnUpdate(gameTime);
        }
    }
}
