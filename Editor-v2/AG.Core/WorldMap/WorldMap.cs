using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WorldMap
{
    public int Id { get; set; }
    public string Caption { get; set; }
    public Model2D Model { get; set; }

    public List<StagesPos> StagesPosList { get; set; }

    public WorldMap()
    {
        StagesPosList = new List<StagesPos>();
    }
}
