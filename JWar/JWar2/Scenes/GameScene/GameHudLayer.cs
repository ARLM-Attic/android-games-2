using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JWar2Core.UI;

namespace JWar2.Scenes
{
    public class GameHudLayer : JLayer
    {
        JImage _image;
        JTextureButton _btnOK;

        JModelViewer _modelViewer;

        public GameHudLayer()
        {
            _image = new JImage(JResource.Global.Load<Texture2D>("Images\\SelectMode"));
            this.AddObject(_image);

            _btnOK = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnOK.Position = new Vector2(100, 100);
            _btnOK.Text = "Start";
            _btnOK.Click += new OnClickEventHandler(_btnOK_Click);
            this.AddObject(_btnOK);

            _modelViewer = new JModelViewer(JResource.Global.GetModel(1));
            _modelViewer.Position = new Vector2(200, 300);
            this.AddObject(_modelViewer);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            JResource.Global.GetModel(1);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.OnDraw(spriteBatch, gameTime);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            return base.OnUpdate(gameTime);
        }
    }
}
