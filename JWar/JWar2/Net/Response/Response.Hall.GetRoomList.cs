using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Client;
using JWar2Net;
using JWar2NetContract;

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
            int offset = 2;
            byte errorCode = BufferUtil.GetByte(buffer, ref offset);
            uint roomCount = BufferUtil.GetUInt(buffer, ref offset);
            for (uint index = 0; index < roomCount; index++)
            {
            }

            JNetVar.Set(0x02, 0x01);
        }
    }
}
