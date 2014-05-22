using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Client;
using JWar2Net;
using JWar2NetContract;

namespace JWar2.Net
{
    public partial class Request
    {
        public static void GetRoomList(JNetClient client)
        {
            byte[] data = new byte[255];
            int offset = 0;
            BufferUtil.SetByte(data, NET_SCENARIO.HALL, ref offset);
            BufferUtil.SetByte(data, NET_COMMAND.GETROOMLIST, ref offset);
            BufferUtil.SetUInt(data, PlayerData.Instance.PlayerId, ref offset);

            client.Send(data, data.Length);
        }
    }
}
