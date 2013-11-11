using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IMoveStrategy
{
    bool Move(Map2D map, Object2D obj);
    bool IsBar(Map2D map, MapPos pos);
}