using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2
{
    class PlayerData
    {
        public static PlayerData Instance = new PlayerData();

        public uint PlayerId { get; set; }
        public string Name { get; set; }

        public uint RoomId { get; set; }
    }
}
