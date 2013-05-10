using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<int> AvailableUnitList { get; set; }
    public long Money { get; set; }
}