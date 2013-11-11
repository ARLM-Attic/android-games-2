using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AI
{
    public static void Run(Map2D map, Camp camp)
    {
        if (map.GameTime % 60 == 0)
        {
            int index = new Random().Next(0, camp.AvailableUnitList.Count);
            if (camp.Income >= camp.AvailableUnitList[index].CostM && camp.Population < camp.PopulationLimit)
            {
                Object2D obj = AGSUtility.CreateObject(map, camp, DATUtility.GetUnit(camp.AvailableUnitList[index].Id), "unknown", camp.StartPos, Direction2DDef.South.Id);
                AGSUtility.MoveTo(obj, camp.TargetPos);
            }

            //for (int iUnit = 0; iUnit < camp.AvailableUnitList.Count; iUnit++)
            //{
            //    if (camp.Income >= camp.AvailableUnitList[iUnit].CostM && camp.Population < camp.PopulationLimit)
            //    {
            //        Object2D obj = AGSUtility.CreateObject(map, camp, DATUtility.GetUnit(camp.AvailableUnitList[iUnit].Id), "unknown", camp.StartPos, Direction2DDef.South.Id);
            //        AGSUtility.MoveTo(obj, camp.TargetPos);
            //    }
            //}
        }
    }
}