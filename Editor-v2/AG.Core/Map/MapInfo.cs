using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapInfo
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public override string ToString()
    {
        return string.Format("{0}({1}", Caption, Id);
    }
}