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
        public static void CreateRoom(byte[] buffer, int length)
        {
            int offset = 2;
            byte errorCode = BufferUtil.GetByte(buffer, ref offset);
            uint roomId = BufferUtil.GetUInt(buffer, ref offset);
            PlayerData.Instance.RoomId = roomId;
            JNetVar.Set(0x01, 0x01);
        }
    }
}
