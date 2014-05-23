using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2.Net.Data
{
    public class Room
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public Room(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
