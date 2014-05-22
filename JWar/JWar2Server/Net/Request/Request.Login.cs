using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Server;
using JWar2Net;
using JWar2Server.Data;
using JWar2NetContract;

namespace JWar2Server.Net
{
    public partial class Request
    {
        public static void Login(JNetClientChannel channel, byte[] buffer, int length)
        {
            int offset = 2;
            string name = BufferUtil.GetString(buffer, 32, ref offset);

            Player player = new Player();
            player.Client = channel;
            player.Name = name;

            CacheData.GetInstance().AddPlayer(player);

            LoginSuccess(player);
        }

        private static void LoginSuccess(Player player)
        {
            byte[] buffer = new byte[255];
            int offset = 0;
            BufferUtil.SetByte(buffer, NET_SCENARIO.CONNECT, ref offset);
            BufferUtil.SetByte(buffer, NET_COMMAND.LOGIN, ref offset);
            BufferUtil.SetByte(buffer, 0x01, ref offset);
            BufferUtil.SetUInt(buffer, player.Id, ref offset);

            player.Client.Client.SendData(buffer, offset);
        }
    }
}
