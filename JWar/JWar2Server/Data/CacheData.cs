using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWar2Net.Server;
using JWar2Server.Data;

namespace JWar2Server
{
    public class CacheData : IJClientManager
    {
        private static object s_playerListLockObject = new object();
        private static object s_mapPlayerListLockObject = new object();
        private static object s_fightListLockObject = new object();

        private static CacheData s_instance;

        public Player[] PlayerList2 = new Player[100];
        public Room[] RoomArray = new Room[20];

        public static void Init()
        {
            s_instance = new CacheData();

            //s_instance.Maps.Add(MapLoader.LoadMap(2));

            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(1));
            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(2));
            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(3));
            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(4));
            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(5));
            //s_instance.MapCollection.Add(new RPGMapPlayerCollection(6));

            //s_instance.CacheMapService.LoadDataFromFile();
        }

        public static CacheData GetInstance()
        {
            return s_instance;
        }

        //public List<ARPGDATA.Maps.AGRPGMap> Maps;

        public List<JNetClientChannel> PlayerList { get; private set; }

        //public List<RPGMapPlayerCollection> MapCollection;

        //public CacheMapService CacheMapService;

        //public List<RPGFight> Fights;

        public CacheData()
        {
            //Maps = new List<ARPGDATA.Maps.AGRPGMap>();

            PlayerList = new List<JNetClientChannel>();
            //MapCollection = new List<RPGMapPlayerCollection>();

            //CacheMapService = new CacheMapService();

            //Fights = new List<RPGFight>();
        }

        public void AddClient(JNetClientChannel client)
        {
            lock (s_playerListLockObject)
            {
                PlayerList.Add(client);
            }
        }

        public bool AddPlayer(Player player)
        {
            for (uint index = 1; index < PlayerList2.Length; index++)
            {
                if (PlayerList2[index] == null)
                {
                    player.Id = index;
                    PlayerList2[index] = player;
                    return true;
                }
            }
            return false;
        }

        //public void RemvoeClient(RPGPlayer player, int clientIndex)
        //{
        //    // remove from map
        //    RPGMapPlayerCollection map = GetMap(player.CurMapId);
        //    if (map != null)
        //    {
        //        lock (s_playerListLockObject)
        //        {
        //            map.RemovePlayer(player);
        //            PlayerList.RemoveAt(clientIndex);
        //        }
        //    }
        //    else
        //    {
        //        lock (s_playerListLockObject)
        //        {
        //            PlayerList.RemoveAt(clientIndex);
        //        }
        //    }
        //}

        //public void AddPlayerToMap(ushort mapId, RPGPlayer player)
        //{
        //    RPGMapPlayerCollection map = GetMap(mapId);
        //    AddPlayerToMap(map, player);
        //}

        //public void AddPlayerToMap(RPGMapPlayerCollection map, RPGPlayer player)
        //{
        //    lock (s_mapPlayerListLockObject)
        //    {
        //        map.PlayerList.Add(player);
        //    }
        //}

        //public void MovePlayerToMap(ushort fromMapId, ushort toMapId, RPGPlayer player)
        //{
        //    RPGMapPlayerCollection fromMap = GetMap(fromMapId);
        //    RPGMapPlayerCollection toMap = GetMap(toMapId);
        //    MovePlayerToMap(fromMap, toMap, player);
        //}

        //public void MovePlayerToMap(RPGMapPlayerCollection fromMap, RPGMapPlayerCollection toMap, RPGPlayer player)
        //{
        //    lock (s_mapPlayerListLockObject)
        //    {
        //        fromMap.PlayerList.Remove(player);
        //        toMap.PlayerList.Add(player);
        //    }
        //}

        //public RPGMapPlayerCollection GetMap(ushort mapId)
        //{
        //    for (int index = 0; index < MapCollection.Count; index++)
        //    {
        //        RPGMapPlayerCollection map = MapCollection[index];
        //        if (map.MapId == mapId)
        //        {
        //            return map;
        //        }
        //    }
        //    return null;
        //}

        //public void AddFight(uint mapId, RPGFight fight)
        //{
        //    lock (s_fightListLockObject)
        //    {
        //        Fights.Add(fight);
        //    }
        //}

        //public RPGFight GetFight(uint mapId, uint fightId)
        //{
        //    for (int index = 0; index < Fights.Count; index++)
        //    {
        //        RPGFight fight = Fights[index];
        //        if (fight.Id == fightId)
        //        {
        //            return fight;
        //        }
        //    }
        //    return null;
        //}

        //public void RemoveFight(RPGFight fight)
        //{
        //    lock (s_fightListLockObject)
        //    {
        //        for (int index = 0; index < Fights.Count; index++)
        //        {
        //            if (Fights[index].Id == fight.Id)
        //            {
        //                Fights.RemoveAt(index);
        //                return;
        //            }
        //        }
        //    }
        //}

        public Player GetPlayer(uint playerId)
        {
            return PlayerList2[playerId];
        }

        public bool AddRoom(Room room)
        {
            for (uint index = 1; index < RoomArray.Length; index++)
            {
                if (RoomArray[index] == null)
                {
                    RoomArray[index] = room;
                    room.Id = index;
                    return true;
                }
            }
            return false;
        }
    }
}
