using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static partial class DATUtility
{
    private static List<Terrain> s_terrainList = null;

    public static List<Terrain> GetTerrains()
    {
        if (s_terrainList == null)
        {
            s_terrainList = new List<Terrain>();

            s_terrainList.Add(new Terrain
            {
                Id = 1,
                BackTerrainId = 1,
                ForeTerrainId = 0,
                Caption = "沙地",
                Value = 0,
                Model = DATUtility.GetModel(401)
            });

            s_terrainList.Add(new Terrain
            {
                Id = 2,
                BackTerrainId = 2,
                ForeTerrainId = 0,
                Caption = "砖地",
                Value = 0,
                Model = DATUtility.GetModel(402)
            });

            s_terrainList.Add(new Terrain
            {
                Id = 3,
                BackTerrainId = 3,
                ForeTerrainId = 0,
                Caption = "草地",
                Value = 0,
                Model = DATUtility.GetModel(403)
            });

            //s_terrainList.Add(new Terrain
            //{
            //    Id = 4,
            //    BackTerrainId = 2,
            //    ForeTerrainId = 3,
            //    Caption = "沙地上的草地",
            //    Value = 0,
            //    Model = DATUtility.GetModel(403)
            //});
        }

        return s_terrainList;
    }

    public static Terrain GetTerrain(int id)
    {
        List<Terrain> list = GetTerrains();
        for (int index = 0; index < list.Count; index++)
        {
            if (list[index].Id == id)
            {
                return list[index];
            }
        }
        return list[0];
    }
}
