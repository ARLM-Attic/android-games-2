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
    public class RoomScene : JScene
    {
        JImage _image;
        JTextureButton _btnStart;
        JTextureButton _btnJoinGame;

        JTextBlock _ctlRoomId;

        public RoomScene()
        {
            _image = new JImage(JResource.Global.Load<Texture2D>("Images\\Room"));
            this.AddObject(_image);

            _ctlRoomId = new JTextBlock();
            _ctlRoomId.Text = string.Format("player:{0}, room:{1}",
                PlayerData.Instance.PlayerId,
                PlayerData.Instance.RoomId);
            this.AddObject(_ctlRoomId);

            _btnStart = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnStart.Position = new Vector2(100, 100);
            _btnStart.Text = "Start";
            _btnStart.Click += new OnClickEventHandler(_btnStart_Click);
            this.AddObject(_btnStart);


            _btnJoinGame = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnJoinGame.Position = new Vector2(100, 300);
            _btnJoinGame.Text = "Exit";
            _btnJoinGame.Click += new OnClickEventHandler(_btnOK_Click);
            this.AddObject(_btnJoinGame);
        }

        void _btnStart_Click(JButtonBase sender)
        {
            JCore.Show(new GameScene());
        }

        void _btnOK_Click(JButtonBase sender)
        {
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            return base.OnUpdate(gameTime);
        }
    }
}
