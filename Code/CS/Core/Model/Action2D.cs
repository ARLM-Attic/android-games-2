using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action2D
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public List<Direction2D> Directions { get; set; }

    public Action2D()
    {
        Directions = new List<Direction2D>();
    }
}