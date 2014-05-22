using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using JWar2Core.UI;
using JWar2.Net;
using JWar2Net.Client;
using JWar2Net;

namespace JWar2.Scenes
{
    public class HallScene : JScene
    {
        JTextureButton _btnCreateGame;
        JTextureButton _btnJoinGame;
        JTextBlock _ctlPlayerId;

        public HallScene()
        {
            _ctlPlayerId = new JTextBlock();
            _ctlPlayerId.Text = PlayerData.Instance.PlayerId.ToString();
            this.AddObject(_ctlPlayerId);

            _btnCreateGame = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnCreateGame.Position = new Vector2(100, 100);
            _btnCreateGame.Text = "Create";
            _btnCreateGame.Click += new OnClickEventHandler(_btnOK_Click);
            this.AddObject(_btnCreateGame);


            _btnJoinGame = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnJoinGame.Position = new Vector2(100, 300);
            _btnJoinGame.Text = "Join";
            _btnJoinGame.Click += new OnClickEventHandler(_btnJoinGame_Click);
            this.AddObject(_btnJoinGame);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            Request.CreateRoom(JNetClient.Instance, string.Format("{0}'s room", PlayerData.Instance.Name));
        }

        void _btnJoinGame_Click(JButtonBase sender)
        {
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            if (JNetVar.Get(0x01) == 0x01)
            {
                JNetVar.Remove(0x01);
                JCore.Show(new RoomScene());
            }
            return base.OnUpdate(gameTime);
        }
    }
}
