using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using JWar2Core.UI;
using JWar2Net.Client;
using JWar2Net;

namespace JWar2.Scenes
{
    public class ConnectScene : JScene
    {
        JTextBlock _ctlText;
        JTextureButton _btnOK;

        int _connectState = 0;

        public ConnectScene()
        {
            PlayerData.Instance.Name = System.Net.Dns.GetHostName();

            _ctlText = new JTextBlock();
            _ctlText.Text = "Connecting...";
            _ctlText.Position = new Vector2(100, 100);
            this.AddObject(_ctlText);

            _btnOK = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnOK.Position = new Vector2(100, 200);
            _btnOK.Text = "Retry";
            _btnOK.Click += new OnClickEventHandler(_btnOK_Click);
            _btnOK.IsVisible = false;
            this.AddObject(_btnOK);

            JNetClient.Instance.SetResponseHandler(new JWar2ResponseHandler());
            _connectState = JNetClient.Instance.Connect("127.0.0.1", 8599);
        }

        void _btnOK_Click(JButtonBase sender)
        {
            _connectState = JNetClient.Instance.Connect("127.0.0.1", 8599);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            if (JNetVar.Get(0x01) == 0x01)
            {
                JNetVar.Remove(0x01);
                JCore.Show(new HallScene());
            }
            else if (_connectState == 2)
            {
                _ctlText.Text = "Connect Failure!";
                _btnOK.IsVisible = true;
                _connectState = 0;
            }
            else if (_connectState == 1)
            {
                //
                Net.Request.Login(JNetClient.Instance, PlayerData.Instance.Name);
            }
            return base.OnUpdate(gameTime);
        }
    }
}
