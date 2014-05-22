using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Server;

namespace JWar2Server.Data
{
    public class Player
    {
        public JNetClientChannel Client { get; set; }

        public bool IsInRoom;

        public string Name { get; set; }

    }
}
