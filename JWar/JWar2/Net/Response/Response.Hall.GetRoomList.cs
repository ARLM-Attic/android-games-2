using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Client;
using JWar2Net;
using JWar2NetContract;
using JWar2.Net.Data;

namespace JWar2.Net
{
    public partial class Response
    {
        /// <summary>
        /// room-count
        /// [room-id
        ///  room-name:str32
        ///  room-f1-player-count
        ///  room-f2-player-count]
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public static void GetRoomList(byte[] buffer, int length)
        {
            List<Room> roomList = new List<Room>();

            int offset = 2;
            byte errorCode = BufferUtil.GetByte(buffer, ref offset);
            uint roomCount = BufferUtil.GetUInt(buffer, ref offset);
            for (uint index = 0; index < roomCount; index++)
            {
                uint roomId = BufferUtil.GetUInt(buffer, ref offset);
                string roomName = BufferUtil.GetString(buffer, 32, ref offset);

                Room room = new Room(roomId, roomName);
                roomList.Add(room);
            }

            JNetVar.SetObj(VarFlag.RoomList, roomList);
        }
    }
}
