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
using JWar2.Net.Data;

namespace JWar2.Scenes
{
    public class HallScene : JScene
    {
        JTextureButton _btnCreateGame;
        JTextureButton _btnJoinGame;
        JTextureButton _btnRefresh;
        JTextBlock _ctlPlayerId;
        JTextBlock _ctlSelectedRoom;

        JListBox _ctlListBox;

        public HallScene()
        {
            _ctlPlayerId = new JTextBlock();
            _ctlPlayerId.Text = PlayerData.Instance.PlayerId.ToString();
            this.AddObject(_ctlPlayerId);

            _ctlSelectedRoom = new JTextBlock();
            _ctlSelectedRoom.Position = new Vector2(0, 200);
            this.AddObject(_ctlSelectedRoom);

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

            _btnRefresh = new JTextureButton(JResource.Global.Load<Texture2D>("UI\\TextButton"),
                JResource.Global.Load<Texture2D>("UI\\TextButton_U"));
            _btnRefresh.Position = new Vector2(200, 300);
            _btnRefresh.Text = "Refresh";
            _btnRefresh.Click += new OnClickEventHandler(_btnRefresh_Click);
            this.AddObject(_btnRefresh);

            _ctlListBox = new JListBox();
            _ctlListBox.Position = new Vector2(100, 400);
            _ctlListBox.Size = new Vector2(100, 100);
            _ctlListBox.SelectChanged += new EventHandler(_ctlListBox_SelectChanged);
            this.AddObject(_ctlListBox);
        }

        void _ctlListBox_SelectChanged(object sender, EventArgs e)
        {
            _ctlSelectedRoom.Text = _ctlListBox.SelectedItem.Text;
        }

        void _btnOK_Click(JButtonBase sender)
        {
            Request.CreateRoom(JNetClient.Instance, string.Format("{0}'s room", PlayerData.Instance.Name));
        }

        void _btnJoinGame_Click(JButtonBase sender)
        {
        }

        void _btnRefresh_Click(JButtonBase sender)
        {
            Request.GetRoomList(JNetClient.Instance);
        }

        protected override bool OnUpdate(GameTime gameTime)
        {
            if (JNetVar.Get(0x01) == 0x01)
            {
                JNetVar.Remove(0x01);
                JNetVar.Remove(0x02);
                JCore.Show(new RoomScene());
            }
            else if (JNetVar.Get(0x02) == 0x01)
            {
                JNetVar.Set(0x02, 0x00);
            }

            if (JNetVar.GetObj(VarFlag.RoomList) != null)
            {
                List<Room> roomList = (List<Room>)JNetVar.GetObj(VarFlag.RoomList);
                for (int index = 0; index < roomList.Count; index++)
                {
                    JListBoxItem item = new JListBoxItem(roomList[index].Name);
                    item.Tag = roomList[index];
                    _ctlListBox.Items.Add(item);
                }
                JNetVar.RemoveObj(VarFlag.RoomList);
            }
            return base.OnUpdate(gameTime);
        }
    }
}
