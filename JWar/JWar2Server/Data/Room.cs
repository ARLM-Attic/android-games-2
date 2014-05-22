using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Server.Data
{
    public class Room
    {
        public string Name { get; set; }

        public List<Player> PlayerList { get; set; }
        public Room()
        {
            PlayerList = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            player.IsInRoom = true;
            PlayerList.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            player.IsInRoom = false;
            PlayerList.Remove(player);
        }
    }
}
