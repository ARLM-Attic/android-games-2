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
        public static void Login(JNetClient client, string name)
        {
            byte[] data = new byte[255];
            int offset = 0;
            BufferUtil.SetByte(data, NET_SCENARIO.CONNECT, ref offset);
            BufferUtil.SetByte(data, NET_COMMAND.LOGIN, ref offset);
            BufferUtil.SetString(data, name, 32, ref offset);

            client.Send(data, data.Length);
        }
    }
}
