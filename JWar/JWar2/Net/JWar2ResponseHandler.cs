using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Client;
using JWar2NetContract;
using JWar2.Net;

namespace JWar2
{
    public class JWar2ResponseHandler : IJNetResponseHandler
    {
        public void OnHandle(byte[] buffer, int length)
        {
            byte scenario = buffer[0];
            byte command = buffer[1];
            if (scenario == NET_SCENARIO.CONNECT)
            {
                if (command == NET_COMMAND.LOGIN)
                {
                    Response.Login(buffer, length);
                }
            }
            else if (scenario == NET_SCENARIO.HALL)
            {
                if (command == NET_COMMAND.CREATEROOM)
                {
                    Response.CreateRoom(buffer, length);
                }
            }
        }
    }
}
