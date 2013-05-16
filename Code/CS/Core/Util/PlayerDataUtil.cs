using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class PlayerDataUtil
{
    public static void Store(PlayerData data)
    {
    }

    public static PlayerData Load(int playerId)
    {
#if DEBUG
        PlayerData data = new PlayerData();
        data.Id = playerId;
        data.Name = "jaeho";
        data.Money = 3456;
        data.AvailableUnitList.Add(301);
        data.AvailableUnitList.Add(302);
        return data;
#else
        PlayerData data = new PlayerData();
        data.Id = playerId;
        data.Name = "jaeho";
        data.Money = 3456;
        data.AvailableUnitList.Add(301);
        data.AvailableUnitList.Add(302);
        return data;
#endif
    }
}
