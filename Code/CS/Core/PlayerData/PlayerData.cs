using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> AvailableUnitList { get; set; }
    public long Money { get; set; }

    public PlayerData()
    {
        AvailableUnitList = new List<int>();
    }

    public static PlayerData Current { get; set; }
}