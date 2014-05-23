using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Server;
using JWar2NetContract;
using JWar2Server.Net;

namespace JWar2Server
{
    public class JWar2RequestHandler : IJNetRequestHandler
    {
        public void OnHandle(JNetClientChannel player, byte[] buffer, int length)
        {
            byte scenario = buffer[0];
            byte command = buffer[1];
            if (scenario == NET_SCENARIO.CONNECT)
            {
                if (command == NET_COMMAND.LOGIN)
                {
                    Request.Login(player, buffer, length);
                }
            }
            else if (scenario == NET_SCENARIO.HALL)
            {
                if (command == NET_COMMAND.CREATEROOM)
                {
                    Request.Hall.CreateRoom(player, buffer, length);
                }
                else if (command == NET_COMMAND.GETROOMLIST)
                {
                    Request.Hall.GetRoomList(player, buffer, length);
                }
            }
        }
    }
}
