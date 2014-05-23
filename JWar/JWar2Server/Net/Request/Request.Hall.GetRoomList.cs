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
        public partial class Hall
        {
            public static void GetRoomList(JNetClientChannel channel, byte[] buffer, int length)
            {
                int offset = 2;
                uint playerId = BufferUtil.GetUInt(buffer, ref offset);
                Player player = CacheData.GetInstance().PlayerList2[playerId];

                Response.Hall.GetRoomListSuccess(player);
            }
        }
    }
}
