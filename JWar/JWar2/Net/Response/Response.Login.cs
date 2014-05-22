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
        public static void Login(byte[] buffer, int length)
        {
            JNetVar.Set(0x01, 0x01);
        }
    }
}
