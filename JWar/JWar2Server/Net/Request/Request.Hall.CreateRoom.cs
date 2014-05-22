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
        public static void Hall_CreateRoom(JNetClientChannel channel, byte[] buffer, int length)
        {
            int offset = 2;
            string roomName = BufferUtil.GetString(buffer, 32, ref offset);

            Room room = new Room();
            room.Name = roomName;
            //room.AddPlayer(null);
            Hall_CreateRoomSuccess(channel, room);
        }

        private static void Hall_CreateRoomSuccess(JNetClientChannel channel, Room room)
        {
            byte[] buffer = new byte[255];
            int offset = 0;
            BufferUtil.SetByte(buffer, NET_SCENARIO.HALL, ref offset);
            BufferUtil.SetByte(buffer, NET_COMMAND.CREATEROOM, ref offset);
            BufferUtil.SetByte(buffer, 0x01, ref offset);

            channel.Client.SendData(buffer, offset);
        }
    }
}
