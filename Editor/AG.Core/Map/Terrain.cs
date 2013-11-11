using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Terrain
{
    public int Id { get; set; }
    public int BackTerrainId { get; set; }
    public int ForeTerrainId { get; set; }
    public string Caption { get; set; }
    public Model2D Model { get; set; }
    public int Value { get; set; }

    public override string ToString()
    {
        return string.Format("{0}({1})", Caption, Id);
    }
}