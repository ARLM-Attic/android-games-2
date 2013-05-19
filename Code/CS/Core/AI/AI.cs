using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AI
{
    public static void Run(Map2D map, Camp camp)
    {
        for (int iUnit = 0; iUnit < camp.AvailableUnitList.Count; iUnit++)
        {
            //if (camp.Income >= camp.AvailableUnitList[iUnit].CostM && camp.Population < camp.PopulationLimit)
            //{
            if (camp.Income >= camp.AvailableUnitList[iUnit].CostM && camp.Population <2)
            {
                Object2D obj = AGSUtility.CreateObject(map, camp, DATUtility.GetUnit(camp.AvailableUnitList[iUnit].Id), "unknown", camp.StartPos, Direction2DDef.South.Id);
                AGSUtility.MoveTo(obj, camp.TargetPos);
            }
        }
    }
}