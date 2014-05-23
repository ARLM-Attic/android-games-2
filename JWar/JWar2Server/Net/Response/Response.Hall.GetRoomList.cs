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
    public partial class Response
    {
        public partial class Hall
        {
            public static void GetRoomListSuccess(Player player)
            {
                byte[] buffer = new byte[1024];
                int offset = 0;
                BufferUtil.SetByte(buffer, NET_SCENARIO.HALL, ref offset);
                BufferUtil.SetByte(buffer, NET_COMMAND.GETROOMLIST, ref offset);
                BufferUtil.SetByte(buffer, 0x01, ref offset);
                BufferUtil.SetUInt(buffer, (uint)CacheData.GetInstance().WaitRoomList.Count, ref offset);

                for (int index = 0; index < CacheData.GetInstance().WaitRoomList.Count; index++)
                {
                    Room room = CacheData.GetInstance().WaitRoomList[index];

                    BufferUtil.SetUInt(buffer, room.Id, ref offset);
                    BufferUtil.SetString(buffer, room.Name, 32, ref offset);
                }

                player.Client.Client.SendData(buffer, offset);
            }
        }
    }
}
